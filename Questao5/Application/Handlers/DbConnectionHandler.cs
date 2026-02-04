using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Questao5.Tests.Application.Handlers
{
    public class TestDbConnection : IDbConnection
    {
        private string _connectionString = string.Empty;

        [AllowNull]
        public string ConnectionString
        {
            get => _connectionString;
            set => _connectionString = value ?? string.Empty;
        }

        public TestDbConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public int ConnectionTimeout => throw new NotImplementedException();

        public string Database => throw new NotImplementedException();

        public ConnectionState State => throw new NotImplementedException();

        public IDbTransaction BeginTransaction() => throw new NotImplementedException();

        public IDbTransaction BeginTransaction(IsolationLevel il) => throw new NotImplementedException();

        public void ChangeDatabase(string databaseName) => throw new NotImplementedException();

        public void Close() => throw new NotImplementedException();

        public IDbCommand CreateCommand() => throw new NotImplementedException();

        public void Dispose() { }
        
        public void Open() => throw new NotImplementedException();
    }
}
