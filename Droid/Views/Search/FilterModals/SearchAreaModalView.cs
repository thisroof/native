using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;
using Android.Graphics.Drawables;
using ThisRoofN.ViewModels;
using Android.Support.V4.View;
using ThisRoofN.Droid.CustomControls.TRSlidingTab;
using ThisRoofN.Droid.Adapters;
using ThisRoofN.Models.Service;
using ThisRoofN.Droid.CustomControls;
using Android.Views.InputMethods;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ThisRoofN.Models.App;
using MvvmCross.Binding.Droid.Views;

namespace ThisRoofN.Droid
{
	public class SearchAreaModalView : MvxDialogFragment, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener, ILocationListener
	{
		ViewPager locationPager;
		TRSlidingTabLayout tabs;
		SearchLocationTabAdapter tabAdapter;
		GoogleApiClient googleApiClient;

		public SearchAreaModalView ()
		{
			this.SetStyle (MvxDialogFragment.StyleNoTitle, Resource.Style.SearchModalStyle);
		}

		public SearchAreaModalViewModel ViewModelInstance {
			get {
				return (SearchAreaModalViewModel)base.ViewModel;
			}
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			googleApiClient = new GoogleApiClient.Builder (Activity)
				.AddApi (Android.Gms.Location.LocationServices.API)
				.AddConnectionCallbacks (this)
				.AddOnConnectionFailedListener (this)
				.Build ();
			googleApiClient.Connect ();
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);
			Dialog.Window.SetBackgroundDrawable (new ColorDrawable (Android.Graphics.Color.Transparent));

			View view = this.BindingInflate (Resource.Layout.fragment_search_modal_location, null);


			EditText text;
			RelativeLayout headerLayout = (RelativeLayout)view.FindViewById (Resource.Id.header_layout);

			// Set modal back events
			KeyboardLayout modal_back = (KeyboardLayout)view.FindViewById (Resource.Id.layout_modal_back);
			modal_back.OnKeyboard += (object sender, bool isShown) => {
				if(isShown) {
					headerLayout.Visibility = ViewStates.Gone;
				} else {
					headerLayout.Visibility = ViewStates.Visible;
				}
			};

			modal_back.Click += (object sender, EventArgs e) => {
				InputMethodManager imm = (InputMethodManager)Activity.GetSystemService (Context.InputMethodService);
				imm.HideSoftInputFromWindow (modal_back.WindowToken, 0);
				ViewModelInstance.ModalCloseCommand.Execute (null);
			};

			ImageView img_back = (ImageView)view.FindViewById (Resource.Id.btn_back);
			img_back.Click += (object sender, EventArgs e) => {
				ViewModelInstance.ModalCloseCommand.Execute (null);
			};


			// Set Location Type tab and pager
			locationPager = view.FindViewById<ViewPager> (Resource.Id.pager_location);
			tabs = view.FindViewById<TRSlidingTabLayout> (Resource.Id.tab_location);

			locationPager.PageSelected += (object sender, ViewPager.PageSelectedEventArgs e) => {
				InputMethodManager imm = (InputMethodManager)Activity.GetSystemService (Context.InputMethodService);
				imm.HideSoftInputFromWindow (modal_back.WindowToken, 0);

				switch (e.Position) {
				case 0:
					ViewModelInstance.DistanceType = (int)TRSearchType.Commute;
					break;
				case 1:
					ViewModelInstance.DistanceType = (int)TRSearchType.Distance;
					break;
				case 2:
					ViewModelInstance.DistanceType = (int)TRSearchType.State;
					break;
				default:
					break;
				}
			};

			tabs = view.FindViewById<TRSlidingTabLayout> (Resource.Id.tab_location);
			tabs.TabViewTextSizeSp = 12;
			tabs.TabViewPaddingDips = 8;
			tabs.TabTitleTextSize = 12;

