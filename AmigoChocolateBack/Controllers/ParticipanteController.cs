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
    public class ParticipantesController : ControllerBase
    {
        private readonly AmigoChocolateBackContext _context;

        public ParticipantesController(AmigoChocolateBackContext context)
        {
            _context = context;
        }

        // GET: api/Participantes
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Participante>>> GetParticipantes()
        {
            try
            {
                var participantes = await _context.Participantes.ToListAsync();
                return Ok(participantes);
            }
            catch (Exception ex)
            {
                // Log detalhado da exceção
                Console.WriteLine($"Erro ao buscar participantes: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        // GET: api/Participantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participante>> GetParticipante(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);

            if (participante == null)
            {
                return NotFound();
            }

            return participante;
        }

        // POST: api/Participantes
        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> CriarParticipante([FromBody] Participante participante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Participantes.Add(participante);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetParticipante), new { id = participante.IDParticipante }, participante);
            }
            catch (DbUpdateException dbEx)
            {
                // Log detalhado da exceção específica do Entity Framework
                Console.WriteLine($"Erro ao atualizar o banco de dados: {dbEx.Message}\n{dbEx.StackTrace}");
                return StatusCode(500, "Erro ao atualizar o banco de dados");
            }
            catch (Exception ex)
            {
                // Log detalhado da exceção genérica
                Console.WriteLine($"Erro ao criar participante: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, "Erro ao criar participante");
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Participante participante)
        {
            try
            {
                var user = _context.Participantes.FirstOrDefault(u => u.EmailParticipante == participante.EmailParticipante && u.SenhaParticipante == participante.SenhaParticipante);
                if (user == null)
                {
                    return Unauthorized();
                }

              //  var token = _authService.GenerateToken(user.Email);
                return Ok(new { Token = "token" });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro ao logar: {ex}");
                return StatusCode(500);
            }
        }

        // PUT: api/Participantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarParticipante(int id, [FromBody] Participante participante)
        {
            if (id != participante.IDParticipante)
            {
                return BadRequest();
            }

            _context.Entry(participante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipanteExists(id))
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

        private bool ParticipanteExists(int id)
        {
            return _context.Participantes.Any(e => e.IDParticipante == id);
        }
    }
}
