using System;
using System.Linq;
using MvvmCross.Plugins.Sqlite;
using SQLite.Net;
using SQLite.Net.Async;
using ThisRoofN.Models;
using System.Collections.Generic;
using MvvmCross.Platform;

namespace ThisRoofN
{
	public class TRDatabase
	{
		private const string DBName = "thisroof_database.sqlite";

		private static TRDatabase _instance = null;

		private SQLiteConnection mSqliteConnection;


		public TRDatabase ()
		{
			IMvxSqliteConnectionFactory connectionFactory = Mvx.Resolve<IMvxSqliteConnectionFactory> ();
			mSqliteConnection = connectionFactory.GetConnection (DBName);
			mSqliteConnection.CreateTable<TRUserSearchProperty> ();
			mSqliteConnection.CreateTable<TRLikedProperty> ();
		}

		public static TRDatabase Instance {
			get {
				if (_instance == null) {
					_instance = new TRDatabase ();
				}

				return _instance;
			}
		}

		public T GetItem<T> (int id) where T : TREntityBase
		{
			return mSqliteConnection.Table<T> ().FirstOrDefault (x => x.ID == id);
		}

		public IEnumerable<T> GetItems<T> () where T : TREntityBase
		{
			return (from item in mSqliteConnection.Table<T> ()
			        select item).ToList ();
		}

		public int SaveItem<T> (T item) where T : TREntityBase
		{
			if (item.ID != 0) {
				mSqliteConnection.Update (item);
				return item.ID;
			} else {
				return mSqliteConnection.Insert (item);
			}
		}

		public void SaveItems<T> (IEnumerable<T> items) where T : TREntityBase
		{
			mSqliteConnection.BeginTransaction ();

			foreach (T item in items) {
				SaveItem<T> (item);
			}

			mSqliteConnection.Commit ();
		}

		public int DeleteItem<T> (int id) where T : TREntityBase
		{
			return mSqliteConnection.Delete<T> (id);
		}

		public int DeleteItem<T> (T item) where T : TREntityBase
		{
			return mSqliteConnection.Delete<T> (item.ID);
		}

		public void ClearTable<T> () where T : TREntityBase
		{
			mSqliteConnection.Execute (string.Format ("delete from \"{0}\"", typeof(T).Name));
		}

		// helper for checking if database has been populated
		public int CountTable<T> () where T : TREntityBase
		{
			string sql = string.Format ("select count (*) from \"{0}\"", typeof(T).Name);
			var c = mSqliteConnection.CreateCommand (sql, new object[0]);
			return c.ExecuteScalar<int> ();
		}
	}
}

