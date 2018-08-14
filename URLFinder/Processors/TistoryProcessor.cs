using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class TistoryProcessor : BaseProcessor
	{
		public override string WebSiteName => "티스토리";
		public override Uri BaseUrl => new Uri ( "http://www.tistory.com" );
		public override bool HasDetailWebSiteName => true;

		public override string GetDetailWebSiteName ( string url )
		{
			return $"{WebSiteName}({url.Substring ( url.IndexOf ( "http://" ) + "http://".Length, url.IndexOf ( ".tistory.com", 0 ) - "http://".Length )})";
		}
	}
}
