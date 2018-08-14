using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

				if ( type == typeof ( SimpleProcessor ) )
					continue;

				BaseProcessor temp = Activator.CreateInstance ( type ) as BaseProcessor;
				string host = temp.BaseUrl.Host;
				if ( host.IndexOf ( "www." ) == 0 )
					host = host.Substring ( 5 );

				processors.Add ( host, temp );
			}

			foreach ( var processor in new [] {
				new SimpleProcessor ( "트위터", new Uri ( "https://twitter.com" ) ),
				new SimpleProcessor ( "페이스북", new Uri ( "https://www.facebook.com" ) ),
				new SimpleProcessor ( "유튜브", new Uri ( "https://www.youtube.com" ) ),
				new SimpleProcessor ( "다음아고라", new Uri ( "http://agora.media.daum.net" ) ),
				new SimpleProcessor ( "수용소", new Uri ( "http://www.suyongso.com" ) ),
				new SimpleProcessor ( "나무라이브", new Uri ( "https://namu.live" ) ),
				new SimpleProcessor ( "에펨코리아", new Uri ( "http://www.fmkorea.com" ) ),
				new SimpleProcessor ( "오르비", new Uri ( "https://orbi.kr" ) ),
				new SimpleProcessor ( "인벤", new Uri ( "http://www.inven.co.kr" ) ),
				new SimpleProcessor ( "클리앙", new Uri ( "http://www.clien.net" ) ),
				new SimpleProcessor ( "와이고수", new Uri ( "https://www.ygosu.com" ) ),
				new SimpleProcessor ( "OP.GG", new Uri ( "http://op.gg" ) ),
				new SimpleProcessor ( "네이트판", new Uri ( "http://pann.nate.com" ) ),
				new SimpleProcessor ( "보배드림", new Uri ( "http://www.bobaedream.co.kr" ) ),
				new SimpleProcessor ( "게임조선", new Uri ( "http://www.gamechosun.co.kr" ) ),
			} )
			{
				string host = processor.BaseUrl.Host;
				if ( host.IndexOf ( "www." ) == 0 )
					host = host.Substring ( 5 );

				processors.Add ( host, processor );
			}
		}

		public static void SearchProcessorsFromExcel ( string findingPathes, string findingPatterns )
		{
			string [] pathes = findingPathes.Split ( '|' );
			string [] patterns = findingPatterns.Split ( '|' );

			foreach ( string path in pathes )
			{
				foreach ( string pattern in patterns )
				{
					//Parallel.ForEach ( Directory.GetFiles ( path, pattern, SearchOption.AllDirectories ), ( file ) =>
					foreach ( var file in Directory.GetFiles ( path, pattern, SearchOption.AllDirectories ) )
					{
						try
						{
							using ( OleDbConnection connection = new OleDbConnection (
								$"Provider=\"Microsoft.ACE.OLEDB.12.0\";Data Source=\"{file}\";Extended Properties=\"Excel 12.0;HDR=NO\";"
							) )
							{
								connection.Open ();

								string tableName = Regex.Replace ( connection.GetSchema ( "Tables" ).Rows [ 1 ] [ "TABLE_NAME" ] as string, "['\"]", "" );
								using ( OleDbCommand command = new OleDbCommand (
									$"SELECT * FROM [{tableName}I2:I9999,AF2:AF9999]",
									connection
								) )
								{
									using ( var reader = command.ExecuteReader () )
									{
										while ( reader.Read () )
										{
											string website = reader.GetString ( 0 );
											string baseUrl = reader.GetString ( 1 );

											Uri tempUrl = new Uri ( baseUrl );
											string host = tempUrl.Host;
											if ( host.IndexOf ( "www." ) == 0 )
												host = host.Substring ( 5 );

											if ( processors.ContainsKey ( host ) )
												continue;

											processors.Add ( host, new SimpleProcessor ( website, tempUrl ) );
										}
									}
								}

								connection.Close ();
							}
						}
						catch
						{

						}
						finally
						{

						}
					}// );
				}
			}
		}

		public static void StoreSearchedProcessors ()
		{

		}

		public static BaseProcessor FindProcessor ( string url )
		{
			foreach ( var processor in processors )
			{
				if ( url.IndexOf ( processor.Key ) >= 0 )
				{
					if ( processor.Value.IsItMe ( url ) )
						return processor.Value;
				}
			}

			return new SimpleProcessor ( "", new Uri ( url.Substring ( 0, url.IndexOf ( '/', 7 ) ) ) );
		}
	}
}
