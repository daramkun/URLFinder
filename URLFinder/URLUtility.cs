using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace URLFinder
{
	public static class URLUtility
	{
		public static string Compress ( string url )
		{
			url = url.Trim ();

			// 일간베스트저장소 및 개드립
			if ( url.IndexOf ( "ilbe.com" ) >= 0 || url.IndexOf ( "dogdrip.net" ) >= 0 )
			{
				if ( url.IndexOf ( "document_srl=" ) >= 0 )
				{
					var match = Regex.Match ( url, "(.*)document_srl=([0-9]+)(.*)" );
					if ( match != null && match.Success )
					{
						url = $"http://{( url.IndexOf ( "ilbe.com" ) >= 0 ? "www.ilbe.com" : "www.dogdrip.net" )}/{match.Groups [ 2 ].Value}";
						return url;
					}
				}
			}
			// 네이버 지식인
			else if ( url.IndexOf ( "kin.naver.com" ) >= 0 )
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
			// 디시인사이드
			else if ( url.IndexOf ( "gall.dcinside.com" ) >= 0 )
			{
				var idMatch = Regex.Match ( url, "(.*)id=([a-zA-Z0-9_]+)(.*)" );
				var noMatch = Regex.Match ( url, "(.*)no=([0-9]+)(.*)" );
				if ( ( idMatch != null && idMatch.Success )
					&& ( noMatch != null && noMatch.Success ) )
				{
					url = $"http://gall.dcinside.com/{( url.IndexOf ( "/mgallery/" ) >= 0 ? "mgallery/" : "" )}board/view/?id={idMatch.Groups [ 2 ].Value}&no={noMatch.Groups [ 2 ].Value}";
					return url;
				}
			}
			// 루리웹
			else if ( url.IndexOf ( "bbs.ruliweb.com" ) >= 0 )
			{
				//http://bbs.ruliweb.com/community/board/300147/read/30543716
				var urlMatch = Regex.Match ( url, "https?://bbs.ruliweb.com/([a-zA-Z0-9_]+)/board/([0-9]+)/read/([0-9]+)" );
				if ( urlMatch != null && urlMatch.Success )
				{
					url = $"http://bbs.ruliweb.com/{urlMatch.Groups [ 1 ].Value}/board/{urlMatch.Groups [ 2 ].Value}/read/{urlMatch.Groups [ 3 ].Value}";
					return url;
				}
			}
			// 뽐뿌
			else if ( url.IndexOf ( "ppomppu.co.kr" ) >= 0 )
			{
				var idMatch = Regex.Match ( url, "(.*)id=([a-zA-Z0-9_]+)(.*)" );
				var noMatch = Regex.Match ( url, "(.*)no=([0-9]+)(.*)" );
				if ( ( idMatch != null && idMatch.Success )
					&& ( noMatch != null && noMatch.Success ) )
				{
					url = $"http://www.ppomppu.co.kr/zboard/view.php?id={idMatch.Groups [ 2 ].Value}&no={noMatch.Groups [ 2 ].Value}";
					return url;
				}
			}
			// 오늘의 유머
			else if ( url.IndexOf ( "todayhumor.co.kr" ) >= 0 )
			{
				var tableMatch = Regex.Match ( url, "(.*)table=([a-zA-Z0-9_]+)(.*)" );
				var noMatch = Regex.Match ( url, "(.*)no=([0-9]+)(.*)" );
				if ( ( tableMatch != null && tableMatch.Success )
					&& ( noMatch != null && noMatch.Success ) )
				{
					url = $"http://www.todayhumor.co.kr/board/view.php?table={tableMatch.Groups [ 2 ].Value}&no={noMatch.Groups [ 2 ].Value}";
					return url;
				}
			}
			// 다음 팁 및 다음 카페
			else if ( url.IndexOf ( "tip.daum.net" ) >= 0 || url.IndexOf ( "cafe.daum.net" ) >= 0 )
			{
				int startArgument = url.IndexOf ( "?" );
				if ( startArgument >= 0 )
				{
					return url.Substring ( 0, startArgument );
				}
			}
			// 네이버 블로그
			else if ( url.IndexOf ( "blog.naver.com" ) >= 0 )
			{
				var idMatch = Regex.Match ( url, "(.*)blogId=([a-zA-Z0-9_]+)(.*)" );
				var noMatch = Regex.Match ( url, "(.*)logNo=([0-9]+)(.*)" );
				if ( ( idMatch != null && idMatch.Success )
					&& ( noMatch != null && noMatch.Success ) )
				{
					url = $"http://blog.naver.com/{idMatch.Groups [ 2 ].Value}/{noMatch.Groups [ 2 ].Value}";
					return url;
				}
			}
			// 웃긴대학
			else if ( url.IndexOf ( "web.humoruniv.com" ) >= 0 )
			{
				var tableMatch = Regex.Match ( url, "(.*)table=([a-zA-Z0-9_]+)(.*)" );
				var noMatch = Regex.Match ( url, "(.*)number=([0-9]+)(.*)" );
				if ( ( tableMatch != null && tableMatch.Success )
					&& ( noMatch != null && noMatch.Success ) )
				{
					url = $"http://web.humoruniv.com/board/humor/read.html?table={tableMatch.Groups [ 2 ].Value}&number={noMatch.Groups [ 2 ].Value}";
					return url;
				}
			}

			url = Regex.Replace ( url, "[a-zA-Z0-9_\\-]+=&", "&" );
			url = Regex.Replace ( url, "[a-zA-Z0-9_\\-]+=$", "" );
			url = url.Replace ( "&&", "&" );
			var commentStart = url.IndexOf ( '#' );
			if ( commentStart >= 0 )
				url = url.Substring ( 0, commentStart );

			return url;
		}

		public static string GetManagedSiteUrl ( string url )
		{
			if ( url.IndexOf ( "ilbe.com" ) >= 0 )
				return "http://www.ilbe.com";
			else if ( url.IndexOf ( "dcinside.com" ) >= 0 )
				return "http://www.dcinside.com";
			else if ( url.IndexOf ( "twitter.com" ) >= 0 )
				return "https://twitter.com";
			else if ( url.IndexOf ( "ruliweb.com" ) >= 0 )
				return "http://www.ruliweb.com";
			else if ( url.IndexOf ( "kin.naver.com" ) >= 0 )
				return "http://kin.naver.com";
			else if ( url.IndexOf ( "cafe.naver.com" ) >= 0
				|| url.IndexOf ( "blog.naver.com" ) >= 0
				|| url.IndexOf ( "cafe.daum.net" ) >= 0 )
			{
				return url.Substring ( 0, url.IndexOf ( '/', 23 ) );
			}
			return url.Substring ( 0, url.IndexOf ( '/', 7 ) );
		}

		private static Dictionary<string, string> webSiteNames = new Dictionary<string, string> {
			{ "ilbe.com", "일간베스트저장소" },
			{ "dcinside.com", "디시인사이드" },
			{ "twitter.com", "트위터" },
			{ "facebook.com", "페이스북" },
			{ "youtube.com", "유튜브" },
			{ "ruliweb.com", "루리웹" },
			{ "kin.naver.com", "네이버지식인" },
			{ "cafe.naver.com", "네이버카페(" },
			{ "blog.naver.com", "네이버블로그(" },
			{ "cafe.daum.net", "다음카페(" },
			{ "tip.daum.net", "다음팁" },
			{ "agora.media.daum.net", "다음아고라" },
			{ "todayhumor.co.kr", "오늘의유머" },
			{ "ppomppu.co.kr", "뽐뿌" },
			{ "dogdrip.net", "개드립" },
			{ "web.humoruniv.com", "웃긴대학" },
			{ "suyongso.com", "수용소" },
			{ "chuing.net", "츄잉" },
			{ "fmkorea.com", "에펨코리아" },
			{ "potsu.net", "팟수넷" },
			{ "orbi.kr", "오르비" },
			{ "inven.co.kr", "인벤" },
			{ "goeyu.com", "바람놀이터" },
			{ "gamecodi.com", "게임코디" },
			{ "nicegame.tv", "나이스게임TV" },
			{ "pann.nate.com", "네이트판" },
			{ "gamechosun.co.kr", "게임조선" },
			{ "fc2live.co.kr", "FC2라이브코리아" },
			{ "op.gg", "OP.GG" },
			{ "metalgall.net", "메탈갤" },
			{ "battlepage.com", "배틀페이지" },
			{ "hungryapp.co.kr", "헝그리앱" },
			{ "ygosu.com", "와이고수" },
			{ "quasarzone.co.kr", "퀘이사존" },
			{ "theqoo.net", "더쿠" },
			{ "bobaedream.co.kr", "보배드림" },
			{ "seeko.kr", "씨코" },
			{ "named.com", "네임드" },
			{ "slrclub.com", "SLRCLUB" },
			{ "hellkorea.com", "헬조선" },
			{ "playxp.com", "PlayXP" },
			{ "jotsu.net", "졷수넷" },
			{ "etobang.com", "이토방" },
			{ "pandora.tv", "판도라TV" },
			{ "tistory.com", "티스토리(" },
			{ "ottl.com", "OTTL" },
			{ "gasengi.com", "가생이닷컴" },
			{ "clien.net", "클리앙" },
		};

		public static string GetWebSiteName ( string url )
		{
			foreach ( var names in webSiteNames )
			{
				if ( url.IndexOf ( names.Key ) >= 0 )
				{
					if ( names.Value [ names.Value.Length - 1 ] == '(' )
					{
						switch ( names.Value )
						{
							case "네이버블로그(":
							case "네이버카페(":
							case "다음카페(":
								return names.Value + url.Substring ( url.IndexOf ( '/', 9 ) + 1 ) + ")";

							case "티스토리(":
								return names.Value + url.Substring ( url.IndexOf ( "http://" ) + "http://".Length,
									url.IndexOf ( ".tistory.com", 0 ) - "http://".Length ) + ")";

							default: return names.Value;
						}
					}
					return names.Value;
				}
			}
			return "";
		}
	}
}
