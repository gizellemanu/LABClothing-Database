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
    }
}