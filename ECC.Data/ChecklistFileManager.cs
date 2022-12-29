using ECC.Data.Models;

namespace ECC.Data
{
    public class ChecklistFileManager
    {
		private readonly string _folderPath;

		public ChecklistFileManager(string folderPath)
        {
			_folderPath = folderPath;
		}

		public List<Airplane> GetAvailableAirplaneChecklists() { return null; }

		public List<ChecklistMain> GetChecklistForAirplane(Airplane airplane) { return null; }

    }
}