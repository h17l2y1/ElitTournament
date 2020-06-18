﻿using AngleSharp.Dom;
using ElitTournament.DAL.Entities;
using System.Collections.Generic;

namespace ElitTournament.Core.Helpers.Interfaces
{
	public interface IGrabberHelper
	{
		List<Schedule> ParseSchedule(IDocument document);

		IEnumerable<string> GetLinks(IDocument document);

		List<League> ParseLeagues(IDocument document);

		List<League> ParseTables(IDocument document);
	}
}
