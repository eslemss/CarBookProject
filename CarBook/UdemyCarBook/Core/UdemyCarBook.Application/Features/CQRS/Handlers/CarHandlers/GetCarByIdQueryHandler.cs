using UdemyCarBook.Application.Features.CQRS.Queries.CarQueries;
using UdemyCarBook.Application.Features.CQRS.Results.CarResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

public class GetCarByIdQueryHandler
{
    private readonly IRepository<Car> _carRepository;
    private readonly IRepository<Brand> _brandRepository; // Marka repository'sini ekledik

    public GetCarByIdQueryHandler(IRepository<Car> carRepository, IRepository<Brand> brandRepository)
    {
        _carRepository = carRepository;
        _brandRepository = brandRepository;
    }

    public async Task<GetCarByIdQueryResult> Handle(GetCarByIdQuery query)
    {
        var car = await _carRepository.GetByIdAsync(query.Id);

        if (car == null) return null;

        // Marka adını ayrıca çekiyoruz (Garantici yol)
        var brand = await _brandRepository.GetByIdAsync(car.BrandID);

        return new GetCarByIdQueryResult
        {
            CarID = car.CarID,
            BrandID = car.BrandID,
            BrandName = brand?.Name, // İşte Tesla/Togg buraya gelecek!
            Model = car.Model,
            CoverImageUrl = car.CoverImageUrl,
            BigImageUrl = car.BigImageUrl,
            Km = car.Km,
            Transmission = car.Transmission,
            Seat = car.Seat,
            Luggage = car.Luggage,
            Fuel = car.Fuel
        };
    }
}