using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using XamarinDO.News.Models;

namespace XamarinDO.News.Rss
{
	public class RssClient
	{
		public static async Task<IEnumerable<RssFeedItem>> Load(Uri uri)
		{
			using (var client = new HttpClient())
			{
				var rssFeed = await client.GetStringAsync(uri);

				var stream = new StringReader(rssFeed);

				var reader = XmlReader.Create(stream);
				var document = XDocument.Load(reader);

				return document.Root.Descendants()
					.Where(x => x.Name.LocalName == "item")
					.Select(x => ParseItem(x));
			}
		}

		private static RssFeedItem ParseItem(XElement item)
		{
			var title = item.Descendants().Single(x => x.Name.LocalName == "title").Value;
			var linkString = item.Descendants().Single(x => x.Name.LocalName == "link").Value;
			var description = item.Descendants().Single(x => x.Name.LocalName == "description").Value;
			var creatorString = item.Descendants().Single(x => x.Name.LocalName == "creator").Value;
			var dateString = item.Descendants().Single(x => x.Name.LocalName == "date").Value;

			var link = new Uri(linkString);

			var creatorSplit = creatorString.Split(new[] {","}, 2, StringSplitOptions.RemoveEmptyEntries);
			var creator = creatorSplit.First().Trim();
			var creatorEMail = creatorSplit.Skip(1).Single().Trim();

			var date = DateTime.Parse(dateString);

			return new RssFeedItem(title, description, link, creator, creatorEMail, date);
		}
	}
}
