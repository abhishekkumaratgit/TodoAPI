using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    public class TodosController : BaseApiController
    {
        private readonly TodoContext _context;

        public TodosController(TodoContext context)
        {
            _context = context;

            if (_context.Todos.Count() == 0)
            {
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 1, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 2, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 3, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 4, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 5, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 6, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 7, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 8, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 9, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 10, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 11, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 12, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 13, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 14, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 15, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 16, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 17, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 18, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 19, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 20, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 21, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 22, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 23, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 24, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 25, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 26, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 27, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 28, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 29, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 30, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 31, IsComplete = false });
                _context.Todos.Add(new Todo { Name = "Buy milk", Id = 32, IsComplete = false });
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns list of all todos.
        /// </summary>
        /// <returns></returns>
        // GET: api/Todos
        [HttpGet]
        public IEnumerable<Todo> Get()
        {
            return _context.Todos;
        }

        /// <summary>
        /// Returns a todo with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">When requested todo not found</response>
        /// <response code="200">Return the requested todo</response>

        // GET: api/Todos/5
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(Todo), 200)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var todo = await _context.Todos.SingleOrDefaultAsync(m => m.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        /// <summary>
        /// Update a specific todo item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todo"></param>
        /// <returns></returns>
        /// <response code="400">For bad request</response>
        /// <response code="204">When update is successful</response>
        /// <response code="404">When to be updated resource is deleted by someone else</response>
        // PUT: api/Todos/5
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Todo todo)
        {
            // Model state checking not required due to use of ApiController
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != todo.Id)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Todos
        /// <summary>
        /// Creates a todo item.
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Todos
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>

        [HttpPost]
        [ProducesResponseType(typeof(Todo), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(Todo todo)
        {
            // Model state checking not required due to use of ApiController
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = todo.Id }, todo);
        }

        /// <summary>
        /// Deletes a specific todo item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">When not found</response>
        /// <response code="204">Success</response>
        // DELETE: api/Todos/5
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var todo = await _context.Todos.SingleOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(int id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}