using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public abstract class XpressEngineBaseProcessor : BaseProcessor
	{
		public override string ConvertUrl ( string url )
		{
			if ( url.IndexOf ( "document_srl=" ) >= 0 )
			{
				var match = Regex.Match ( url, "(.*)document_srl=([0-9]+)(.*)" );
				if ( match != null && match.Success )
				{
					url = $"http://{BaseUrl.Host}/{match.Groups [ 2 ].Value}";
					return url;
				}
			}
			return base.ConvertUrl ( url );
		}
	}
}
