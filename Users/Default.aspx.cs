using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
        public UsuarioController Controller { get; set; }

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
            Controller = new UsuarioController();
            var usuarios = Controller.ListarUsuarios();
            gridUsers.DataSource = usuarios;
            gridUsers.DataBind();

            if(usuarios.Count == 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "swal", "Swal.fire({ icon: 'error', title: 'Oops...', text: 'Nenhum usuário disponível!'});", true);
                return;
            }

            MultiView1.ActiveViewIndex = 2;
        }
        protected void AddUserClick(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void AdicionarTelefone(object sender, EventArgs e)
        {
            if(telefone.Value != null || telefone.Value != "")
            {
                telefones.Add(telefone.Value);
            }
        }
        protected void RemoverTelefones(object sender, EventArgs e)
        {
            
            telefones.Clear();
            telefone.Value = "";
        }

        protected void cadastrar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Controller = new UsuarioController();

                var model = new UsuarioModel();
                model.Nome = txt_nome.Value;
                model.Email = txt_email.Value;
                model.Senha = txt_senha.Value;
                model.CPF = txt_cpf.Value;
                model.DataNascimento = DateTime.Parse(txt_datanascimento.Value);
                model.PerfilID = Convert.ToInt32(DropDownList1.SelectedValue);

                var id = Controller.CadastrarUsuario(model);

                var endereco = new EnderecoModel();
                endereco.Cep = txt_cep.Value;
                endereco.Logradouro = txt_logradouro.Value;
                endereco.Complemento = txt_complemento.Value;
                endereco.Numero = Convert.ToInt32(txt_numero.Value);
                endereco.Bairro = txt_bairro.Value;
                endereco.Cidade = txt_cidade.Value;
                endereco.Estado = txt_estado.Value;
                endereco.Pais = txt_pais.Value;
                endereco.UsuarioID = id;

                Controller.CadastrarEndereco(endereco);

                var telefonesModel = new List<TelefoneModel>();

                foreach(var telefone in telefones)
                {
                    var telModel = new TelefoneModel();
                    telModel.Contato = telefone;
                    telModel.UsuarioID = id;
                    telefonesModel.Add(telModel);
                }

                if(telefonesModel.Count != 0)
                {
                    Controller.CadastrarTelefone(telefonesModel);
                }

                LimparDados();

                ClientScript.RegisterClientScriptBlock(this.GetType(), "swal", "Swal.fire({ icon: 'success', title: 'Obaa!!!', text: 'Cadastro realizado com sucesso!!!'});", true);

                MultiView1.ActiveViewIndex = 0;

            } catch 
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "swal", "Swal.fire({ icon: 'error', title: 'Oops...', text: 'Não foi possível cadastrar o usuario!'});", true);
            }
        }

        private void LimparDados()
        {
            txt_nome.Value = "";
            txt_email.Value = "";
            txt_senha.Value = "";
            txt_cpf.Value = "";
            telefone.Value = "";
            txt_datanascimento.Value = "";
            DropDownList1.SelectedValue = "0";
            txt_cep.Value = "";
            txt_logradouro.Value = "";
            txt_complemento.Value = "";
            txt_numero.Value = "";
            txt_bairro.Value = "";
            txt_cidade.Value = "";
            txt_estado.Value = "";
            txt_pais.Value = "";
            telefones.Clear();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            var dado = (sender as Button).CommandArgument;
            Response.Redirect("~/Editar.aspx?dado=" + dado);
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            try
            {
                var dado = (sender as Button).CommandArgument;

                Controller = new UsuarioController();
                Controller.DeletarUsuario(Convert.ToInt32(dado));
                ClientScript.RegisterClientScriptBlock(this.GetType(), "swal", "Swal.fire({ icon: 'success', title: 'Obaa!!!', text: 'Cliente removido com sucesso!!!'});", true);
                
                var usuarios = Controller.ListarUsuarios();
                gridUsers.DataSource = usuarios;
                gridUsers.DataBind();

                if(usuarios.Count == 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "swal", "Swal.fire({ icon: 'error', title: 'Oops...', text: 'Nenhum usuário disponível!'});", true);
                }
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "swal", "Swal.fire({ icon: 'error', title: 'Oops...', text: 'Não foi possível remover o usuario!'});", true);
            }
        }
    }
}