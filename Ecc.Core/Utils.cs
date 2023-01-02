using System.Net.Sockets;
using System.Net;
using ECC.Data.Models;
using System.Text.Json;

namespace Ecc.Core
{
	public static class Utils
	{
		public static string GetLocalIPAddress()
		{
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					return ip.ToString();
				}
			}
			throw new Exception("No network adapters with an IPv4 address in the system!");
		}

		public static void ChecklistFileExampleGeneration(string checklistFolder)
		{

			Airplane example = new Airplane()
			{
				Name = "Example",
				SimConnectName = null,
				Checklist = new ChecklistMain()
				{
					Sections = new List<ChecklistSection> {
						new ChecklistSection()
						{
							SectionName = "Secure cabin",
							ChecklistItems = new List<ChecklistItem>
							{
								new ChecklistItem()
								{
									Description="Landing gear",
									Value = "DOWN"
								},
								new ChecklistItem()
								{
									Description="Speed-brakes",
									Value = "DOWN"
								}
							}
						},
						new ChecklistSection()
						{
							SectionName = "Before start",
							ChecklistItems = new List<ChecklistItem>
							{
								new ChecklistItem()
								{
									Description="Parking brake",
									Value = "SET"
								},
								new ChecklistItem()
								{
									Description="Chocks",
									Value = "REMOVED"
								}
							}
						}
					}
				}
			};

			var options = new JsonSerializerOptions { WriteIndented = true };
			string jsonString = JsonSerializer.Serialize(example, options);
			System.IO.File.WriteAllText($@"{checklistFolder}\example.json", jsonString);
		}
	}
}