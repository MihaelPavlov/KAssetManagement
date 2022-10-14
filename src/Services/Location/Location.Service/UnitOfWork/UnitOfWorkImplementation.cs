namespace Location.Service.UnitOfWork
{
    using System.Transactions;

    public class UnitOfWorkImplementation : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly TransactionScope _transactionScope;

        public UnitOfWorkImplementation()
        {
            _transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadUncommitted,
                    Timeout = TransactionManager.MaximumTimeout
                },
                TransactionScopeAsyncFlowOption.Enabled);
        }
        public void Commit()
        {
            _transactionScope.Complete();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transactionScope.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
