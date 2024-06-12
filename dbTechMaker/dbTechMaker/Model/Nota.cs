using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbTechMaker.Model
{
    public class Nota : BaseModel
    {
        // Atributos de otras tablas
        public int Id { get; set; }
        public int IdProyect { get; set; } // Ejemplo de ID de carrera
        public int IdEvaluator { get; set; } // Ejemplo de nombre de carrera
        public int IdMetrics { get; set; }

        

        public int Note { get; set; } // Ejemplo de nombre de evento

        public Nota(int nota)
        {
            Note = nota;
        }

       

        public Nota(int idProyect, int idEvaluator, int nota)
        {
            IdProyect = idProyect;
            IdEvaluator = idEvaluator;
            Note = nota;
           
        }

        public Nota()
        {
            
        }

        public Nota(int id, int idProyect, int idEvaluator, int idMetrics, int note )
        {
            Id = id;
            IdProyect = idProyect;
            IdEvaluator = idEvaluator;
            IdMetrics = idMetrics;
            Note = note;
        }

        public Nota(int idProyect, int idEvaluator, int idMetrics, int note)
        {
            IdProyect = idProyect;
            IdEvaluator = idEvaluator;
            IdMetrics = idMetrics;
            Note = note;
        }

        
    }
}
