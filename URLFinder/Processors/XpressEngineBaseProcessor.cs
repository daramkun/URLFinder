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
					url = $"{BaseUrl.Scheme}://{BaseUrl.Host}/{match.Groups [ 2 ].Value}";
					return url;
				}
			}
			else
			{
				var match = Regex.Match ( url, $"https?://{BaseUrl.Host}/[a-zA-Z0-9_]+/([0-9]+)(.*)" );
				if ( match != null && match.Success )
				{
					url = $"{BaseUrl.Scheme}://{BaseUrl.Host}/{match.Groups [ 1 ].Value}";
					return url;
				}
			}
			return base.ConvertUrl ( url );
		}
	}

	public class XpressEngineSimpleProcessor : XpressEngineBaseProcessor
	{
		readonly string webSiteName;
		readonly Uri baseUrl;

		public override string WebSiteName => webSiteName;
		public override Uri BaseUrl => baseUrl;

		public XpressEngineSimpleProcessor ( string webSiteName, Uri baseUrl )
		{
			this.webSiteName = webSiteName;
			this.baseUrl = baseUrl;
		}
	}
}
