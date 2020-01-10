﻿using AngleSharp.Dom;
using ElitTournament.Domain.Entities;
using System.Collections.Generic;

namespace ElitTournament.Domain.Helpers.Interfaces
{
	public interface IScheduleHelper : IBaseHelper
	{
		List<Schedule> Parse(IDocument document);
	}
}
