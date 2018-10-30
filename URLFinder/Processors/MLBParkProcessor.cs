using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class MLBParkProcessor : BaseProcessor
	{
		public override string WebSiteName => "엠엘비파크";

		public override Uri BaseUrl => new Uri ( "http://mlbpark.donga.com" );

		public override string ConvertUrl ( string url )
		{
			if ( url.IndexOf ( "mlbpark.donga.com/mp/b.php" ) >= 0 )
			{
				var tableMatch = Regex.Match ( url, "(.*)b=([a-zA-Z0-9_]+)(.*)" );
				var noMatch = Regex.Match ( url, "(.*)id=([0-9]+)(.*)" );
				if ( ( tableMatch != null && tableMatch.Success )
					&& ( noMatch != null && noMatch.Success ) )
				{
					url = $"http://mlbpark.donga.com/mp/b.php?b={tableMatch.Groups [ 2 ].Value}&id={noMatch.Groups [ 2 ].Value}";
					return url;
				}
			}
			return base.ConvertUrl ( url );
		}
	}
}
