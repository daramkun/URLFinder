using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using URLFinder.Finder;

namespace URLFinder.Processors
{
	public static class ProcessorFinder
	{
		static Dictionary<string, BaseProcessor> processors = new Dictionary<string, BaseProcessor> ();

		static ProcessorFinder ()
		{
			foreach ( var type in Assembly.GetExecutingAssembly ().GetTypes () )
			{
				if ( type.IsAbstract )
					continue;

				if ( !type.IsSubclassOf ( typeof ( BaseProcessor ) ) )
					continue;

				if ( type == typeof ( SimpleProcessor ) || type == typeof ( XpressEngineSimpleProcessor )
					|| type == typeof ( Zeroboard4SimpleProcessor ) )
					continue;

				BaseProcessor temp = Activator.CreateInstance ( type ) as BaseProcessor;
				string host = temp.BaseUrl.Host;
				if ( host.IndexOf ( "www." ) == 0 )
					host = host.Substring ( 5 );

				processors.Add ( host, temp );
			}

			foreach ( var processor in new BaseProcessor [] {
				new SimpleProcessor ( "트위터", new Uri ( "https://twitter.com" ) ),
				new SimpleProcessor ( "페이스북", new Uri ( "https://www.facebook.com" ) ),
				new SimpleProcessor ( "유튜브", new Uri ( "https://www.youtube.com" ) ),
				new SimpleProcessor ( "다음아고라", new Uri ( "http://agora.media.daum.net" ) ),
				new SimpleProcessor ( "수용소", new Uri ( "http://www.suyongso.com" ) ),
				new SimpleProcessor ( "나무라이브", new Uri ( "https://namu.live" ) ),
				new SimpleProcessor ( "에펨네이션", new Uri ( "http://www.fmnation.net" ) ),
				new SimpleProcessor ( "오르비", new Uri ( "https://orbi.kr" ) ),
				new SimpleProcessor ( "클리앙", new Uri ( "http://www.clien.net" ) ),
				new SimpleProcessor ( "와이고수", new Uri ( "https://www.ygosu.com" ) ),
				new SimpleProcessor ( "OP.GG", new Uri ( "http://op.gg" ) ),
				new SimpleProcessor ( "네이트판", new Uri ( "http://pann.nate.com" ) ),
				new SimpleProcessor ( "보배드림", new Uri ( "http://www.bobaedream.co.kr" ) ),
				new SimpleProcessor ( "게임조선", new Uri ( "http://www.gamechosun.co.kr" ) ),
				new XpressEngineSimpleProcessor ( "에펨코리아", new Uri ( "http://www.fmkorea.com" ) ),
				new XpressEngineSimpleProcessor ( "일간베스트저장소", new Uri ( "http://www.ilbe.com" ) ),
				new XpressEngineSimpleProcessor ( "개드립넷", new Uri ( "http://www.dogdrip.net" ) ),
				new XpressEngineSimpleProcessor ( "팟수넷", new Uri ( "http://potsu.net" ) ),
				new Zeroboard4SimpleProcessor ( "츄잉", new Uri ( "http://www.chuing.net" ) ),
				new Zeroboard4SimpleProcessor ( "뽐뿌", new Uri ( "http://www.ppomppu.co.kr" ) ),
			} )
			{
				string host = processor.BaseUrl.Host;
				if ( host.IndexOf ( "www." ) == 0 )
					host = host.Substring ( 5 );

				processors.Add ( host, processor );
			}
		}

		public static BaseProcessor FindProcessor ( string url, bool makeCustomProcessor = true )
		{
			foreach ( var processor in processors )
			{
				if ( url.IndexOf ( processor.Key ) >= 0 )
				{
					if ( processor.Value.IsItMe ( url ) )
						return processor.Value;
				}
			}

			if ( !makeCustomProcessor )
				return null;

			return ExcelIndexer.SharedExcelIndexer.GetGuessedProcessor ( url )
				?? new SimpleProcessor ( "", new Uri ( url.Substring ( 0, url.IndexOf ( '/', 9 ) ) ) );
		}
	}
}
