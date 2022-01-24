using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelacionTablas.Dtos
{
    public class GetPlayersDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Jersey { get; set; }
        public string TeamName { get; set; }
    }
}