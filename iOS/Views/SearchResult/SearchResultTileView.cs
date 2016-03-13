// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using ThisRoofN.ViewModels;
using CoreGraphics;

namespace ThisRoofN.iOS
{
	public partial class SearchResultTileView : BaseViewController,  IUICollectionViewDelegateFlowLayout, IUICollectionViewDelegate
	{
		public SearchResultTileView (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var propertyTypeSource = new MvxCollectionViewSource (cv_results, new NSString("SRTileImageCell"));

			cv_results.AllowsSelection = true;
			cv_results.Source = propertyTypeSource;
			cv_results.Delegate = this;

			var bindingSet = this.CreateBindingSet<SearchResultTileView, SearchResultTileViewModel> ();
			bindingSet.Bind (propertyTypeSource).To (vm => vm.TileItems);
			bindingSet.Apply ();
		}

		// UICollectionView Delegate
		[Export ("collectionView:layout:sizeForItemAtIndexPath:")]
		public CoreGraphics.CGSize GetSizeForItem (UIKit.UICollectionView collectionView, UIKit.UICollectionViewLayout layout, Foundation.NSIndexPath indexPath)
		{
			return new CGSize ((collectionView.Frame.Width - 6)/3, (collectionView.Frame.Width - 6)/3);
		}

		[Export ("collectionView:layout:minimumInteritemSpacingForSectionAtIndex:")]
		public System.nfloat GetMinimumInteritemSpacingForSection (UIKit.UICollectionView collectionView, UIKit.UICollectionViewLayout layout, System.nint section)
		{
			return 2.0f;
		}

		[Export ("collectionView:didSelectItemAtIndexPath:")]
		public void ItemSelected (UIKit.UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
//			masterView.ViewModelInstance.PropertyTypes [indexPath.Row].Selected = !masterView.ViewModelInstance.PropertyTypes [indexPath.Row].Selected;
		}


	}
}
