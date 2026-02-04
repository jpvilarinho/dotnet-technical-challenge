namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public string Id { get; set; } = default!;
        public long Numero { get; set; }
        public string NomeTitular { get; set; } = default!;
        public bool Ativo { get; set; }

        public ContaCorrente() { }

        public ContaCorrente(string id, long numero, string nomeTitular)
        {
            if (numero <= 0)
                throw new ArgumentException("O número da conta deve ser maior que zero.", nameof(numero));
            if (string.IsNullOrWhiteSpace(nomeTitular))
                throw new ArgumentException("O nome do titular da conta não pode ser nulo ou vazio.", nameof(nomeTitular));

            Id = id;
            Numero = numero;
            NomeTitular = nomeTitular;
            Ativo = true;
        }
    }
}
