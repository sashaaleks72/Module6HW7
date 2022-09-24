﻿using Module6HW7.Exceptions;
using Module6HW7.Interfaces;
using Module6HW7.Models;
using Module6HW7.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Module6HW7.Services
{
    public class TeapotService : ITeapotService
    {
        private readonly IDataProvider _dataProvider;

        public TeapotService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task<List<Teapot>> GetTeapots()
        {
            var teapots = await _dataProvider.GetTeapots();

            if (teapots.Count == 0)
            {
                throw new BusinessException("Db is empty!");
            }

            return teapots;
        }

        public async Task<Teapot> GetTeapotById(Guid id)
        {
            var teapot = await _dataProvider.GetTeapotById(id);

            if (teapot == null)
            {
                throw new BusinessException("Teapot with this id is absent!");
            }

            return teapot;
        }

        public async Task<bool> AddTeapot(TeapotViewModel teapotModel)
        {
            var teapot = new Teapot { 
                Title = teapotModel.Title, 
                Capacity = teapotModel.Capacity, 
                Description = teapotModel.Description, 
                Price = teapotModel.Price, 
                Quantity = teapotModel.Quantity, 
                ManufacturerCountry = teapotModel.ManufacturerCountry,
                WarrantyInMonths = teapotModel.WarrantyInMonths, 
                ImgUrl = teapotModel.ImgUrl 
            };

            return await _dataProvider.AddTeapot(teapot);
        }

        public async Task<bool> EditTeapotById(Guid id, TeapotViewModel teapotModel)
        {
            var editedTeapot = await _dataProvider.GetTeapotById(id);

            if (editedTeapot == null)
            {
                throw new BusinessException("Teapot with this id is absent!");
            }

            editedTeapot.Title = teapotModel.Title;
            editedTeapot.Price = teapotModel.Price;
            editedTeapot.Description = teapotModel.Description;
            editedTeapot.Capacity = teapotModel.Capacity;
            editedTeapot.Quantity = teapotModel.Quantity;
            editedTeapot.ImgUrl = teapotModel.ImgUrl;
            editedTeapot.ManufacturerCountry = teapotModel.ManufacturerCountry;
            editedTeapot.WarrantyInMonths = teapotModel.WarrantyInMonths;

            return await _dataProvider.EditTeapot(editedTeapot);
        }

        public async Task<bool> DeleteTeapotById(Guid id)
        {
            var teapotToDelete = await _dataProvider.GetTeapotById(id);

            if (teapotToDelete == null)
            {
                throw new BusinessException("Teapot with this id is absent!");
            }

            return await _dataProvider.DeleteTeapot(teapotToDelete);
        }
    }
}
