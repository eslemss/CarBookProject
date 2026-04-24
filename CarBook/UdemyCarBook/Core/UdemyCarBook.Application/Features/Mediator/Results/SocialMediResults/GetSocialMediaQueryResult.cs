using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyCarBook.Application.Features.Mediator.Results.SocialMediResults
{
    public class GetSocialMediaQueryResult
    {
        public int SocialMediaID { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
        public string Icon { get; set; }
    }
}
