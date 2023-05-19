using SeaBreeze.Service.DTOS.Account;
using SeaBreeze.Service.DTOS.Consumer;
using SeaBreeze.Service.DTOS.User;

namespace SeaBreeze.Service.Interfaces
{
    public interface IAccountService
    {
        Task RegisterConsumer(RegisterConsumerDto consumer);
        Task RegisterResident(RegisterResidentDto resident);
        Task<string> Login(UserLoginDto loginDto);
        Task<string> LoginResident(UserLoginDto loginDto);

        Task<string> ChangeLanguage(string userId, string langCode);
        Task<bool> DeleteAccount(string currentUserId);
        Task<GetUserInfoDto> GetUserInfo(string userId);
        Task<string> ChangeUserPass(string userId, ChangePasswordDto passwordDto);
    }
}
