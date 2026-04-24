using System;
using System.Collections.Generic;
using System.Text;
using UdemyCarBook.Application.Features.CQRS.Commands.ContactCommands;
//using UdemyCarBook.Application.Features.CQRS.Results.ContactResult;
using UdemyCarBook.Application.Features.CQRS.Results.ContactResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.ContactHandlers
{
    public class GetContactCommandQueryHandler
    {

        private readonly IRepository<Contact> _repository;

        public GetContactCommandQueryHandler(IRepository<Contact> repository)
        {
            _repository = repository;
        }
        public async Task<List<GetContactQueryResult>> Handle()
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetContactQueryResult
            {
                ContactID = x.ContactID,
                Name = x.Name,
                Email= x.Email,
                Message = x.Message,
                SendDate = x.SendDate,  
                Subject = x.Subject
            }).ToList();
        }
    }
}

