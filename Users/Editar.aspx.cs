using System;
using System.Collections.Generic;
using Users.Controller;
using Users.Model;

namespace Users
{
    public partial class Editar : System.Web.UI.Page
    {
        public UsuarioController Controller { get; set; }
        public List<TelefoneModel> telefones
        {
            get
            {
                return ViewState["telefones"] as List<TelefoneModel>;
            }
            set { ViewState["telefones"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            PovoarDados();
        }

        private void PovoarDados()
        {
            var dado = Request.QueryString["dado"];
            Controller = new UsuarioController();
            var usuario = Controller.BuscarUsuarioPorID(Convert.ToInt32(dado));

            txt_nome.Value = usuario.Nome;
            txt_email.Value = usuario.Email;
            txt_senha.Value = usuario.Senha;
            txt_cpf.Value = usuario.CPF;
            txt_datanascimento.Value = usuario.DataNascimento.ToShortDateString();
            DropDownList1.SelectedValue = usuario.Perfil.ID.ToString();
            txt_cep.Value = usuario.Endereco.Cep;
            txt_logradouro.Value = usuario.Endereco.Logradouro;
            txt_complemento.Value = usuario.Endereco.Complemento;
            txt_numero.Value = usuario.Endereco.Numero.ToString();
            txt_bairro.Value = usuario.Endereco.Bairro;
            txt_cidade.Value = usuario.Endereco.Cidade;
            txt_estado.Value = usuario.Endereco.Estado;
            txt_pais.Value = usuario.Endereco.Pais;
            telefones = usuario.telefones;
        }
        protected void AdicionarTelefone(object sender, EventArgs e)
        {
            if (telefone.Value != null || telefone.Value != "")
            {
                var model = new TelefoneModel()
                {
                    Contato = telefone.Value
                };

                telefones.Add(model);
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
                var dado = Request.QueryString["dado"];

                var model = new UsuarioModel();
                model.Nome = txt_nome.Value;
                model.Email = txt_email.Value;
                model.Senha = txt_senha.Value;
                model.CPF = txt_cpf.Value;
                model.DataNascimento = DateTime.Parse(txt_datanascimento.Value);
                model.PerfilID = Convert.ToInt32(DropDownList1.SelectedValue);

                model.Endereco = new EnderecoModel();
                model.Endereco.Cep = txt_cep.Value;
                model.Endereco.Logradouro = txt_logradouro.Value;
                model.Endereco.Complemento = txt_complemento.Value;
                model.Endereco.Numero = Convert.ToInt32(txt_numero.Value);
                model.Endereco.Bairro = txt_bairro.Value;
                model.Endereco.Cidade = txt_cidade.Value;
                model.Endereco.Estado = txt_estado.Value;
                model.Endereco.Pais = txt_pais.Value;

                Controller.AtualizarUsuario(model, Convert.ToInt32(txt_numero.Value));
                var telefonesModel = new List<TelefoneModel>();

                //LimparDados();

                ClientScript.RegisterClientScriptBlock(this.GetType(), "swal", "Swal.fire({ icon: 'success', title: 'Obaa!!!', text: 'Atualização realizada com sucesso!!!'});", true);


            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "swal", "Swal.fire({ icon: 'error', title: 'Oops...', text: 'Não foi possível editar o usuario!'});", true);
            }
        }
    }
}