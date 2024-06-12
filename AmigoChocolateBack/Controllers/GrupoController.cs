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
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Grupo>>> GetGrupos()
        //{
        //    return await _context.Grupos.ToListAsync();
        //}

        //    [HttpGet]
        //    [Route("Get")]
        //public async Task<ActionResult<IEnumerable<Grupo>>> GetGrupos()
        //    {
        //        try
        //        {
        //            var grupos = await _context.Grupos.ToListAsync();
        //            return Ok(grupos);
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log detalhado da exceção
        //            Console.WriteLine($"Erro ao buscar grupos: {ex.Message}\n{ex.StackTrace}");
        //            return StatusCode(500, "Erro interno no servidor");
        //        }
        //    }

        // GET: api/Grupos/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Grupo>> GetGrupo(int id)
        //{
        //    var grupo = await _context.Grupos.FindAsync(id);

        //    if (grupo == null)
        //    {
        //        return NotFound();
        //    }

        //    return grupo;
        //}

        //[HttpGet]
        //[Route("BuscarGrupoPorID/{id}")]
        //public async Task<IActionResult> ObterGrupoPorId(int id)
        //{
        //    Console.WriteLine("Chamado ObterGrupoPorId com ID: " + id);
        //    try
        //    {
        //        var grupo = await _context.Grupos.FindAsync(id);
        //        if (grupo == null)
        //        {
        //            return NotFound(); // Retorna 404 se o grupo não for encontrado
        //        }

        //        var grupoDto = new GrupoDto
        //        {
        //            IDGrupo = grupo.IDGrupo,
        //            NomeGrupo = grupo.NomeGrupo,
        //            QuantidadeMaxima = grupo.QuantidadeMaxima,
        //            ValorChocolate = grupo.ValorChocolate,
        //            DataRevelacao = grupo.DataRevelacao.ToString("yyyy-MM-ddTHH:mm:ss"),
        //            Descricao = grupo.Descricao,
        //            Icone = grupo.Icone
        //        };

        //        return Ok(grupoDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return StatusCode(500, "Erro interno no servidor: " + ex.Message);
        //    }
        //}

        [HttpGet]
        [Route("BuscarGrupoPorID/{id}")]
        public async Task<IActionResult> ObterGrupoPorId(int id)
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

                return CreatedAtAction(nameof(GetGrupo), new { id = novoGrupo.IDGrupo }, novoGrupo);
            }

            // Método de exemplo para obter um grupo pelo ID
            //[HttpGet("{id}")]
            //public async Task<ActionResult<Grupo>> GetGrupo(int id)
            //{
            //    var grupo = await _context.Grupos.FindAsync(id);

            //    if (grupo == null)
            //    {
            //        return NotFound();
            //    }

            //    return grupo;
            //}
        }


        // POST: api/Grupos
        //[HttpPost]
        //[Route("api/Grupos")]
        ////public async Task<ActionResult<Grupo>> PostGrupo(Grupo grupo)
        //public async Task<IActionResult> CriarGrupo([FromForm] GrupoDto grupoDto)
        //{
        //    try
        //    {
        //        _context.Grupos.Add(grupo);
        //        await _context.SaveChangesAsync();
        //        return CreatedAtAction("GetGrupo", new { id = grupo.IDGrupo }, grupo);
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        // Log detalhado da exceção específica do Entity Framework
        //        Console.WriteLine($"Erro ao atualizar o banco de dados: {dbEx.Message}\n{dbEx.StackTrace}");
        //        return StatusCode(500, "Erro ao atualizar o banco de dados");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log detalhado da exceção genérica
        //        Console.WriteLine($"Erro ao criar grupo: {ex.Message}\n{ex.StackTrace}");
        //        return StatusCode(500, "Erro ao criar grupo");
        //    }
        //}

    //}

}
