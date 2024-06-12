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
    public class NotaImpl : BaseImpl, INota
    {
        public int Delete(Nota t)
        {
            throw new NotImplementedException();
        }

        public Nota Get2(int ideva, int idproye)
        {
            Nota t = null;
            query = @"SELECT idProyect,idEvaluator,note
                        FROM Score
                        WHERE idProyect = @idProyect AND idEvaluator = @idEvaluator";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@idProyect", idproye);
            command.Parameters.AddWithValue("@idEvaluator", ideva);
            try
            {
                DataTable table = ExecuteDataTableCommand(command);
                if (table.Rows.Count > 0)
                {
                    t = new Nota();
                    t.IdProyect = short.Parse(table.Rows[0]["idProyect"].ToString());
                    t.IdEvaluator =int.Parse( table.Rows[0]["idEvaluator"].ToString());
                    t.Note = int.Parse(table.Rows[0]["note"].ToString());

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return t;
        }

        public int Insert(Nota t)
        {
            string query = "INSERT INTO Score ( note, idProyect, idEvaluator, idMetrics) VALUES (@note , @idProyect, @idEvaluador, @idMetrics)";

            SqlCommand command = CreateBasicCommand(query);
            
                // Agregar los parámetros
                command.Parameters.AddWithValue("@note", t.Note);
                command.Parameters.AddWithValue("@idProyect", t.IdProyect);
                command.Parameters.AddWithValue("@idEvaluador", t.IdEvaluator);
                command.Parameters.AddWithValue("@idMetrics", t.IdMetrics);
                



            

            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable Calificaciones(int id)
        {
            query = @"SELECT P.proyectName AS 'Proyecto', AVG(N.note) AS 'Promedio'
                      FROM Score N
                      INNER JOIN Proyect P ON N.idProyect = P.id
					  INNER JOIN [Event] E on P.eventId = E.id
					  WHERE E.id=@id
                      GROUP BY P.proyectName
                      ORDER BY AVG(N.note) DESC;";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                return ExecuteDataTableCommand(command);
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

        public int Update(Nota t)
        {
            throw new NotImplementedException();
        }

        public int Calificacion_Repetida(int ideva, int idproye)
        {
            query = @"SELECT id
                        FROM Score
                        WHERE idProyect = @idProyect AND idEvaluator = @idEvaluator";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@idProyect", ideva);
            command.Parameters.AddWithValue("@idEvaluator", idproye);
            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Nota Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
