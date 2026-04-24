using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyCarBook.Application.Features.Mediator.Commands.FeatureCommands
{
    public class CreateFeatureCommand:IRequest
    {
        public string Name { get; set; }
    }
}
