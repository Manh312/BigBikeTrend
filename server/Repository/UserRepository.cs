using Microsoft.EntityFrameworkCore;
using server.Entities;
using server.Interface.Repository;

namespace server.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Data.DataContext _context;
        public UserRepository(Data.DataContext context)
        {
            this._context = context;
        }
        public async Task<bool> AddUser(User user)
        {
            if (user.RefreshToken == null)
            {
                user.RefreshToken = string.Empty; // Gán giá trị mặc định nếu NULL
            }
            if (user.RefreshTokenExpire == default) // Gán giá trị mặc định nếu chưa có
            {
                user.RefreshTokenExpire = DateTime.UtcNow.AddDays(7); // Ví dụ: hết hạn sau 7 ngày
            }
            await this._context.Users.AddAsync(user);
            return await this._context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await this._context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await this._context.Users.Where(u => u.Email == email).SingleOrDefaultAsync();
        }

        public async Task<bool> UpdateUser(User user)
        {
            this._context.Users.Attach(user);
            return await this._context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
