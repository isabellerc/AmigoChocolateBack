using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AmigoChocolateBack.Dados;
using AmigoChocolateBack.Models;
using System.Data.SqlClient;

namespace AmigoChocolateBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoParticipanteController : ControllerBase
    {
        private readonly AmigoChocolateBackContext _context;

        public GrupoParticipanteController(AmigoChocolateBackContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostGrupoParticipante([FromBody] GrupoParticipante2Dto grupoParticipanteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var grupoParticipante = new GrupoParticipante2
            {
                IDGrupo = grupoParticipanteDto.IDGrupo,
                IDParticipante = grupoParticipanteDto.IDParticipante,
                IDGrupoParticipante = grupoParticipanteDto.IDGrupo // Garante que IDGrupoParticipante seja igual a IDGrupo
            };

            try
            {
                _context.GrupoParticipante2.Add(grupoParticipante);
                await _context.SaveChangesAsync();

                return Ok("Participante adicionado ao grupo com sucesso");
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
            {
                return Conflict("Participante já adicionado ao grupo");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar participante ao grupo: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        // Método GET para obter todos os participantes de um grupo específico
        [HttpGet("BuscarParticipantesDoGrupo/{idGrupo}")]
        public async Task<IActionResult> BuscarParticipantesDoGrupo(int idGrupo)
        {
            try
            {
                var participantes = await _context.GrupoParticipante2
                    .Where(gp => gp.IDGrupo == idGrupo)
                    .Select(gp => new
                    {
                        gp.IDGrupoParticipante,
                        gp.IDGrupo,
                        gp.IDParticipante,
                        Participante = gp.Participante != null ? new
                        {
                            gp.Participante.IDParticipante,
                            gp.Participante.NomeParticipante,
                            gp.Participante.EmailParticipante
                        } : null,
                        Grupo = gp.Grupo != null ? new
                        {
                            gp.Grupo.IDGrupo,
                            gp.Grupo.NomeGrupo,
                            gp.Grupo.Descricao
                        } : null
                    })
                    .ToListAsync();

                if (participantes == null || !participantes.Any())
                {
                    return NotFound("Nenhum participante encontrado para o grupo especificado.");
                }

                return Ok(participantes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter participantes do grupo: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        // Método GET para obter todos os registros de participantes de grupos
        [HttpGet("GetAllGrupoParticipantes")]
        public async Task<IActionResult> GetAllGrupoParticipantes()
        {
            try
            {
                var grupoParticipantes = await _context.GrupoParticipante2
                    .Select(gp => new
                    {
                        gp.IDGrupoParticipante,
                        gp.IDGrupo,
                        gp.IDParticipante,
                        Participante = gp.Participante != null ? new
                        {
                            gp.Participante.IDParticipante,
                            gp.Participante.NomeParticipante,
                            gp.Participante.EmailParticipante
                        } : null,
                        Grupo = gp.Grupo != null ? new
                        {
                            gp.Grupo.IDGrupo,
                            gp.Grupo.NomeGrupo,
                            gp.Grupo.Descricao
                        } : null
                    })
                    .ToListAsync();

                if (grupoParticipantes == null || !grupoParticipantes.Any())
                {
                    return NotFound("Nenhum participante encontrado.");
                }

                return Ok(grupoParticipantes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter todos os participantes de grupos: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }
    }
}
