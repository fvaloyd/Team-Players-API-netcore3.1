using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelacionTablas.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Player> Players { get; set; }
    }
}