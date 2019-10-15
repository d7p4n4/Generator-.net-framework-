using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Generator_.net_framework_
{
    class GenerateHelperMethods
    {
        public static Dictionary<string, string> getProps(Type anyType)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            PropertyInfo[] prop = anyType.GetProperties();

            foreach (var p in prop)
            {
                if (p.PropertyType.ToString().StartsWith("System.Collections.Generic."))
                {
                    string type = p.PropertyType.ToString();
                    string outType = type.Substring(0, type.IndexOf("`")).Replace("System.Collections.Generic.", "");
                    string innerType = type.Substring(type.IndexOf("`"));
                    string finalInnerType = innerType.Substring(innerType.IndexOf(".") + 1).Replace("]", "");
                    Console.WriteLine(finalInnerType);

                    props.Add(p.Name, outType + "<" + finalInnerType + ">");

                }
                else
                {
                    props.Add(p.Name, p.PropertyType.ToString().Substring(p.PropertyType.ToString().IndexOf(".") + 1));
                }
            }

            return props;
        }
    }
}
