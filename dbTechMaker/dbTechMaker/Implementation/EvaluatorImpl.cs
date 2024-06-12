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
    public class EvaluatorImpl : BaseImpl, IEvaluator
    {
        public int Delete(Evento t)
        {
            query = @"UPDATE Event SET status = 0, UpdateDate = CURRENT_TIMESTAMP, UserId = @UserId
                      WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@UserId", 1); //ojo
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

        public Evento Get(int id)
        {
            Evento t = null;
            query = @"SELECT id, eventName AS 'Nombre del evento', description AS 'Descripcion del evento', management AS 'Gestion del evento', startDate AS 'Fecha de inicio', endDate AS 'Fecha de finalisacion' 
                      FROM Event
                      WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                DataTable table = ExecuteDataTableCommand(command);
                if (table.Rows.Count > 0)
                {
                    t = new Evento();
                    t.Id = short.Parse(table.Rows[0]["id"].ToString());
                    t.Name = table.Rows[0]["Nombre del evento"].ToString();
                    t.Description = table.Rows[0]["Descripcion del evento"].ToString();
                    t.Gestion = table.Rows[0]["Gestion del evento"].ToString();
                    t.Fecha_inicio = DateTime.Parse(table.Rows[0]["Fecha de inicio"].ToString());
                    t.Fecha_fin = DateTime.Parse(table.Rows[0]["Fecha de finalisacion"].ToString());

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return t;
        }

        public int Insert(Evento t)
        {
            query = @"INSERT INTO Event(eventName, description, management, startDate, endDate, UserId)
                      VALUES (@eventName, @description, @management, @startDate, @endDate, @UserId)";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@eventName", t.Name);
            command.Parameters.AddWithValue("@description", t.Description);
            command.Parameters.AddWithValue("@management", t.Gestion);
            command.Parameters.AddWithValue("@startDate", t.Fecha_inicio);
            command.Parameters.AddWithValue("@endDate", t.Fecha_fin);
            command.Parameters.AddWithValue("@UserId", 1);//ojo
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
            query = @"SELECT id, eventName, description, management, startDate, endDate FROM Event
                      WHERE status = 1";
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

        public int Update(Evento t)
        {
            query = @"UPDATE Event SET eventName = @eventName, description = @description, management = @management, startDate = @startDate, endDate = @endDate, UpdateDate = CURRENT_TIMESTAMP, UserId = @UserId
                      WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@eventName", t.Name);
            command.Parameters.AddWithValue("@description", t.Description);
            command.Parameters.AddWithValue("@management", t.Gestion);
            command.Parameters.AddWithValue("@startDate", t.Fecha_inicio);
            command.Parameters.AddWithValue("@endDate", t.Fecha_fin);
            command.Parameters.AddWithValue("@UserId", 1); //ojo
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
        public DataTable Eventos()
        {
            query = @"SELECT id, eventName AS 'Evento'
                      FROM [Event]
                      WHERE status=1";
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

        Usuario IEvaluator.Get(int id)
        {
            throw new NotImplementedException();
        }

        public int Delete(Usuario t)
        {
            throw new NotImplementedException();
        }
        public Evaluador existEvaluador(int id)
        {
            Evaluador t = null;
            query = @" SELECT id FROM Evaluator WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                DataTable table = ExecuteDataTableCommand(command);
                if (table.Rows.Count > 0)
                {
                    t = new Evaluador();
                    t.Id = int.Parse(table.Rows[0]["id"].ToString());

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return t;
        }

        public int Insert(Usuario t)
        {
            try
            {
                query = @"INSERT INTO [Evaluator] (id, username, password, name, firstLastName, secondLastName, mail, role, idCareer, status, registerDate, lastUpdate, userID)
                  VALUES (@id, @username, HASHBYTES('MD5', @password), @name, @firstLastName, @secondLastName, @mail, @role, @idCareer, @status, @registerDate, @lastUpdate, @userID)";

                SqlCommand command = CreateBasicCommand(query);
                command.Parameters.AddWithValue("@id", t.Id);
                command.Parameters.AddWithValue("@username", t.UserName);
                command.Parameters.AddWithValue("@password", t.Password).SqlDbType = SqlDbType.VarChar;
                command.Parameters.AddWithValue("@name", t.Name);
                command.Parameters.AddWithValue("@firstLastName", t.LastName);
                command.Parameters.AddWithValue("@secondLastName", t.SecondLastName);
                command.Parameters.AddWithValue("@mail", t.Mail);
                command.Parameters.AddWithValue("@role", t.Role);
                command.Parameters.AddWithValue("@idCareer", t.CarreraID);
                command.Parameters.AddWithValue("@status", t.Status);
                command.Parameters.AddWithValue("@registerDate", t.RegisterDate);
                command.Parameters.AddWithValue("@lastUpdate", t.LastUpdate);
                command.Parameters.AddWithValue("@userID", t.UserID);

                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int Update(Usuario t)
        {
            throw new NotImplementedException();
        }
    }
}
