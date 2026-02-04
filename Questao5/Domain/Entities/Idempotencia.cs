namespace Questao5.Domain.Entities
{
    public class Idempotencia
    {
        public string ChaveIdempotencia { get; private set; } = string.Empty;
        public string Requisicao { get; private set; } = string.Empty;
        public string Resultado { get; private set; } = string.Empty;

        public Idempotencia(string chaveIdempotencia, string requisicao, string resultado)
        {
            ChaveIdempotencia = chaveIdempotencia;
            Requisicao = requisicao;
            Resultado = resultado;
        }

        public Idempotencia() { }
    }
}
