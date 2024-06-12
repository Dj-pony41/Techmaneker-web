using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbTechMaker.Model
{
    public class Evento : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Gestion { get; set; }
        public DateTime Fecha_inicio { get; set; }
        public DateTime Fecha_fin { get; set; }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="gestion"></param>
        /// <param name="fecha_inicio"></param>
        /// <param name="fecha_fin"></param>
        /// <param name="status"></param>
        /// <param name="registerDate"></param>
        /// <param name="lastUpdate"></param>
        /// <param name="UserID"></param>
        public Evento(int id, string name, string description, string gestion, DateTime fecha_inicio, DateTime fecha_fin, byte status, DateTime registerDate, DateTime lastUpdate, int UserID)
                    : base(status, registerDate, lastUpdate, UserID)
        {
            Id = id;
            Name = name;
            Description = description;
            Gestion = gestion;
            Fecha_inicio = fecha_inicio;
            Fecha_fin = fecha_fin;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="gestion"></param>
        /// <param name="fecha_inicio"></param>
        /// <param name="fecha_fin"></param>
        public Evento(string name, string description, string gestion, DateTime fecha_inicio, DateTime fecha_fin)
        {
            Name = name;
            Description = description;
            Gestion = gestion;
            Fecha_inicio = fecha_inicio;
            Fecha_fin = fecha_fin;
        }

        public Evento()
        {
        }
    }
}
