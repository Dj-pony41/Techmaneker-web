using dbTechMaker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbTechMaker.Interface
{
    internal interface IEvento : IBase<Evento>
    {
        Evento Get(int id);

        
    }
}
