using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Generator_.net_framework_;

namespace Generator_.net_framework_
{
    class ReadIn
    {
        [GUID("")]
        private string s = "";
        public static Type ReadLines(string inputPath, string namespaceAndClass)
        {
            
            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            var csc = new CSharpCodeProvider();
            var cc = csc.CreateCompiler();
            CompilerParameters cp = new CompilerParameters();
            

            cp.ReferencedAssemblies.Add("mscorlib.dll");
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("System.ComponentModel.DataAnnotations.dll");
            cp.ReferencedAssemblies.Add("System.ComponentModel.dll");
            cp.ReferencedAssemblies.Add(typeof(GUID).Assembly.Location);

            StringBuilder sb = new StringBuilder();

            System.IO.StreamReader file =
                 new System.IO.StreamReader(inputPath);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("GUID") || line.Contains("Persistent"))
                {
                    counter++;
                }
                else
                {
                    sb.Append(line + "\n");
                    counter++;
                }
            }

            CompilerResults results = csc.CompileAssemblyFromSource(cp, sb.ToString());
            System.Reflection.Assembly _assembly = results.CompiledAssembly;
            Type[] _types = _assembly.GetTypes();
            Type eType = _assembly.GetType(namespaceAndClass);


            return eType;
        }
    }
}