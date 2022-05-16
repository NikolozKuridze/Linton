using Linton.Domain.Interfaces.Repositories;
using Linton.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linton.Domain.Repositories
{
    public class PersonRepository : RepositoryBase<Person>,IPersonRepository
    {
        public PersonRepository(LintonDbContext context) : base(context) { }
         
        public async Task<IEnumerable<Person>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            { 
                return new List<Person>();
            }
        }

        public override async Task<bool> Save(Person entity)
        {
            try
            {
                if (entity !=null) 
                return await Add(entity);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.ID == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            { 
                return false;
            }
        }
        public async Task<bool> Update(Person person)
        {
            try
            {
                var exist = await dbSet.Where(x => x.ID == person.ID)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Update(person);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
