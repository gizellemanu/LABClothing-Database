using labclothingcollectionbd.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace labclothingcollectionbd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColecoesController : ControllerBase
    {
        private readonly List<Colecoes> _colecoes;
        private int _idCounter;

        public ColecoesController()
        {
            _colecoes = new List<Colecoes>();
            _idCounter = 1;

            _colecoes.Add(new Colecoes { IdColecaoRelacionada = 1, NomeColecao = "Coleção 1", IdResponsavel = 10, Marca = "Antix", Orcamento = 1000, AnoLancamento = 2022, Estacao = "Verão", EstadoSistema = "Ativo" });
            _colecoes.Add(new Colecoes { IdColecaoRelacionada = 2, NomeColecao = "Coleção 2", IdResponsavel = 20, Marca = "Jhin-Jhon", Orcamento = 2000, AnoLancamento = 2023, Estacao = "Inverno", EstadoSistema = "Inativo" });
            _colecoes.Add(new Colecoes { IdColecaoRelacionada = 3, NomeColecao = "Coleção 3", IdResponsavel = 30, Marca = "Via Mia 3", Orcamento = 1500, AnoLancamento = 2023, Estacao = "Primavera", EstadoSistema = "Ativo" });
            _colecoes.Add(new Colecoes { IdColecaoRelacionada = 4, NomeColecao = "Coleção 4", IdResponsavel = 40, Marca = "Guiusepe Zanot", Orcamento = 1800, AnoLancamento = 2022, Estacao = "Outono", EstadoSistema = "Ativo" });
            _colecoes.Add(new Colecoes { IdColecaoRelacionada = 5, NomeColecao = "Coleção 5", IdResponsavel = 50, Marca = "Schuts", Orcamento = 2200, AnoLancamento = 2023, Estacao = "Verão", EstadoSistema = "Inativo" });
        }


        /// <summary>Listagem de todas as coleções. </summary>
        /// <param name="status">Filtro opcional por estado da coleção</param>
        /// <returns>Resposta HTTP com a lista de coleções</returns>
        /// <response code = "200"> Sucesso no retorno do objeto coleções cadastradas! </response>
        [HttpGet]
        public ActionResult<IEnumerable<Colecoes>> Get([FromQuery] string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return Ok(_colecoes);
            }

            var colecoesFiltradas = _colecoes.Where(c => c.EstadoSistema == status).ToList();

            return Ok(colecoesFiltradas);
        }


        /// <summary>Consultar coleção por Id.</summary>
        /// <param name="id">Id da coleção.</param>
        /// <returns> Resposta HTTP com os dados da coleção.</returns>
        /// <response code="200"> Sucesso no retorno da coleção no objeto coleções!</response>
        /// <response code="404"> Não foi encontrado registro com o Id informado. Id inválido! </response>
        [HttpGet("{id}")]
        public ActionResult<Colecoes> GetById(int id)
        {
            var colecao = _colecoes.FirstOrDefault(c => c.IdColecaoRelacionada == id);

            if (colecao == null)
            {
                return NotFound($"A coleção com Id {id} não foi encontrada!");
            }

            return Ok(colecao);
        }


        /// <summary>Cadastro de Coleções</summary>
        /// <param name="colecao"> Dados da coleção a ser cadastrada </param>
        /// <returns> Resposta HTTP com o código e dados da nova coleção. </returns>
        /// <response code = "201"> Sucesso no post do objeto Coleções. </response>
        /// <response code = "400"> Requisição com dados invalidos para o objeto Coleções! </response>
        /// <response code = "409"> Coleção já cadastrada no objeto Coleções. </response>
        [HttpPost]
        public ActionResult<Colecoes> Create([FromBody] Colecoes colecao)
        {
            if (_colecoes.Any(c => c.NomeColecao == colecao.NomeColecao))
            {
                return Conflict("Já existe uma coleção cadastrada com o mesmo nome!");
            }

            colecao.IdColecaoRelacionada = _idCounter++;
            _colecoes.Add(colecao);

            return CreatedAtAction(nameof(Get), new { id = colecao.IdColecaoRelacionada }, colecao);
        }


        /// <summary>Atualização dos dados de Coleções</summary>
        /// <param name="id">ID da coleção.</param>
        /// <param name="colecaoPatch">Operação de patch para atualizar os dados.</param>
        /// <returns> HTTP com os dados atualizados da coleção. </returns>
        /// <response code = "200"> Sucesso na atualização da coleção no objeto Coleções. </response>
        /// <response code = "400"> Requisição com dados inválidos! </response>
        /// <response code = "404"> Não foi encontrado  registro com o Id informado. Id inválido! </response>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] JsonPatchDocument<Colecoes> colecaoPatch)
        {
            var colecao = _colecoes.FirstOrDefault(c => c.IdColecaoRelacionada == id);

            if (colecao == null)
            {
                return NotFound($"A coleção com ID {id} não foi encontrada!");
            }

            colecaoPatch.ApplyTo(colecao, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(colecao);
        }


        /// <summary>Atualização de estado de Coleções</summary>
        /// <param name="id">Id da coleção.</param>
        /// <param name="status">Novo estado da coleção</param>
        /// <returns>Resposta HTTP com o estado atualizado da coleção</returns>
        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] string status)
        {
            var colecao = _colecoes.FirstOrDefault(c => c.IdColecaoRelacionada == id);

            if (colecao == null)
            {
                return NotFound($"A coleção com Id {id} não foi encontrada!");
            }

            colecao.EstadoSistema = status;

            return Ok(colecao);
        }


        /// <summary>Exclusão de coleção</summary>
        /// <param name="id"> Id da coleção a ser excluida. </param>
        /// <returns>Resposta HTTP indicando o resultado da exclusão.</returns>
        /// <reponse code="204">Coleção removida do objeto Coleções, com sucesso.</reponse>
        /// <reponse code="404">Não foi encontrado  registro com o Id informado.</reponse>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var colecao = _colecoes.FirstOrDefault(c => c.IdColecaoRelacionada == id);

            if (colecao == null)
            {
                return NotFound($"A coleção com Id {id} não foi encontrada!");
            }

            if (colecao.EstadoSistema == "Ativo")
            {
                return BadRequest("Não é possível excluir uma coleção com estado 'Ativo'!");
            }

            _colecoes.Remove(colecao);

            return NoContent();
        }
    }
}