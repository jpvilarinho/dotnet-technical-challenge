using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentoRequest : IRequest<MovimentoResponse>
    {
        public string? IdempotencyKey { get; set; }
        public string? IdContaCorrente { get; set; }
        public decimal Valor { get; set; }
        public string? TipoMovimento { get; set; }
    }
}
