using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class PotsuNetProcessor : XpressEngineBaseProcessor
	{
		public override string WebSiteName => "팟수넷";
		public override Uri BaseUrl => new Uri ( "http://potsu.net" );
	}
}
