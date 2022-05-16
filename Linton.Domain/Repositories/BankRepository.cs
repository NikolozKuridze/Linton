using Linton.Domain.Interfaces.Repositories;
using Linton.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Linton.Domain.Repositories
{
    public class BankRepository : RepositoryBase<Bank>, IBankRepository
    {
        public BankRepository(LintonDbContext context) : base(context) { }
        public async Task<IEnumerable<Bank>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<Bank>();
            }
        }
    }
}
