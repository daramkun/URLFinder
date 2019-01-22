using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public abstract class BaseProcessor
	{
		public abstract string WebSiteName { get; }
		public abstract Uri BaseUrl { get; }
		public virtual bool HasDetailWebSiteName => false;

		public virtual bool IsItMe ( string url )
		{
			if ( url.Contains ( BaseUrl.Host ) )
				return true;
			if ( BaseUrl.Host.IndexOf ( "www." ) == 0
				&& url.Contains ( BaseUrl.Host.Substring ( 5 ) ) )
				return true;
			return false;
		}

		public virtual string ConvertUrl ( string url )
		{
			url = Regex.Replace ( url, "[a-zA-Z0-9_\\-]+=&", "&" );
			url = Regex.Replace ( url, "[a-zA-Z0-9_\\-]+=$", "" );
			while ( url.IndexOf ( "&&" ) >= 0 )
				url = url.Replace ( "&&", "&" );
			var commentStart = url.IndexOf ( '#' );
			if ( commentStart >= 0 )
				url = url.Substring ( 0, commentStart );
			if ( url [ url.Length - 1 ] == '?' )
				url = url.Substring ( 0, url.Length - 1 );
			if ( url [ url.Length - 1 ] == '/' )
				url = url.Substring ( 0, url.Length - 1 );

			return url;
		}

		public virtual Uri GetDetailBaseUrl ( string url )
		{
			return BaseUrl;
		}

		public virtual string GetDetailWebSiteName ( string url )
		{
			return WebSiteName;
		}
	}

	public class SimpleProcessor : BaseProcessor
	{
		readonly string _webSiteName;
		readonly Uri _baseUrl;

		public override string WebSiteName => _webSiteName;
		public override Uri BaseUrl => _baseUrl;

		public SimpleProcessor ( string webSiteName, Uri baseUrl )
		{
			_webSiteName = webSiteName;
			_baseUrl = baseUrl;
		}
	}
}
