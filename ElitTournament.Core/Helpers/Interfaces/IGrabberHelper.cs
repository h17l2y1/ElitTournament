using AngleSharp.Dom;
using ElitTournament.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElitTournament.Core.Helpers.Interfaces
{
	public interface IGrabberHelper
	{
		IEnumerable<Schedule> ParseSchedule(IDocument document);

		IEnumerable<string> GetLinks(IDocument document);

		IEnumerable<League> ParseLeagues(IDocument document);

		Task<IEnumerable<League>> ParseTables(IDocument document);
	}
}
