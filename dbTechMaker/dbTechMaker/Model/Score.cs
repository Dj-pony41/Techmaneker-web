using dbTechMaker.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dbTechMaker.Model
{
    public class Score : BaseModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int IdCarrer { get; set; }
        public int IdEvent { get; set; }
        public int IdUser { get; set; }
        public string Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UserId { get; set; }

        public Score()
        {
        }

        public Score(int id)
        {
            Id = id;
        }

        public Score(string description, string status, DateTime registerDate, DateTime updateDate, int userId)
        {
            Description = description;
            Status = status;
            RegisterDate = registerDate;
            UpdateDate = updateDate;
            UserId = userId;
        }

        public Score(int id, string description, string status, DateTime registerDate, DateTime updateDate, int userId)
        {
            Id = id;
            Description = description;
            Status = status;
            RegisterDate = registerDate;
            UpdateDate = updateDate;
            UserId = userId;
        }

        public Score(int id, string description, int idCarrer, int idEvent, int idUser, string status, DateTime registerDate, DateTime updateDate, int userId)
        {
            Id = id;
            Description = description;
            IdCarrer = idCarrer;
            IdEvent = idEvent;
            IdUser = idUser;
            Status = status;
            RegisterDate = registerDate;
            UpdateDate = updateDate;
            UserId = userId;
        }
    }
}
