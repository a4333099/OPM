﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPM.Core.Config
{
   public  interface IOPMConfig
   {
       OPMConfig CreateConfig();

        string  ConnetionString { get; set; }
   }
}
