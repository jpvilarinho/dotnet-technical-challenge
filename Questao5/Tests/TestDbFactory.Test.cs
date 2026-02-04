using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Questao5.Tests
{
    internal static class TestDbFactory
    {
        public static IDbConnection CreateOpenInMemoryDb()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            connection.Execute(@"
                CREATE TABLE contacorrente (
                    idcontacorrente TEXT(37) PRIMARY KEY,
                    numero INTEGER(10) NOT NULL UNIQUE,
                    nome TEXT(100) NOT NULL,
                    ativo INTEGER(1) NOT NULL default 0,
                    CHECK(ativo in (0, 1))
                );");

            connection.Execute(@"
                CREATE TABLE movimento (
                    idmovimento TEXT(37) PRIMARY KEY,
                    idcontacorrente TEXT(37) NOT NULL,
                    datamovimento TEXT(25) NOT NULL,
                    tipomovimento TEXT(1) NOT NULL,
                    valor REAL NOT NULL,
                    CHECK(tipomovimento in ('C', 'D')),
                    FOREIGN KEY(idcontacorrente) REFERENCES contacorrente(idcontacorrente)
                );");

            connection.Execute(@"
                CREATE TABLE idempotencia (
                    chave_idempotencia TEXT(37) PRIMARY KEY,
                    requisicao TEXT(1000),
                    resultado TEXT(1000)
                );");

            connection.Execute(@"
                INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo)
                VALUES('B6BAFC09-6967-ED11-A567-055DFA4A16C9', 123, 'Katherine Sanchez', 1);");

            connection.Execute(@"
                INSERT INTO contacorrente(idcontacorrente, numero, nome, ativo)
                VALUES('F475F943-7067-ED11-A06B-7E5DFA4A16C9', 741, 'Ameena Lynn', 0);");

            return connection;
        }
    }
}
