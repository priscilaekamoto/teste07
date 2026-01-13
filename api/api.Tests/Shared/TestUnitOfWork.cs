using api.Data;

namespace api.Tests.Shared
{
    public class TestUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public TestUnitOfWork(AppDbContext db)
        {
            _db = db;
        }

        public Task BeginTransactionAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
            => await _db.SaveChangesAsync(cancellationToken);

        public Task CommitAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task RollbackAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }

}
