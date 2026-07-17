using AuthenticationService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business
{
    public class clsProfile
    {
        public static ProfileDTO? GetProfile(int UserID)
        {
            return clsProfileRepository.GetProfile(UserID);
        }
    }
}
