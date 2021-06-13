
using WebApi.Domain.Models;

namespace WebApi.Infra
{

    public interface IUserRoleRepository
    : IRepository<UserRole>
    {
    }

    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(QuestionBackEndDbContext context) : base(context)
        {
        }
    }
}
