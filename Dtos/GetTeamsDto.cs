using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelacionTablas.Dtos
{
    public class GetTeamsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> PlayersName { get; set; }
    }
}