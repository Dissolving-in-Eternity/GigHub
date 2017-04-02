using System.Collections.Generic;
using System.Collections;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool ShowActions { get; set; }
    }
}