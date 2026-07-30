using AuthenticationService.Dtos.AuditLogs;
using AuthenticationService.Dtos.LoginHistory;
using AuthenticationService.Repository;

namespace AuthenticationService.Business
{
    public static class LoginHistoryErrors
    {
        public static readonly Error IsNull = new Error("LoginHistory.IsNull", "LoginHistory is null", HttpStatus.BadRequest);
        public static readonly Error NotCreated = new Error("LoginHistory.NotCreated", "LoginHistory not created successfully.", HttpStatus.InternalServerError);
        public static readonly Error InvalidID = new Error("LoginHistory.InvalidID", "ID must be greater than zero.", HttpStatus.BadRequest);
        public static readonly Error InvalidUserID = new Error("LoginHistory.InvalidUserID", "UserID must be greater than zero.", HttpStatus.BadRequest);
        public static readonly Error InvalidStatus = new Error("LoginHistory.InvalidStatus", "Status must be 0 or 1.", HttpStatus.BadRequest);
        public static readonly Error NotFound = new Error("LoginHistory.NotFound", "LoginHistory is not found.", HttpStatus.NotFound);

    }

    public static class LoginHistory
    {
        public static Result<int> AddLoginHistory(CreateLoginHistoryDto loginHistory)
        {
            if (loginHistory == null)
                return Result<int>.Failure(LoginHistoryErrors.IsNull);

            int newLoginHistoryId = LoginHistoryRepository.AddLoginHistory(loginHistory); ;

            if (newLoginHistoryId == -1)
                return Result<int>.Failure(LoginHistoryErrors.NotCreated);

            return Result<int>.Success(newLoginHistoryId);
        }

        public static Result<LoginHistoryDetailsDto> Find(int id)
        {
            if (id <= 0)
                return Result<LoginHistoryDetailsDto>.Failure(LoginHistoryErrors.InvalidID);

            var loginHistory = LoginHistoryRepository.GetLoginHistoryByID(id);

            if (loginHistory == null)
                return Result<LoginHistoryDetailsDto>.Failure(LoginHistoryErrors.NotFound);

            return Result<LoginHistoryDetailsDto>.Success(loginHistory);
        }

        public static Result<List<LoginHistoryDetailsDto>> GetAll()
        {
            List<LoginHistoryDetailsDto> loginHistoryList = LoginHistoryRepository.GetAllLoginHistory();

            return Result<List<LoginHistoryDetailsDto>>.Success(loginHistoryList);
        }

        public static Result<List<LoginHistoryDetailsDto>> GetByUserID(int userId)
        {
            if (userId <= 0)
                return Result<List<LoginHistoryDetailsDto>>.Failure(LoginHistoryErrors.InvalidUserID);

            List<LoginHistoryDetailsDto> loginHistoryList = LoginHistoryRepository.GetLoginHistoryByUserID(userId);

            return Result<List<LoginHistoryDetailsDto>>.Success(loginHistoryList);
        }

        public static Result<List<LoginHistoryDetailsDto>> Search(string searchText)
        {
            List<LoginHistoryDetailsDto> loginHistoryList = LoginHistoryRepository.SearchLoginHistory(searchText);

            return Result<List<LoginHistoryDetailsDto>>.Success(loginHistoryList);
        }

        public static Result<List<LoginHistoryDetailsDto>> FilterByStatus(byte status)
        {
            if (!(status == 0 || status == 1))
                return Result<List<LoginHistoryDetailsDto>>.Failure(LoginHistoryErrors.InvalidStatus);

            List<LoginHistoryDetailsDto> loginHistoryList = LoginHistoryRepository.FilterLoginHistoryByStatus(status);

            return Result<List<LoginHistoryDetailsDto>>.Success(loginHistoryList);
        }
    }
}
