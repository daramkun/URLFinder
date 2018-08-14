using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class GaedripProcessor : XpressEngineBaseProcessor
	{
		public override string WebSiteName => "개드립";
		public override Uri BaseUrl => new Uri ( "http://www.dogdrip.net" );
	}
}
