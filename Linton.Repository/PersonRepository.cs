using Linton.Domain.Models;
using Linton.Repository.Interfaces;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linton.Repository
{
    public class PersonRepository : RepositoryBase<Person>,IPersonRepository
    {
        public PersonRepository(LintonDbContext context) : base(context) { }
         
        public override IEnumerable<Person> All()
        { 
                return  dbSet.ToList(); 
        }

        public override async Task<bool> Save(Person entity)
        { 
                if (entity !=null) 
                return await Add(entity);
                return true; 
        }
        public  override async Task<bool> Delete(int id)
        { 
                var exist = await dbSet.Where(x => x.ID == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist); 
                return true; 
        }
        public override async Task<bool> Update(Person person)
        { 
                var exist = await dbSet.Where(x => x.ID == person.ID)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false; 
                dbSet.Update(person); 
                return true; 
        }
    }
}