			return view;
		}

		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);
			Dialog.Window.Attributes.WindowAnimations = Resource.Style.DialogAnimation;
			Dialog.Window.SetSoftInputMode (SoftInput.AdjustResize);

			var fragments = new List<MvxViewPagerFragmentAdapter.FragmentInfo> {
				new MvxViewPagerFragmentAdapter.FragmentInfo {
					FragmentType = typeof(LocationCommuteTabItemView),
					Title = "COMMUTE",
					ViewModel = this.ViewModelInstance
				},
				new MvxViewPagerFragmentAdapter.FragmentInfo {
					FragmentType = typeof(LocationDistanceTabItemView),
					Title = "DISTANCE",
					ViewModel = this.ViewModelInstance
				},
				new MvxViewPagerFragmentAdapter.FragmentInfo {
					FragmentType = typeof(LocationNationTabItemView),
					Title = "NATION WIDE",
					ViewModel = this.ViewModelInstance
				},
			};

			tabAdapter = new SearchLocationTabAdapter (this.ChildFragmentManager, fragments);
			locationPager.Adapter = tabAdapter;
			locationPager.Adapter.NotifyDataSetChanged ();

			tabs.SetDistributeEvenly (true);
			tabs.SetViewPager (locationPager);
		}

		public override void OnResume ()
		{
			base.OnResume ();
			Dialog.Window.SetLayout (RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.MatchParent);
		}

		#region Google Play Service Callbacks
		public async void OnConnected (Bundle connectionHint)
		{           
			// Get Last known location
			var lastLocation = LocationServices.FusedLocationApi.GetLastLocation (googleApiClient);

			if (lastLocation == null) {
				await RequestLocationUpdates ();
			} else {
				ViewModelInstance.GetCurrentAddressCommand.Execute (new Location() {
					lat = lastLocation.Latitude,
					lng = lastLocation.Longitude
				});
			}
		}

		public void OnConnectionSuspended (int cause)
		{
			UserDialogs.Instance.Alert (string.Format ("GooglePlayServices Connection Suspended: {0}", cause), "GooglePlayServices");
		}

		public void OnConnectionFailed (Android.Gms.Common.ConnectionResult result)
		{
			UserDialogs.Instance.Alert (string.Format ("GooglePlayServices Connection Failed: {0}", result), "GooglePlayServices");
		}

		public void OnLocationChanged (Android.Locations.Location location)
		{
			LocationServices.FusedLocationApi.RemoveLocationUpdatesAsync (googleApiClient, this);
			ViewModelInstance.GetCurrentAddressCommand.Execute (new Location() {
				lat = location.Latitude,
				lng = location.Longitude
			});
		}

		async Task RequestLocationUpdates ()
		{
			// Describe our location request
			var locationRequest = new LocationRequest ()
				.SetInterval (10000)
				.SetFastestInterval (1000)
				.SetPriority (LocationRequest.PriorityHighAccuracy);

			// Check to see if we can request updates first
			if (await CheckLocationAvailability (locationRequest)) {
				// Request updates
				await LocationServices.FusedLocationApi.RequestLocationUpdates (googleApiClient,
					locationRequest,
					this);
			}
		}

		async Task<bool> CheckLocationAvailability (LocationRequest locationRequest)
		{
			// Build a new request with the given location request
			var locationSettingsRequest = new LocationSettingsRequest.Builder ()
				.AddLocationRequest (locationRequest)
				.Build ();

			// Ask the Settings API if we can fulfill this request
			var locationSettingsResult = await LocationServices.SettingsApi.CheckLocationSettingsAsync (googleApiClient, locationSettingsRequest);

			// If false, we might be able to resolve it by showing the location settings 
			// to the user and allowing them to change the settings
			if (!locationSettingsResult.Status.IsSuccess) {
				UserDialogs.Instance.Alert ("Location Services Not Available!", "GooglePlayServices");
				return false;
			}

			return true;
		}
		#endregion
	}
}

