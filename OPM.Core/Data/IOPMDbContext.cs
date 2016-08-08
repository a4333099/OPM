using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPM.Core.Data
{
  public   interface IOPMDbContext
    {
        string ConnectionString { get; }

        string DbName { get; }

        string Uid { get; }

        string Pwd { get; }


    }
}
