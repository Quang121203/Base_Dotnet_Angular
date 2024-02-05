using API.DataAccess;
using API.Migrations;
using API.Models.Domains;
using API.Models.DTOS;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ITokenService tokenService;
        private readonly IUnitOfWork unitOfWork;

        public AuthService(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService,
            IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.tokenService = tokenService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<object> Login(LoginVM model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var password = await userManager.CheckPasswordAsync(user, model.Password);
            if (user != null && password)
            {
                var token = await tokenService.CreateToken(user);


                return (new
                {
                    EM = "login successfully",
                    EC=0,
                    DT = ( new
                    {
                        accsessToken=token.AccessToken,
                        refeshToken=token.RefeshToken,
                    })
                });
            }

            return (new
            {
                EM = "email or password not right",
                EC=1,
                DT=""
            });
        }

        public async Task<object?> Register(RegisterVM model)
        {
            var check = await userManager.FindByEmailAsync(model.Email);
            if (check == null)
            {
                var user = new User
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    ProfilePic = "",
                };

                var result = await userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    if(! await roleManager.RoleExistsAsync(RoleVM.Guest))
                    {
                        await roleManager.CreateAsync(new IdentityRole(RoleVM.Guest));

                    }

                    await userManager.AddToRoleAsync(user, RoleVM.Guest);

                    return (new {
                        EM = "register successfully",
                        EC=0,
                        DT = ""
                    });
                }

                return null;
            }

            return (new
            {
                EM = "email have exits",
                EC=1,
                DT=""
            });
        }

        public async Task<object> Refesh(string accessToken, string refeshToken)
        {      
            var token = await unitOfWork.TokenRepository.GetSingleAsync(refeshToken);
            if (token.ExpiresRefeshToken < DateTime.Now)
            {
                return (new
                {
                    EM= "Token expired",
                    EC=1,
                    DT="",
                });
            }

            if (token.AccessToken != accessToken)
            {
                return (new
                {
                    EM = "Invalid Refresh Token",
                    EC = 1,
                    DT = "",
                });
            }

            var user = await userManager.FindByIdAsync(token.UserId); 

            await unitOfWork.TokenRepository.DeleteAsync(refeshToken);
            await unitOfWork.SaveChangesAsync();

            token = await tokenService.CreateToken(user);

            return (new
            {
                EM = "",
                EC = 0,
                DT = token,
            });
        }
    }
}
