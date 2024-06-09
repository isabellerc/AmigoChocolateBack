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

            [HttpGet]
            [Route("Get")]
        public async Task<ActionResult<IEnumerable<Grupo>>> GetGrupos()
            {
                try
                {
                    var grupos = await _context.Grupos.ToListAsync();
                    return Ok(grupos);
                }
                catch (Exception ex)
                {
                    // Log detalhado da exceção
                    Console.WriteLine($"Erro ao buscar grupos: {ex.Message}\n{ex.StackTrace}");
                    return StatusCode(500, "Erro interno no servidor");
                }
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

        [HttpGet]
        [Route("BuscarGrupoPorID/{id}")]
        public async Task<ActionResult<Grupo>> BuscarGrupoPorID(int id)
        {
            try
            {
                var grupo = await _context.Grupos.FindAsync(id);

                if (grupo == null)
                {
                    return NotFound();
                }

                return Ok(grupo);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro ao buscar grupo: {ex}");
                return StatusCode(500);
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

                var novoGrupo = new Grupo
                {
                    NomeGrupo = grupoDto.NomeGrupo,
                    QuantidadeMaxima = grupoDto.QuantidadeMaxima,
                    ValorChocolate = grupoDto.ValorChocolate,
                    DataRevelacao = grupoDto.DataRevelacao,
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
