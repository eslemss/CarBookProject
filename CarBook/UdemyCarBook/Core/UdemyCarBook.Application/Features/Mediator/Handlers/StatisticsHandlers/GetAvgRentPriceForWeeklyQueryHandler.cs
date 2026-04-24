using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyCarBook.Application.Features.Mediator.Queries.StatisticsQueries;
using UdemyCarBook.Application.Features.Mediator.Results.StatisticsResults;
using UdemyCarBook.Application.Interfaces.StatisticsInterfaces;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.StatisticsHandlers
{
    public class GetAvgRentPriceForWeeklyQueryHandler : IRequestHandler< GetAvgRentPriceForWeeklyQuery,  GetAvgRentPriceForWeeklyQueryResult>
    {

        private readonly IStatisticsRepository _repository;

        public  GetAvgRentPriceForWeeklyQueryHandler(IStatisticsRepository repository)
        {
            _repository = repository;
        }

        public async Task< GetAvgRentPriceForWeeklyQueryResult> Handle( GetAvgRentPriceForWeeklyQuery request, CancellationToken cancellationToken)
        {
            var value = _repository. GetAvgRentPriceForWeekly();
            return new  GetAvgRentPriceForWeeklyQueryResult
            {
              AvgRentPriceForWeekly = value,
            };
        }
   
    }
}
