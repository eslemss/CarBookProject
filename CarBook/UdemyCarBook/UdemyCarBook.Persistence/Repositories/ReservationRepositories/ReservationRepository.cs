using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces.ReservationInterfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistence.Context;

namespace UdemyCarBook.Persistence.Repositories.ReservationRepositories;

public class ReservationRepository(CarBookContext context) : Repository<Reservation>(context), IReservationRepository
{
    public async Task<List<Reservation>> GetReservationsByAppUserIdWithRelations(int id)
    {
        // 'context' parametresini direkt kullanıyoruz
        return await context.Reservations
            .Include(x => x.Car)
                .ThenInclude(y => y.Brand) // Araç üzerinden markaya erişim
            .Include(x => x.PickUpLocation) // Alış lokasyonu ilişkisi
            .Include(x => x.DropOffLocation) // İade lokasyonu ilişkisi
            .Where(x => x.AppUserId == id)
            .ToListAsync();
    }
}