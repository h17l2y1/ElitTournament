﻿using AngleSharp.Dom;
using System.Threading.Tasks;

namespace ElitTournament.Core.Providers.Interfaces
{
	public interface IBaseGrabberProvider
	{
		Task<IDocument> GetPage(string url);
	}
}
