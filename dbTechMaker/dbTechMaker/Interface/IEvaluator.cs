using dbTechMaker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbTechMaker.Interface
{
    internal interface IEvaluator : IBase<Usuario>
    {
        Usuario Get(int id);
    }
}
