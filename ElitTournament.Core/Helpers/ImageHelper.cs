using System;
using System.IO;
using System.Text;
using CoreHtmlToImage;
using ElitTournament.Core.Helpers.Interfaces;

namespace ElitTournament.Core.Helpers
{
	public class ImageHelper : IImageHelper
	{
		private readonly string _path;
		private const string head = "<head><style>table{font-family: arial, sans-serif; border: 2px solid black;border-collapse:collapse; }td,th{text-align:left;padding:8px;width:20px;text-align:center;}tr:nth-child(even){background-color: #dddddd;}</style></head>";

		public ImageHelper()
		{
			_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		}
		
		private string GetHtml(string table)
		{
			return $"{head}{table}";
		}
		
		public void CreateImage(string table, string tableName)
		{
			string html = GetHtml(table);
			string fullPath = $"{_path}/Images/{tableName}.jpg";
			HtmlConverter converter = new HtmlConverter();
			byte[] bytes = converter.FromHtmlString(html);
			
			using (FileStream stream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite))
			{
				stream.Write(bytes);
				stream.Close();
			}
		}
	}
}