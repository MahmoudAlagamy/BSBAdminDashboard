using BSBAdminDashboard.BL.Interface;
using BSBAdminDashboard.DAL.Database;
using BSBAdminDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard.BL.Repository
{
    public class CountryRep : ICountryRep
    {
        //private DbContainer db = new DbContainer();

        private readonly DbContainer db;

        public CountryRep(DbContainer db)
        {
            this.db = db;
        }

        public IQueryable<CountryVM> Get()
        {
            IQueryable<CountryVM> data = GetAllCountries();

            return data;
        }

        public CountryVM GetById(int id)
        {
            CountryVM data = GetCountryById(id);
            // FirstOrDefault couse if not found id back null
            return data;
        }

        // Refactor

        private CountryVM GetCountryById(int id)
        {
            return db.Country.Where(a => a.Id == id)
                                    .Select(a => new CountryVM
                                    {
                                        Id = a.Id,
                                        CountryName = a.CountryName
                                    }).FirstOrDefault();
        }

        private IQueryable<CountryVM> GetAllCountries()
        {
            return db.Country.Select(a => new CountryVM
            {
                Id = a.Id,
                CountryName = a.CountryName
            });
        }
    }
}
