using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_.net_framework_
{ 
        class Program
        {
            static void Main(string[] args)
            {

                GenerateClass.generateClass("cs", "GuidGenerate", "Person", typeof(PersonStart));

            }
        }
    
}
