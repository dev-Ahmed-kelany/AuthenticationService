using AuthenticationService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business
{
    public class Profile
    {
        public static ProfileDTO? GetProfile(int userId)
        {
            return ProfileRepository.GetProfile(userId);
        }
    }
}
