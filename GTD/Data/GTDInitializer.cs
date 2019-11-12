using GTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTD.Data
{
    public class GTDInitializer
    {
        public static void Initialize(GTDContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Semana.Any())
            {
                return;
            }

            var semanas = new Semana[]
            {
                     new Semana {
                         SemanaNum = 1,
                         DataInicio = DateTime.Today,
                         DataFim = DateTime.Today.AddDays(7)
                     },
                     new Semana {
                         SemanaNum = 2,
                         DataInicio = DateTime.Today.AddDays(7),
                         DataFim = DateTime.Today.AddDays(14)
                     },
                     new Semana {
                         SemanaNum = 3,
                         DataInicio = DateTime.Today.AddDays(14),
                         DataFim = DateTime.Today.AddDays(21)
                     }
            };

            foreach (Semana s in semanas)
            {
                context.Semana.Add(s);
            }

            context.SaveChanges();
        }
    }
}
