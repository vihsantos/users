using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Model
{
    public class UsuarioModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public List<TelefoneModel> telefones { get; set; }
        public PerfilModel Perfil { get; set; }
    }
}