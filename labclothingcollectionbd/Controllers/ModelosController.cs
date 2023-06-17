using labclothingcollection.DTO.ModelosDTO.Request;
using labclothingcollection.DTO.ModelosDTO.Response;
using labclothingcollectionbd.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace labclothingcollectionbd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelosController : ControllerBase
    {
        private readonly List<Modelos> _modelos;

        public ModelosController()
        {
            _modelos = new List<Modelos>();

            _modelos.Add(new Modelos { IdModelo = 1, NomeModelo = "modelo 1", IdColecaoRelacionada = 1, Tipo = "bermuda", Layout = "bordado" });
            _modelos.Add(new Modelos { IdModelo = 2, NomeModelo = "modelo 2", IdColecaoRelacionada = 2, Tipo = "biquini", Layout = "Bordado"});
            _modelos.Add(new Modelos { IdModelo = 3, NomeModelo = "modelo 3", IdColecaoRelacionada = 3, Tipo = "bolsa",   Layout = "estampa" });
            _modelos.Add(new Modelos { IdModelo = 4, NomeModelo = "modelo 4", IdColecaoRelacionada = 4, Tipo = "bone",    Layout = "Estampa"});
            _modelos.Add(new Modelos { IdModelo = 5, NomeModelo = "modelo 5", IdColecaoRelacionada = 5, Tipo = "calça",   Layout = "estampa"});
            _modelos.Add(new Modelos { IdModelo = 6, NomeModelo = "modelo 6", IdColecaoRelacionada = 5, Tipo = "camisa",  Layout = "Liso" });
            _modelos.Add(new Modelos { IdModelo = 7, NomeModelo = "modelo 7", IdColecaoRelacionada = 5, Tipo = "saia",    Layout = "liso" });
        }


        /// <summary> Consulta do objeto Modelos cadastrados. </summary>
        /// <returns> Retorna o objeto Modelos, com os modelos cadastrados. </returns>
        /// <response code = "200"> Sucesso no retorno do objeto modelos cadastrados. </response>
        [HttpGet]
        public ActionResult<IEnumerable<ModelosResponseDTO>> Get()
        {
            var modelosResponse = _modelos.Select(modelo => new ModelosResponseDTO
            {
                IdModelo = modelo.IdModelo,
                NomeModelo = modelo.NomeModelo,
                IdColecaoRelacionada = modelo.IdColecaoRelacionada,
                Tipo = modelo.Tipo,
                Layout = modelo.Layout
            });

            return Ok(modelosResponse);
        }


        /// <summary> Consulta de modelos, pelo seu código identificador, no objeto Modelos. </summary>
        /// <param name = "id"> Id do modelo cadstrado. </param>
        /// <returns> Retorno do modelo cadastrado no objeto modelos cadastrados. </response>
        /// <response code = "200"> Sucesso no retorno do modelo no objeto Modelos! </response>
        /// <response code = "404"> Não foi encontrado registro com o Id informado. Id inválido! </response>      
        [HttpGet("{id}")]
        public ActionResult<ModelosResponseDTO> Get(int id)
        {
            var modelo = _modelos.FirstOrDefault(c => c.IdModelo == id);
            if (modelo == null)
            {
                return NotFound("Modelo não encontrado na base de dados.");
            }

            var modeloResponse = new ModelosResponseDTO
            {
                IdModelo = modelo.IdModelo,
                NomeModelo = modelo.NomeModelo,
                IdColecaoRelacionada = modelo.IdColecaoRelacionada,
                Tipo = modelo.Tipo,
                Layout = modelo.Layout
            };

            return Ok(modeloResponse);
        }


        /// <summary> Cadastrando uma novo modelo  no objeto Modelos. </summary>
        /// <param name = "modelo"> Objeto Modelo. </param>
        /// <returns> Criação de Modelo. </returns>
        /// <response code = "201"> Sucesso no post do objeto Modelos. </response>
        /// <response code = "400"> Requisição com dados invalidos para o objeto Modelos. </response>
        /// <response code = "409"> Modelo já cadastrado no objeto Modelos. </response>        
        [HttpPost]
        public IActionResult Post([FromBody] ModelosRequestDTO modeloRequest)
        {
            if (_modelos.Any(c => c.IdModelo == modeloRequest.IdModelo))
            {
                return Conflict("Modelo já cadastrado na Lista Modelos.");
            }

            if (_modelos.Any(c => c.NomeModelo.Equals(modeloRequest.NomeModelo, StringComparison.OrdinalIgnoreCase)))
            {
                return Conflict("Modelo já cadastrado na Lista Modelos.");
            }

            var modelo = new Modelos
            {
                IdModelo = modeloRequest.IdModelo,
                NomeModelo = modeloRequest.NomeModelo,
                IdColecaoRelacionada = modeloRequest.IdColecaoRelacionada,
                Tipo = modeloRequest.Tipo,
                Layout = modeloRequest.Layout
            };

            _modelos.Add(modelo);

            var modeloResponse = new ModelosResponseDTO
            {
                IdModelo = modelo.IdModelo,
                NomeModelo = modelo.NomeModelo,
                IdColecaoRelacionada = modelo.IdColecaoRelacionada,
                Tipo = modelo.Tipo,
                Layout = modelo.Layout
            };

            return CreatedAtAction(nameof(Get), new { id = modeloResponse.IdModelo }, modeloResponse);
        }


        /// <summary> Atualização de um  modelo no objeto Modelos. </summary>
        /// <param name = "id"> Id do Modelo. </param>
        /// <param name = "modelo"> Objeto Modelos com as novas caracteristicas do modelo. </param>
        /// <returns> Atualização do modelo. </returns>
        /// <response code = "200"> Sucesso na atualização do modelo no objeto Modelos. </response>
        /// <response code = "400"> Requisição com dados inválidos! </response>
        /// <response code = "404"> Não foi encontrado  registro com o Id informado. Id inválido! </response>       
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Modelos modelo)
        {
            if (id != modelo.IdModelo)
            {
                return BadRequest();
            }

            var existingModelo = _modelos.Find(c => c.IdModelo == id);
            if (existingModelo == null)
            {
                return NotFound();
            }

            existingModelo.NomeModelo = modelo.NomeModelo;
            existingModelo.IdColecaoRelacionada = modelo.IdColecaoRelacionada;
            existingModelo.Tipo = modelo.Tipo;
            existingModelo.Layout = modelo.Layout;

            return Ok(existingModelo); ;
        }



        /// <summary> Remoção de um modelo no objeto Modelos. </summary>
        /// <param name="id"> Id do modelo. </param>
        /// <returns> Remoção do modelo da lista! </returns>
        /// <reponse code="204"> Modelo removido da lista Modelos, com sucesso! </reponse>
        /// <reponse code="404"> Não foi encontrado  registro com o Id informado. Id inválido! </reponse>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var modelo = _modelos.FirstOrDefault(c => c.IdModelo == id);
            if (modelo == null)
            {
                return NotFound("Modelo não encontrado na base de dados.");
            }

            _modelos.Remove(modelo);
            return NoContent();
        }
    }
}