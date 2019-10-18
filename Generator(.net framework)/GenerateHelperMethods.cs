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

            PropertyInfo[] propInfo = anyType.GetProperties();

            foreach (var prop in propInfo)
            {
                if (prop.PropertyType.ToString().StartsWith("System.Collections.Generic."))
                {
                    string type = prop.PropertyType.ToString();
                    string outType = type.Substring(0, type.IndexOf("`")).Replace("System.Collections.Generic.", "");
                    string innerType = type.Substring(type.IndexOf("`"));
                    string finalInnerType = innerType.Substring(innerType.IndexOf(".") + 1).Replace("]", "");
                    Console.WriteLine(finalInnerType);

                    props.Add(prop.Name, outType + "<" + finalInnerType + ">");

                }
                else
                {
                    props.Add(prop.Name, prop.PropertyType.ToString().Substring(prop.PropertyType.ToString().IndexOf(".") + 1));
                }
            }

            return props;
        }
    }
}
