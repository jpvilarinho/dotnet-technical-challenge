using System.Threading.Tasks;
using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> GetContaCorrenteByIdAsync(string id);
    }
}
