using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Users.Controller;
using Users.Model;

namespace Users
{
    public partial class _Default : Page
    {
        public List<string> telefones
        {
            get
            {
                return ViewState["telefones"] as List<string>;
            }
            set { ViewState["telefones"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                telefones = new List<string>();
                MultiView1.ActiveViewIndex = 0;
            }
        }
        protected void ListUserClick(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
        }
        protected void AddUserClick(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void AdicionarTelefone(object sender, EventArgs e)
        {
            if(telefone.Value!= null)
            {
                telefones.Add(telefone.Value);
            }
        }
        protected void RemoverTelefones(object sender, EventArgs e)
        {
            telefones.Clear();
        }

        protected void cadastrar_ServerClick(object sender, EventArgs e)
        {
            var model = new UsuarioModel();
            model.Nome = txt_nome.Value;
            model.Email = txt_senha.Value;
            model.Senha = txt_senha.Value;
            model.CPF = txt_cpf.Value;
            model.DataNascimento = DateTime.Parse(txt_datanascimento.Value);
            model.PerfilID = Convert.ToInt32(DropDownList1.SelectedValue);
        }
    }
}