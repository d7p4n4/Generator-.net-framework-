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
using System.Xml.Serialization;

namespace Generator_.net_framework_
{
    class ReadIn
    {
        private string s = "";
        public static Type ReadLines(string inputPath)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            var CSCProvider = new CSharpCodeProvider();
            var _compiler = CSCProvider.CreateCompiler();
            CompilerParameters _cParameters = new CompilerParameters();


            _cParameters.ReferencedAssemblies.Add("mscorlib.dll");
            _cParameters.ReferencedAssemblies.Add("System.dll");
            _cParameters.ReferencedAssemblies.Add("System.ComponentModel.DataAnnotations.dll");
            _cParameters.ReferencedAssemblies.Add("System.ComponentModel.dll");
            _cParameters.ReferencedAssemblies.Add("System.Runtime.InteropServices.dll");
            _cParameters.ReferencedAssemblies.Add("System.Runtime.dll");
            _cParameters.ReferencedAssemblies.Add("System.Xml.dll");
            _cParameters.ReferencedAssemblies.Add("System.Xml.Serialization.dll");
            _cParameters.ReferencedAssemblies.Add("ParentClassLibrary.dll");
            _cParameters.ReferencedAssemblies.Add(typeof(GUID).Assembly.Location);

            StringBuilder _stringBuilder = new StringBuilder();

            System.IO.StreamReader file =
                 new System.IO.StreamReader(inputPath);
            while ((line = file.ReadLine()) != null)
            {
                _stringBuilder.Append(line + '\n');
                counter++;
            }

            CompilerResults results = CSCProvider.CompileAssemblyFromSource(_cParameters, _stringBuilder.ToString());
            System.Reflection.Assembly _assembly = results.CompiledAssembly;
            Type[] _types = _assembly.GetTypes();
            Type eType = _types[0];

            return eType;
        }

    }
}