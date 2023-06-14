using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data.DataContext;
using UKParliament.CodeTest.Data.Models;

namespace UKParliament.CodeTest.Services.Interfaces.InMemory
{
    public class PersonServiceInMemory : IPersonService
    {
        public List<Person> _persons;
        private readonly PersonManagerContext _context;

        public PersonServiceInMemory()
        {
            _persons = new List<Person>()
            {
                new Person()
                {
                    Id = 1,
                    Name = "David WhiteLaw"
                },
                new Person()
                {
                    Id = 2,
                    Name = "David Room"
                },
                new Person()
                {
                    Id = 3,
                    Name = "Lisa Abraham"
                },
                new Person()
                {
                    Id = 4,
                    Name = "Joseph Law"
                },
                new Person()
                {
                    Id = 5,
                    Name = "Salman Tariq"
                }
            };
        }
        public Task<List<Person>> AddPersonyAsync(Person person)
        {

            if (_persons.Any(x => x.Name.Equals(person.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.FromResult(_persons.ToList());
            }
            var maxId = _persons.Max(x => x.Id);

            person.Id = maxId + 1;
            _persons.Add(person);

            //_context.Add(_persons);
            //_context.SaveChanges();

            return Task.FromResult(_persons.ToList());
        }

        public async Task<bool> ExistsAsync(Person person)
        {
            return await Task.FromResult(_persons.Any(x => x.Name.Equals(person.Name, StringComparison.OrdinalIgnoreCase)));
        }

        public async Task<List<Person>> GetAllPersons()
        {
            return await Task.FromResult(_persons.ToList());
        }

        public async Task<Person> GetpersonByIdAsync(int personId)
        {
            var per = _persons.First(x => x.Id == personId);

            return await Task.FromResult(per);
        }

        public async Task<IEnumerable<Person>> GetpersonsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await Task.FromResult(_persons);

            return _persons.Where(n => n.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public Task RemovePerson(int Id)
        {
            if (_persons.First(x => x.Id == Id) != null)
            {
                _persons.Remove(_persons.First(x => x.Id == Id));
            }

            return Task.CompletedTask;
        }

        public Task UpdatePersonAsync(Person person)
        {
            if (_persons.Any(x => x.Id == person.Id &&
            x.Name.Equals(person.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }

            var per = _persons.FirstOrDefault(x => x.Id == person.Id);

            if (per != null)
            {
                per.Name = person.Name;
            }

            return Task.CompletedTask;
        }
    }
}
