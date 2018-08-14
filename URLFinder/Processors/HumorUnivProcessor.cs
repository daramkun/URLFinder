using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class HumorUnivProcessor : BaseProcessor
	{
		public override string WebSiteName => "웃긴대학";
		public override Uri BaseUrl => new Uri ( "http://web.humoruniv.com" );

		public override string ConvertUrl ( string url )
		{
			if ( url.IndexOf ( "web.humoruniv.com" ) >= 0 )
			{
				var tableMatch = Regex.Match ( url, "(.*)table=([a-zA-Z0-9_]+)(.*)" );
				var noMatch = Regex.Match ( url, "(.*)number=([0-9]+)(.*)" );
				if ( ( tableMatch != null && tableMatch.Success )
					&& ( noMatch != null && noMatch.Success ) )
				{
					url = $"http://web.humoruniv.com/board/humor/read.html?table={tableMatch.Groups [ 2 ].Value}&number={noMatch.Groups [ 2 ].Value}";
					return url;
				}
			}
			return base.ConvertUrl ( url );
		}
	}
}
