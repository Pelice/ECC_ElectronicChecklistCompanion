using ECC.Data.Models;
using NLog;

namespace ECC.Core
{
	public interface IChecklistManager
	{
		Task<List<Airplane>> GetAvailableAirplaneChecklists();
		Task<Airplane?> GetChecklistForAirplane(int checklistId);
	}
}