using System;

namespace VirtualLibrary.Business.Validators.Helpers
{
    public static class CalculateAge
    {
        public static bool CompareAge(DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;

            if(today.Month < dob.Month || 
            today.Month == dob.Month && today.Day < dob.Day) 
            {
                age--;
            }
            
            return age < 18 ? false : true;
        }
    }
}