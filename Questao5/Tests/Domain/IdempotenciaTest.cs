using FluentAssertions;
using Questao5.Domain.Entities;
using Xunit;

namespace Questao5.Tests.Domain
{
    public class IdempotenciaTest
    {
        [Fact]
        public void Constructor_AssignsProperties()
        {
            var chave = "KEY";
            var req = "REQ";
            var res = "RES";

            var idem = new Idempotencia(chave, req, res);

            idem.ChaveIdempotencia.Should().Be(chave);
            idem.Requisicao.Should().Be(req);
            idem.Resultado.Should().Be(res);
        }
    }
}
