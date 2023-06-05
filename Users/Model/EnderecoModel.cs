using System;

namespace Users.Model
{
    [Serializable]
    public class EnderecoModel : PrimaryKey
    {
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public int UsuarioID { get; set; }

    }
}