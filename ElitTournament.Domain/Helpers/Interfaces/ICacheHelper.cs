using System;

namespace ElitTournament.Domain.Helpers.Interfaces
{
	public interface ICacheHelper
	{
		void Clear();

		void Save(Object data);

		Object Get();
	}
}
