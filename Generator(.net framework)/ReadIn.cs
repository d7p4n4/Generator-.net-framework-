using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_.net_framework_
{
    class ReadIn
    {
        public static Type ReadLines()
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            var csc = new CSharpCodeProvider();
            var cc = csc.CreateCompiler();
            CompilerParameters cp = new CompilerParameters();

            cp.ReferencedAssemblies.Add("mscorlib.dll");
            cp.ReferencedAssemblies.Add("System.dll");
            StringBuilder sb = new StringBuilder();

            System.IO.StreamReader file =
                new System.IO.StreamReader(@"d:\Server\Visual_studio\GeneratedClasses\GeneratedClasses\PersonPersistentPreProcessed.cs");
            while ((line = file.ReadLine()) != null)
            {
                sb.Append(line);
                counter++;
            }

            // The string can contain any valid c# code
            // "results" will usually contain very detailed error messages
            var results = csc.CompileAssemblyFromSource(cp, sb.ToString());
            System.Reflection.Assembly _assembly = results.CompiledAssembly;
            Type[] _types = _assembly.GetTypes();
            Type eType = _assembly.GetType("GuidGenerate.PersonPreProcessed");

            file.Close();

            return eType;
        }
    }
}
