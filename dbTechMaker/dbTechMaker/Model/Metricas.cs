using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbTechMaker.Model
{
    public class Metricas : BaseModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Idcarrera { get; set; }
        public int Idevento { get; set; }
        public int Idusuario { get; set; }


        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="descripcion"></param>
        /// <param name="idcarrera"></param>
        /// <param name="idevento"></param>
        /// <param name="idusuario"></param>
        /// <param name="status"></param>
        /// <param name="registerDate"></param>
        /// <param name="lastUpdate"></param>
        /// <param name="UserID"></param>
        public Metricas(int id, string descripcion, int idcarrera, int idevento, int idusuario, byte status, DateTime registerDate, DateTime lastUpdate, int UserID)
                    : base(status, registerDate, lastUpdate, UserID)
        {
            Id = id;
            Descripcion = descripcion;
            Idcarrera = idcarrera;
            Idevento = idevento;
            Idusuario = idusuario;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="idcarrera"></param>
        /// <param name="idevento"></param>
        /// <param name="idusuario"></param>
        public Metricas(string descripcion, int idcarrera, int idevento, int idusuario)
        {
            Descripcion = descripcion;
            Idcarrera = idcarrera;
            Idevento = idevento;
            Idusuario = idusuario;
        }

        public Metricas()
        {
        }
    }
}
