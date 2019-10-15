using System;
using System.Collections.Generic;
using System.Text;

namespace GuidGenerate
{
	public class PersonAlgebra : PersonBase
	{
		public Boolean hasId(){
			if(this.getId() != null){

				return true;
			}
			else
			{
				return false;
			}
		}

		public Boolean hasName(){
			if(this.getName() != null){

				return true;
			}
			else
			{
				return false;
			}
		}

		public Boolean hasAddress(){
			if(this.getAddress() != null){

				return true;
			}
			else
			{
				return false;
			}
		}

		public Boolean hasAge(){
			if(this.getAge() != null){

				return true;
			}
			else
			{
				return false;
			}
		}

    
		public Boolean isGender() {
        		return this.getGender();

    		}



    		public int countList()
        	{
        	        int i = this.getList().Count;

            		return i;
        	}



    		public Boolean hasListMember()
        	{
        	        int i = this.getList().Count;

			if(i > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
        	}

	}
}
