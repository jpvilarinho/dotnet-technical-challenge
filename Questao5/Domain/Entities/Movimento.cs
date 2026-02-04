namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public string Id { get; set; }
        public string IdContaCorrente { get; set; }
        public DateTime DataMovimento { get; set; }
        public char TipoMovimento { get; set; }
        public decimal Valor { get; set; }

        public Movimento(string id, string idContaCorrente, DateTime dataMovimento, char tipoMovimento, decimal valor)
        {
            Id = id;
            IdContaCorrente = idContaCorrente;
            DataMovimento = dataMovimento;
            TipoMovimento = tipoMovimento;
            Valor = valor;
        }
    }
}
