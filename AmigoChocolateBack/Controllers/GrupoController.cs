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
    [ApiController]
    [Route("api/[controller]")]
        
        public class GruposController : ControllerBase
        {
            private readonly AmigoChocolateBackContext _context;

            public GruposController(AmigoChocolateBackContext context)
            {
                _context = context;
            }

        [HttpGet]
        [Route("BuscarGrupoPorID")]
        public async Task<IActionResult> BuscarGrupoPorId(int id)
        {
            try
            {
                var grupo = await _context.Grupos.FindAsync(id);
                if (grupo == null)
                {
                    Console.WriteLine($"Grupo não encontrado: {id}");
                    return NotFound(); // Retorna 404 se o grupo não for encontrado
                }

                var grupoDto = new GrupoDto
                {
                    IDGrupo = grupo.IDGrupo,
                    NomeGrupo = grupo.NomeGrupo,
                    QuantidadeMaxima = grupo.QuantidadeMaxima,
                    ValorChocolate = grupo.ValorChocolate,
                    DataRevelacao = grupo.DataRevelacao.ToString("yyyy-MM-ddTHH:mm:ss"),
                    Descricao = grupo.Descricao,
                    Icone = grupo.Icone
                };

                Console.WriteLine($"Grupo encontrado: {grupoDto.IDGrupo}");
                return Ok(grupoDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar grupo por ID {id}: {ex.Message}");
                return StatusCode(500, "Erro interno no servidor: " + ex.Message);
            }
        }



        [HttpGet]
        [Route("buscarGrupos")]
        public async Task<IActionResult> ObterGrupos()
        {
            try
            {
                var grupos = await _context.Grupos.ToListAsync();

                var gruposDto = grupos.Select(g => new GrupoDto
                {
                    IDGrupo = g.IDGrupo,  // Certifique-se de mapear IDGrupo
                    NomeGrupo = g.NomeGrupo,
                    QuantidadeMaxima = g.QuantidadeMaxima,
                    ValorChocolate = g.ValorChocolate,
                    DataRevelacao = g.DataRevelacao.ToString("yyyy-MM-ddTHH:mm:ss"), // Formate aqui para string
                    Descricao = g.Descricao,
                    Icone = g.Icone
                }).ToList();

                return Ok(gruposDto);
            }
            catch (Exception ex)
            {
                // Use logging para capturar o erro
                Console.WriteLine(ex);
                return StatusCode(500, "Erro interno no servidor: " + ex.Message);
            }
        }


        [HttpPost]
            [Route("Post")]
        public async Task<IActionResult> CriarGrupo([FromForm] GrupoDto grupoDto)
            {
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("TESTE");
                    return BadRequest(ModelState);
                
                }

            string formatoData = "yyyy-MM-ddTHH:mm:ss";

            if (!DateTime.TryParseExact(grupoDto.DataRevelacao, formatoData, null, System.Globalization.DateTimeStyles.None, out DateTime dataRevelacao))
            {
                return BadRequest("Formato de data inválido. O formato correto é: " + formatoData);
            }

            var novoGrupo = new Grupo
            {
                NomeGrupo = grupoDto.NomeGrupo,
                QuantidadeMaxima = grupoDto.QuantidadeMaxima,
                ValorChocolate = grupoDto.ValorChocolate,
                DataRevelacao = dataRevelacao,
                Descricao = grupoDto.Descricao
            };

            // Processamento do arquivo de ícone
            //if (grupoDto.Icone != null)
            //{
            //    using (var memoryStream = new MemoryStream())
            //    {
            //        await grupoDto.Icone.CopyToAsync(memoryStream);
            //        novoGrupo.Icone = Convert.ToBase64String(memoryStream.ToArray()); // Salva como base64
            //    }
            //}

            _context.Grupos.Add(novoGrupo);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(ObterGrupos), new { id = novoGrupo.IDGrupo }, novoGrupo);
            }
        }
    

}
