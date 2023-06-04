<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Users._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="flex">
                <button class="card__button" runat="server" onserverclick="ListUserClick">
                    <span class="material-symbols-outlined" style="font-size: 80px;">groups
                    </span>
                    <div style="font-weight: bolder; font-size: 22px; text-align: center;">
                        Listar Usuários
                    </div>
                </button>
                <button class="card__button" runat="server" onserverclick="AddUserClick">
                    <span class="material-symbols-outlined " style="font-size: 80px;">person_add
                    </span>
                    <div style="font-weight: bolder; font-size: 22px; text-align: center;">
                        Cadastrar Novo Usuario
                    </div>
                </button>
            </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="center">
                <div class="card__cadastro">
                    <div class="card__title">
                        Cadastrar Usuário
                    </div>
                    <div class="row">
                        <div class="input__group col-12">
                            <label>Nome</label>
                            <input type="text" id="txt_nome" placeholder="Digite o seu nome" runat="server">
                        </div>
                        <div class="input__group col-6">
                            <label>Email</label>
                            <input type="text" id="txt_email" placeholder="Digite o seu email" runat="server">
                        </div>
                        <div class="input__group col-6">
                            <label>Senha</label>
                            <input type="password" id="txt_senha" placeholder="Digite a sua senha" runat="server">
                        </div>
                        <div class="input__group col-4">
                            <label>CPF</label>
                            <input type="text" id="txt_cpf" placeholder="000.000.000-00" runat="server">
                        </div>
                        <div class="input__group col-4">
                            <label>Data de Nascimento</label>
                            <input type="text" id="txt_datanascimento" placeholder="DD/MM/AAAA" runat="server">
                        </div>
                        <div class="input__group col-4">
                            <label>Perfil</label>
                            <asp:DropDownList ID="DropDownList1" CssClass="input_dd" runat="server">
                                <asp:ListItem Value="-1">Selecione o perfil</asp:ListItem>
                                <asp:ListItem Value="1">Admin</asp:ListItem>
                                <asp:ListItem Value="2">Supervisor</asp:ListItem>
                                <asp:ListItem Value="3">Operador</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="input__group">
                        <label>Telefone</label>
                        <div>
                            <input type="text" id="telefone" placeholder="71 99999-9999" runat="server">
                            <button type="button" class="add" runat="server" onserverclick="AdicionarTelefone">Adicionar</button>
                            <button type="reset" class="remover" runat="server" onserverclick="RemoverTelefones">Remover telefones</button>
                        </div>
                        <% if (telefones != null)
                            {%>
                        <% if (telefones.Count != 0)
                            {%>
                        <ul id="list__telefones">
                            <% for (int x = 0; x < telefones.Count; x++)
                                {  %>
                            <li class="tel__item"><%=telefones[x] %></li>
                            <% } %>
                        </ul>
                        <%}%>
                        <%} %>
                    </div>
                    <div class="row">
                        <div class="input__group col-3">
                            <label>CEP</label>
                            <input type="text" id="txt_cep" placeholder="00000-000" runat="server">
                        </div>
                        <div class="input__group col-9">
                            <label>Logradouro</label>
                            <input type="text" id="txt_logradouro" placeholder="Digite o logradouro" runat="server">
                        </div>
                        <div class="input__group col-9">
                            <label>Complemento</label>
                            <input type="text" id="txt_complemento" placeholder="Digite o complemento" runat="server">
                        </div>
                        <div class="input__group col-3">
                            <label>Numero</label>
                            <input type="text" id="txt_numero" runat="server">
                        </div>
                        <div class="input__group col-4">
                            <label>Bairro</label>
                            <input type="text" id="txt_bairro" runat="server">
                        </div>
                        <div class="input__group col-4">
                            <label>Cidade</label>
                            <input type="text" id="txt_cidade" placeholder="Digite a cidade" runat="server">
                        </div>
                        <div class="input__group col-4">
                            <label>Estado</label>
                            <input type="text" id="txt_estado" placeholder="Digite o estado" runat="server">
                        </div>
                        <div class="input__group col-4">
                            <label>País</label>
                            <input type="text" id="txt_pais" placeholder="Digite o país" runat="server">
                        </div>
                    </div>

                    <div class="center">
                        <button id="cadastrar" class="cadastrar" type="submit" onserverclick="cadastrar_ServerClick" runat="server">Cadastrar</button>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="View3" runat="server">
        </asp:View>
    </asp:MultiView>
</asp:Content>
