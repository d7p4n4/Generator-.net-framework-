using System;
using System.Collections.Generic;
using System.Text;

namespace Generator_.net_framework_
{
    [AttributeUsage(AttributeTargets.All)]
    class GUID : Attribute
    {
        private string guid { get; set; }

        public GUID()
        {
            this.guid = Guid.NewGuid().ToString();
        }

        public GUID(string value)
        {
            this.guid = value;
        }

        public string getGuid()
        {
            return guid;
        }
    }
}