using AngleSharp.Dom;
using System.Threading.Tasks;

namespace ElitTournament.Domain.Providers.Interfaces
{
	public interface IBaseProvider
	{
		Task<IDocument> GetPage(string url);
	}
}
