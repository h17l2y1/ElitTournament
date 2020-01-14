using AngleSharp.Dom;
using ElitTournament.Domain.Entities;
using System.Collections.Generic;

namespace ElitTournament.Domain.Helpers.Interfaces
{
	public interface IGrabbScoreHelper : IBaseHelper
	{
		List<League> Parse(IDocument document);
	}
}
