using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmigoChocolateBack.Models;
using AmigoChocolateBack.Dados;
using Microsoft.Extensions.Logging;

namespace AmigoChocolateBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantesController : ControllerBase
    {
        private readonly AmigoChocolateBackContext _context;
        private readonly ILogger<ParticipantesController> _logger;

        public ParticipantesController(AmigoChocolateBackContext context, ILogger<ParticipantesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Participantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participante>>> GetParticipantes()
        {
            try
            {
                var participantes = await _context.Participantes.ToListAsync();
                return Ok(participantes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao buscar participantes: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        // GET: api/Participantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participante>> GetParticipante(int id)
        {
            try
            {
                var participante = await _context.Participantes.FindAsync(id);
                if (participante == null)
                {
                    return NotFound();
                }
                return participante;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao buscar participante: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        // POST: api/Participantes
        [HttpPost]
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
                _logger.LogError($"Erro ao atualizar o banco de dados: {dbEx.Message}\n{dbEx.StackTrace}");
                return StatusCode(500, "Erro ao atualizar o banco de dados");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao criar participante: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, "Erro ao criar participante");
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
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar participante: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, "Erro ao atualizar participante");
            }

            return NoContent();
        }

        // DELETE: api/Participantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirParticipante(int id)
        {
            try
            {
                var participante = await _context.Participantes.FindAsync(id);
                if (participante == null)
                {
                    return NotFound();
                }

                _context.Participantes.Remove(participante);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir participante: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, "Erro ao excluir participante");
            }
        }

        private bool ParticipanteExists(int id)
        {
            return _context.Participantes.Any(e => e.IDParticipante == id);
        }
    }
}
