using System;

namespace Users.Model
{
    [Serializable]
    public class PerfilModel : PrimaryKey
    {
        public string Descricao { get; set; }
    }
}