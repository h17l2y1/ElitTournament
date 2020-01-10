using System.Collections.Generic;
using AngleSharp.Dom;
using ElitTournament.Domain.Entities;
using ElitTournament.Domain.Helpers.Interfaces;

namespace ElitTournament.Domain.Helpers
{
	public class ScoreHelper : IScoreHelper
	{
		public string GetData(IElement iElement, string selector)
		{
			throw new System.NotImplementedException();
		}

		public string GetData(IElement iElement, string selector, string attribute)
		{
			throw new System.NotImplementedException();
		}

		public List<Score> Parse(IDocument document)
		{
			throw new System.NotImplementedException();
		}
	}
}
