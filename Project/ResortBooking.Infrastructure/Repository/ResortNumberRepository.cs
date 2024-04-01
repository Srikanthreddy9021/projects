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
    public class ResortNumberRepository : Repository<ResortNumber>, IResortNumberRepository
    {

        private readonly ApplicationDbContext _db;

        public ResortNumberRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
        

        //public void Save()
        //{
        //    _db.SaveChanges();
        //}

        public void Update(ResortNumber entity)
        {
            _db.ResortNumbers.Update(entity);
        }
    }
}
