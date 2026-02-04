using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories
{
    public interface IMovimentoRepository
    {
        Task<Movimento> GetByIdAsync(string id);
        Task<IEnumerable<Movimento>> GetAllAsync();
        Task AddAsync(Movimento movimento);
        Task UpdateAsync(Movimento movimento);
        Task DeleteAsync(string id);
        Task<ContaCorrente> GetContaCorrenteByIdAsync(string idContaCorrente);

    }
}
