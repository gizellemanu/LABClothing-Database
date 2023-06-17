﻿using System;
using System.ComponentModel.DataAnnotations;

namespace labclothingcollection.DTO.UsuariosDTO.Request
{
    public class UsuariosRequestDTO
    {
        [Required(ErrorMessage = "O campo Nome Completo é de preenchimento obrigatório")]
        [MaxLength(50, ErrorMessage = "O campo Nome Completo não pode exceder 50 caracteres")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O campo Genero é de preenchimento obrigatório")]
        [MaxLength(10, ErrorMessage = "O campo Genero não pode exceder 10 caracteres")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "O campo Data Nascimento é de preenchimento obrigatório")]
        [RegularExpression(@"^(0[1-9]|1[0-2])-(0[1-9]|1\d|2\d|3[01])-\d{4}$", ErrorMessage = "O campo Data Nascimento possui um formato inválido!")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo Cpf/CNPJ é de preenchimento obrigatório")]
        [MaxLength(20, ErrorMessage = "O campo Cpf/CNPJ não pode exceder 20 caracteres")]
        [RegularExpression(@"^\d{11}$|^\d{14}$", ErrorMessage = "O campo CPF/CNPJ possui um formato inválido")]
        public string CPFouCNPJ { get; set; }

        [Required(ErrorMessage = "O campo Telefone é de preenchimento obrigatório. O campo Email possui um formato inválido. dev@provedor.com ou dev@provedor.com.br")]
        [MaxLength(15, ErrorMessage = "O campo Telefone não pode exceder 15 caracteres")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "O campo Telefone possui um formato inválido!")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo Email é de preenchimento obrigatório")]
        [MaxLength(50, ErrorMessage = "O campo Email não pode exceder 50 caracteres")]
        [RegularExpression(@"^[\w\.-]+@[\w\.-]+\.\w+$", ErrorMessage = "O campo Email possui um formato inválido. dev@provedor.com ou dev@provedor.com.br")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Tipo Usuario é de preenchimento obrigatório. O campo Tipo Usuario deve receber somente as seguintes atribuições: Administrador, Gerente, Criador, Outro.")]
        [RegularExpression("^(Administrador|Gerente|Criador|Outro)$", ErrorMessage = "O campo Tipo Usuario deve receber somente as seguintes atribuições: Administrador, Gerente, Criador, Outro.")]
        public string TipoUsuario { get; set; }

        [Required(ErrorMessage = "O campo Estado Usuário é de preenchimento obrigatório")]
        [RegularExpression("^(Ativo|ativo|Inativo|inativo)$", ErrorMessage = "O campo Estado no Sistema deve receber somentte as seguintes atribuições: Ativo ou ativo, Inativo ou inativo")]
        public string EstadoUsuario { get; set; }
    }
}
