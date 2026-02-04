using System;
using System.Globalization;

namespace Questao1
{
    class Program
    {
        static void Main(string[] args)
        {
            ContaBancaria conta;

            Console.Write("Digite o número da conta: ");
            string? inputNumero = Console.ReadLine();
            int numero;
            while (!int.TryParse(inputNumero, out numero))
            {
                Console.WriteLine("Número inválido. Por favor, digite um número válido.");
                Console.Write("Entre o número da conta: ");
                inputNumero = Console.ReadLine();
            }

            Console.Write("Digite o titular da conta: ");
            string? titular = Console.ReadLine();
            while (string.IsNullOrEmpty(titular))
            {
                Console.WriteLine("O titular da conta não pode ser vazio. Por favor, digite um titular válido.");
                Console.Write("Entre o titular da conta: ");
                titular = Console.ReadLine();
            }

            Console.Write("Haverá depósito inicial (s/n)? ");
            string? inputResp = Console.ReadLine();
            char resp;
            while (!char.TryParse(inputResp, out resp) || (resp != 's' && resp != 'S' && resp != 'n' && resp != 'N'))
            {
                Console.WriteLine("Resposta inválida. Por favor, digite 's' para sim ou 'n' para não.");
                Console.Write("Haverá depósito inicial (s/n)? ");
                inputResp = Console.ReadLine();
            }

            if (resp == 's' || resp == 'S')
            {
                Console.Write("Entre o valor de depósito inicial: ");
                string? inputDepositoInicial = Console.ReadLine();
                double depositoInicial;
                while (!double.TryParse(inputDepositoInicial, NumberStyles.Any, CultureInfo.InvariantCulture, out depositoInicial))
                {
                    Console.WriteLine("Valor inválido. Por favor, digite um valor numérico válido.");
                    Console.Write("Entre o valor de depósito inicial: ");
                    inputDepositoInicial = Console.ReadLine();
                }
                conta = new ContaBancaria(numero, titular, depositoInicial);
            }
            else
            {
                conta = new ContaBancaria(numero, titular);
            }

            Console.WriteLine();
            Console.WriteLine("Dados da conta:");
            Console.WriteLine(conta);

            Console.WriteLine();
            Console.Write("Entre um valor para depósito: ");
            string? inputQuantiaDeposito = Console.ReadLine();
            double quantiaDeposito;
            while (!double.TryParse(inputQuantiaDeposito, NumberStyles.Any, CultureInfo.InvariantCulture, out quantiaDeposito))
            {
                Console.WriteLine("Valor inválido. Por favor, digite um valor numérico válido.");
                Console.Write("Entre um valor para depósito: ");
                inputQuantiaDeposito = Console.ReadLine();
            }
            conta.Deposito(quantiaDeposito);
            Console.WriteLine("Dados da conta atualizados:");
            Console.WriteLine(conta);

            Console.WriteLine();
            Console.Write("Entre um valor para saque: ");
            string? inputQuantiaSaque = Console.ReadLine();
            double quantiaSaque;
            while (!double.TryParse(inputQuantiaSaque, NumberStyles.Any, CultureInfo.InvariantCulture, out quantiaSaque))
            {
                Console.WriteLine("Valor inválido. Por favor, digite um valor numérico válido.");
                Console.Write("Entre um valor para saque: ");
                inputQuantiaSaque = Console.ReadLine();
            }
            conta.Saque(quantiaSaque);
            Console.WriteLine("Dados da conta atualizados:");
            Console.WriteLine(conta);
        }
    }
}
