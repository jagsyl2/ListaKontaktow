using ListaKontaktow.BusinessLayer.Services;
using ListaKontaktow.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ListaKontaktow.Controllers
{
    [Route("api/persons")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController (IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        public void PostPerson([FromBody] Person person)
        {
            _personService.Add(person);
        }

        [HttpPut("{id}")]
        public void UpdatePerson([FromBody] Person person, int id)
        {
            person.Id = id;
            _personService.Update(person);
        }

        [HttpGet]
        public List<Person> GetAllPersons()
        {
            return _personService.GetAll();
        }

        //http://localhost:10500/api/persons/find?name=XXX&surname=YYY
        [HttpGet("find")]
        public Person GetPersonByNameAndSurname ([FromQuery] string name, [FromQuery] string surname)
        {
            return _personService.GetPersonByNameAndSurname(name, surname);
        }

        [HttpDelete("{id}")]
        public void DeletePerson(int id)
        {
            _personService.Delete(new Person { Id = id });
        }
    }
}
