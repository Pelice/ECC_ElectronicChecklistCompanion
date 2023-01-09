using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC.Data.Models
{
    public class Airplane
    {
        public string AirplaneName { get; set; }
        public string? SimConnectName { get; set; }
        public List<Checklist> Checklist { get; set; }
        public int AirplaneId { get; set; }
    }
}
