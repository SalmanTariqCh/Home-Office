using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data.Models;
using UKParliament.CodeTest.Services.DataContext;
using UKParliament.CodeTest.Services.Interfaces.InMemory;

namespace UKParliament.CodeTest.Services.Interfaces.EFCoreSql
{
    public class PersonServiceEFCore : IPersonServiceEFCore
    {
        private readonly EFCorePersonContext _db;

        public PersonServiceEFCore(EFCorePersonContext db)
        {
            _db = db;
        }
        public Task AddPersonyAsync(Person person)
        {

            if (!_db.People.Any(x => x.Name.Equals(person.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }
            var maxId = _db.People.Max(x => x.Id);

            person.Id = maxId + 1;
            _db.Add(person);

            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Person person)
        {
            return await Task.FromResult(_db.People.Any(x => x.Name.Equals(person.Name, StringComparison.OrdinalIgnoreCase)));
        }

        public async Task<Person> GetpersonByIdAsync(int personId)
        {
            var per = _db.People.First(x => x.Id == personId);

            return await Task.FromResult(per);
        }

        public async Task<IEnumerable<Person>> GetpersonsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await Task.FromResult(_db.People);

            return _db.People.Where(n => n.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public Task RemovePerson(int Id)
        {
            if (_db.People.First(x => x.Id == Id) != null)
            {
                _db.People.Remove(_db.People.First(x => x.Id == Id));
            }

            return Task.CompletedTask;
        }

        public Task UpdatePersonAsync(Person person)
        {
            if (_db.People.Any(x => x.Id == person.Id &&
            x.Name.Equals(person.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }

            var per = _db.People.FirstOrDefault(x => x.Id == person.Id);

            if (per != null)
            {
                per.Name = person.Name;
            }

            return Task.CompletedTask;
        }
    }
}
