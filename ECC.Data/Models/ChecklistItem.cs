using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC.Data.Models
{
	public class ChecklistItem
	{
		public int ChecklistItemId { get; set; }
		public int SectionId { get; set; }
		public int ItemOrder { get; set; }
		public string? Description { get; set; }
		public string? Value { get; set; }
		public string? Notes { get; set; }
		public string? Image { get; set; }
		public bool Checked { get; set; } = false;
		public string TextColor { get; set; }
		public string TextBackgroundColor { get; set; }
		public bool TextBold { get; set; }
	}
}
