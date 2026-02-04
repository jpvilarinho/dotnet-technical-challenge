using Dapper;
using FluentAssertions;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Xunit;

namespace Questao5.Tests.Application
{
    public class SaldoHandlerTest
    {
        [Fact]
        public async Task Handle_ValidRequest_ReturnsSaldoResponse()
        {
            using var db = TestDbFactory.CreateOpenInMemoryDb();

            await db.ExecuteAsync(@"
                INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
                VALUES ('M1', 'B6BAFC09-6967-ED11-A567-055DFA4A16C9', '01/01/2026', 'C', 100.00);");

            await db.ExecuteAsync(@"
                INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
                VALUES ('M2', 'B6BAFC09-6967-ED11-A567-055DFA4A16C9', '01/01/2026', 'D', 40.00);");

            var handler = new SaldoHandler(db);

            var request = new SaldoRequest
            {
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9"
            };

            var response = await handler.Handle(request, CancellationToken.None);

            response.Should().NotBeNull();
            response.Numero.Should().Be("123");
            response.Nome.Should().Be("Katherine Sanchez");
            response.Saldo.Should().Be(60.00m);
        }

        [Fact]
        public async Task Handle_NonexistentContaCorrente_Throws()
        {
            using var db = TestDbFactory.CreateOpenInMemoryDb();
            var handler = new SaldoHandler(db);

            var request = new SaldoRequest { IdContaCorrente = "nonexistentId" };

            var act = async () => await handler.Handle(request, CancellationToken.None);
            var ex = await Assert.ThrowsAsync<HttpRequestException>(act);

            ex.Message.Should().Be("INVALID_ACCOUNT");
        }

        [Fact]
        public async Task Handle_InactiveContaCorrente_Throws()
        {
            using var db = TestDbFactory.CreateOpenInMemoryDb();
            var handler = new SaldoHandler(db);

            var request = new SaldoRequest { IdContaCorrente = "F475F943-7067-ED11-A06B-7E5DFA4A16C9" };

            var act = async () => await handler.Handle(request, CancellationToken.None);
            var ex = await Assert.ThrowsAsync<HttpRequestException>(act);

            ex.Message.Should().Be("INACTIVE_ACCOUNT");
        }

        [Fact]
        public async Task Handle_NoMovements_ReturnsZero()
        {
            using var db = TestDbFactory.CreateOpenInMemoryDb();
            var handler = new SaldoHandler(db);

            var request = new SaldoRequest { IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9" };

            var response = await handler.Handle(request, CancellationToken.None);

            response.Saldo.Should().Be(0m);
        }
    }
}
