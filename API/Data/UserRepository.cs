using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        public UserRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void AdminUpdate(AdminUserUpdateDto admin)
        {
            context.Entry(admin).State = EntityState.Modified;
        }

        public void DeletePhoto(Photo photo)
        {
            context.Photo.Remove(photo);
        }

        public void DeleteUser(AppUser appUser)
        {
            context.Users.Remove(appUser);
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByKnownAs(string knownAs)
        {
            return await context.Users.AsQueryable()
                .Where((x => x.UserName.ToLower() == knownAs || x.GamerTag.ToLower() == knownAs))
                .SingleOrDefaultAsync();
        }

        public async Task<AppUser> GetUserByUsernameAndTagAsync(ForgotPasswordDto forgotPasswordDto)
        {
            return await context.Users.AsQueryable()
                    .Where(a => a.GamerTag == forgotPasswordDto.GamerTag && a.UserName == forgotPasswordDto.Username)
                    .SingleOrDefaultAsync();
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await context.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            var user = await context.Users
                .Include(p => p.Photos).ToListAsync();

            return user;
            
        }

        public void Update(AppUser user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

        public async Task UpdatePassword(AppUser appUser, IUserPasswordStore<AppUser> passwordStore)
        {
            await passwordStore.UpdateAsync(appUser, cancellationToken: default);
        }
    }
}