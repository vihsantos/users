using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Model
{
    public class TelefoneModel : PrimaryKey
    {
        public int UsuarioID { get; set; }
        public string Contato { get; set; }
    }
}