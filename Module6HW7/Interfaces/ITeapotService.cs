using Module6HW7.Models;
using Module6HW7.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Module6HW7.Interfaces
{
    public interface ITeapotService
    {
        public Task<List<Teapot>> GetTeapots();

        public Task<Teapot> GetTeapotById(Guid id);

        public Task<bool> AddTeapot(TeapotViewModel teapotModel);

        public Task<bool> EditTeapotById(Guid id, TeapotViewModel teapotModel);

        public Task<bool> DeleteTeapotById(Guid id);
    }
}
