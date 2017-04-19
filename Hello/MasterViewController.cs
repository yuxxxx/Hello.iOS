using System;
using System.Collections.Generic;
using info.yunnxx.Hello;

using UIKit;
using Foundation;

namespace Hello
{
	public partial class MasterViewController : UITableViewController
	{
		public DetailViewController DetailViewController { get; set; }

		DataSource dataSource;

		protected MasterViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = NSBundle.MainBundle.LocalizedString("Master", "Master");

			// Perform any additional setup after loading the view, typically from a nib.
			DetailViewController = (DetailViewController)((UINavigationController)SplitViewController.ViewControllers[1]).TopViewController;

			TableView.Source = dataSource = new DataSource(this);
		}

		public override void ViewWillAppear(bool animated)
		{
			ClearsSelectionOnViewWillAppear = SplitViewController.Collapsed;
			base.ViewWillAppear(animated);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "showDetail")
			{
				var controller = (DetailViewController)((UINavigationController)segue.DestinationViewController).TopViewController;
				var indexPath = TableView.IndexPathForSelectedRow;
				var item = dataSource.Objects[indexPath.Row];

				controller.SetDetailItem(item);
				controller.NavigationItem.LeftBarButtonItem = SplitViewController.DisplayModeButtonItem;
				controller.NavigationItem.LeftItemsSupplementBackButton = true;
			}
		}

		class DataSource : UITableViewSource
		{
			static readonly NSString CellIdentifier = new NSString("Cell");
			readonly IReadOnlyList<object> objects = new List<object>();
			readonly MasterViewController controller;

			public DataSource(MasterViewController controller)
			{
				this.controller = controller;
                objects = new MasterList();
			}

			public IReadOnlyList<object> Objects
			{
				get { return objects; }
			}

			// Customize the number of sections in the table view.
			public override nint NumberOfSections(UITableView tableView)
			{
				return 1;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return objects.Count;
			}

			// Customize the appearance of table view cells.
			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);

                cell.TextLabel.Text = (objects[indexPath.Row] as MasterItem).Title;

				return cell;
			}

			public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
			{
				// Return false if you do not want the specified item to be editable.
				return true;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
					controller.DetailViewController.SetDetailItem(objects[indexPath.Row]);
			}
		}
	}
}
