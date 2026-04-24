using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces.RentACarInterfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistence.Context;

namespace UdemyCarBook.Persistence.Repositories.RentACarRepositories
{
    public class RentACarRepository : IRentACarRepository
    {
        private readonly CarBookContext _context;
        public RentACarRepository(CarBookContext context)
        {
            _context = context;
        }

        public async Task<List<RentACar>> GetByFilterAsync(Expression<Func<RentACar, bool>> filter)
        {
            return await _context.RentACars
                .Include(x => x.Car)
                    .ThenInclude(c => c.Brand)
                .Include(x => x.Car)
                    .ThenInclude(c => c.CarPricings)
                        .ThenInclude(cp => cp.Pricing)
                .Where(filter)
                .ToListAsync();
        }

        // Hata veren kısım burasıydı, bu metodu ekleyerek sözleşmeyi tamamlıyoruz:
        public async Task UpdateRentACarAvailableStatusAsync(int carId, int locationId, bool status)
        {
            var value = await _context.RentACars.FirstOrDefaultAsync(x => x.CarID == carId && x.LocationID == locationId);
            if (value != null)
            {
                value.Available = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}