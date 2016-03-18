using System;
using MvvmCross.Plugins.Sqlite;
using SQLite.Net;
using SQLite.Net.Async;
using ThisRoofN.Models;
using System.Collections.Generic;
using MvvmCross.Platform;
using ThisRoofN.Models.App;
using ThisRoofN.Models.Service;
using System.Linq;
using ThisRoofN.Database.Entities;

namespace ThisRoofN.Database
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
			mSqliteConnection.CreateTable<SearchFilters> ();
			mSqliteConnection.CreateTable<TREntityLikes> ();
		}

		public static TRDatabase Instance {
			get {
				if (_instance == null) {
					_instance = new TRDatabase ();
				}

				return _instance;
			}
		}

		#region SearchFilter Methods
		public int SaveItem(SearchFilters filter) {
			if (filter.ID != 0) {
				filter.RegistrationDate = DateTime.Now;
				mSqliteConnection.Update (filter);
				return filter.ID;
			} else {
				return mSqliteConnection.Insert (filter);
			}
		}

		public SearchFilters GetSearchFilter(int userID) {
			return mSqliteConnection.Table<SearchFilters> ().Where (i => i.UserID == userID).FirstOrDefault ();
		}

		public void DeleteSearchFilter(int userID) {
			List<SearchFilters> filters = mSqliteConnection.Table<SearchFilters> ().Where (i => i.UserID == userID).ToList ();
			foreach (SearchFilters filter in filters) {
				mSqliteConnection.Delete<SearchFilters> (filter.ID);
			}
		}
		#endregion

		#region Like Table Methods
		public int SaveItem(TREntityLikes info) {
			TREntityLikes cottageLike = GetCottageLikeInfo (info.UserID, info.PropertyID);
			if (cottageLike != null) {
				cottageLike.LikeDislike = info.LikeDislike;
				mSqliteConnection.Update (cottageLike);
				return cottageLike.ID;
			} else {
				return mSqliteConnection.Insert (info);
			}
		}

		public TREntityLikes GetCottageLikeInfo(int userID, string propertyID) {
			return mSqliteConnection.Table<TREntityLikes> ().Where (i => i.UserID == userID && i.PropertyID == propertyID).FirstOrDefault ();
		}

		public void RemoveCottageLikeInfo(int userID, string propertyID) {
			TREntityLikes cottageLike = GetCottageLikeInfo (userID, propertyID);
			mSqliteConnection.Delete<TREntityLikes> (cottageLike.ID);
		}

		public void ClearLiked(int userID, bool likeDislike) {
			List<TREntityLikes> likes = mSqliteConnection.Table<TREntityLikes> ().Where (i => i.UserID == userID && i.LikeDislike == likeDislike).ToList ();
			foreach (TREntityLikes likeEntity in likes) {
				mSqliteConnection.Delete<TREntityLikes> (likeEntity.ID);
			}
		}
		#endregion

		#region Common Methods
		public IEnumerable<T> GetItems<T> () where T : TREntityBase
		{
			return (from item in mSqliteConnection.Table<T> ()
			        select item).ToList ();
		}

		// helper for checking if database has been populated
		public int CountTable<T> () where T : TREntityBase
		{
			string sql = string.Format ("select count (*) from \"{0}\"", typeof(T).Name);
			var c = mSqliteConnection.CreateCommand (sql, new object[0]);
			return c.ExecuteScalar<int> ();
		}

		public void ClearDatabase() {
			mSqliteConnection.DeleteAll<SearchFilters> ();
			mSqliteConnection.DeleteAll<CottageLikeInfo> ();
		}

		public void ClearTable<T> () where T : TREntityBase
		{
			mSqliteConnection.Execute (string.Format ("delete from \"{0}\"", typeof(T).Name));
		}
		#endregion
	}
}

