using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class PpomppuProcessor : Zeroboard4BaseProcessor
	{
		public override string WebSiteName => "뽐뿌";
		public override Uri BaseUrl => new Uri ( "http://www.ppomppu.co.kr" );
	}
}
