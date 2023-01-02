using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC.Data.Models
{
	public class ChecklistItem
	{
		public string? Description { get; set; }
		public string? Value { get; set; }
		public string? Notes { get; set; }
		public string? ImageName { get; set; }
		public bool Checked { get; set; } = false;
		public string itemId  => $"{Description?.Replace(" ","").ToLower()}";
	}
}
