using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces.CarPricingInterfaces;
using UdemyCarBook.Application.ViewModels;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistence.Context;

namespace UdemyCarBook.Persistence.Repositories.CarPricingRepositories
{
    public class CarPricingRepository : ICarPricingRepository
    {
        private readonly CarBookContext _context;

        public CarPricingRepository(CarBookContext context)
        {
            _context = context;
        }

        // Güncel metot: İçerisinde RentACars Include edildiği için hata vermez.
        public List<CarPricing> GetCarPricingWithCars()
        {
            var values = _context.CarPricings
                .Include(x => x.Car)
                    .ThenInclude(y => y.Brand)
                .Include(x => x.Car)
                    .ThenInclude(y => y.RentACars) // Müsaitlik kontrolü için bu kısım eklendi
                .Include(x => x.Pricing)
                .Where(z => z.PricingID == 2)
                .ToList();

            return values;
        }

        public List<CarPricing> GetCarPricingWithTimePeriod()
        {
            throw new NotImplementedException();
        }

        public List<CarPricingViewModel> GetCarPricingWithTimePeriod1()
        {
            List<CarPricingViewModel> values = new List<CarPricingViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                // En dıştaki Select kısmına PivotTable.CarID ekledik
                command.CommandText = @"Select PivotTable.CarID, Model, Name, CoverImageUrl, Available, [2], [3], [4] From (
            Select Cars.CarID, Model, Name, CoverImageUrl, PricingID, Amount, 
            (Select Top 1 Available From RentACars Where RentACars.CarID = Cars.CarID) as Available 
            From CarPricings 
            Inner Join Cars On Cars.CarID=CarPricings.CarId 
            Inner Join Brands On Brands.BrandID=Cars.BrandID
        ) As SourceTable 
        Pivot (Sum(Amount) For PricingID In ([2],[3],[4])) as PivotTable;";

                command.CommandType = System.Data.CommandType.Text;
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        values.Add(new CarPricingViewModel()
                        {
                            // reader[0] diyerek ilk sütun olan CarID'yi garantiye alıyoruz
                            CarID = reader["CarID"] != DBNull.Value ? Convert.ToInt32(reader["CarID"]) : 0,
                            Brand = reader["Name"].ToString(),
                            Model = reader["Model"].ToString(),
                            CoverImageUrl = reader["CoverImageUrl"].ToString(),
                            Available = reader["Available"] != DBNull.Value && Convert.ToBoolean(reader["Available"]),
                            Amounts = new List<decimal>
                    {
                        reader["2"] != DBNull.Value ? Convert.ToDecimal(reader["2"]) : 0,
                        reader["3"] != DBNull.Value ? Convert.ToDecimal(reader["3"]) : 0,
                        reader["4"] != DBNull.Value ? Convert.ToDecimal(reader["4"]) : 0
                    }
                        });
                    }
                }
                _context.Database.CloseConnection();
                return values;
            }
        }
    }
    }
