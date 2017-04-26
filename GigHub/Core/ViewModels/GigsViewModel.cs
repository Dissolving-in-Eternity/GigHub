using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using PagedList;

namespace GigHub.Core.ViewModels
{
    public class GigsViewModel
    {
        public IPagedList<Gig> UpcomingGigs { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
        public string SearchTerm { get; set; }
        public ILookup<int, Attendance> Attendances { get; set; }
        public ILookup<string, Following> Followings { get; set; }
    }
}