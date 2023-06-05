using System;
using System.Collections.Generic;

namespace Users.Model
{
    [Serializable]
    public class UsuarioModel : PrimaryKey
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public List<TelefoneModel> telefones { get; set; }
        public int PerfilID { get; set; }
        public int EnderecoID { get; set; }
        public PerfilModel Perfil { get; set; }
        public EnderecoModel Endereco { get; set; }
    }
}