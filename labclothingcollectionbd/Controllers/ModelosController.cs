using labclothingcollection.DTO.ModelosDTO.Request;
using labclothingcollection.DTO.ModelosDTO.Response;
using labclothingcollectionbd.Context;
using labclothingcollectionbd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labclothingcollectionbd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelosController : ControllerBase
    {
        private readonly LabClothingCollectionBDContext _context;

        public ModelosController(LabClothingCollectionBDContext context)
        {
            _context = context;
        }


        /// <summary>Consulta do objeto Modelos cadastrados.</summary>
        /// <returns>Retorna o objeto Modelos, com os modelos cadastrados</returns>
        /// <response code = "200"> Sucesso no retorno do objeto modelos cadastrados</response>
        [HttpGet]
        public ActionResult<List<ModelosResponseDTO>> GetAll()
        {
            var modelos = _context.Modelos.ToList();

            return Ok(modelos);
        }


        /// <summary> Consulta de modelos, pelo seu código identificador, no objeto Modelos. </summary>
        /// <param name = "id">Id do modelo cadstrado</param>
        /// <returns>Resposta HTTP com a lista de Modelos</response>
        /// <response code = "200">Sucesso no retorno do modelo no objeto Modelos</response>
        /// <response code = "404">Não foi encontrado registro com o Id informado</response>      
        [HttpGet("{id}")]
        public ActionResult<ModelosResponseDTO> GetById(int id)
        {
            var modelo = _context.Modelos.FirstOrDefault(c => c.IdModelo == id);
            if (modelo == null)
            {
                return NotFound("Não foi possivel encontrar o Modelo na base de dados!");
            }

            return Ok(modelo);
        }


        /// <summary>Cadastra um novo modelo  na lista Modelos</summary>
        /// <param name = "modelo">Modelo</param>
        /// <returns>Resposta HTTP com os dados da coleção</returns>
        /// <response code = "201">Sucesso no post do objeto Modelos. </response>
        /// <response code = "400">Requisição com dados invalidos para o objeto Modelos. </response>
        /// <response code = "409">Modelo já cadastrado no objeto Modelos. </response>        
        [HttpPost]
        public async Task<ActionResult<ModelosRequestDTO>> Create([FromBody] Modelos modelo)
        {
            if (await _context.Modelos.AnyAsync(c => c.NomeModelo == modelo.NomeModelo))
            {
                return Conflict("Este Modelo já está cadastrado na base de dados.");
            }

            _context.Modelos.Add(modelo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = modelo.IdModelo }, modelo);
        }


        /// <summary>Atualização de um  modelo no objeto Modelos</summary>
        /// <param name = "id">Id do Modelo</param>
        /// <param name = "modelo">Lista Modelos com as novas caracteristicas do modelo</param>
        /// <returns>HTTP com os dados atualizados da coleção</returns>
        /// <response code = "200">Sucesso na atualização do modelo no objeto Modelos</response>
        /// <response code = "400">Requisição com dados inválidos</response>
        /// <response code = "404">Não foi encontrado  registro com o Id informado</response>       
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ModelosRequestDTO modelo)
        {
            var modeloExistente = _context.Modelos.FirstOrDefault(c => c.IdModelo == id);
            if (modeloExistente == null)
            {
                return NotFound("Não foi possível encontrar o Modelo na base de dados!");
            }

            if (modeloExistente.NomeModelo != modelo.NomeModelo && _context.Modelos.Any(c => c.NomeModelo == modelo.NomeModelo))
            {
                return Conflict("O nome de Modelo informado já está cadastrado na base de dados.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Os dados fornecidos são inválidos!");
            }

            modeloExistente.NomeModelo = modelo.NomeModelo;
            modeloExistente.Tipo = modelo.Tipo;
            modeloExistente.Layout = modelo.Layout;
            modeloExistente.IdColecaoRelacionada = modelo.IdColecaoRelacionada;

            _context.SaveChanges();

            return Ok(modeloExistente);
        }



        /// <summary>Remoção de um modelo na lista Modelos</summary>
        /// <param name="id"> Id do Modelo</param>
        /// <returns>Resposta HTTP indicando o resultado da exclusão</returns>
        /// <reponse code="204"> Modelo removido da lista Modelos</reponse>
        /// <reponse code="404"> Não foi encontrado  registro com o Id informado</reponse>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var modelo = _context.Modelos.FirstOrDefault(c => c.IdModelo == id);
            if (modelo == null)
            {
                return NotFound("Modelo não encontrado na base de dados!");
            }

            _context.Modelos.Remove(modelo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}