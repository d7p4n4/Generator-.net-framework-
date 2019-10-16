using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_.net_framework_
{
    class ReadIn
    {
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
            cp.ReferencedAssemblies.Add("System.Data.dll");
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

            // The string can contain any valid c# code
            // "results" will usually contain very detailed error messages
            /*sb.Append("using System;");
            sb.Append("using System.Text;");
            sb.Append("using System.Data.Entity;");
            sb.Append("namespace onTheFlyClass{");
            sb.Append("public class MyClass : DbContext{");
            sb.Append("public String Name { get; set; }");
            sb.Append("}}");*/
            CompilerResults results = csc.CompileAssemblyFromSource(cp, sb.ToString());
            System.Reflection.Assembly _assembly = results.CompiledAssembly;
            Type[] _types = _assembly.GetTypes();
            Type eType = _assembly.GetType(namespaceAndClass);


            return eType;
        }
    }
}
