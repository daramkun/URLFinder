using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class ChuingProcessor : Zeroboard4BaseProcessor
	{
		public override string WebSiteName => "츄잉";
		public override Uri BaseUrl => new Uri ( "http://www.chuing.net" );
	}
}
