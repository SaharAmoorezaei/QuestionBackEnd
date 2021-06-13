
using WebApi.Domain.Models;

namespace WebApi.Infra
{
    public interface IRoleRepository
        : IRepository<Role>
    {
    }

    public class RoleRepository : Repository<Role>, IRoleRepository
    {
   
        public RoleRepository(QuestionBackEndDbContext context) : base(context)
        {
        }

    }
}
