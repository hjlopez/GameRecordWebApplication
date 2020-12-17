using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        void AdminUpdate(AdminUserUpdateDto admin);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        //Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        //Task<MemberDto> GetMemberAsync(string username);
        void DeleteUser(AppUser appUser);
        Task UpdatePassword(AppUser appUser, IUserPasswordStore<AppUser> passwordStore);
        Task<AppUser> GetUserByUsernameAndTagAsync(ForgotPasswordDto forgotPasswordDto);
        void DeletePhoto(Photo photo);
    }
}