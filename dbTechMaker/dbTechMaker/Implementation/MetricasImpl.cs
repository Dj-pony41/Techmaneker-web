using dbTechMaker.Interface;
using dbTechMaker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbTechMaker.Implementation
{
    public class MetricasImpl : BaseImpl, IMetrica
    {
        public int Delete(Metricas t)
        {
            query = @"UPDATE Metrics SET status = 0, UpdateDate = CURRENT_TIMESTAMP, UserId = @UserId
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

        public Metricas Get(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Metricas t)
        {
            query = @"INSERT INTO Metrics(description, idCarrer, idEvent, idUser, UserId)
                      VALUES (@description, @idCarrer, @idEvent, @idUser, @UserId)";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@description", t.Descripcion);
            command.Parameters.AddWithValue("@idCarrer", t.Idcarrera);
            command.Parameters.AddWithValue("@idEvent", t.Idevento);
            command.Parameters.AddWithValue("@idUser", t.Idusuario);
            command.Parameters.AddWithValue("@UserId", 2);//ojo
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
            throw new NotImplementedException();
        }

        public DataTable Select_all_metrics_event(int id)
        {
            query = @"SELECT description
                      FROM Metrics
                      WHERE idEvent = @idEvent AND status = 1";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@idEvent", id);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable Select_id_evento()
        {
            query = @"SELECT TOP 1 id
                      FROM Event
                      ORDER BY registerDate DESC;";
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

        public int Update(Metricas t)
        {
            throw new NotImplementedException();
        }
    }
}
