using Microsoft.EntityFrameworkCore;
using Module6HW7.DB;
using Module6HW7.Interfaces;
using Module6HW7.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Module6HW7.Providers
{
    public class DataProvider : IDataProvider
    {
        private readonly ApplicationDbContext _dbContext;

        public DataProvider(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Teapot>> GetTeapots()
        {
            var teapots = await _dbContext.Teapots.ToListAsync();

            return teapots;
        }

        public async Task<Teapot> GetTeapotById(Guid id)
        {
            var teapot = await _dbContext.Teapots.FirstOrDefaultAsync(x => x.Id == id);

            return teapot;
        }

        public async Task<bool> AddTeapot(Teapot teapot)
        {
            await _dbContext.Teapots.AddAsync(teapot);
            int quanOfAddedTeapots = await _dbContext.SaveChangesAsync();

            var isAdded = quanOfAddedTeapots > 0;

            return isAdded;
        }

        public async Task<bool> EditTeapot(Teapot teapot)
        {
            _dbContext.Update(teapot);
            int quanOfChangedTeapots = await _dbContext.SaveChangesAsync();

            var isChanged = quanOfChangedTeapots > 0;

            return isChanged;
        }

        public async Task<bool> DeleteTeapot(Teapot teapot)
        {
            _dbContext.Teapots.Remove(teapot);
            int quanOfDeletedTeapots = await _dbContext.SaveChangesAsync();

            var isRemoved = quanOfDeletedTeapots > 0;

            return isRemoved;
        }
    }
}
