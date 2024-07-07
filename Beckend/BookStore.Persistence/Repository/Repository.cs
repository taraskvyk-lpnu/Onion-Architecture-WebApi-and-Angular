﻿using BookStore.API.Domain;
using BookStore.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly BookDbContext _context;
        internal readonly DbSet<T> _dbSet;

        public Repository(BookDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await SaveChangesAsync();
        }

        public async Task<bool> RemoveByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            return await SaveChangesAsync();
        }

        protected async Task<bool> SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                return false;
            }
        }
    }
}