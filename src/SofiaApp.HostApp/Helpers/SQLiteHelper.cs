using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using SofiaApp.Host.Entities;

namespace SofiaApp.HostApp
{
	public static class SQLiteHelper
	{
		const string DataBaseName = "sofia.db";

		public static List<FirePoint> GetFirePoints (this SqliteDataReader sender)
		{
			var firePoints = new List<FirePoint> ();
			while (sender.Read ()) {
				firePoints.Add (new FirePoint () { 
					ID = sender.GetString (0), 
					Title = sender.GetString (1),
					Description = sender.GetString (2),
					Point = new GeoPoint (sender.GetFloat (3), sender.GetFloat (4))
				});
			}
			return firePoints;
		}

		public static List<NasaFirePoint> GetNasaFirePoints (this SqliteDataReader sender)
		{
			var firePoints = new List<NasaFirePoint> ();
			while (sender.Read ()) {
				var where = new WhereAreFiresResponse () {
					lat = sender.GetFloat (3), lon = sender.GetFloat (4)
				};
				firePoints.Add (new NasaFirePoint (where) {
					ID = sender.GetString (0),
					Title = sender.GetString (1),
					Description = sender.GetString (2), 
				});
			}
			return firePoints;
		}

		public static void Select (Action<SqliteDataReader> action, string sql)
		{
			var stringBuilder = new SqliteConnectionStringBuilder { DataSource = DataBaseName };
			using (var connection = new SqliteConnection (stringBuilder.ToString ())) {
				connection.Open ();

				var selectCommand = connection.CreateCommand ();
				selectCommand.CommandText = sql;
				using (var reader = selectCommand.ExecuteReader ()) {
					while (reader.Read ()) {
						action?.Invoke (reader);
					}
				}
			}
		}

		public static void Update (string query, List<(string parameter, string value)> collection) 
		{
			var stringBuilder = new SqliteConnectionStringBuilder { DataSource = DataBaseName };
			using (var connection = new SqliteConnection (stringBuilder.ToString ())) {
				var cmd = connection.CreateCommand ();
				cmd.CommandText = query;
				foreach (var item in collection) {
					cmd.Parameters.AddWithValue (item.parameter, item.value);
				}
				connection.Open ();
				cmd.ExecuteNonQuery ();
			}
		}
	}
}
