
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuidGenerate
{            [GUID("06dbc0d3-597a-4ea7-bd68-e776af8b8557")]
	public class PersonBase 
	{
            [GUID("d6416308-a56b-48c5-87d4-17242faed838")]
    		public Int32 id { get; set; }
            [GUID("05b42cc3-10a0-41a0-99b9-f838fc0eaa77")]
    		public String name { get; set; }
            [GUID("5ad535e6-14a5-4a08-8789-945495918743")]
    		public String address { get; set; }
            [GUID("ee0b2df1-6e5f-4842-9cf3-a4e0a30f25cb")]
    		public Int32 age { get; set; }
            [GUID("f1b838af-d3b0-4697-8fec-9e9e6efcfec5")]
    		public Boolean gender { get; set; }
            [GUID("ff301346-3b22-4d12-9ef2-d806a60740d8")]
    		public List<String> list { get; set; }

		public PersonBase(){}
    
		public Int32 getId() {
        		return id;
        }

		public String getName() {
        		return name;
        }

		public String getAddress() {
        		return address;
        }

		public Int32 getAge() {
        		return age;
        }

		public Boolean getGender() {
        		return gender;
        }

		public List<String> getList() {
        		return list;
        }


    		public void setId(Int32 newValue) {
        		id = newValue;
        }

    		public void setName(String newValue) {
        		name = newValue;
        }

    		public void setAddress(String newValue) {
        		address = newValue;
        }

    		public void setAge(Int32 newValue) {
        		age = newValue;
        }

    		public void setGender(Boolean newValue) {
        		gender = newValue;
        }

    		public void setList(List<String> newValue) {
        		list = newValue;
        }

	}
}