using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Interfaces.RentACarInterfaces
{
    public interface IRentACarRepository
    {
        Task<List<RentACar>> GetByFilterAsync(Expression<Func<RentACar, bool>> filter);
        Task UpdateRentACarAvailableStatusAsync(int carId, int locationId, bool status);

    }
}
