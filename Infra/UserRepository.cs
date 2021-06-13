
using System.Linq;
using WebApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApi.Infra
{
    public interface IUserRepository
        : IRepository<User>
    {
        User GetUser(string username, string password);
    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly QuestionBackEndDbContext _context;
        public UserRepository(QuestionBackEndDbContext context) : base(context)
        {
            _context = context;
        }

        public User GetUser(string username, string password)
        {
            return _context.Users.Where(p => p.Password == password && p.UserName == username).Include(p => p.UserRoles).ThenInclude(p=>p.Role).FirstOrDefault();
        }

        
    }
}
