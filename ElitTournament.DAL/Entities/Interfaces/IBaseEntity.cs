using System;

namespace ElitTournament.DAL.Entities.Interfaces
{
	public interface IBaseEntity
	{
		int Id { get; set; }
		DateTime CreationDate { get; set; }
	}
}
