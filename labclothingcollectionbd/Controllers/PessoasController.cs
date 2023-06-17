using labclothingcollectionbd.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace labclothingcollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly List<Pessoas> _pessoas;

        public PessoasController()
        {
            _pessoas = new List<Pessoas>();

            _pessoas.Add(new Pessoas { IdPessoa = 1, NomeCompleto = "Joao Maria Antunes",         Genero = "masculino",DataNascimento = "12/03/1982", CpfCnpj = "00000000000", Telefone = "319999999", });
            _pessoas.Add(new Pessoas { IdPessoa = 2, NomeCompleto = "Maria Antonieta Guilermino", Genero = "feminino", DataNascimento = "22/07/1953", CpfCnpj = "11111111111", Telefone = "326666666", });
            _pessoas.Add(new Pessoas { IdPessoa = 3, NomeCompleto = "Luan Garibaldi",             Genero = "masculino",DataNascimento = "18/08/1988", CpfCnpj = "55555555555", Telefone = "331111111", });
            _pessoas.Add(new Pessoas { IdPessoa = 4, NomeCompleto = "Helena de Canto e Melo",     Genero = "feminino", DataNascimento = "29/09/1990", CpfCnpj = "99999999999", Telefone = "317777777", });
            _pessoas.Add(new Pessoas { IdPessoa = 5, NomeCompleto = "Ana Paula Jacomo",           Genero = "feminino", DataNascimento = "14/10/2000", CpfCnpj = "33333333333", Telefone = "382222222", });
        }


        /// <summary> Lista de pessoas cadastradas. </summary>
        /// <returns> Retorna uma lista de pessoas cadastradas. </returns>
        /// <response code = "200"> Sucesso no retorno do objeto lista de pessoas cadastradas! </response>
        [HttpGet]
        public ActionResult<IEnumerable<Pessoas>> Get()
        {
            return _pessoas;
        }


        /// <summary> Consulta de pessoas pelo seu código identificador, no objeto lista usuarios. </summary>
        /// <param name = "id"> Id de pessoa cadastrada. </param>
        /// <returns> Retorno do objeto pessoas cadastradas. </returns>
        /// <response code = "200"> Sucesso no retorno do objeto pessoas. </response>
        /// <response code = "404"> Não foi encontrado registro com o Id informado. Id inválido! </response>
        [HttpGet("{id}")]
        public ActionResult<Pessoas> Get(int id)
        {
            var pessoa = _pessoas.Find(c => c.IdPessoa == id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return pessoa;
        }


        /// <summary> Cadastrando um nova pessoa na lista Pessoas. </summary>
        /// <param name = "pessoa"> Lista Pessoas. </param>
        /// <returns> Retorno da Lista Pessoas cadastradas. </returns>
        /// <response code = "201"> Sucesso no post de pessoa na lista Pessoas. </response>
        /// <response code = "400"> Requisição com dados inválidos. </response>
        /// <response code = "409"> Nome já cadastrado na lista Pessoas. </response>
        [HttpPost]
        public ActionResult<Colecoes> Post([FromBody] Pessoas pessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_pessoas.Any(c => c.IdPessoa == pessoa.IdPessoa))
            {
                return Conflict("Pessoa já cadastrada na lista Pessoas");
            }

            pessoa.IdPessoa = GetNextId();
            _pessoas.Add(pessoa);

            return CreatedAtAction(nameof(Get), new { id = pessoa.IdPessoa }, pessoa);
        }


        /// <summary> Atualização dos dados de determinada pessoa na lista de Pessoas. </summary>
        /// <param name = "id"> Id do pessoas. </param>
        /// <param name = "pessoa"> Lista com as novos dados da pessoa. </param>
        /// <returns> Retorno do objeto pessoas atualizado. </returns>
        /// <response code = "200"> Sucesso na atualização de pessoas na lista. </response>
        /// <response code = "400"> Requisição com dados inválidos. </response>
        /// <response code = "404"> Não foi encontrado  registro com o Id informado. Id inválido! </response>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Pessoas pessoa)
        {
            if (id != pessoa.IdPessoa)
            {
                return BadRequest();
            }

            var existingPessoa = _pessoas.Find(c => c.IdPessoa == id);
            if (existingPessoa == null)
            {
                return NotFound();
            }

            existingPessoa.NomeCompleto = pessoa.NomeCompleto;
            existingPessoa.Genero = pessoa.Genero;
            existingPessoa.DataNascimento = pessoa.DataNascimento;
            existingPessoa.CpfCnpj = pessoa.CpfCnpj;
            existingPessoa.Telefone = pessoa.Telefone;


            return Ok(existingPessoa); ;
        }


        /// <summary> Atualização parcia de uma pessoa. </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Pessoas> patchDoc)
        {
            var pessoa = _pessoas.Find(c => c.IdPessoa == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(pessoa, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }


        /// <summary> Remoção de uma pessoa. </summary>
        /// <param name = "id"> Id da pessoa. </param>
        /// <returns> Remoção de pessoas da lista Pessoas </returns>
        /// <reponse code = "204"> Pessoa removido com sucesso! </reponse>
        /// <reponse code = "404"> Não foi encontrado  registro com o Id informado. Id inválido! </reponse>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var colecao = _pessoas.Find(c => c.IdPessoa == id);
            if (colecao == null)
            {
                return NotFound();
            }

            _pessoas.Remove(colecao);

            return NoContent();
        }

        private int GetNextId()
        {
            if (_pessoas.Count > 0)
            {
                return _pessoas[_pessoas.Count - 1].IdPessoa + 1;
            }
            return 1;
        }
    }
}