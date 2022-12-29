using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC.Data.Models
{
    public class ChecklistSection
    {
        public string? SectionName { get; set; }
        public List<ChecklistItem> ChecklistItems{ get; set; } = new List<ChecklistItem>();
    }
}
