using GigHub.Core.Models;
using PagedList;
using System.Collections.Generic;

namespace GigHub.Core.ViewModels
{
    public class GigsViewModel
    {
        public IPagedList<Gig> UpcomingGigs { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<ApplicationUser> NewBands { get; set; }
    }
}