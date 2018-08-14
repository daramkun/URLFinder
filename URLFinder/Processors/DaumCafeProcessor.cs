using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class DaumCafeProcessor : BaseProcessor
	{
		public override string WebSiteName => "다음카페";
		public override Uri BaseUrl => new Uri ( "http://cafe.daum.net" );
		public override bool HasDetailWebSiteName => true;

		public override string ConvertUrl ( string url )
		{
			if ( url.IndexOf ( "cafe.daum.net" ) >= 0 )
			{
				int startArgument = url.IndexOf ( "?" );
				if ( startArgument >= 0 )
					url = url.Substring ( 0, startArgument );
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
