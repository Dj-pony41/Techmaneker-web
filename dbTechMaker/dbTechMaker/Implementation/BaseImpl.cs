using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbTechMaker.Implementation
{
    public class BaseImpl
    {
        //string connectionString = @"Server=DESKTOP-4NTHUDS\SQLEXPRESS;Database=dbTechMaker2;User Id=sa;Password=Andy6803924;";
        //string connectionString = @"Server=DESKTOP-074H5R4\SQLEXPRESS;Database=dbTechMaker;User Id=sa;Password=Univalle;";
        //string connectionString = @"Server=DESKTOP-6S0RIB9\SQLEXPRESS;Database=dbTechMaker;User Id=sa;Password=Univalle;";
        string connectionString = @"Server=LAPTOP-VICTUS-D\SQLEXPRESS;Database=dbTechMaker;User Id=sa;Password=Tri2012campeon;";



        internal string query = "";
        public SqlCommand CreateBasicCommand()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            return command;
        }

        public string GetGenerateIDTable(string table)
        {
            query = "SELECT IDENT_CURRENT('" + table + "') + IDENT_INCR('" + table + "')";
            SqlCommand command = CreateBasicCommand(query);
            try
            {
                command.Connection.Open();
                return command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }
        }
        public SqlCommand CreateBasicCommand(string sql)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            return command;
        }

        public List<SqlCommand> CreateBasicCommand2(string sql1, string sql2)
        {
            List<SqlCommand> commands = new List<SqlCommand>();
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command1 = new SqlCommand(sql1, connection);
            commands.Add(command1);

            SqlCommand command2 = new SqlCommand(sql2, connection);
            commands.Add(command2);

            return commands;
        }

        public int ExecuteBasicCommand(SqlCommand command)
        {
            try
            {
                command.Connection.Open();
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }
        }
        public int ExecuteNBasicCommand(List<SqlCommand> commands)
        {
            SqlCommand command0 = commands[0];
            int n = 0;
            try
            {
                command0.Connection.Open();
                foreach (SqlCommand item in commands)
                {
                    n += item.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command0.Connection.Close();
            }
            return n;
        }

        public DataTable ExecuteDataTableCommand(SqlCommand command)
        {
            DataTable table = new DataTable();
            try
            {
                command.Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                command.Connection.Close();
            }

            return table;
        }
    }
}
