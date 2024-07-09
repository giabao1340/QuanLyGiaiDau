    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLGiaiDau.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string University { get; set; }
        public string Coach { get; set; }
        public int FoundedYear { get; set; }  // Năm Thành Lập
        public string Stadium { get; set; }   // Sân Vận Động
        public ICollection<Team> Teams { get; set; }

    }
}