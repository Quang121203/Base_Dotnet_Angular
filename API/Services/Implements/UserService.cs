using API.Models.Domains;
using API.Models.DTOS;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<object?> DeleteUser(string id)
        {
            if (httpContextAccessor.HttpContext != null)
            {
                var currentIdUser = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentRoleUser = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
                if (id == currentIdUser || currentRoleUser == RoleVM.Admin)
                {
                    var user = await userManager.FindByIdAsync(id);
                    if (user != null)
                    {
                        var delete = await userManager.DeleteAsync(user);

                        if (delete != null)
                        {
                            return (new
                            {
                                EM = "delete this user successfully",
                                EC = 0,
                                DT = ""
                            });
                        }

                        return null;

                    }

                    return (new
                    {
                        EM = "user does not exits",
                        EC = 1,
                        DT = ""
                    });
                }
                return (new
                {
                    EM = "You don't have permission to delete orther users",
                    EC = 1,
                    DT = ""
                });
            }
            return (new
            {
                EM = "login please",
                EC = 1,
                DT = ""
            });
        }

        public async Task<object?> GetAllUser()
        {
            var users = await userManager.Users.OrderByDescending(u => u.DateCreated).ToListAsync();
            var usersVM = new List<UserVM>();
            foreach (var user in users)
            {
                UserVM userVM = new UserVM();
                userVM.Id = user.Id;
                userVM.UserName = user.UserName;
                userVM.Email = user.Email;
                userVM.ProfilePic = user.ProfilePic;
                userVM.DateCreated = user.DateCreated;
                usersVM.Add(userVM);
            }
            return (new
            {
                EM = "",
                EC = 0,
                DT = usersVM
            });
        }

        public async Task<object?> GetUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userVM = new UserVM
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                ProfilePic = user.ProfilePic,
                DateCreated = user.DateCreated
            };
            return (new
            {
                EM = "",
                EC = 0,
                DT = userVM
            });
        }

        public async Task<object?> GetUserStats()
        {
            var lastYear = DateTime.Now.AddYears(-1);

            var users = await userManager.Users
                .Where(user => user.DateCreated >= lastYear)
                .ToListAsync();

            var data = users
                .GroupBy(user => user.DateCreated.Month)
                .Select(group => new
                {
                    Month = group.Key,
                    Total = group.Count()
                })
                .ToList();
            return (new
            {
                EM = "",
                EC = 0,
                DT = data
            });
        }

        public async Task<object?> UpdateUser(UserVM model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return (new
                {
                    EM = "user doesn't exits yet",
                    EC = 1,
                    DT = ""
                });
            }

            if (httpContextAccessor.HttpContext == null)
            {
                return (new
                {
                    EM = "login please",
                    EC = 1,
                    DT = ""
                });
            }
            var currentIdUser = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentRoleUser = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (user.Id == currentIdUser || currentRoleUser == RoleVM.Admin)
            {
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.ProfilePic = model.ProfilePic;

                var update = await userManager.UpdateAsync(user);

                if (update.Succeeded)
                {
                    return (new
                    {
                        EM = "update successfully",
                        EC = 0,
                        DT = ""
                    });
                }
                return null;
            }

            return (new
            {
                EM = "You don't have permission to delete orther users",
                EC = 1,
                DT = ""
            });
        }


    }
}
