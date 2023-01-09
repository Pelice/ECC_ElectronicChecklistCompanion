using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using ECC.Data.Models;
using NLog;

namespace ECC.Core
{
	public class ChecklistFileManager : IChecklistManager
	{
		private readonly string _folderPath;
		private readonly ILogger _logger;
		private List<Airplane> _airplanes = new List<Airplane>();

		public ChecklistFileManager(string folderPath, ILogger logger)
		{
			if (string.IsNullOrWhiteSpace(folderPath))
				throw new ArgumentNullException(nameof(folderPath));

			if (!Directory.Exists(folderPath))
				throw new ArgumentException($"Directory {folderPath} not exists");

			_folderPath = folderPath;
			_logger = logger;
		}

		public async Task<List<Airplane>> GetAvailableAirplaneChecklists()
		{
			string[] allFiles = Directory.GetFiles(_folderPath, "*.json", SearchOption.AllDirectories);
			if (allFiles.Length == 0)
				throw new Exception($"There are no checklists in folder {_folderPath}");

			_airplanes.Clear();

			foreach (var file in allFiles)
			{
				var jsonString = File.ReadAllText(file, Encoding.UTF8);

				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};
				var airplane = JsonSerializer.Deserialize<Airplane>(jsonString, options);

				if (airplane != null)
				{
					var existId = _airplanes.Any(x => x.AirplaneId == airplane.AirplaneId);
					if (existId)
					{
						_logger.Warn($"Unable to use file {file}: checklist Name already exists (ignoring spaces)");
						continue;
					}

					_airplanes.Add(airplane);
				}
			}

			return _airplanes;
		}

		public async Task<Airplane?> GetChecklistForAirplane(int checklistId)
		{
			var airplaneData = _airplanes.Where(x => x.AirplaneId == checklistId).FirstOrDefault();

			if (airplaneData != null && airplaneData.Checklist != null)
				return airplaneData;
			else
				return null;
		}

	}
}