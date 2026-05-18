using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Interfaces.ReservationInterfaces
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetReservationsByAppUserIdWithRelations(int appUserId);
    }
}