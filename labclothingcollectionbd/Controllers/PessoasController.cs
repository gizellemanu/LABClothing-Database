using labclothingcollectionbd.Context;
using labclothingcollectionbd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labclothingcollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly LabClothingCollectionBDContext _context;

        public PessoasController(LabClothingCollectionBDContext context)
        {
            _context = context;
        }


        /// <summary> Lista de pessoas cadastradas. </summary>
        /// <returns> Retorna uma lista de pessoas cadastradas. </returns>
        /// <response code = "200"> Sucesso no retorno do objeto lista de pessoas cadastradas! </response>
        [HttpGet]
        public ActionResult<List<Pessoas>> GetAll()
        {
            var pessoas = _context.Pessoas.ToList();

            return Ok(pessoas);
        }


        /// <summary> Consulta de pessoas pelo seu código identificador, no objeto lista usuarios. </summary>
        /// <param name = "id"> Id de pessoa cadastrada. </param>
        /// <returns> Retorno do objeto pessoas cadastradas. </returns>
        /// <response code = "200"> Sucesso no retorno do objeto pessoas. </response>
        /// <response code = "404"> Não foi encontrado registro com o Id informado. Id inválido! </response>
        [HttpGet("{id}")]
        public ActionResult<Pessoas> GetById(int id)
        {
            var pessoa = _context.Pessoas.FirstOrDefault(c => c.IdPessoa == id);
            if (pessoa == null)
            {
                return NotFound("Não foi possivel encontrar a Pessoa na base de dados!");
            }

            return Ok(pessoa);
        }


        /// <summary> Cadastrando um nova pessoa na lista Pessoas. </summary>
        /// <param name = "pessoa"> Lista Pessoas. </param>
        /// <returns> Retorno da Lista Pessoas cadastradas. </returns>
        /// <response code = "201"> Sucesso no post de pessoa na lista Pessoas. </response>
        /// <response code = "400"> Requisição com dados inválidos. </response>
        /// <response code = "409"> Nome já cadastrado na lista Pessoas. </response>
        [HttpPost]
        public async Task<ActionResult<Pessoas>> Create([FromBody] Pessoas pessoa)
        {
            if (await _context.Pessoas.AnyAsync(c => c.NomeCompleto == pessoa.NomeCompleto))
            {
                return Conflict("Esta Pessoa já está cadastrada na base de dados.");
            }

            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = pessoa.NomeCompleto }, pessoa);
        }


        /// <summary> Atualização dos dados de determinada pessoa na lista de Pessoas. </summary>
        /// <param name = "id"> Id do pessoas. </param>
        /// <param name = "pessoa"> Lista com as novos dados da pessoa. </param>
        /// <returns> Retorno do objeto pessoas atualizado. </returns>
        /// <response code = "200"> Sucesso na atualização de pessoas na lista. </response>
        /// <response code = "400"> Requisição com dados inválidos. </response>
        /// <response code = "404"> Não foi encontrado  registro com o Id informado. Id inválido! </response>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Pessoas pessoa)
        {
            var pessoaExistente = _context.Pessoas.FirstOrDefault(c => c.IdPessoa == id);
            if (pessoaExistente == null)
            {
                return NotFound("Não foi possivel encontrar a Pessoa na base de dados!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Os dados fornecidos são invalidos!");
            }

            pessoaExistente.NomeCompleto = pessoa.NomeCompleto;
            pessoaExistente.Genero = pessoa.Genero;
            pessoaExistente.DataNascimento = pessoa.DataNascimento;
            pessoaExistente.CpfCnpj = pessoa.CpfCnpj;
            pessoaExistente.Telefone = pessoa.Telefone;
;

            _context.SaveChanges();

            return Ok(pessoaExistente);
        }


        /// <summary> Atualização parcia de uma pessoa. </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
 //       [HttpPatch("{id}")]
 //       public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Pessoas> patchDoc)
 //       {
 //         var pessoa = _pessoas.Find(c => c.IdPessoa == id);
 //           if (pessoa == null)
 //           {
 //               return NotFound();
 //           }

 //           patchDoc.ApplyTo(pessoa, ModelState);

 //           if (!ModelState.IsValid)
 //           {
 //               return BadRequest(ModelState);
 //           }

 //           return NoContent();
 //       }


        /// <summary> Remoção de uma pessoa. </summary>
        /// <param name = "id"> Id da pessoa. </param>
        /// <returns> Remoção de pessoas da lista Pessoas </returns>
        /// <reponse code = "204"> Pessoa removido com sucesso! </reponse>
        /// <reponse code = "404"> Não foi encontrado  registro com o Id informado. Id inválido! </reponse>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pessoa = _context.Pessoas.FirstOrDefault(c => c.IdPessoa == id);
            if (pessoa == null)
            {
                return NotFound("Pessoa não encontrada na base de dados!");
            }

            _context.Pessoas.Remove(pessoa);
            _context.SaveChanges();

            return NoContent();
        }
    }
}