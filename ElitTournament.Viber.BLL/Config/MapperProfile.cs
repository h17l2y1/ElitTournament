using ElitTournament.Core.Entities;
using ElitTournament.Viber.Core.Models;

namespace ElitTournament.Viber.BLL.Config
{
	public class MapperProfile : AutoMapper.Profile
	{
		public MapperProfile()
		{
			CreateMap<UserDetails, User>()
				.ForMember(from => from.ClientId, to => to.MapFrom(source => source.Id))
				.ForMember(from => from.IsViber, to => to.MapFrom(source => true));
		}
	}
}
