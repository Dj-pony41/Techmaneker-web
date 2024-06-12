using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dbTechMaker.Model
{
    public class Listado_proyecto : BaseModel
    {
        public int id { get; set; }
        public string proyectName { get; set; }
        public string description { get; set; }
        public string approval { get; set; }
        public string qrCode { get; set; }
        public int careerId { get; set; }
        public int eventId { get; set; }
        public int idUser { get; set; }

        public string Observation { get; set; }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="proyectName"></param>
        /// <param name="description"></param>
        /// <param name="approval"></param>
        /// <param name="qrCode"></param>
        /// <param name="careerId"></param>
        /// <param name="eventId"></param>
        /// <param name="idUser"></param>
        /// <param name="status"></param>
        /// <param name="registerDate"></param>
        /// <param name="lastUpdate"></param>
        /// <param name="userID"></param>
        public Listado_proyecto(int id, string proyectName, string description, string approval, string qrCode, int careerId, int eventId, int idUser, byte status, DateTime registerDate, DateTime lastUpdate, int UserID)
                 : base(status, registerDate, lastUpdate, UserID)
        {
            this.id = id;
            this.proyectName = proyectName;
            this.description = description;
            this.approval = approval;
            this.qrCode = qrCode;
            this.careerId = careerId;
            this.eventId = eventId;
            this.idUser = idUser;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="proyectName"></param>
        /// <param name="description"></param>
        /// <param name="approval"></param>
        /// <param name="qrCode"></param>
        /// <param name="careerId"></param>
        /// <param name="eventId"></param>
        /// <param name="idUser"></param>
        public Listado_proyecto(string proyectName, string description, string approval, string qrCode, int careerId, int eventId, int idUser)
        {
            this.proyectName = proyectName;
            this.description = description;
            this.approval = approval;
            this.qrCode = qrCode;
            this.careerId = careerId;
            this.eventId = eventId;
            this.idUser = idUser;
        }

        public Listado_proyecto()
        {
        }

        public Listado_proyecto(int idUser, string observation)
        {
            this.idUser = idUser;
            Observation = observation;
        }

        public Listado_proyecto(int id, string approval, string observation)
        {
            this.id = id;
            this.approval = approval;
            Observation = observation;
        }
    }
}
