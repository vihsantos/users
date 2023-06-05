using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Users.Model;

namespace Users.Controller
{
    public class UsuarioController
    {
        public List<UsuarioModel> ListarUsuarios()
         {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;";

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT [Usuario].[ID] as UsuarioID ,[Nome] ,[Email] ,[Senha] ,[CPF] ,[DataNascimento] ,[DataCriacao] ,[DataAtualizacao] ,PerfilID ,Descricao as PERFIL ,[Cep] ,[Logradouro] ,[Complemento] ,[Numero] ,[Cidade] ,[Estado] ,[Pais] ,[Bairro]" +
                        " FROM [users].[dbo].[Usuario] " +
                        " INNER JOIN [users].[dbo].Endereco on ([Usuario].ID = Endereco.UsuarioID)" +
                        " INNER JOIN [users].[dbo].Perfil on (PerfilID = Perfil.ID)";

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        var lista = new List<UsuarioModel>();
                        
                        while (reader.Read())
                        {
                            var model = new UsuarioModel();
                            model.ID = Convert.ToInt32(reader["UsuarioID"].ToString());
                            model.Nome = reader["Nome"].ToString();
                            model.Email = reader["Email"].ToString();
                            model.Senha = reader["Senha"].ToString();
                            model.CPF = reader["CPF"].ToString();
                            model.DataNascimento = DateTime.Parse(reader["DataNascimento"].ToString());
                            model.DataCriacao = DateTime.Parse(reader["DataCriacao"].ToString());
                            if (reader["DataAtualizacao"].ToString() != "")
                            {
                                model.DataAtualizacao = DateTime.Parse(reader["DataAtualizacao"].ToString());
                            }
                            model.Perfil = new PerfilModel()
                            {
                                ID = Convert.ToInt32(reader["PerfilID"].ToString()),
                                Descricao = reader["PERFIL"].ToString()
                            };
                            model.Endereco = new EnderecoModel();
                            model.Endereco.Cep = reader["Cep"].ToString();
                            model.Endereco.Logradouro = reader["Logradouro"].ToString();
                            model.Endereco.Complemento = reader["Complemento"].ToString();
                            model.Endereco.Numero = Convert.ToInt32(reader["Numero"].ToString());
                            model.Endereco.Bairro = reader["Bairro"].ToString();
                            model.Endereco.Cidade = reader["Cidade"].ToString();
                            model.Endereco.Estado = reader["Estado"].ToString();
                            model.Endereco.Pais = reader["Pais"].ToString();

                            lista.Add(model);
                        }

                        reader.Close();

                        return lista;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UsuarioModel BuscarUsuarioPorID (int UsuarioID)
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;";

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT [Usuario].[ID] as UsuarioID ,[Nome] ,[Email] ,[Senha] ,[CPF] ,[DataNascimento] ,[DataCriacao] ,[DataAtualizacao] ,PerfilID ,Descricao as PERFIL ,[Cep] ,[Logradouro] ,[Complemento] ,[Numero] ,[Cidade] ,[Estado] ,[Pais] ,[Bairro]" +
                        " FROM [users].[dbo].[Usuario] " +
                        " INNER JOIN [users].[dbo].Endereco on ([Usuario].ID = Endereco.UsuarioID)" +
                        " INNER JOIN [users].[dbo].Perfil on (PerfilID = Perfil.ID)" +
                        " WHERE UsuarioID = @ID";

                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int);
                    command.Parameters["@ID"].Value = UsuarioID;

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        var model = new UsuarioModel();
                        while (reader.Read())
                        {
                            
                            model.ID = Convert.ToInt32(reader["UsuarioID"].ToString());
                            model.Nome = reader["Nome"].ToString();
                            model.Email = reader["Email"].ToString();
                            model.Senha = reader["Senha"].ToString();
                            model.CPF = reader["CPF"].ToString();
                            model.PerfilID = Convert.ToInt32(reader["PerfilID"].ToString());
                            model.DataNascimento = DateTime.Parse(reader["DataNascimento"].ToString());
                            model.DataCriacao = DateTime.Parse(reader["DataCriacao"].ToString());
                            if (reader["DataAtualizacao"].ToString() != "")
                            {
                                model.DataAtualizacao = DateTime.Parse(reader["DataAtualizacao"].ToString());
                            }
                            model.Perfil = new PerfilModel()
                            {
                                ID = Convert.ToInt32(reader["PerfilID"].ToString()),
                                Descricao = reader["PERFIL"].ToString()
                            };
                            model.Endereco = new EnderecoModel();
                            model.Endereco.Cep = reader["Cep"].ToString();
                            model.Endereco.Logradouro = reader["Logradouro"].ToString();
                            model.Endereco.Complemento = reader["Complemento"].ToString();
                            model.Endereco.Numero = Convert.ToInt32(reader["Numero"].ToString());
                            model.Endereco.Bairro = reader["Bairro"].ToString();
                            model.Endereco.Cidade = reader["Cidade"].ToString();
                            model.Endereco.Estado = reader["Estado"].ToString();
                            model.Endereco.Pais = reader["Pais"].ToString();

                        }

