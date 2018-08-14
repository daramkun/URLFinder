using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class NaverBlogProcessor : BaseProcessor
	{
		public override string WebSiteName => "네이버블로그";
		public override Uri BaseUrl => new Uri( "https://blog.naver.com" );
		public override bool HasDetailWebSiteName => true;

		public override string ConvertUrl ( string url )
		{
			if ( url.IndexOf ( "blog.naver.com" ) >= 0 )
			{
				var idMatch = Regex.Match ( url, "(.*)blogId=([a-zA-Z0-9_]+)(.*)" );
				var noMatch = Regex.Match ( url, "(.*)logNo=([0-9]+)(.*)" );
				if ( ( idMatch != null && idMatch.Success )
					&& ( noMatch != null && noMatch.Success ) )
				{
					url = $"http://blog.naver.com/{idMatch.Groups [ 2 ].Value}/{noMatch.Groups [ 2 ].Value}";
					return url;
				}
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
