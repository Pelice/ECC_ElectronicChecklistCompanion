using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC.Data.Models
{
    public class Airplane
    {
        public string? Name { get; set; }
        public string? SimConnectName { get; set; }
        public ChecklistMain? Checklist { get; set; }
        public string? AirplaneId => Name?.Replace(" ", "").ToLower();
    }
}
