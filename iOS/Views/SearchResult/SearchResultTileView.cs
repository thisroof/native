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
		public UIActivityIndicatorView spinner;
		public SearchResultTileView (IntPtr handle) : base (handle)
		{
		}

		public SearchResultTileViewModel ViewModelInstance {
			get {
				return (SearchResultTileViewModel)this.ViewModel;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var propertyTypeSource = new TileCollectionViewSource (this, cv_results, new NSString ("SRTileImageCell"));

			cv_results.AllowsSelection = true;
			cv_results.Source = propertyTypeSource;
			cv_results.Delegate = this;
			cv_results.AlwaysBounceVertical = true;

			var bindingSet = this.CreateBindingSet<SearchResultTileView, SearchResultTileViewModel> ();
			bindingSet.Bind (propertyTypeSource).To (vm => vm.TileItems);
			bindingSet.Apply ();
		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();

		}
			

		// UICollectionView Delegate
		[Export ("collectionView:layout:sizeForItemAtIndexPath:")]
		public CoreGraphics.CGSize GetSizeForItem (UIKit.UICollectionView collectionView, UIKit.UICollectionViewLayout layout, Foundation.NSIndexPath indexPath)
		{
			return new CGSize ((collectionView.Frame.Width - 6) / 3, (collectionView.Frame.Width - 6) / 3);
		}

		[Export ("collectionView:layout:minimumInteritemSpacingForSectionAtIndex:")]
		public System.nfloat GetMinimumInteritemSpacingForSection (UIKit.UICollectionView collectionView, UIKit.UICollectionViewLayout layout, System.nint section)
		{
			return 2.0f;
		}

		[Export ("collectionView:didSelectItemAtIndexPath:")]
		public void ItemSelected (UIKit.UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			ViewModelInstance.DetailCommand.Execute (indexPath.Row);
//			masterView.ViewModelInstance.PropertyTypes [indexPath.Row].Selected = !masterView.ViewModelInstance.PropertyTypes [indexPath.Row].Selected;
		}

		[Export ("collectionView:willDisplaySupplementaryView:forElementKind:atIndexPath:")]
		public void WillDisplaySupplementaryView (UIKit.UICollectionView collectionView, UIKit.UICollectionReusableView view, string elementKind, Foundation.NSIndexPath indexPath)
		{
			
		}

		[Export ("collectionView:layout:referenceSizeForFooterInSection:")]
		public CoreGraphics.CGSize GetReferenceSizeForFooter (UIKit.UICollectionView collectionView, UIKit.UICollectionViewLayout layout, System.nint section)
		{
			return new CGSize (UIScreen.MainScreen.Bounds.Width, 44.0f);
		}

		[Export ("scrollViewDidEndDragging:willDecelerate:")]
		public void DraggingEnded (UIKit.UIScrollView scrollView, bool willDecelerate)
		{
			CGPoint offset = cv_results.ContentOffset;
			CGRect bounds = cv_results.Bounds;
			CGSize size = cv_results.ContentSize;
			UIEdgeInsets inset = cv_results.ContentInset;
			nfloat y = offset.Y + bounds.Size.Height - inset.Bottom;
			nfloat h = size.Height;

			float reload_distance = 50;

			if (y > h + reload_distance) {
				spinner.StartAnimating ();
				Console.WriteLine ("loadmore data");
			}
		}

		public class TileCollectionViewSource : MvxCollectionViewSource
		{
			private SearchResultTileView masterViewInstance;
			public TileCollectionViewSource (SearchResultTileView masterView, UICollectionView cv, NSString reuseIdentifier) : base (cv, reuseIdentifier)
			{
				masterViewInstance = masterView;
				masterView.spinner = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.Gray);
				masterView.spinner.StopAnimating ();
				masterView.spinner.HidesWhenStopped = false;
				masterView.spinner.Frame = new CGRect (0, 0, UIScreen.MainScreen.Bounds.Width, 44);
			}

			public override UICollectionReusableView GetViewForSupplementaryElement (UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
			{
				Console.WriteLine (elementKind);
				UICollectionReusableView footerview = collectionView.DequeueReusableSupplementaryView (UICollectionElementKindSection.Footer, "SRTileFooter", indexPath);
				footerview.Add (masterViewInstance.spinner);
				return footerview;
			}

			protected override UICollectionViewCell GetOrCreateCellFor (UICollectionView collectionView, NSIndexPath indexPath, object item)
			{
				SRTileImageCell cell = (SRTileImageCell)base.GetOrCreateCellFor (collectionView, indexPath, item);
				cell.IVItem.DefaultImagePath = NSBundle.MainBundle.PathForResource ("img_placeholder", "png");
				return cell;
			}
		}
	}
}
