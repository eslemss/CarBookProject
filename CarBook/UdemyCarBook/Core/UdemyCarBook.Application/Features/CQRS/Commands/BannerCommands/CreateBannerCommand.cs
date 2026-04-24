using System;
using System.Collections.Generic;
using System.Text;
using UdemyCarBook.Application.Interfaces;

namespace UdemyCarBook.Application.Features.CQRS.Commands.BannerCommands
{
    public class CreateBannerCommand
    {
       public string Title { get; set; }    
        public string Description { get; set; }

        public string VideoDescription { get; set; }

        public string VideoUrl { get; set; }
    }
}
