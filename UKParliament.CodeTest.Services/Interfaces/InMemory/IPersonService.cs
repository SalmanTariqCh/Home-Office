using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data.Models;

namespace UKParliament.CodeTest.Services.Interfaces.InMemory
{
    public interface IPersonService
    {
        Task<List<Person>> AddPersonyAsync(Person person);
        Task<IEnumerable<Person>> GetpersonsByNameAsync(string name);
        Task<bool> ExistsAsync(Person person);
        Task UpdatePersonAsync(Person person);
        Task<Person> GetpersonByIdAsync(int personId);
        Task RemovePerson(int Id);
        Task<List<Person>> GetAllPersons();
    }
}
