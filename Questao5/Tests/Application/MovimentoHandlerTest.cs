using Dapper;
using FluentAssertions;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Xunit;

namespace Questao5.Tests.Application
{
    public class MovimentoHandlerTest
    {
        [Fact]
        public async Task Handle_ValidRequest_ReturnsIdMovimento_AndPersists()
        {
            using var db = TestDbFactory.CreateOpenInMemoryDb();
            var handler = new MovimentoHandler(db);

            var request = new MovimentoRequest
            {
                IdempotencyKey = Guid.NewGuid().ToString(),
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                Valor = 100.00m,
                TipoMovimento = "C"
            };

            var response = await handler.Handle(request, CancellationToken.None);

            response.Should().NotBeNull();
            response.IdMovimento.Should().NotBeNullOrWhiteSpace();

            var countMov = await db.QuerySingleAsync<long>(
                "SELECT COUNT(1) FROM movimento WHERE idmovimento = @Id",
                new { Id = response.IdMovimento });

            countMov.Should().Be(1);

            var countIdem = await db.QuerySingleAsync<long>(
                "SELECT COUNT(1) FROM idempotencia WHERE chave_idempotencia = @Key",
                new { Key = request.IdempotencyKey });

            countIdem.Should().Be(1);
        }

        [Fact]
        public async Task Handle_InvalidMovementType_Throws()
        {
            using var db = TestDbFactory.CreateOpenInMemoryDb();
            var handler = new MovimentoHandler(db);

            var request = new MovimentoRequest
            {
                IdempotencyKey = Guid.NewGuid().ToString(),
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                Valor = 100.00m,
                TipoMovimento = "X"
            };

            var act = async () => await handler.Handle(request, CancellationToken.None);
            var ex = await Assert.ThrowsAsync<HttpRequestException>(act);

            ex.Message.Should().Be("INVALID_TYPE");
        }

        [Fact]
        public async Task Handle_NegativeValue_Throws()
        {
            using var db = TestDbFactory.CreateOpenInMemoryDb();
            var handler = new MovimentoHandler(db);

            var request = new MovimentoRequest
            {
                IdempotencyKey = Guid.NewGuid().ToString(),
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                Valor = -100.00m,
                TipoMovimento = "C"
            };

            var act = async () => await handler.Handle(request, CancellationToken.None);
            var ex = await Assert.ThrowsAsync<HttpRequestException>(act);

            ex.Message.Should().Be("INVALID_VALUE");
        }

        [Fact]
        public async Task Handle_NonexistentContaCorrente_Throws()
        {
            using var db = TestDbFactory.CreateOpenInMemoryDb();
            var handler = new MovimentoHandler(db);

            var request = new MovimentoRequest
            {
                IdempotencyKey = Guid.NewGuid().ToString(),
                IdContaCorrente = "nonexistentId",
                Valor = 100.00m,
                TipoMovimento = "C"
            };

            var act = async () => await handler.Handle(request, CancellationToken.None);
            var ex = await Assert.ThrowsAsync<HttpRequestException>(act);

            ex.Message.Should().Be("INVALID_ACCOUNT");
        }

        [Fact]
        public async Task Handle_InactiveContaCorrente_Throws()
        {
            using var db = TestDbFactory.CreateOpenInMemoryDb();
            var handler = new MovimentoHandler(db);

            var request = new MovimentoRequest
            {
                IdempotencyKey = Guid.NewGuid().ToString(),
                IdContaCorrente = "F475F943-7067-ED11-A06B-7E5DFA4A16C9",
                Valor = 100.00m,
                TipoMovimento = "C"
            };

            var act = async () => await handler.Handle(request, CancellationToken.None);
            var ex = await Assert.ThrowsAsync<HttpRequestException>(act);

            ex.Message.Should().Be("INACTIVE_ACCOUNT");
        }

        [Fact]
        public async Task Handle_SameIdempotencyKey_ReturnsSameResult()
        {
            using var db = TestDbFactory.CreateOpenInMemoryDb();
            var handler = new MovimentoHandler(db);

            var key = Guid.NewGuid().ToString();

            var request1 = new MovimentoRequest
            {
                IdempotencyKey = key,
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                Valor = 50.00m,
                TipoMovimento = "C"
            };

            var r1 = await handler.Handle(request1, CancellationToken.None);

            var request2 = new MovimentoRequest
            {
                IdempotencyKey = key,
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                Valor = 50.00m,
                TipoMovimento = "C"
            };

            var r2 = await handler.Handle(request2, CancellationToken.None);

            r2.IdMovimento.Should().Be(r1.IdMovimento);

            var countMov = await db.QuerySingleAsync<long>("SELECT COUNT(1) FROM movimento");
            countMov.Should().Be(1);
        }
    }
}
