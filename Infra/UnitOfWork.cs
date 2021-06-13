


namespace WebApi.Infra
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        
        void Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private QuestionBackEndDbContext context;

        public UnitOfWork(QuestionBackEndDbContext context)
        {
            this.context = context;
        }
        public IUserRepository UserRepository
        {
            get
            {
                return new UserRepository(context);
            }
        }


        public IRoleRepository RoleRepository
        {
            get
            {
                return new RoleRepository(context);
            }
        }

        public IUserRoleRepository UserRoleRepository
        {
            get
            {
                return new UserRoleRepository(context);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}

