using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class RuliwebProcessor : BaseProcessor
	{
		public override string WebSiteName => "루리웹";
		public override Uri BaseUrl => new Uri ( "http://www.ruliweb.com" );

		public override string ConvertUrl ( string url )
		{
			if ( url.IndexOf ( "bbs.ruliweb.com" ) >= 0 )
			{
				//http://bbs.ruliweb.com/community/board/300147/read/30543716
				var urlMatch = Regex.Match ( url, "https?://bbs.ruliweb.com/([a-zA-Z0-9_]+)/board/([0-9]+)/read/([0-9]+)" );
				if ( urlMatch != null && urlMatch.Success )
				{
					url = $"http://bbs.ruliweb.com/{urlMatch.Groups [ 1 ].Value}/board/{urlMatch.Groups [ 2 ].Value}/read/{urlMatch.Groups [ 3 ].Value}";
					return url;
				}
			}
			return base.ConvertUrl ( url );
		}
	}
}
