using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.Model.Author;
using AutoMapper;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using BookStoreApp.API.Static;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper mapper;
        private readonly ILogger<AuthorsController> logger;
        public AuthorsController(BookStoreDbContext context ,IMapper mapper, ILogger<AuthorsController> Logger)
        {
            _context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadOnlyDTO>>> GetAuthor()
        {
     
            try
            {

                var authors = await _context.Author.ToListAsync();
                var authorsDtos = mapper.Map<IEnumerable<AuthorReadOnlyDTO>>(authors);
                return Ok(authorsDtos);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Perfoming Gets in{nameof(GetAuthor)}");
                return StatusCode(500, Messages.Error500Message);

            }

        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadOnlyDTO>>  GetAuthor(int id)
        {
            
            try
            {

                var author = await _context.Author.FindAsync(id);

                if (author == null)
                {
                    logger.LogWarning($"Record not found : {nameof(GetAuthor)} - ID {id}");
                    return NotFound();
                }

                var authorDto = mapper.Map<AuthorReadOnlyDTO>(author);
                return Ok(authorDto);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Perfoming Gets in{nameof(GetAuthor)}");
                return StatusCode(500, Messages.Error500Message);

            }
           
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDTO authorDto)
        { 
            if (id != authorDto.Id)
            {
                return BadRequest();
            }
            var author = await _context.Author.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }    

              mapper.Map(authorDto, author);
            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AuthorExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorCreateDTO>> PostAuthor(AuthorCreateDTO authorDto)
        {
            try
            {
                var author = mapper.Map<Author>(authorDto);
                await _context.Author.AddAsync(author);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Author.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private async Task<bool> AuthorExists(int id)
        {
            return await _context.Author.AnyAsync(e => e.Id == id);
        }
    }
}
