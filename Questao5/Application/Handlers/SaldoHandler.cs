using MediatR;
using Dapper;
using System.Data;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Application.Handlers
{
    public class SaldoHandler : IRequestHandler<SaldoRequest, SaldoResponse>
    {
        private readonly IDbConnection _dbConnection;

        public SaldoHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<SaldoResponse> Handle(SaldoRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.IdContaCorrente))
                throw new HttpRequestException("INVALID_ACCOUNT");

            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();

            var contaCorrente = await _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(
                @"SELECT 
                    idcontacorrente AS Id,
                    numero AS Numero,
                    nome AS NomeTitular,
                    CASE WHEN ativo = 1 THEN 1 ELSE 0 END AS Ativo
                FROM contacorrente
                WHERE idcontacorrente = @Id",
                new { Id = request.IdContaCorrente });

            if (contaCorrente == null)
                throw new HttpRequestException("INVALID_ACCOUNT");

            if (!contaCorrente.Ativo)
                throw new HttpRequestException("INACTIVE_ACCOUNT");

            var creditos = await _dbConnection.QuerySingleAsync<decimal>(
                @"SELECT COALESCE(SUM(valor), 0)
                FROM movimento
                WHERE idcontacorrente = @Id AND tipomovimento = 'C'",
                new { Id = request.IdContaCorrente });

            var debitos = await _dbConnection.QuerySingleAsync<decimal>(
                @"SELECT COALESCE(SUM(valor), 0)
                FROM movimento
                WHERE idcontacorrente = @Id AND tipomovimento = 'D'",
                new { Id = request.IdContaCorrente });

            var saldo = creditos - debitos;

            return new SaldoResponse
            {
                Numero = contaCorrente.Numero.ToString(),
                Nome = contaCorrente.NomeTitular,
                DataConsulta = DateTime.Now,
                Saldo = saldo
            };
        }
    }
}
