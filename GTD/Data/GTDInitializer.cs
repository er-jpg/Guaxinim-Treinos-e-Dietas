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
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
