using FluentAssertions;
using Questao5.Domain.Entities;
using Xunit;

namespace Questao5.Tests.Domain
{
    public class MovimentoTest
    {
        [Fact]
        public void Constructor_AssignsProperties()
        {
            var id = "M1";
            var idConta = "C1";
            var data = DateTime.Parse("2024-05-10");
            var tipo = 'C';
            var valor = 100.00m;

            var mov = new Movimento(id, idConta, data, tipo, valor);

            mov.Id.Should().Be(id);
            mov.IdContaCorrente.Should().Be(idConta);
            mov.DataMovimento.Should().Be(data);
            mov.TipoMovimento.Should().Be(tipo);
            mov.Valor.Should().Be(valor);
        }
    }
}
