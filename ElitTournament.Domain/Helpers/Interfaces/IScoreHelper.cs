using AngleSharp.Dom;
using ElitTournament.Domain.Entities;
using System.Collections.Generic;

namespace ElitTournament.Domain.Helpers.Interfaces
{
	public interface IScoreHelper : IBaseHelper
	{
		List<Score> Parse(IDocument document);
	}
}
