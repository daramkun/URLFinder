using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class DCInsideProcessor : BaseProcessor
	{
		public override string WebSiteName => "디시인사이드";
		public override Uri BaseUrl => new Uri ( "http://www.dcinside.com" );

		public override string ConvertUrl ( string url )
		{
			if ( url.IndexOf ( "gall.dcinside.com" ) >= 0 )
			{
				var idMatch = Regex.Match ( url, "(.*)id=([a-zA-Z0-9_]+)(.*)" );
				var noMatch = Regex.Match ( url, "(.*)no=([0-9]+)(.*)" );
				if ( ( idMatch != null && idMatch.Success )
					&& ( noMatch != null && noMatch.Success ) )
				{
					url = $"http://gall.dcinside.com/{( url.IndexOf ( "/mgallery/" ) >= 0 ? "mgallery/" : "" )}board/view/?id={idMatch.Groups [ 2 ].Value}&no={noMatch.Groups [ 2 ].Value}";
					return url;
				}
			}
			return base.ConvertUrl ( url );
		}
	}
}
