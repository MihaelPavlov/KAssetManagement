namespace Location.Service.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new UnitOfWorkImplementation();
        }
    }
}
