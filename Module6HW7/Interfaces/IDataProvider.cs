using Module6HW7.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Module6HW7.Interfaces
{
    public interface IDataProvider
    {
        public Task<List<Teapot>> GetTeapots();

        public Task<Teapot> GetTeapotById(Guid id);

        public Task<bool> AddTeapot(Teapot teapot);

        public Task<bool> EditTeapot(Teapot teapot);

        public Task<bool> DeleteTeapot(Teapot teapot);
    }
}
