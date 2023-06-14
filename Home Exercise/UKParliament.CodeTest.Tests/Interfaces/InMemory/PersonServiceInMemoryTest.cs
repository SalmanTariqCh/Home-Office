using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data.Models;
using UKParliament.CodeTest.Services.Interfaces.InMemory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UKParliament.CodeTest.Tests.Interfaces.InMemory
{
    public class PersonServiceInMemoryTest
    {
        private PersonServiceInMemory personServiceInMemory;
        private Mock<IPersonService> personServiceMock;
        private Person mockPerson;
        private List<Person> mockPersonList;
        private string mockPersonName;

        [SetUp]
        public async Task SetUp()
        {
            mockPerson = new Person()
            {
                Id = 3,
                Name = "Test",
            };

            mockPersonList = new List<Person>()
            {
                 new Person()
                {
                    Id = 1,
                    Name = "Test1 Name"
                },
                new Person()
                {
                    Id = 2,
                    Name = "Test2 Name"
                }
            };

            mockPersonName = "Donald";

            personServiceInMemory = new PersonServiceInMemory();
            personServiceMock = new Mock<IPersonService>();
            
            personServiceMock.Setup(a => a.AddPersonyAsync(mockPerson)).ReturnsAsync(mockPersonList);

            await personServiceInMemory.AddPersonyAsync(mockPerson);
            await personServiceInMemory.ExistsAsync(mockPerson);
            await personServiceInMemory.GetAllPersons();
            await personServiceInMemory.GetpersonsByNameAsync(mockPerson.Name);

        }

        [Test]
        public async Task CheckPersonServiceInMemory_WhenCalled_AddPersonyAsyncAsync()
        {
            var res = await personServiceInMemory.AddPersonyAsync(mockPerson);
            Assert.That(res, Has.Count.EqualTo(6));
        }

        [Test]
        public async Task CheckPersonServiceInMemory_WhenCalled_ExistsAsyncReturnsTrue()
        {
            var res = await personServiceInMemory.ExistsAsync(mockPerson);
            Assert.That(res, Is.EqualTo(true));
        }

        [Test]
        public async Task CheckPersonServiceInMemory_WhenCalled_GetAllPersonsReturnsData()
        {
            var res = await personServiceInMemory.GetAllPersons();
            Assert.That(res, Has.Count.EqualTo(6));
        }

        [Test]
        public async Task CheckPersonServiceInMemory_WhenCalled_GetpersonsByNameAsyncReturnsPerson()
        {
            var result = await personServiceInMemory.GetpersonsByNameAsync(mockPersonName);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task CheckPersonServiceInMemory_WhenCalled_RemovePersonThrowsNothing()
        {
            Assert.That(async () => await personServiceInMemory.RemovePerson(1), Throws.Nothing);
        }

        [Test]
        public async Task CheckPersonServiceInMemory_WhenCalled_UpdatePersonAsyncThrowsNothing()
        {
            Assert.That(async () => await personServiceInMemory.UpdatePersonAsync(mockPerson), Throws.Nothing);
        }
    }
}
