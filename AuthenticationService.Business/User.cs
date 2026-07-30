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
        public static bool UserExists(int userId) { return UserRepository.UserExists(userId); }
        public static bool UsernameExists(string username) { return UserRepository.UsernameExists(username); }
        public static bool UsernameExists(string username, int excludeUserId) { return UserRepository.UsernameExists(username, excludeUserId); }
        public static bool EmailExists(string email) { return UserRepository.EmailExists(email); }
        public static bool EmailExists(string email, int excludeUserId) { return UserRepository.EmailExists(email, excludeUserId); }

        public static Result<int> AddUser(CreateUserDto user)
        {
            var validationResult = UserValidator.ValidateCreate(user);
            if (!validationResult.IsSuccess) return new Result<int>(validationResult);

            if (UsernameExists(user.Username)) return Result<int>.Failure(UserErrors.UsernameAlreadyExists);
            if (EmailExists(user.Email)) return Result<int>.Failure(UserErrors.EmailAlreadyExists);
            if (!Role.RoleExists(user.RoleID)) return Result<int>.Failure(UserErrors.RoleNotFound);
            if (!Status.StatusExists(user.StatusID)) return Result<int>.Failure(UserErrors.StatusNotFound);

            var newUserId = UserRepository.AddUser(user);

            if (newUserId == -1) return Result<int>.Failure(UserErrors.NotCreated);

            return Result<int>.Success(newUserId);
        }

        public static Result UpdateUserByID(int id, UpdateUserDto user)
        {
            var validationResult = UserValidator.ValidateUpdate(id, user);
            if (!validationResult.IsSuccess) return validationResult;

            if (!UserExists(id)) return Result.Failure(UserErrors.NotFound);
            if (UsernameExists(user.Username, id)) return Result.Failure(UserErrors.UsernameAlreadyExists);
            if (EmailExists(user.Email, id)) return Result.Failure(UserErrors.EmailAlreadyExists);
            if (!Role.RoleExists(user.RoleID)) return Result.Failure(UserErrors.RoleNotFound);
            if (!Status.StatusExists(user.StatusID)) return Result.Failure(UserErrors.StatusNotFound);

            var result = UserRepository.UpdateUserByID(id, user);

            if (!result) return Result.Failure(UserErrors.NotUpdated);

            return Result.Success();
        }

        public static Result DeleteUserByID(int id)
        {
            var validationResult = UserValidator.ValidateId(id);
            if (!validationResult.IsSuccess) return validationResult;

            if (!UserExists(id)) return Result.Failure(UserErrors.NotFound);

            var result = UserRepository.DeleteUserByID(id);
            if (!result) return Result.Failure(UserErrors.NotDeleted);

            return Result.Success();
        }

        public static Result<List<UserDetailsDto>> SearchUsers(string searchText)
        {
            var usersList = UserRepository.SearchUsers(searchText);

            return Result<List<UserDetailsDto>>.Success(usersList);
        }

        public static Result<List<UserDetailsDto>> FilterUsersByRoleID(int roleId)
        {
            var validationResult = UserValidator.ValidateRoleId(roleId);
            if (!validationResult.IsSuccess) return new Result<List<UserDetailsDto>>(validationResult);

            var usersList = UserRepository.FilterUsersByRoleID(roleId);

            return Result<List<UserDetailsDto>>.Success(usersList);
        }

        public static Result<List<UserDetailsDto>> FilterUsersByStatusID(int statusId)
        {
            var validationResult = UserValidator.ValidateStatusId(statusId);
            if (!validationResult.IsSuccess) return new Result<List<UserDetailsDto>>(validationResult);

            var usersList = UserRepository.FilterUsersByStatusID(statusId);

            return Result<List<UserDetailsDto>>.Success(usersList);

        }

        public static Result<List<UserDetailsDto>> GetAllUsers()
        {
            var usersList = UserRepository.GetAllUsers();

            return Result<List<UserDetailsDto>>.Success(usersList);
        }

        public static Result<UserDetailsDto> GetUserByID(int id)
        {
            var validationResult = UserValidator.ValidateId(id);
            if (!validationResult.IsSuccess) return new Result<UserDetailsDto>(validationResult);

            var user = UserRepository.GetUserByID(id);
            if (user == null) return Result<UserDetailsDto>.Failure(UserErrors.NotFound);

            return Result<UserDetailsDto>.Success(user);
        }

    }
}
