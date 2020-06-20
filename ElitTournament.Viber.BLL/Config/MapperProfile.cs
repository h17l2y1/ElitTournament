using AutoMapper;
using ElitTournament.DAL.Entities;
using ElitTournament.Viber.Core.Models;

namespace ElitTournament.Viber.BLL.Config
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<UserDetails, User>()
				.ForMember(to => to.Id, from => from.Ignore())
				.ForMember(to => to.ClientId, from => from.MapFrom(source => source.Id))
				.ForMember(to => to.IsViber, from => from.MapFrom(source => true));
		}
	}
}
