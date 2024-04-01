using Microsoft.EntityFrameworkCore;
using ResortBooking.Application.Common.Interfaces;
using ResortBooking.Domain.Entities;
using ResortBooking.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ResortBooking.Infrastructure.Repository
{
    public class ResortRepository : Repository<Resort>, IResortRepository
    {

        private readonly ApplicationDbContext _db;

        public ResortRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
        

        //public void Save()
        //{
        //    _db.SaveChanges();
        //}

        public void Update(Resort entity)
        {
            _db.Resorts.Update(entity);
        }
    }
}
