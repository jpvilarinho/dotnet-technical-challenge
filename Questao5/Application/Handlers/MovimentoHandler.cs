using MediatR;
using Dapper;
using System.Data;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Application.Handlers
{
    public class MovimentoHandler : IRequestHandler<MovimentoRequest, MovimentoResponse>
    {
        private readonly IDbConnection _dbConnection;

        public MovimentoHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<MovimentoResponse> Handle(MovimentoRequest request, CancellationToken cancellationToken)
        {
             if (string.IsNullOrWhiteSpace(request.IdempotencyKey))
                throw new HttpRequestException("INVALID_IDEMPOTENCY_KEY");

            if (string.IsNullOrWhiteSpace(request.IdContaCorrente))
                throw new HttpRequestException("INVALID_ACCOUNT");

            if (request.Valor <= 0)
                throw new HttpRequestException("INVALID_VALUE");

            var tipo = (request.TipoMovimento ?? string.Empty).Trim().ToUpperInvariant();
            if (tipo != "C" && tipo != "D")
                throw new HttpRequestException("INVALID_TYPE");

            var idem = await _dbConnection.QueryFirstOrDefaultAsync<Idempotencia>(
                @"SELECT 
                    chave_idempotencia AS ChaveIdempotencia,
                    requisicao AS Requisicao,
                    resultado AS Resultado
                  FROM idempotencia
                  WHERE chave_idempotencia = @Key",
                new { Key = request.IdempotencyKey });

            if (idem?.Resultado is { Length: > 0 })
            {
                var existing = JsonConvert.DeserializeObject<MovimentoResponse>(idem.Resultado);
                if (existing != null)
                    return existing;
            }

            var contaCorrente = await _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(
                @"SELECT 
                    idcontacorrente AS Id,
                    numero AS Numero,
                    nome AS NomeTitular,
                    ativo AS Ativo
                  FROM contacorrente
                  WHERE idcontacorrente = @Id",
                new { Id = request.IdContaCorrente });

            if (contaCorrente == null)
                throw new HttpRequestException("INVALID_ACCOUNT");

            if (!contaCorrente.Ativo)
                throw new HttpRequestException("INACTIVE_ACCOUNT");

            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();

            using var tx = _dbConnection.BeginTransaction();

            try
            {
                var idMovimento = Guid.NewGuid().ToString();

                await _dbConnection.ExecuteAsync(
                    @"INSERT INTO movimento 
                      (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
                      VALUES 
                      (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)",
                    new
                    {
                        IdMovimento = idMovimento,
                        IdContaCorrente = request.IdContaCorrente,
                        DataMovimento = DateTime.Now.ToString("dd/MM/yyyy"),
                        TipoMovimento = tipo,
                        Valor = Math.Round(request.Valor, 2)
                    },
                    tx
                );

                var response = new MovimentoResponse { IdMovimento = idMovimento };

                await _dbConnection.ExecuteAsync(
                    @"INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) 
                      VALUES (@Key, @Req, @Res)",
                    new
                    {
                        Key = request.IdempotencyKey,
                        Req = JsonConvert.SerializeObject(request),
                        Res = JsonConvert.SerializeObject(response)
                    },
                    tx
                );

                tx.Commit();
                return response;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }
    }
}
