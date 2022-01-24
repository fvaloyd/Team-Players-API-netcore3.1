using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelacionTablas.Dtos
{
    public class PostPlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Jersey  { get; set; }
        public int TeamId { get; set; }
    }
}