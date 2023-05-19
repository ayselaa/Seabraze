
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SeaBreeze.Domain;
using SeaBreeze.Domain.Entity.Users;
using SeaBreeze.Service.DTOS.Account;
using SeaBreeze.Service.DTOS.Consumer;
using SeaBreeze.Service.DTOS.User;
using SeaBreeze.Service.Enums;
using SeaBreeze.Service.Helpers.TokenService;
using SeaBreeze.Service.Interfaces;

namespace SeaBreeze.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public AccountService(UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager, ITokenService tokenService, AppDbContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _context = context;
        }

        public async Task RegisterConsumer(RegisterConsumerDto consumer)
        {
            var user = _mapper.Map<AppUser>(consumer);

            var checkUser = await _userManager.FindByEmailAsync(consumer.Email);

            if (checkUser is not null)
            {
                throw new Exception("İstifadəçi artıq mövcuddur");
            }


            try
            {
                user.UserName = consumer.Email;
                user.Lang = "en";
                var userCreateResult = await _userManager.CreateAsync(user, consumer.PassWord);
                if (userCreateResult.Succeeded)
                {
                    await CheckRoleExist(DefinedUSerRoles.Consumer.ToString());
                    await _userManager.AddToRoleAsync(user, DefinedUSerRoles.Consumer.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }

        }



        public async Task RegisterResident(RegisterResidentDto resident)
        {
            var residentInfo = await _context.ResidentInfos.Where(m => m.FIN == resident.FIN).FirstOrDefaultAsync();

            if (residentInfo is null) throw new Exception("Rezident tapılmadı.");

            var user = _mapper.Map<AppUser>(resident);

            var checkUser = await _userManager.FindByEmailAsync(resident.Email);

            if (checkUser is not null)
            {
                throw new Exception("İstifadəçi artıq mövcuddur");
            }

            try
            {

                user.UserName = resident.Email;
                user.IsResident = true;
                user.Lang = "en";
                var userCreateResult = await _userManager.CreateAsync(user, resident.PassWord);
                if (userCreateResult.Succeeded)
                {
                    await CheckRoleExist(DefinedUSerRoles.Resident.ToString());
                    await _userManager.AddToRoleAsync(user, DefinedUSerRoles.Resident.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<string> Login(UserLoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Email);
            if (user == null) throw new Exception("İstifadəçi mövcud deyil");


            if (user.IsDelete)
            {
                throw new Exception("İstifadəçi mövcud deyil");
            }


            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result) throw new Exception("Mail və ya şifrə yalnışdır");

            var roles = await _userManager.GetRolesAsync(user);


            if (roles.Contains("Resident"))
            {
                throw new Exception("Rezident girişi edin");
            }

            return _tokenService.CreateToken(user, roles);
        }



        public async Task<string> LoginResident(UserLoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Email);
            if (user == null) throw new Exception("İstifadəçi mövcud deyil");


            if (user.IsDelete)
            {
                throw new Exception("İstifadəçi mövcud deyil");
            }


            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result) throw new Exception("Mail və ya şifrə yalnışdır");

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("Resident"))
            {
                throw new Exception("İstifadəçi rezident deyil.");
            }

            return _tokenService.CreateToken(user, roles);
        }


        private async Task<bool> CheckRoleExist(string roleName)
        {
            var isRoleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!isRoleExist)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                return result.Succeeded;
            }

            return isRoleExist;
        }

        public async Task<string> ChangeLanguage(string userId, string langCode)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user.Lang = langCode;

            await _userManager.UpdateAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            return _tokenService.CreateToken(user, roles);
        }


        public async Task<bool> DeleteAccount(string currentUserId)
        {
            var user = await _userManager.FindByIdAsync(currentUserId);

            user.IsDelete = true;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<GetUserInfoDto> GetUserInfo(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);


            GetUserInfoDto userInfoDto = new GetUserInfoDto()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsResident = user.IsResident,
                FullName = user.FullName
            };

            return userInfoDto;
        }


        public async Task<string> ChangeUserPass(string userId, ChangePasswordDto passwordDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.CheckPasswordAsync(user, passwordDto.CurrentPass);

            if (result)
            {
                var resetPassToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                var passChangeResult = await _userManager.ResetPasswordAsync(user, resetPassToken, passwordDto.NewPass);

                var roles = await _userManager.GetRolesAsync(user);

                return _tokenService.CreateToken(user, roles);
            }
            else
            {
                throw new Exception("Köhnə şifrə yalnışdır");
            }
        }
    }

}
