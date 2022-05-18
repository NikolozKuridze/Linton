using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options; 
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Linton.Domain.Models;
using System.Text.RegularExpressions;
using Linton.Repository;
using LintonAPI.Models; 

namespace LintonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LintonController
    { 
        private IUnitOfWork _unitOfWork;
        public Helper.Helper helper = new Helper.Helper();
        public LintonController(IUnitOfWork UnitOfWork)
        { 
            _unitOfWork = UnitOfWork; 
        }  
        // კონკრეტული პირის ძებნა სახელის და გვარის ინიციალებით ან პირიქით 
        [HttpGet]
        [Route("find")]
        public  PersonModel  Get(string search)
        { 
            var persons = from p in _unitOfWork.Persons.All() join b in _unitOfWork.Banks.All() on p.Bank.ID equals b.ID  
                          select p;
            PersonModel fp  = new PersonModel();  
            foreach (var item in persons)
            {
                var ff = item.FullName.ToLower().Split(' ');
                var s = search.Split(' ');
                for (int i = 0; i < ff.Length; i++)
                {
                    for (int e = 0; e < s.Length; e++)
                    {
                        if ( ff[i].ToLower().Contains(s[e].ToLower())  == true)
                        {
                            fp.FullName = item.FullName ;
                            fp.Age = item.Age  ;
                            fp.IBAN = item.Bank.SwiftCode;
                            fp.BankName = item.Bank.Name;
                        }
                    } 
                }
            }   
            var banks = from b in _unitOfWork.Banks.All()
                        select b;
            if (fp!=null)
            {     
                return fp;
            }
            return null;
        }
        // პირების სია
        [HttpGet]
        [Route("list")]
        public IEnumerable<List<PersonModel>> Get()
        {

            var persons = from p in _unitOfWork.Persons.All() join b in _unitOfWork.Banks.All() on p.Bank.ID equals b.ID select new List<PersonModel> { new PersonModel { FullName = p.FirstName +" "+p.LastName, Age= p.Age,IBAN  = p.Bank.SwiftCode, BankName= p.Bank.Name } } ;
            return persons;
        } 

        //პირის აიბანის განახლება
        [HttpPut]
        [Route("update")]
        public async Task<bool> UpdateIban(int id,string newIBAN)
        { 
            var s = helper.checker(newIBAN);

            var person =from p in _unitOfWork.Persons.All() join b in _unitOfWork.Banks.All() on p.Bank.ID equals b.ID where p.ID == id select p;
            if (person != null)
            {
                if (s == true)
                {
                    person.Last().Bank.SwiftCode = newIBAN;
                    await _unitOfWork.Persons.Update(person.LastOrDefault());
                    await _unitOfWork.CompleteAsync();
                    return true;
                }
                return false;
            }
            return false;
        }
        //პირის დამატება
        [HttpPost]
        [Route("add")]
        public async Task<bool> AddPerson(Person person)
        {
            if (person.Bank.SwiftCode !=null)
            { 
            person.Bank.Name = helper.GetBankName(person.Bank.SwiftCode);
            }
            if (person != null) 
            {
                var s = helper.checker(person.Bank.SwiftCode);
                if (s==true)
                { 
                    await _unitOfWork.Persons.Save(person);
                    await _unitOfWork.CompleteAsync();
                    return true;
                }
            }
            return false;
           
        }
        // პირის წაშლა
        [HttpDelete]
        [Route("remove")]
        public async Task<bool> RemovePerson(int ID)
        {
            if (_unitOfWork.Persons.Get(ID).Result != null)
            { 
                    await _unitOfWork.Persons.Delete(ID); 
                    await _unitOfWork.CompleteAsync(); 
                return true; 
            }
            return false; 
        }  
    }
}
