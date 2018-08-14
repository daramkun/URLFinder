using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class DaumTipProcessor : BaseProcessor
	{
		public override string WebSiteName => "다음팁";
		public override Uri BaseUrl => new Uri ( "http://tip.daum.net" );

		public override string ConvertUrl ( string url )
		{
			if ( url.IndexOf ( "tip.daum.net" ) >= 0 )
			{
				int startArgument = url.IndexOf ( "?" );
				if ( startArgument >= 0 )
					url = url.Substring ( 0, startArgument );
			}
			return base.ConvertUrl ( url );
		}
	}
}
