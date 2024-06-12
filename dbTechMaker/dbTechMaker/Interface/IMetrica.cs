using dbTechMaker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbTechMaker.Interface
{
    internal interface IMetrica : IBase<Metricas>
    {
        Metricas Get(int id);

        DataTable Select_id_evento();
        DataTable Select_all_metrics_event(int id);
    }
}
