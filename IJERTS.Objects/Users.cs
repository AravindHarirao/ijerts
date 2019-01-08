using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.Objects
{
    public class Users : IJERTSAbstract
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

        public string Organisation { get; set; }

        public string Qualification { get; set; }

        public string Position { get; set; }

        public int AddressId { get; set; }

        public int SpecializationGroupId { get; set; }

        public int SpecializationId { get; set; }

        public string Specialization { get; set; }

        public string UserType { get; set; }

        public string UserActivationValue { get; set; }

        public bool UserActivated { get; set; }

    }
}
