using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public class NaverKINProcessor : BaseProcessor
	{
		public override string WebSiteName => "네이버지식인";
		public override Uri BaseUrl => new Uri ( "http://kin.naver.com" );

		public override string ConvertUrl ( string url )
		{
			if ( url.IndexOf ( "kin.naver.com" ) >= 0 )
			{
				var dirIdMatch = Regex.Match ( url, "(.*)dirId=([0-9]+)(.*)" );
				var docIdMatch = Regex.Match ( url, "(.*)docId=([0-9]+)(.*)" );
				if ( ( dirIdMatch != null && dirIdMatch.Success )
					&& ( docIdMatch != null && docIdMatch.Success ) )
				{
					url = $"http://kin.naver.com/qna/detail.nhn?dirId={dirIdMatch.Groups [ 2 ].Value}&docId={docIdMatch.Groups [ 2 ].Value}";
					return url;
				}
			}
			return base.ConvertUrl ( url );
		}
	}
}
