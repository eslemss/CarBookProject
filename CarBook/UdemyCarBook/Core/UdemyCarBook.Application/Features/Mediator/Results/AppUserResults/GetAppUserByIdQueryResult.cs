using System;

namespace UdemyCarBook.Application.Features.Mediator.Results.AppUserResults
{
    public class GetAppUserByIdQueryResult
    {
        public int AppUserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
    }
}