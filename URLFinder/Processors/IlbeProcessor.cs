using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class IlbeProcessor : XpressEngineBaseProcessor
	{
		public override string WebSiteName => "일간베스트저장소";
		public override Uri BaseUrl => new Uri ( "http://www.ilbe.com" );
	}
}
