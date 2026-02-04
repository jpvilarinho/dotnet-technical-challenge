using FluentAssertions;
using Questao5.Domain.Entities;
using Xunit;

namespace Questao5.Tests.Domain
{
    public class ContaCorrenteTest
    {
        [Fact]
        public void Constructor_ValidParameters_CreatesInstance()
        {
            var id = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            var numero = 123;
            var nomeTitular = "Katherine Sanchez";

            var conta = new ContaCorrente(id, numero, nomeTitular);

            conta.Should().NotBeNull();
            conta.Id.Should().Be(id);
            conta.Numero.Should().Be(numero);
            conta.NomeTitular.Should().Be(nomeTitular);
            conta.Ativo.Should().BeTrue();
        }

        [Fact]
        public void Constructor_NumeroLessOrEqualZero_ThrowsArgumentException()
        {
            var act = () => new ContaCorrente("id", 0, "Nome");
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Constructor_EmptyNome_ThrowsArgumentException()
        {
            var act = () => new ContaCorrente("id", 1, "");
            act.Should().Throw<ArgumentException>();
        }
    }
}
