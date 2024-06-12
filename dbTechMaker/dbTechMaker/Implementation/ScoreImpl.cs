using dbTechMaker.Interface;
using System;
using System.Data;
using System.Data.SqlClient;
using dbTechMaker.Model;

namespace dbTechMaker.Implementation
{
    public class ScoreImpl : BaseImpl, IScore
    {
        public int Delete(Score t)
        {
            throw new NotImplementedException();
        }

        public Score Get(int id)
        {
            Score t = null;
            string query = @"SELECT id, description, idCarrer, idEvent, idUser, status, registerDate, updateDate, UserId
                             FROM Metrics
                             WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                DataTable table = ExecuteDataTableCommand(command);
                if (table.Rows.Count > 0)
                {
                    t = new Score();
                    t.Id = Convert.ToByte(table.Rows[0]["id"]);
                    t.Description = table.Rows[0]["description"].ToString();
                    t.IdCarrer = Convert.ToByte(table.Rows[0]["idCarrer"]);
                    t.IdEvent = Convert.ToByte(table.Rows[0]["idEvent"]);
                    t.IdUser = Convert.ToByte(table.Rows[0]["idUser"]);
                    t.Status = table.Rows[0]["status"].ToString();
                    t.RegisterDate = Convert.ToDateTime(table.Rows[0]["registerDate"]);
                    t.UpdateDate = Convert.ToDateTime(table.Rows[0]["updateDate"]);
                    t.UserId = Convert.ToByte(table.Rows[0]["UserId"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return t;
        }

        public int Insert(Score t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {
            string query = @"SELECT id, description FROM Metrics";
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

        public DataTable SelectByEventId(int eventId)
        {
            query = @"SELECT * FROM Metrics WHERE idEvent = @eventId";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@eventId", eventId);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Score t)
        {
            throw new NotImplementedException();
        }


    }
}
