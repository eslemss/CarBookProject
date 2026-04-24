using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyCarBook.Application.Features.Mediator.Results.StatisticsResults;
using UdemyCarBook.Application.Interfaces;

namespace UdemyCarBook.Application.Features.Mediator.Queries.StatisticsQueries
{
    public class GetBrandCountQuery:IRequest<GetBrandCountQueryResult>
    {
    }
}
