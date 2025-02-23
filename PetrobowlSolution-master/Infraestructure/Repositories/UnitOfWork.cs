﻿using Application.Persistence;
using Infraestructure.Persistence;
using System.Collections;

namespace Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public Hashtable? _repositories;

        private readonly PetrobowlDbContext _context;

        public UnitOfWork(PetrobowlDbContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error en transaccion", e);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories is null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator
                    .CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type]!;

        }
    }
}
