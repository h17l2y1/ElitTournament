using System.Threading.Tasks;
using CoreHtmlToImage;
using ElitTournament.Core.Helpers.Interfaces;
using Imgur.API.Authentication;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;

namespace ElitTournament.Core.Helpers
{
	public class ImageHelper : IImageHelper
	{
		private const string HEAD = "<head><style>table{font-family: arial, sans-serif; border: 2px solid black;border-collapse:collapse; }td,th{text-align:left;padding:8px;width:20px;text-align:center;}tr:nth-child(even){background-color: #dddddd;}</style></head>";
		private readonly IImgurClient _imgurClient;
		
		public ImageHelper(IImgurClient imgurClient)
		{
			_imgurClient = imgurClient;
		}
		
		private string AddStylesToTable(string table)
		{
			return $"{HEAD}{table}";
		}
		
		public async Task<IImage> CreateImage(string table, string tableName)
		{
			string html = AddStylesToTable(table);
			HtmlConverter converter = new HtmlConverter();
			byte[] bytes = converter.FromHtmlString(html);
			IImage image = await UploadImageBinary(bytes);
			return image;
		}

		private async Task<IImage> UploadImageBinary(byte[] bytes)
		{
			ImageEndpoint endpoint = new ImageEndpoint(_imgurClient);
			IImage image = await endpoint.UploadImageBinaryAsync(bytes);
			return image;
		}
		
	}
}