                        reader.Close();

                        model.telefones = ListarTelefonesPorUsuario(UsuarioID);

                        return model;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarUsuario (UsuarioModel usuario, int UsuarioID)
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;";

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "UPDATE [users].[dbo].[Usuario] " +
                                          "SET [Nome] = @Nome, " +
                                          "[Email] = @Email," +
                                          "[Senha] = @Senha," +
                                          "[CPF] = @CPF," +
                                          "[DataNascimento] = @DataNascimento," +
                                          "[DataAtualizacao] = GETDATE(),"+
                                          "[PerfilID] = @PerfilID" +
                                          "WHERE ID = @UsuarioID;" +
                                          "UPDATE [users].[dbo].[Endereco]" +
                                          "SET [Cep] = @Cep," +
                                          "[Logradouro] = @Logradouro," +
                                          "[Complemento] = @Complemento," +
                                          "[Numero] = @Numero," +
                                          "[Cidade] = @Cidade," +
                                          "[Estado] = @Estado" +
                                          ",[Pais] = @Pais" +
                                          ",[Bairro] = @Bairro WHERE UsuarioID = @UsuarioID";

                    command.Parameters.Add("@Nome", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Email", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Senha", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@CPF", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@DataNascimento", System.Data.SqlDbType.Date);
                    command.Parameters.Add("@PerfilID", System.Data.SqlDbType.Int);
                    command.Parameters.Add("@UsuarioID", System.Data.SqlDbType.Int);
                    command.Parameters.Add("@Cep", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Logradouro", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Complemento", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Numero", System.Data.SqlDbType.Int);
                    command.Parameters.Add("@Cidade", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Estado", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Pais", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Bairro", System.Data.SqlDbType.VarChar);

                    command.Parameters["@Nome"].Value = usuario.Nome;
                    command.Parameters["@Email"].Value = usuario.Email;
                    command.Parameters["@Senha"].Value = usuario.Senha;
                    command.Parameters["@CPF"].Value = usuario.CPF;
                    command.Parameters["@DataNascimento"].Value = usuario.DataNascimento;
                    command.Parameters["@PerfilID"].Value = usuario.PerfilID;
                    command.Parameters["@UsuarioID"].Value = UsuarioID;
                    command.Parameters["@Cep"].Value = usuario.Endereco.Cep;
                    command.Parameters["@Logradouro"].Value = usuario.Endereco.Logradouro;
                    command.Parameters["@Complemento"].Value = usuario.Endereco.Complemento;
                    command.Parameters["@Numero"].Value = usuario.Endereco.Numero;
                    command.Parameters["@Cidade"].Value = usuario.Endereco.Cidade;
                    command.Parameters["@Estado"].Value = usuario.Endereco.Estado;
                    command.Parameters["@Pais"].Value = usuario.Endereco.Pais;
                    command.Parameters["@Bairro"].Value = usuario.Endereco.Bairro;

                    try
                    {
                        connection.Open();
                        var result = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TelefoneModel> ListarTelefonesPorUsuario(int UsuarioID)
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;";

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT [ID], [Contato] ,[UsuarioID] FROM [users].[dbo].[Telefone] WHERE UsuarioID = @UsuarioID";
                    command.Parameters.Add("@UsuarioID", System.Data.SqlDbType.Int);
                    command.Parameters["@UsuarioID"].Value = UsuarioID;

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        var lista = new List<TelefoneModel>();

                        while (reader.Read())
                        {
                            var model = new TelefoneModel();
                            model.ID = Convert.ToInt32(reader["ID"].ToString());
                            model.UsuarioID = Convert.ToInt32(reader["UsuarioID"].ToString());
                            model.Contato = reader["Contato"].ToString();
                            lista.Add(model);
                        }

                        reader.Close();

                        return lista;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CadastrarTelefone(List<TelefoneModel> telefones)
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;";

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    foreach (var telefone in telefones)
                    {
                        SqlCommand command = new SqlCommand();
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO [users].[dbo].[Telefone] " +
                            "([Contato] ,[UsuarioID])" +
                            " VALUES (@Contato, @UsuarioID) ";
                        command.Parameters.Add("@Contato", System.Data.SqlDbType.VarChar);
                        command.Parameters.Add("@UsuarioID", System.Data.SqlDbType.Int);

                        command.Parameters["@Contato"].Value = telefone.Contato;
                        command.Parameters["@UsuarioID"].Value = telefone.UsuarioID;

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int CadastrarUsuario(UsuarioModel usuario)
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;";

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO [users].[dbo].[Usuario]" +
                    " ([Nome], [Email], [Senha], [CPF], [DataNascimento], [DataCriacao], [PerfilID])" +
                    " VALUES (@Nome,  @Email,   @Senha,   @CPF,   @DataNascimento, GETDATE(),  @PerfilID);" +
                    "SELECT ID FROM [users].[dbo].[Usuario] WHERE Email = @Email";
                    command.Parameters.Add("@Nome", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Email", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Senha", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@CPF", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@DataNascimento", System.Data.SqlDbType.Date);
                    command.Parameters.Add("@PerfilID", System.Data.SqlDbType.Int);

                    command.Parameters["@Nome"].Value = usuario.Nome;
                    command.Parameters["@Email"].Value = usuario.Email;
                    command.Parameters["@Senha"].Value = usuario.Senha;
                    command.Parameters["@CPF"].Value = usuario.CPF;
                    command.Parameters["@DataNascimento"].Value = usuario.DataNascimento;
                    command.Parameters["@PerfilID"].Value = usuario.PerfilID;

                    try
                    {
                        connection.Open();
                        var result = command.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CadastrarEndereco(EnderecoModel endereco)
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;";

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO [users].[dbo].[Endereco]" +
                        "([Cep] ,[Logradouro] ,[Complemento] ,[Numero] ,[Cidade] ,[Estado] ,[Pais] ,[Bairro] ,[UsuarioID]) " +
                        "VALUES (@Cep, @Logradouro, @Complemento, @Numero, @Cidade, @Estado, @Pais, @Bairro, @UsuarioID) ";

                    command.Parameters.Add("@Cep", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Logradouro", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Complemento", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Numero", System.Data.SqlDbType.Int);
                    command.Parameters.Add("@Cidade", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Estado", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Pais", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Bairro", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@UsuarioID", System.Data.SqlDbType.VarChar);

                    command.Parameters["@Cep"].Value = endereco.Cep;
                    command.Parameters["@Logradouro"].Value = endereco.Logradouro;
                    command.Parameters["@Complemento"].Value = endereco.Complemento;
                    command.Parameters["@Numero"].Value = endereco.Numero;
                    command.Parameters["@Cidade"].Value = endereco.Cidade;
                    command.Parameters["@Estado"].Value = endereco.Estado;
                    command.Parameters["@Pais"].Value = endereco.Pais;
                    command.Parameters["@Bairro"].Value = endereco.Bairro;
                    command.Parameters["@UsuarioID"].Value = endereco.UsuarioID;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletarUsuario(int UsuarioID)
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;";

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM [users].[dbo].[Usuario] WHERE ID = @UsuarioID;";

                    command.Parameters.Add("@UsuarioID", System.Data.SqlDbType.VarChar);

                    command.Parameters["@UsuarioID"].Value = UsuarioID;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}