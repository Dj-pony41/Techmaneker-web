using dbTechMaker.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbTechMaker.Model;

namespace dbTechMaker.Implementation
{
    public class UsuarioImpl:BaseImpl, IUsuario
    {
        public int Delete(Usuario t)
        {
            query = @"UPDATE [User] SET status=0,UpdateDate=CURRENT_TIMESTAMP,userID=@userID
                      WHERE id=@id";
            SqlCommand command = CreateBasicCommand(query);
            //command.Parameters.AddWithValue("@userID", SessionClass.SessionId);
            command.Parameters.AddWithValue("@id", t.Id);
            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public int Delete(int t)
        {
            query = @"UPDATE [User] SET status=0,UpdateDate=CURRENT_TIMESTAMP,userID=1
                      WHERE id=@id";
            SqlCommand command = CreateBasicCommand(query);
            //command.Parameters.AddWithValue("@userID", SessionClass.SessionId);
            command.Parameters.AddWithValue("@id", t);
            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Usuario Get(int id)
        {
            Usuario u = null;
            query = @"  SELECT id,username,password,name,firstLastName,secondLastName,mail,role,idCareer,status,registerDate,ISNULL(UpdateDate,CURRENT_TIMESTAMP),userID
                        FROM [User]
                        WHERE id=@id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);

            try
            {
                DataTable table = ExecuteDataTableCommand(command);
                if (table.Rows.Count > 0)
                {

                    u = new Usuario(int.Parse(table.Rows[0][0].ToString()), table.Rows[0][1].ToString(), table.Rows[0][2].ToString(), table.Rows[0][3].ToString(),
                        table.Rows[0][4].ToString(), table.Rows[0][5].ToString(), table.Rows[0][6].ToString(), table.Rows[0][7].ToString(), byte.Parse(table.Rows[0][8].ToString())
                        , byte.Parse(table.Rows[0][9].ToString()),DateTime.Parse(table.Rows[0][10].ToString()), DateTime.Parse(table.Rows[0][11].ToString()),
                        byte.Parse(table.Rows[0][12].ToString()));

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return u;
        }

        public int Insert(Usuario t)
        {
            query = @"  INSERT INTO [User] (username,password,name,firstLastName,secondLastName,mail,role,idCareer,userID)
                        VALUES ( @username,HASHBYTES('MD5',@password),@name,@lastName,@secondLastName,@mail,@role,@idCareer,@userID)";

            SqlCommand command = CreateBasicCommand(query);
            //command.Parameters.AddWithValue("@id", 1);
            command.Parameters.AddWithValue("@username", t.UserName);
            command.Parameters.AddWithValue("@password", t.Password).SqlDbType = SqlDbType.VarChar;
            command.Parameters.AddWithValue("@name", t.Name);
            command.Parameters.AddWithValue("@lastName", t.LastName);
            command.Parameters.AddWithValue("@secondLastName", t.SecondLastName);
            command.Parameters.AddWithValue("@mail", t.Mail);
            command.Parameters.AddWithValue("@role", t.Role);
            command.Parameters.AddWithValue("@idCareer", t.CarreraID);
            command.Parameters.AddWithValue("@userID", 1);

            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable Select()
        {
            query = @"SELECT id,CONCAT(name,' ',firstlastName,' ',secondLastName) AS 'Nombre Completo'
                      FROM [User]
                      WHERE status=1
                      ORDER BY 2";
            SqlCommand command = CreateBasicCommand(query);

            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable verificarMail(string mail)
        {
            query = @" SELECT id,username,mail
                       FROM [User]
                       WHERE status=1 AND mail=@mail";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@mail", mail);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable passTemp(string mail, string password, string userName)
        {
            query = @" UPDATE [User] SET password=HASHBYTES('MD5',@password)
                       WHERE status=1 AND mail=@mail AND username=@username";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@password", password).SqlDbType = SqlDbType.VarChar;
            command.Parameters.AddWithValue("@mail", mail);
            command.Parameters.AddWithValue("@username", userName);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int Update(Usuario t)
        {
            query = @"  UPDATE [User] SET role=@role,name=@name,firstlastName=@lastName,secondLastName=@secondLastName,mail=@mail,UpdateDate=CURRENT_TIMESTAMP,userId=@userId
                        WHERE id=@id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@role", t.Role);
            command.Parameters.AddWithValue("@name", t.Name);
            command.Parameters.AddWithValue("@lastName", t.LastName);
            command.Parameters.AddWithValue("@secondLastName", t.SecondLastName);
            command.Parameters.AddWithValue("@mail", t.Mail);
            command.Parameters.AddWithValue("@userID", 1);

            command.Parameters.AddWithValue("@id", t.Id);
            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable Login(string userName, string password)
        {
            query = @" SELECT id,username,role,password,idCareer
                       FROM [User]
                       WHERE status=1 AND username=@username AND password=HASHBYTES('MD5',@password)";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@username", userName);
            command.Parameters.AddWithValue("@password", password).SqlDbType = SqlDbType.VarChar;
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable ComboBoxCarrera()
        {
            query = @" SELECT id,Name
                       FROM Career";
            SqlCommand command = CreateBasicCommand(query);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable UsuariosE()
        {
            query = @"  SELECT id, userName, name,CONCAT(firstLastname,secondLastname), mail,role AS 'Estado', role AS 'boton'
						FROM [User]
						WHERE (role = 'Usuario' OR role = 'Evaluador' ) AND idCareer = @idCareer
						ORDER BY 6 DESC";

            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@idCareer", Session_Class.Session_Career);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        

        public int Evaluador(Usuario t, string type)
        {
            int valor = 0;
            if (type == "V")
            {
                query = @"INSERT INTO Evaluator(id,UserId)Values(@idd,@userIdd)";
                string query2 = @"UPDATE [User] SET role='Evaluador', UserId = @UserId
                       WHERE id = @id";
                SqlCommand commandd = CreateBasicCommand(query);
                commandd.Parameters.AddWithValue("@idd", t.Id);
                commandd.Parameters.AddWithValue("@userIdd", Session_Class.Session_ID);

                SqlCommand command = CreateBasicCommand(query2);
                command.Parameters.AddWithValue("@id", t.Id);
                command.Parameters.AddWithValue("@userId", Session_Class.Session_ID);
                try
                {
                    valor = ExecuteBasicCommand(commandd);
                    ExecuteBasicCommand(command);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else if(type == "Q")
            {
                string query2 = @"UPDATE [User] SET role='Usuario', UserId = @UserId
                       WHERE id = @id";
                SqlCommand command = CreateBasicCommand(query2);
                command.Parameters.AddWithValue("@id", t.Id);
                command.Parameters.AddWithValue("@userId", Session_Class.Session_ID);
                try
                {
                    ExecuteBasicCommand(command);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else if (type == "VQ")
            {
                string query2 = @"UPDATE [User] SET role='Evaluador', UserId = @UserId
                       WHERE id = @id";
                SqlCommand command = CreateBasicCommand(query2);
                command.Parameters.AddWithValue("@id", t.Id);
                command.Parameters.AddWithValue("@userId", Session_Class.Session_ID);
                try
                {
                    ExecuteBasicCommand(command);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return valor;
        }

        public Usuario existEvaluador(int id)
        {
            Usuario t = null;
            query = @"SELECT id
                     FROM [User]
                     WHERE id = @id AND (role = 'Evaluador');";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                DataTable table = ExecuteDataTableCommand(command);
                if (table.Rows.Count > 0)
                {
                    t = new Usuario();
                    t.Id = int.Parse(table.Rows[0]["id"].ToString());

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return t;
        }
    }
}
