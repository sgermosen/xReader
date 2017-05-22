using System;

namespace XamarinDO.News.Models
{
	public class RssFeedItem
	{
		public string Title { get; private set; }
		public string Description { get; private set; }
		public Uri Link { get; private set; }
		public string Creator { get; private set; }
		public string CreatorEMail { get; private set; }
		public DateTime Date { get; private set; }

		public RssFeedItem(string title, string description, Uri link, string creator, string creatorEMail, DateTime date)
		{
			Title = title;
			Description = description;
			Link = link;
			Creator = creator;
			CreatorEMail = creatorEMail;
			Date = date;
		}
	}
}