using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class TodayHumorProcessor : BaseProcessor
	{
		public override string WebSiteName => "오늘의유머";
		public override Uri BaseUrl => new Uri ( "http://www.todayhumor.co.kr" );

		public override string ConvertUrl ( string url )
		{
			if ( url.IndexOf ( "m.todayhumor.co.kr" ) >= 0 )
				url = url.Replace ( "m.todayhumor.co.kr/view.php", "www.todayhumor.co.kr/board/view.php" );

			if ( url.IndexOf ( "todayhumor.co.kr" ) >= 0 )
			{
				var tableMatch = Regex.Match ( url, "(.*)table=([a-zA-Z0-9_]+)(.*)" );
				var noMatch = Regex.Match ( url, "(.*)no=([0-9]+)(.*)" );
				if ( ( tableMatch != null && tableMatch.Success )
					&& ( noMatch != null && noMatch.Success ) )
				{
					url = $"http://www.todayhumor.co.kr/board/view.php?table={tableMatch.Groups [ 2 ].Value}&no={noMatch.Groups [ 2 ].Value}";
					return url;
				}
			}
			return base.ConvertUrl ( url );
		}
	}
}
