using System;

namespace Users.Model
{
    [Serializable]
    public class TelefoneModel : PrimaryKey
    {
        public int UsuarioID { get; set; }
        public string Contato { get; set; }
    }
}