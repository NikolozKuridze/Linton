﻿using Linton.Domain.Interfaces;
using Linton.Domain.Interfaces.Repositories;
using Linton.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linton.Domain
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly LintonDbContext _context; 

        public IPersonRepository Persons { get; private set; }
        public IBankRepository Banks { get; private set; }

        public UnitOfWork(LintonDbContext context)
        {
            _context = context; 

            Persons = new PersonRepository(context); 
            Banks = new BankRepository(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
