using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyCarBook.Application.Features.Mediator.Commands.PricingCommands
{
    public class CreatePricingCommand:IRequest
    {
        public string Name { get; set; }
    }
}
