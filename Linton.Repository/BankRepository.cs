using Linton.Domain;
using Linton.Domain.Models;
using Linton.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linton.Repository
{
    public class BankRepository : RepositoryBase<Bank>, IBankRepository
    {
        public BankRepository(LintonDbContext context) : base(context) { }
        public override IEnumerable<Bank> All()
        {  
                return  dbSet.ToList(); 
        }
    }
}
