using FlexeraAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FlexeraAPI.Controllers
{
    [Route("app/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly FlexeraContext _context;

        public PeopleController(FlexeraContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lists all existing people.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople(string orderby)
        {
            var people = await _context.People.ToListAsync();
            PropertyInfo prop = typeof(Person).GetProperty(orderby);

            return prop != null ? people.OrderBy(x => prop.GetValue(x)).ToList() : people;
        }

        /// <summary>
        /// Lists a specfic person.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.People.FindAsync(id);

            return person == null ? NotFound() : (ActionResult<Person>)person;
        }

        /// <summary>
        /// Creates a new person.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Person>> CreatePerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest(GetErrorMessages(ModelState));

            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);
        }

        /// <summary>
        /// Deletes an existing person.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.People.FindAsync(id);

            if (person == null) { return NotFound(); }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Updates an existing person.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static List<string> GetErrorMessages(ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                             .Select(m => m.ErrorMessage)
                             .ToList();
        }
    }
}
