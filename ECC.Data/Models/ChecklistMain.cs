using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC.Data.Models
{
    public class ChecklistMain
    {
        public List<ChecklistSection> Sections { get; set; } = new List<ChecklistSection>();
    }
}
