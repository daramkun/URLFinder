using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class InvenProcessor : BaseProcessor
	{
		public override string WebSiteName => "인벤";
		public override Uri BaseUrl => new Uri ( "http://www.inven.co.kr" );

		public override string ConvertUrl ( string url )
		{
			if ( url.IndexOf ( "m.inven.co.kr" ) >= 0 )
			{
				var request = WebRequest.CreateHttp ( url.Replace ( "m.", "www." ) + "&vtype=pc" );
				request.AllowAutoRedirect = false;
				using ( var response = request.GetResponse () )
					url = response.Headers [ "Location" ];
			}

			int q = url.IndexOf ( "?" );
			if ( q > 0 )
				url = url.Substring ( 0, q );

			return base.ConvertUrl ( url );
		}
	}
}
