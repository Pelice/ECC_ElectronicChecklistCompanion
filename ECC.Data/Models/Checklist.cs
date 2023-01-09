using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC.Data.Models
{
    public class Checklist
    {
        public int ChecklistId { get; set; }
        public string ChecklistName { get; set; }
        public int AirplaneId { get; set; }
        public List<ChecklistSection> Sections { get; set; } = new List<ChecklistSection>();
    }
}
