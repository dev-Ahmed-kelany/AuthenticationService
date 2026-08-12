using AuthenticationService.Repository;
using AuthenticationService.Dtos.Users;
using AuthenticationService.Business.Validation;

namespace AuthenticationService.Business
{
    public static class UserErrors
    {
        public static readonly Error UsernameAlreadyExists = new("User.UsernameAlreadyExists", "Username already exists.", HttpStatus.Conflict);
        public static readonly Error EmailAlreadyExists = new("User.EmailAlreadyExists", "Email already exists.", HttpStatus.Conflict);
        public static readonly Error RoleNotFound = new("User.RoleNotFound", "Role is not found.", HttpStatus.NotFound);
        public static readonly Error StatusNotFound = new("User.StatusNotFound", "Status is not found.", HttpStatus.NotFound);
        public static readonly Error NotCreated = new Error("User.NotCreated", "User not created successfully.", HttpStatus.InternalServerError);
        public static readonly Error NotUpdated = new Error("User.NotUpdated", "User not updated successfully.", HttpStatus.InternalServerError);
        public static readonly Error NotDeleted = new Error("User.NotDeleted", "User not deleted successfully.", HttpStatus.InternalServerError);
        public static readonly Error NotFound = new Error("User.NotFound", "User is not found.", HttpStatus.NotFound);
        public static readonly Error InvalidID = new Error("User.InvalidID", "ID must be greater than zero.", HttpStatus.BadRequest);
    }

    public class User
    {
        public static async Task<bool> ExistsAsync(int userId) { return await UserRepository.ExistsAsync(userId); }
        public static async Task<bool> UsernameExistsAsync(string username) { return await UserRepository.UsernameExistsAsync(username); }
        public static async Task<bool> UsernameExistsAsync(string username, int excludeUserId) { return await UserRepository.UsernameExistsAsync(username, excludeUserId); }
        public static async Task<bool> EmailExistsAsync(string email) { return await UserRepository.EmailExistsAsync(email); }
        public static async Task<bool> EmailExistsAsync(string email, int excludeUserId) { return await UserRepository.EmailExistsAsync(email, excludeUserId); }

        public static async Task<Result<int>> AddAsync(CreateUserDto user)
        {
            var validationResult = UserValidator.ValidateCreate(user);
            if (!validationResult.IsSuccess) return new Result<int>(validationResult);
            
            user.Password = Utilities.Utilities.HashPassword(user.Password);

            if (await UsernameExistsAsync(user.Username)) return Result<int>.Failure(UserErrors.UsernameAlreadyExists);
            if (await EmailExistsAsync(user.Email)) return Result<int>.Failure(UserErrors.EmailAlreadyExists);
            if (!await Role.ExistsAsync(user.RoleID)) return Result<int>.Failure(UserErrors.RoleNotFound);
            if (!await Status.ExistsAsync(user.StatusID)) return Result<int>.Failure(UserErrors.StatusNotFound);

            var newUserId = await UserRepository.AddAsync(user);

            if (newUserId == -1) return Result<int>.Failure(UserErrors.NotCreated);

            return Result<int>.Success(newUserId);
        }

        public static async Task<Result> UpdateByIDAsync(int id, UpdateUserDto user)
        {
            var validationResult = UserValidator.ValidateUpdate(id, user);
            if (!validationResult.IsSuccess) return validationResult;

            if (!await ExistsAsync(id)) return Result.Failure(UserErrors.NotFound);
            if (await UsernameExistsAsync(user.Username, id)) return Result.Failure(UserErrors.UsernameAlreadyExists);
            if (await EmailExistsAsync(user.Email, id)) return Result.Failure(UserErrors.EmailAlreadyExists);
            if (!await Role.ExistsAsync(user.RoleID)) return Result.Failure(UserErrors.RoleNotFound);
            if (!await Status.ExistsAsync(user.StatusID)) return Result.Failure(UserErrors.StatusNotFound);

            var result = await UserRepository.UpdateByIDAsync(id, user);

            if (!result) return Result.Failure(UserErrors.NotUpdated);

            return Result.Success();
        }

        public static async Task<Result> DeleteByIDAsync(int id)
        {
            var validationResult = UserValidator.ValidateId(id);
            if (!validationResult.IsSuccess) return validationResult;

            if (!await ExistsAsync(id)) return Result.Failure(UserErrors.NotFound);

            var result = await UserRepository.DeleteByIDAsync(id);
            if (!result) return Result.Failure(UserErrors.NotDeleted);

            return Result.Success();
        }

        public static async Task<Result<List<UserDetailsDto>>> SearchAsync(string searchText)
        {
            var usersList = await UserRepository.SearchAsync(searchText);

            return Result<List<UserDetailsDto>>.Success(usersList);
        }

        public static async Task<Result<List<UserDetailsDto>>> FilterByRoleIDAsync(int roleId)
        {
            var validationResult = UserValidator.ValidateRoleId(roleId);
            if (!validationResult.IsSuccess) return new Result<List<UserDetailsDto>>(validationResult);

            var usersList = await UserRepository.FilterByRoleIDAsync(roleId);

            return Result<List<UserDetailsDto>>.Success(usersList);
        }

        public static async Task<Result<List<UserDetailsDto>>> FilterByStatusIDAsync(int statusId)
        {
            var validationResult = UserValidator.ValidateStatusId(statusId);
            if (!validationResult.IsSuccess) return new Result<List<UserDetailsDto>>(validationResult);

            var usersList = await UserRepository.FilterByStatusIDAsync(statusId);

            return Result<List<UserDetailsDto>>.Success(usersList);

        }

        public static async Task<Result<List<UserDetailsDto>>> GetAllAsync()
        {
            var usersList = await UserRepository.GetAllAsync();

            return Result<List<UserDetailsDto>>.Success(usersList);
        }

        public static async Task<Result<UserDetailsDto>> GetByIDAsync(int id)
        {
            var validationResult = UserValidator.ValidateId(id);
            if (!validationResult.IsSuccess) return new Result<UserDetailsDto>(validationResult);

            var user = await UserRepository.GetByIDAsync(id);
            if (user == null) return Result<UserDetailsDto>.Failure(UserErrors.NotFound);

            return Result<UserDetailsDto>.Success(user);
        }

    }
}
