using APICleanArchi.Data;
using APICleanArchi.Entities;
using Microsoft.EntityFrameworkCore;

namespace APICleanArchi.Repositories
{
    public class UserRepository: IUserRepository
    
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            return await _context.Users.AsQueryable().Include(u=>u.Gradles).ToListAsync();
        }

        public async Task<UserEntity> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<UserEntity> CreateUserAsync(UserEntity user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserEntity> UpdateUserAsync(UserEntity user)
        {
            //_context.Entry(user).State = EntityState.Modified; // this is another way to update
            _context.Users.Update(user); // this is another way to update
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            var result = _context.Users.Remove(user);
            if (result.State == EntityState.Deleted)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

    public interface IUserRepository
    {
        // get all users
        Task<IEnumerable<UserEntity>> GetAllUsersAsync();

        // get user by id
        Task<UserEntity> GetUserByIdAsync(int id);

        // create user
        Task<UserEntity> CreateUserAsync(UserEntity user);

        // update user
        Task<UserEntity> UpdateUserAsync(UserEntity user);

        // delete user
        Task<bool> DeleteUserAsync(int id);


    }
}
