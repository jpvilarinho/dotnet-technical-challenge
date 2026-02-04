using FluentAssertions;
using Questao5.Infrastructure.Sqlite;
using Xunit;

namespace Questao5.Tests.Infrastructure
{
    public class DatabaseBootstrapTest
    {
        [Fact]
        public async Task InitializeDatabaseAsync_ReturnsTrue()
        {
            var cfg = new DatabaseConfig { Name = "Data Source=:memory:" };
            var bootstrap = new DatabaseBootstrap(cfg);

            var result = await bootstrap.InitializeDatabaseAsync(CancellationToken.None);

            result.Should().BeTrue();
        }
    }
}
