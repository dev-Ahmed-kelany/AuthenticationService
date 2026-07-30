using AuthenticationService.Business.Validation;
using AuthenticationService.Dtos.Profile;
using AuthenticationService.Dtos.Users;
using AuthenticationService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business
{
    public static class ProfileErrors
    {
        public static readonly Error NotFound = new Error("User.NotFound", "User is not found.", HttpStatus.NotFound);
    }

    public class Profile
    {
        public static async Task<Result<ProfileDetailsDto>> GetProfileAsync(int userId)
        {
            var validationResult = UserValidator.ValidateId(userId);
            if (!validationResult.IsSuccess) return new Result<ProfileDetailsDto>(validationResult);

            var profile = await ProfileRepository.GetProfileAsync(userId);
            if (profile == null) return Result<ProfileDetailsDto>.Failure(ProfileErrors.NotFound);

            return Result<ProfileDetailsDto>.Success(profile);
        }
    }
}
