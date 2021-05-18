using BSBAdminDashboard.BL.Interface;
using BSBAdminDashboard.DAL.Database;
using BSBAdminDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard.BL.Repository
{
    public class CityRep : ICityRep
    {
        //private DbContainer db = new DbContainer();

        private readonly DbContainer db;

        public CityRep(DbContainer db)
        {
            this.db = db;
        }

        public IQueryable<CityVM> Get()
        {
            IQueryable<CityVM> data = GetAllCities();

            return data;
        }

        public CityVM GetById(int id)
        {
            CityVM data = GetCityById(id);
            // FirstOrDefault couse if not found id back null
            return data;
        }

        // Refactor

        private CityVM GetCityById(int id)
        {
            return db.City.Where(a => a.Id == id)
                                    .Select(a => new CityVM
                                    {
                                        Id = a.Id,
                                        CityName = a.CityName,
                                        CountryId = a.CountryId
                                    }).FirstOrDefault();
        }

        private IQueryable<CityVM> GetAllCities()
        {
            return db.City.Select(a => new CityVM
            {
                Id = a.Id,
                CityName = a.CityName,
                CountryId = a.CountryId
            });
        }
    }
}
