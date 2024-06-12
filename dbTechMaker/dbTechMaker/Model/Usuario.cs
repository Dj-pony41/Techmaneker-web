using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dbTechMaker.Model
{
    public class Usuario:BaseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Mail { get; set; }
        public string Role { get; set; }
        public byte CarreraID { get; set; }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="secondLastName"></param>
        /// <param name="mail"></param>
        /// <param name="status"></param>
        /// <param name="registerDate"></param>
        /// <param name="lastUpdate"></param>
        /// <param name="userID"></param>
        public Usuario(int id, string userName, string password, string name, string lastName, string secondLastName, string mail, string role, byte carreraID, byte status, DateTime registerDate, DateTime lastUpdate, byte userID)
            : base(status, registerDate, lastUpdate, userID)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Role = role;
            Name = name;
            LastName = lastName;
            SecondLastName = secondLastName;
            Mail = mail;
            Role = role;
            CarreraID = carreraID;
        }
        public Usuario(string userName, string password, string name, string lastName, string secondLastName, string mail, string role, byte carreraID)
        {
            UserName = userName;
            Password = password;
            Role = role;
            Name = name;
            LastName = lastName;
            SecondLastName = secondLastName;
            Mail = mail;
            Role = role;
            CarreraID = carreraID;
        }
        public Usuario(int id, string name, string lastName, string secondLastName, string mail, string role, byte carreraID)
        {
            Id = id;
            Role = role;
            Name = name;
            LastName = lastName;
            SecondLastName = secondLastName;
            Mail = mail;
            Role = role;
            CarreraID = carreraID;
        }

        public Usuario(int id, string name, string lastName, string mail, string role)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Mail = mail;
            Role = role;
        }

        public Usuario(int id)
        {
            Id = id;
        }

        public Usuario() { }
    }
}
