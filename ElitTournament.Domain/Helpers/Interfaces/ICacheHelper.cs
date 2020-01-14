using ElitTournament.Domain.Entities;
using System.Collections.Generic;

namespace ElitTournament.Domain.Helpers.Interfaces
{
	public interface ICacheHelper
	{
		void Clear();

		void SaveSchedule(List<Schedule> data);

		List<Schedule> Get();

		void Update(List<Schedule> data);
	}
}
