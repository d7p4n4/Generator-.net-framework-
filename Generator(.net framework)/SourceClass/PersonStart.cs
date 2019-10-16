using System;
using System.Collections.Generic;

namespace Generator_.net_framework_
{
    [Persistent()]
    public class PersonStart
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public int age { get; set; }
        public Boolean gender { get; set; }
        public List<string> list { get; set; }

    }
}