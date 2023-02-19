using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    public class PersonFormInformation
    {
        public string PersonID{ get; set; }

        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string PhoneNumber { get; set; }


        public string NationalCode { get; set; }




        public PersonFormInformation()
        {

        }


        public PersonFormInformation(string firstName, string lastName, string phoneNumber, string nationalCode = "")
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.NationalCode = nationalCode;
        }


        public PersonFormInformation(string personID, string firstName, string lastName, string phoneNumber, string nationalCode = "") 
            : this(firstName, lastName, phoneNumber, nationalCode)
        {
            this.PersonID = personID;
        }


    }
}
