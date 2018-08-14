using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace URLFinder.Processors
{
	public abstract class Zeroboard4BaseProcessor : BaseProcessor
	{
		public virtual string ZeroboardRootDirectory => "zboard/";

		public override string ConvertUrl ( string url )
		{
			var idMatch = Regex.Match ( url, "(.*)id=([a-zA-Z0-9_]+)(.*)" );
			var noMatch = Regex.Match ( url, "(.*)no=([0-9]+)(.*)" );
			if ( ( idMatch != null && idMatch.Success )
				&& ( noMatch != null && noMatch.Success ) )
			{
				url = $"{BaseUrl}/{ZeroboardRootDirectory}view.php?id={idMatch.Groups [ 2 ].Value}&no={noMatch.Groups [ 2 ].Value}";
				return url;
			}
			return base.ConvertUrl ( url );
		}
	}
}
