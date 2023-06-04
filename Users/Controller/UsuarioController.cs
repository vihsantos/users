using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Users.Model;

namespace Users.Controller
{
    public class UsuarioController
    {
        public void ListarUsuarios()
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;";

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT * from users.dbo.Usuarios";

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        var lista = new List<UsuarioModel>();
                        while (reader.Read())
                        {
                            var model = new UsuarioModel();
                            model.ID = Convert.ToInt32(reader["ID"].ToString());
                        }
                        reader.Close();
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

        public void CadastrarTelefone(List<TelefoneModel> telefones, int UsuarioID)
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
                        command.CommandText = "INSERT INTO [dbo].[Telefone] " +
                            "([Contato] ,[UsuarioID])" +
                            " VALUES (@Contato, @UsuarioID) GO";
                        command.Parameters.Add("@Contato", System.Data.SqlDbType.VarChar);
                        command.Parameters.Add("@UsuarioID", System.Data.SqlDbType.Int);

                        command.Parameters["@Contato"].Value = telefone;
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
                    command.CommandText = "INSERT INTO [dbo].[Usuario]" +
                    " ([Nome], [Email], [Senha], [CPF], [DataNascimento], [DataCriacao], [PerfilID])" +
                    " VALUES (@Nome,  @Email,   @Senha,   @CPF,   @DataNascimento, GETDATE(),  @PerfilID) GO" +
                    "SELECT * FROM [dbo].[Usuario] WHERE Email = @Email";
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

        public void DeletarUsuario(int UsuarioID)
        {

        }


    }
}