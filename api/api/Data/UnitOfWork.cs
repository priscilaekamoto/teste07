using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using api.Data;
using api.Application.Mediator.Interfaces;

namespace api.Data
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null) return;
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null) return;

            // garante que mudanças pendentes sejam salvas antes do commit
            await _context.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null) return;
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                try { await _transaction.DisposeAsync(); } catch { }
                _transaction = null;
            }
            // Não dispose o _context: o container DI gerencia o ciclo de vida
        }
    }
}