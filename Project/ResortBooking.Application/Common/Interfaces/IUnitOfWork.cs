using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortBooking.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IResortRepository Resort { get; }
        IResortNumberRepository ResortNumber { get; }
        IAmenityRepository Amenity { get; }
        IApplicationUserRepository User { get; }
        IBookingRepository Booking { get; }

        void Save();
    }
}
