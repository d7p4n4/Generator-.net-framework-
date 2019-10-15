
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuidGenerate
{            [GUID("e6d9524b-a2d6-4016-a9de-71a0ceaecaf7")]
	public class PersonBase 
	{
            [GUID("048f1684-74b6-4cc6-bf29-e9c499bf1c4d")]
    		public Int32 id { get; set; }
            [GUID("b3a29a08-fa56-48bc-93ae-af1866179510")]
    		public String name { get; set; }
            [GUID("777792b6-fe73-41ad-a7af-89253b14884f")]
    		public String address { get; set; }
            [GUID("bdbc25e0-a275-4080-bb18-d3c5dd0b85d2")]
    		public Int32 age { get; set; }
            [GUID("b39d3801-c446-4f2e-8843-e8246f0217c3")]
    		public Boolean gender { get; set; }
            [GUID("adf3df8e-d0de-43dd-9380-6cb4a921caf7")]
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