using dbTechMaker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace dbTechMaker.Interface
{
    internal interface IScore : IBase<Score>
    {
        Score Get(int id);

        


    }
}
