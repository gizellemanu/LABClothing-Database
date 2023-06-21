using labclothingcollection.DTO.ColecoesDTO.Request;
using labclothingcollection.DTO.ColecoesDTO.Response;
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
    public class ColecoesController : ControllerBase
    {
        private readonly LabClothingCollectionBDContext _context;

        public ColecoesController(LabClothingCollectionBDContext context)
        {
            _context = context;
        }


        /// <summary>Listagem de todas as coleções. </summary>
        /// <param name="status">Filtro opcional por estado da coleção</param>
        /// <returns>Resposta HTTP com a lista de coleções</returns>
        /// <response code = "200"> Sucesso no retorno do objeto coleções cadastradas! </response>
        [HttpGet]
        public ActionResult<List<Colecoes>> GetAll(string estadoSistema)
        {
            var colecoes = _context.Colecoes.ToList();

            if (!string.IsNullOrEmpty(estadoSistema))
            {
                bool isAtivo = estadoSistema.ToLower() == "ativo";
                colecoes = colecoes.Where(u => u.EstadoSistema.ToLower() == estadoSistema.ToLower()).ToList();
            }

            if (colecoes.Count == 0)
            {
                return NotFound("Nenhuma Coleção encontrado para o estado especificado.");
            }

            return Ok(colecoes);
        }


        /// <summary>Consultar coleção por Id.</summary>
        /// <param name="id">Id da coleção.</param>
        /// <returns>Resposta HTTP com os dados da coleção</returns>
        /// <response code="200"> Sucesso no retorno da coleção na Lista coleções!</response>
        /// <response code="404"> Não foi encontrado registro com o Id informado. Id inválido! </response>
        [HttpGet("{id}")]
        public ActionResult<ColecoesResponseDTO> GetById(int id)
        {
            var colecao = _context.Colecoes.FirstOrDefault(c => c.IdColecaoRelacionada == id);
            if (colecao == null)
            {
                return NotFound("Não foi possível encontrar a Coleção na base de dados!");
            }

            return Ok(colecao);
        }


        /// <summary>Cadastro de Coleções</summary>
        /// <param name="colecao">Dados da coleção a ser cadastrada</param>
        /// <returns>Resposta HTTP com o código e dados da nova coleção</returns>
        /// <response code = "201"> Sucesso no post do objeto Coleções. </response>
        /// <response code = "400"> Requisição com dados invalidos para o objeto Coleções! </response>
        /// <response code = "409"> Coleção já cadastrada no objeto Coleções. </response>

        [HttpPost]
        public async Task<ActionResult<ColecoesRequestDTO>> Create([FromBody] ColecoesRequestDTO colecao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorMessage = "Requisição inválida. Verifique os dados enviados." });
            }

            if (await _context.Colecoes.AnyAsync(c => c.NomeColecao == colecao.NomeColecao))
            {
                return Conflict("Esta coleção já está cadastrada na base de dados.");
            }

            var novaColecao = new Colecoes
            {
                NomeColecao = colecao.NomeColecao,
                IdResponsavel = colecao.IdResponsavel,
                Marca = colecao.Marca,
                Orcamento = colecao.Orcamento,
                AnoLancamento = colecao.AnoLancamento,
                Estacao = colecao.Estacao,
                EstadoSistema = colecao.EstadoSistema
            };

            _context.Colecoes.Add(novaColecao);
            await _context.SaveChangesAsync();

            var responseDTO = new ColecoesResponseDTO
            {
                IdColecaoRelacionada = novaColecao.IdColecaoRelacionada,
                NomeColecao = novaColecao.NomeColecao,
                IdResponsavel = novaColecao.IdResponsavel,
                Marca = novaColecao.Marca,
                Orcamento = novaColecao.Orcamento,
                AnoLancamento = novaColecao.AnoLancamento,
                Estacao = novaColecao.Estacao,
                EstadoSistema = novaColecao.EstadoSistema
            };

            return CreatedAtAction(nameof(GetById), new { id = responseDTO.IdColecaoRelacionada }, responseDTO);
        }


        /// <summary>Atualização dos dados de Coleções</summary>
        /// <param name="id">ID da coleção.</param>
        /// <param name="colecaoPatch">Operação de patch para atualizar os dados.</param>
        /// <returns>HTTP com os dados atualizados da coleção</returns>
        /// <response code = "200"> Sucesso na atualização da coleção no objeto Coleções. </response>
        /// <response code = "400"> Requisição com dados inválidos! </response>
        /// <response code = "404"> Não foi encontrado  registro com o Id informado. Id inválido! </response>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ColecoesRequestDTO colecao)
        {
            var colecaoExistente = _context.Colecoes.FirstOrDefault(c => c.IdColecaoRelacionada == id);
            if (colecaoExistente == null)
            {
                return NotFound("Não foi possível encontrar a Coleção na base de dados!");
            }

            if (colecaoExistente.NomeColecao != colecao.NomeColecao && _context.Colecoes.Any(c => c.NomeColecao == colecao.NomeColecao))
            {
                return Conflict("O nome da Coleção informado já está cadastrado na base de dados.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Os dados fornecidos são inválidos!");
            }
            colecaoExistente.NomeColecao = colecao.NomeColecao;
            colecaoExistente.IdResponsavel = colecao.IdResponsavel;
            colecaoExistente.Marca = colecao.Marca;
            colecaoExistente.Orcamento = colecao.Orcamento;
            colecaoExistente.AnoLancamento = colecao.AnoLancamento;
            colecaoExistente.Estacao = colecao.Estacao;
            colecaoExistente.EstadoSistema = colecao.EstadoSistema;

            _context.SaveChanges();

            return Ok(colecaoExistente);
        }


        /// <summary>Atualização de estado de Coleções</summary>
        /// <param name="id">Id da coleção.</param>
        /// <param name="status">Novo estado da coleção</param>
        /// <returns>Resposta HTTP com o estado atualizado da coleção</returns>
        [HttpPut("{id}/status")]
        public IActionResult UpdateEstadoSistema(int id, [FromBody] EstadoSistemaColecaoRequestDTO estadoSistemaColecaoRequestDTO)
        {
            var colecaoExistente = _context.Colecoes.FirstOrDefault(c => c.IdColecaoRelacionada == id);
            if (colecaoExistente == null)
            {
                return NotFound("Não foi possível encontrar a Colecao na base de dados!");
            }

            if (estadoSistemaColecaoRequestDTO.EstadoSistema != "Ativo" && estadoSistemaColecaoRequestDTO.EstadoSistema != "Inativo")
            {
                return BadRequest("Os dados fornecidos são inválidos!");
            }

            if (estadoSistemaColecaoRequestDTO.EstadoSistema == colecaoExistente.EstadoSistema)
            {
                return Conflict("A Coleção informada já se encontra neste estado!");
            }

            colecaoExistente.EstadoSistema = estadoSistemaColecaoRequestDTO.EstadoSistema;

            _context.SaveChanges();

            return Ok(colecaoExistente);
        }


        /// <summary>Exclusão de coleção</summary>
        /// <param name="id">Id da coleção a ser excluida</param>
        /// <returns>Resposta HTTP indicando o resultado da exclusão</returns>
        /// <reponse code="204">Coleção removida da lista Coleções</reponse>
        /// <reponse code="404">Não foi encontrado  registro com o Id informado</reponse>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var colecao = _context.Colecoes.FirstOrDefault(c => c.IdColecaoRelacionada == id);
            if (colecao == null)
            {
                return NotFound("Coleção não encontrado na base de dados!");
            }

            _context.Colecoes.Remove(colecao);
            _context.SaveChanges();

            return Ok("Coleção removida com sucesso da base de dados!");
        }
    }
}