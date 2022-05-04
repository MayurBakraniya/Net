using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoapi.Models;

namespace todoapi.Controllers
{
    [Route("api/todoitems")]
    [ApiController]
    public class todoitemsController : ControllerBase
    {
        private readonly todocontext _context;

        public todoitemsController(todocontext context)
        {
            _context = context;
        }

        // GET: api/todoitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<todoitemdto>>> Gettodoitems()
        {
            return await _context.todoitems
                .Select(x => ItemtoDTO(x))
                .ToListAsync();
        }

        // GET: api/todoitems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<todoitemdto>> Gettodoitem(long id)
        {
            var todoitem = await _context.todoitems.FindAsync(id);

            if (todoitem == null)
            {
                return NotFound();
            }

            return ItemtoDTO(todoitem);
        }

        // PUT: api/todoitems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Puttodoitem(long id, todoitemdto todoitemdto)
        {
            if (id != todoitemdto.Id)
            {
                return BadRequest();
            }

            var todoitem = await _context.todoitems.FindAsync(id);

            if(todoitem == null)
            {
                return NotFound();
            }

            todoitem.Name = todoitemdto.Name;
            todoitem.IsComplete = todoitemdto.IsComplete;

            //_context.Entry(todoitem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!todoitemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/todoitems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<todoitemdto>> Posttodoitem(todoitemdto todoitemdto)
        {
            var todoitem = new todoitem
            {
                IsComplete = todoitemdto.IsComplete,
                Name = todoitemdto.Name
            };
            _context.todoitems.Add(todoitem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Gettodoitem),
                new {id = todoitem.Id},
                ItemtoDTO(todoitem)
                );
        }

        // DELETE: api/todoitems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletetodoitem(long id)
        {
            var todoitem = await _context.todoitems.FindAsync(id);
            if (todoitem == null)
            {
                return NotFound();
            }

            _context.todoitems.Remove(todoitem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool todoitemExists(long id) =>
            _context.todoitems.Any(e => e.Id == id);

        private static todoitemdto ItemtoDTO(todoitem item) =>
            new todoitemdto
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete
            };
    }
}
