using AutoMapper;
using Telegram.Bot.Types;

namespace ElitTournament.Telegram.BLL.Config
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<User, DAL.Entities.User>()
				.ForMember(to => to.Id, from => from.Ignore())
				.ForMember(to => to.ClientId, from => from.MapFrom(source => source.Id.ToString()))
				.ForMember(to => to.Name, from => from.MapFrom(source => $"{source.FirstName} {source.LastName}"))
				.ForMember(to => to.Language, from => from.MapFrom(source => source.LanguageCode))
				.ForMember(to => to.IsTelegram, from => from.MapFrom(source => true));
		}
	}
}
