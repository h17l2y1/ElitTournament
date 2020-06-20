using System.Threading.Tasks;
using Imgur.API.Models;

namespace ElitTournament.Core.Helpers.Interfaces
{
	public interface IImageHelper
	{
		Task<IImage> CreateImage(string html, string tableName);
	}
}