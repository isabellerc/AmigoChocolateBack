using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmigoChocolateBack.Models;
using AmigoChocolateBack.Dados;
using System.Text.RegularExpressions;


namespace AmigoChocolateBack.Controllers
{
   

        [Route("api/[controller]")]
        [ApiController]
        public class GruposController : ControllerBase
        {
            private readonly AmigoChocolateBackContext _context;

            public GruposController(AmigoChocolateBackContext context)
            {
                _context = context;
            }

            // GET: api/Grupos
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Grupo>>> GetGrupos()
            {
                return await _context.Grupos.ToListAsync();
            }

            // GET: api/Grupos/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Grupo>> GetGrupo(int id)
            {
                var grupo = await _context.Grupos.FindAsync(id);

                if (grupo == null)
                {
                    return NotFound();
                }

                return grupo;
            }

            // POST: api/Grupos
            [HttpPost]
            public async Task<ActionResult<Grupo>> PostGrupo(Grupo grupo)
            {
                _context.Grupos.Add(grupo);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetGrupo", new { id = grupo.IDGrupo }, grupo);
            }
        }
    
}
