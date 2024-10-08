﻿using labclothingcollection.DTO.UsuariosDTO.Request;
using labclothingcollection.DTO.UsuariosDTO.Response;
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
    public class UsuariosController : ControllerBase
    {
        private readonly LabClothingCollectionBDContext _context;

        public UsuariosController(LabClothingCollectionBDContext context)
        {
            _context = context;
        }


        /// <summary> Lista de Usuarios cadastrados. </summary>
        /// <returns> Retorna uma lista de Usuarios cadastrados. </returns>
        /// <response code = "200"> Sucesso no retorno de uma Usuarios cadastrados! </response>
        [HttpGet]
        public ActionResult<List<Usuarios>> GetAll(string estadoUsuario)
        {
            var usuarios = _context.Usuarios.ToList();

            if (!string.IsNullOrEmpty(estadoUsuario))
            {
                bool isAtivo = estadoUsuario.ToLower() == "ativo";
                usuarios = usuarios.Where(u => u.EstadoUsuario.ToLower() == estadoUsuario.ToLower()).ToList();
            }

            if (usuarios.Count == 0)
            {
                return NotFound("Nenhum Usuário encontrado para o estado especificado na base de dados.");
            }

            return Ok(usuarios);
        }


        /// <summary> Consulta de Usuarios pelo seu código identificador, na lista de Usuarios. </summary>
        /// <param name = "id"> Id de usuario cadastrado. </param>
        /// <returns> Retorno de uma lista de Usuarios cadastrados. </returns>
        /// <response code = "200"> Sucesso no retorno de lista de usuarios. </response>
        /// <response code = "404"> Não foi encontrado registro com o Id informado. Id inválido! </response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound("Usuário não encontrado na base de dados.");

            return Ok(usuario);
        }


        /// <summary> Cadastrando um novo usuario na lista Usuarios. </summary>
        /// <param name = "pessoa"> Lista usuario. </param>
        /// <returns> Retorno da Lista Usuarios cadastradas. </returns>
        /// <response code = "201"> Sucesso no post do usuario na lista Usuarios. </response>
        /// <response code = "400"> Requisição com dados inválidos. </response>
        /// <response code = "409"> Usuario já cadastrado na lista Usuarios. </response>
        [HttpPost]
        public async Task<IActionResult> CreateUsuario(UsuariosRequestDTO requestDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Usuarios.AnyAsync(u => u.CpfCnpj == requestDTO.CpfCnpj))
                return Conflict("Ja existe um Usuario na base de dados com o CPF (ou CNPJ) informado.");

            var usuario = new Usuarios
            {
                NomeCompleto = requestDTO.NomeCompleto,
                Genero = requestDTO.Genero,
                DataNascimento = requestDTO.DataNascimento,
                CpfCnpj = requestDTO.CpfCnpj,
                Telefone = requestDTO.Telefone,
                Email = requestDTO.Email,
                TipoUsuario = requestDTO.TipoUsuario,
                EstadoUsuario = requestDTO.EstadoUsuario
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdPessoa }, usuario);
        }


        /// <summary> Atualização dos dados de determinado usuario na lista de Usuarios. </summary>
        /// <param name = "id"> Id do usuario. </param>
        /// <param name = "pessoa"> Lista com as novos dados de usuario. </param>
        /// <returns> Retorno do objeto usuario atualizado. </returns>
        /// <response code = "200"> Sucesso na atualização de usuario na lista Usuarios. </response>
        /// <response code = "400"> Requisição com dados inválidos. </response>
        /// <response code = "404"> Não foi encontrado  registro com o Id informado. Id inválido! </response>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UsuariosRequestDTO usuario)
        {
            var usuarioExistente = _context.Usuarios.FirstOrDefault(c => c.IdPessoa == id);
            if (usuarioExistente == null)
            {
                return NotFound("Não foi possível encontrar o Usuario na base de dados!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Os dados fornecidos são inválidos!");
            }

            usuarioExistente.NomeCompleto = usuario.NomeCompleto;
            usuarioExistente.Genero = usuario.Genero;
            usuarioExistente.DataNascimento = usuario.DataNascimento;
            usuarioExistente.CpfCnpj = usuario.CpfCnpj;
            usuarioExistente.Telefone = usuario.Telefone;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.TipoUsuario = usuario.TipoUsuario;
            usuarioExistente.EstadoUsuario = usuario.EstadoUsuario;

            _context.SaveChanges();

            return Ok(usuarioExistente);
        }

        /// <summary>Atualização de estado de Usuarios</summary>
        /// <param name="id">Id do usuario</param>
        /// <param name="statusDto">Resposta HTTP com o estado atualizado do usuario</param>
        /// <returns></returns>
        [HttpPut("{id}/status")]
        public IActionResult UpdateEstadoUsuario(int id, [FromBody] EstadoUsuarioRequestDTO estadoUsuarioDTO)
        {
            var usuarioExistente = _context.Usuarios.FirstOrDefault(c => c.IdPessoa == id);
            if (usuarioExistente == null)
            {
                return NotFound("Não foi possível encontrar o Usuário na base de dados!");
            }

            if (estadoUsuarioDTO.EstadoUsuario != "Ativo" && estadoUsuarioDTO.EstadoUsuario != "Inativo")
            {
                return BadRequest("Os dados fornecidos são inválidos!");
            }

            if (estadoUsuarioDTO.EstadoUsuario == usuarioExistente.EstadoUsuario)
            {
                return Conflict("O Usuario informado já se encontra neste estado!");
            }

            usuarioExistente.EstadoUsuario = estadoUsuarioDTO.EstadoUsuario;

            _context.SaveChanges();

            return Ok(usuarioExistente);
        }


        /// <summary> Remoção de uma usuario. </summary>
        /// <param name = "id"> Id de usuario. </param>
        /// <returns> Remoção de um usuario da lista Usuarios </returns>
        /// <reponse code = "204"> Usuario removido com sucesso! </reponse>
        /// <reponse code = "404"> Não foi encontrado  registro com o Id informado. Id inválido! </reponse>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(c => c.IdPessoa == id);
            if (usuario == null)
            {
                return NotFound("Usuario não encontrado na base de dados!");
            }

            _context.Pessoas.Remove(usuario);
            _context.SaveChanges();

            return Ok("Usuario removido com sucesso da base de dados!");
        }
    }
}