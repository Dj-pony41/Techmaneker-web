using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
namespace dbTechMaker.Model
{
    public class Proyecto : BaseModel
    {
        public int Id { get; set; }
        public string ProyectName { get; set; }
        public string Description { get; set; }
        public string ApprovalStatus { get; set; }
        public string QrCode { get; set; }
        public int Careerid { get; set; }
        public int Eventid { get; set; }
        public int IdUser { get; set; }
        public int Userid { get; set; }


        public Proyecto(string proyectName, string description, string approvalStatus, string qrCode, int careerid, int eventid, int idUser, int userid)
        {

            ProyectName = proyectName;
            Description = description;
            ApprovalStatus = approvalStatus;
            QrCode = qrCode;
            Careerid = careerid;
            Eventid = eventid;
            IdUser = idUser;
            Userid = userid;
        }


        public Proyecto(int id, string proyectName, string description, string approvalStatus, string qrCode, int careerid, int eventid, int idUser)
        {
            Id = id;
            ProyectName = proyectName;
            Description = description;
            ApprovalStatus = approvalStatus;
            QrCode = qrCode;
            Careerid = careerid;
            Eventid = eventid;
            IdUser = idUser;

        }

        public Proyecto(string proyectName, string description, string approvalStatus)
        {
            ProyectName = proyectName;
            Description = description;
            ApprovalStatus = approvalStatus;
        }

        public Proyecto(string proyectName, string description, string approvalStatus, int eventid)
        {
            ProyectName = proyectName;
            Description = description;
            ApprovalStatus = approvalStatus;
            Eventid = eventid;
        }

        public Proyecto(int id, string proyectName, string description)
        {
            Id = id;
            ProyectName = proyectName;
            Description = description;
        }

        public Proyecto()
        {
            
        }

        public Proyecto(string proyectName, string description, int careerid, int eventid, int idUser)
        {
            ProyectName = proyectName;
            Description = description;
            Careerid = careerid;
            Eventid = eventid;
            IdUser = idUser;
        }

        public Proyecto(int id, string proyectName, string description, string approvalStatus, int careerid, int eventid, int idUser)
        {
            Id = id;
            ProyectName = proyectName;
            Description = description;
            ApprovalStatus = approvalStatus;
            Careerid = careerid;
            Eventid = eventid;
            IdUser = idUser;
        }
    }
}
