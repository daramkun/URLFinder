using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class NaverCafeProcessor : BaseProcessor
	{
		public override string WebSiteName => "네이버카페";
		public override Uri BaseUrl => new Uri ( "https://cafe.naver.com" );
		public override bool HasDetailWebSiteName => true;

		public override string ConvertUrl ( string url )
		{
			if ( url.IndexOf ( "cafe.naver.com" ) >= 0 )
			{
				if ( url.IndexOf ( "articleid=" ) >= 0 )
				{
					var cafeMatch = Regex.Match ( url, "cafe.naver.com/([a-zA-Z0-9_]+)(.*)" );
					var noMatch = Regex.Match ( url, "(.*)articleid=([0-9]+)(.*)" );
					if ( ( cafeMatch != null && cafeMatch.Success )
						&& ( noMatch != null && noMatch.Success ) )
					{
						url = $"http://cafe.naver.com/{cafeMatch.Groups [ 2 ].Value}/{noMatch.Groups [ 2 ].Value}";
						return url;
					}
				}
				else
					return Regex.Replace ( url, "https?://cafe.naver.com/[a-zA-Z0-9_]+/[0-9]+(.*)", "" );
			}
			return base.ConvertUrl ( url );
		}

		public override Uri GetDetailBaseUrl ( string url )
		{
			return new Uri ( url.Substring ( 0, url.IndexOf ( '/', 23 ) ) );
		}

		public override string GetDetailWebSiteName ( string url )
		{
			url = GetDetailBaseUrl ( url ).AbsoluteUri;
			return $"{WebSiteName}({url.Substring ( url.IndexOf ( '/', 9 ) + 1 )})";
		}
	}
}
