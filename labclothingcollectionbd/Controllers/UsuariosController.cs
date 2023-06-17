using labclothingcollection.DTO.UsuariosDTO.Request;
using labclothingcollection.DTO.UsuariosDTO.Response;
using labclothingcollectionbd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace labclothingcollectionbd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly List<Usuarios> _usuarios;

        public UsuariosController()
        {
            _usuarios = new List<Usuarios>();

            _usuarios.Add(new Usuarios { IdPessoa = 1, NomeCompleto = "Joao Maria Antunes", Genero = "masculino", DataNascimento = "12-03-1982", CpfCnpj = "00000000000", Telefone = "319999999", Email = "joao@hotmail.com", TipoUsuario = "Gerente", EstadoUsuario = "Ativo" });
            _usuarios.Add(new Usuarios { IdPessoa = 2, NomeCompleto = "Maria Antonieta Guilermino", Genero = "feminino", DataNascimento = "22-07-1953", CpfCnpj = "11111111111", Telefone = "326666666", Email = "maria@hotmail.com", TipoUsuario = "Administrador", EstadoUsuario = "Ativo" });
            _usuarios.Add(new Usuarios { IdPessoa = 3, NomeCompleto = "Luan Garibaldi", Genero = "masculino", DataNascimento = "18-08-1988", CpfCnpj = "55555555555", Telefone = "331111111", Email = "luan@hotmail.com", TipoUsuario = "Criador", EstadoUsuario = "Ativo" });
            _usuarios.Add(new Usuarios { IdPessoa = 4, NomeCompleto = "Helena de Canto e Melo", Genero = "feminino", DataNascimento = "29-09-1990", CpfCnpj = "99999999999", Telefone = "317777777", Email = "helena@hotmail.com", TipoUsuario = "Criador", EstadoUsuario = "Ativo" });
            _usuarios.Add(new Usuarios { IdPessoa = 5, NomeCompleto = "Ana Paula Jacomo", Genero = "feminino", DataNascimento = "14-10-2000", CpfCnpj = "33333333333", Telefone = "382222222", Email = "anapaula@hotmail.com", TipoUsuario = "criador", EstadoUsuario = "Inativo" });

        }


        /// <summary> Lista de Usuarios cadastrados. </summary>
        /// <returns> Retorna uma lista de Usuarios cadastrados. </returns>
        /// <response code = "200"> Sucesso no retorno de uma Usuarios cadastrados! </response>
        [HttpGet]
        public IActionResult GetUsuarios(string status)
        {
            IEnumerable<Usuarios> usuarios = _usuarios;
            if (!string.IsNullOrEmpty(status))
            {
                usuarios = usuarios.Where(u => u.EstadoUsuario == status);
            }

            var usuariosDto = usuarios.Select(u => new UsuariosResponseDTO
            {
                Email = u.Email,
                TipoUsuario = u.TipoUsuario,
                EstadoUsuario = u.EstadoUsuario
            }).ToList();

            return Ok(usuariosDto);
        }


        /// <summary> Consulta de Usuarios pelo seu código identificador, na lista de Usuarios. </summary>
        /// <param name = "id"> Id de usuario cadastrado. </param>
        /// <returns> Retorno de uma lista de Usuarios cadastrados. </returns>
        /// <response code = "200"> Sucesso no retorno de lista de usuarios. </response>
        /// <response code = "404"> Não foi encontrado registro com o Id informado. Id inválido! </response>
        [HttpGet("{id}")]
        public IActionResult GetUsuarioById(int id)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.IdPessoa == id);
            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioDto = new UsuariosResponseDTO
            {
                Email = usuario.Email,
                TipoUsuario = usuario.TipoUsuario,
                EstadoUsuario = usuario.EstadoUsuario
            };

            return Ok(usuarioDto);
        }


        /// <summary> Cadastrando um novo usuario na lista Usuarios. </summary>
        /// <param name = "pessoa"> Lista usuario. </param>
        /// <returns> Retorno da Lista Usuarios cadastradas. </returns>
        /// <response code = "201"> Sucesso no post do usuario na lista Usuarios. </response>
        /// <response code = "400"> Requisição com dados inválidos. </response>
        /// <response code = "409"> Usuario já cadastrado na lista Usuarios. </response>
        [HttpPost]
        public IActionResult CreateUsuario(UsuariosRequestDTO usuarioDto)
        {
            // Verificar se o usuário com o mesmo e-mail já existe
            var existingUsuario = _usuarios.FirstOrDefault(u => u.Email == usuarioDto.Email);
            if (existingUsuario != null)
            {
                return Conflict("Um usuário com o mesmo endereço de e-mail já está cadastrado.");
            }

            // Simular gerar um ID único para o novo usuário
            int newUserId = _usuarios.Max(u => u.IdPessoa) + 1;

            var usuario = new Usuarios
            {
                IdPessoa = newUserId,
                Email = usuarioDto.Email,
                TipoUsuario = usuarioDto.TipoUsuario,
                EstadoUsuario = usuarioDto.EstadoUsuario
            };

            _usuarios.Add(usuario);

            var usuarioResponseDto = new UsuariosResponseDTO
            {
                Email = usuario.Email,
                TipoUsuario = usuario.TipoUsuario,
                EstadoUsuario = usuario.EstadoUsuario
            };

            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.IdPessoa }, usuarioResponseDto);
        }


        /// <summary> Atualização dos dados de determinado usuario na lista de Usuarios. </summary>
        /// <param name = "id"> Id do usuario. </param>
        /// <param name = "pessoa"> Lista com as novos dados de usuario. </param>
        /// <returns> Retorno do objeto usuario atualizado. </returns>
        /// <response code = "200"> Sucesso na atualização de usuario na lista Usuarios. </response>
        /// <response code = "400"> Requisição com dados inválidos. </response>
        /// <response code = "404"> Não foi encontrado  registro com o Id informado. Id inválido! </response>
        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, UsuariosRequestDTO usuarioDto)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.IdPessoa == id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Email = usuarioDto.Email;
            usuario.TipoUsuario = usuarioDto.TipoUsuario;
            usuario.EstadoUsuario = usuarioDto.EstadoUsuario;

            var usuarioResponseDto = new UsuariosResponseDTO
            {
                Email = usuario.Email,
                TipoUsuario = usuario.TipoUsuario,
                EstadoUsuario = usuario.EstadoUsuario
            };

            return Ok(usuarioResponseDto);
        }

        /// <summary>Atualização de estado de Usuarios</summary>
        /// <param name="id">Id do usuario</param>
        /// <param name="statusDto">Resposta HTTP com o estado atualizado do usuario</param>
        /// <returns></returns>
        [HttpPut("{id}/status")]
        public IActionResult UpdateStatusUsuario(int id, [FromBody] UsuariosRequestDTO statusDto)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.IdPessoa == id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.EstadoUsuario = statusDto.EstadoUsuario;

            return NoContent();
        }
            /// <summary> Remoção de uma usuario. </summary>
            /// <param name = "id"> Id de usuario. </param>
            /// <returns> Remoção de um usuario da lista Usuarios </returns>
            /// <reponse code = "204"> Usuario removido com sucesso! </reponse>
            /// <reponse code = "404"> Não foi encontrado  registro com o Id informado. Id inválido! </reponse>
            [HttpDelete("{id}")]
            public IActionResult DeleteUsuario(int id)
            {
                var usuario = _usuarios.FirstOrDefault(u => u.IdPessoa == id);
                if (usuario == null)
                {
                    return NotFound();
                }

                _usuarios.Remove(usuario);

                return NoContent();
            }
        }
    } 