using Linton.Domain;
using Linton.Domain.Interfaces; 
using Linton.Domain.Repositories; 
using LintonAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Linton.Domain.Models;

namespace LintonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LintonController
    {
        private readonly IOptions<MySettingsModel> _appSettings; 
        private IUnitOfWork _unitOfWork;
        public LintonController(IOptions<MySettingsModel> app , IUnitOfWork UnitOfWork)
        {
            _appSettings = app;
            _unitOfWork = UnitOfWork; 
        }
        [HttpGet]
        [Route("find")]
        public List<MyPerson> Get(string search)
        { 
            var persons = _unitOfWork.Persons.All().Result.ToList();
            persons = persons.Where(f => (f.FirstName.ToLower()+" "+ f.LastName).ToLower().Contains(search.ToLower())).ToList();

           // persons = persons.Where(f => f.FirstName.ToLower().Contains(search.ToLower()) ||f.LastName.ToLower().Contains(search.ToLower())).ToList();
            var banks = _unitOfWork.Banks.All().Result.ToList();
            List<MyPerson> mp = new List<MyPerson>();
            foreach (var item in persons)
            {
                var b = ibanChecker(item.IBAN);
                var bName = banks.Where(x => x.SwiftCode == b.Result).Select(a => a.Name).FirstOrDefault();
                mp.Add(new MyPerson { Person = item, Bank = bName });
            }
            return mp;
        }
        [HttpGet]
        [Route("list")]
        public List<MyPerson> Get()
        {
            var persons = _unitOfWork.Persons.All().Result.ToList(); 
            var banks = _unitOfWork.Banks.All().Result.ToList();
            List<MyPerson> mp = new List<MyPerson>();
            foreach (var item in persons)
            {
               var b = ibanChecker(item.IBAN);
                var bName = banks.Where(x => x.SwiftCode == b.Result).Select(a=>a.Name).FirstOrDefault();
                mp.Add(new MyPerson {Person = item,Bank = bName });
            }
            return mp;
        } 

        [HttpPut]
        [Route("update")]
        public async Task<bool> UpdateIban(int id,string newIBAN)
        { 
            var s = ibanChecker(newIBAN);
            var person =_unitOfWork.Persons.Get(id).Result;
            if (person != null)
            {
                if (!string.IsNullOrEmpty(s.Result))
                {
                    person.IBAN = newIBAN;
                    await _unitOfWork.Persons.Update(person);
                    await _unitOfWork.CompleteAsync();
                    return true;
                }
                return false;
            }
            return false;
        }
        [HttpPost]
        [Route("add")]
        public async Task<bool> AddPerson(Person person)
        {
            if (person != null) 
            {
                var s = ibanChecker(person.IBAN);
                if (!string.IsNullOrEmpty(s.Result))
                { 
                await _unitOfWork.Persons.Save(person);
                    await _unitOfWork.CompleteAsync();
                }
             return true;
            }
            return false;
           
        }
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
        [HttpGet]
        [Route("ibancheck")]
        public async Task<string> ibanChecker(string IBAN)
        { 
            string apiKey = _appSettings.Value.apiKey;
            string apiUrl = $"https://api.ibanapi.com/v1/validate/{IBAN}?api_key={apiKey}";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var parsedata = JObject.Parse(data);
                    var bank =  parsedata["data"]["bank"];
                    var swiftCode = bank["bic"].ToString();
                    if (!string.IsNullOrEmpty(swiftCode)) 
                        return swiftCode; 
                } 
                return null;
            } 
        }
    }
}
