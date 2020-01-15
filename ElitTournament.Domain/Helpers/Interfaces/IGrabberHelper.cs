using AngleSharp.Dom;
using ElitTournament.Domain.Entities;
using System.Collections.Generic;

namespace ElitTournament.Domain.Helpers.Interfaces
{
	public interface IGrabberHelper
	{
		List<Schedule> ParseSchedule(IDocument document);

		IEnumerable<string> GetLinks(IDocument document);

		List<League> ParseLeagues(IDocument document);
	}
}
