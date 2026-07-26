using AuthenticationService.Dtos.Profile;
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
        public static ProfileDetailsDto? GetProfile(int userId)
        {
            return ProfileRepository.GetProfile(userId);
        }
    }
}
