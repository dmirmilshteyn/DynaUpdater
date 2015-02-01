using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Updater.Storage;
using Updater;
using Updater.Android;
using System.Xml;
using System.IO.Compression;
using System.Threading.Tasks;
using Updater.Installation;
using Android.Graphics;

namespace Android.IntegrationTestRunner
{
	[Activity(Label = "Android.IntegrationTestRunner", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		ImageView displayImage;
		string baseDirectory;

		protected override void OnCreate(Bundle bundle) {
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			this.baseDirectory = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Updater");

			// Get our button from the layout resource,
			// and attach an event to it
			Button performUpdateButton = FindViewById<Button>(Resource.Id.performUpdate);
			performUpdateButton.Click += delegate {
				Task updaterTask = HandleUpdater();
				updaterTask.Wait();

				performUpdateButton.Text = "Updated!";

				RefreshDisplayImage();
			};

			Button clearCacheButton = FindViewById<Button>(Resource.Id.clearCache);
			clearCacheButton.Click += delegate {
				// Clean the old test environment if it already exist
				if (Directory.Exists(baseDirectory)) {
					Directory.Delete(baseDirectory, true);
				}
				// Setup the test environment
				Directory.CreateDirectory(baseDirectory);

				RefreshDisplayImage();
				performUpdateButton.Text = "Perform Update";
			};

			displayImage = FindViewById<ImageView>(Resource.Id.imageView);
		}

		private void RefreshDisplayImage() {
			string imagePath = System.IO.Path.Combine(baseDirectory, "Image.png");

			if (File.Exists(imagePath)) {
				Java.IO.File file = new Java.IO.File(imagePath);
				Bitmap bitmap = BitmapFactory.DecodeFile(file.AbsolutePath);
				displayImage.SetImageBitmap(bitmap);
			} else {
				displayImage.SetImageBitmap(null);
			}
		}

		private async Task HandleUpdater() { 
			try {
				string updateSiteString = "https://onedrive.live.com/download?resid=D332F531B200D073%218103";

				Uri updateSite = new Uri(updateSiteString);
				Uri remotePackageStorageDirectory = new Uri(updateSite, "Packages/");

				if (Directory.Exists(baseDirectory) == false) {
					Directory.CreateDirectory(baseDirectory);
				}

				IStorageProvider storageProvider = new StorageProvider(baseDirectory);
				using (ICacheStorageProvider cacheStorageProvider = new CacheStorageProvider(System.IO.Path.Combine(baseDirectory, "Cache"))) {
					IPackageAcquisitionFactory packageAcquisitionFactory = new PackageAcquisitionFactory();
					IUpdaterCache updaterCache = UpdaterCache.InitializeCache(cacheStorageProvider);

					IPackageMetadataCollection packageMetadataCollection = null;

					IUpdater updater = new Updater.Updater();
					using (XmlReader xmlReader = XmlReader.Create(updateSite.AbsoluteUri)) {
						packageMetadataCollection = updater.ParseMetadataCollectionXml(xmlReader);
					}

					IUpdateState updateState = updater.DetermineUpdateState(updaterCache.InstalledPackages, packageMetadataCollection);
					IPackageInstaller packageInstaller = updater.CreateInstaller();
					foreach (IPackageMetadata packageMetadata in updateState.Packages) {
						IPackageAcquisition packageAcquisition = packageAcquisitionFactory.BuildPackageAcquisition(remotePackageStorageDirectory, cacheStorageProvider);
						using (ZipArchive packageArchive = await packageAcquisition.AcquirePackageArchive(packageMetadata)) {
							using (IPackage package = Package.OpenPackage(packageMetadata, packageArchive)) {
								packageInstaller.Install(storageProvider, package);
								updaterCache.MarkPackageAsInstalled(package.Metadata);
							}
						}
					}
				}
			} catch (Exception ex) {
				string baseDirectory = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Updater");

				File.WriteAllText(System.IO.Path.Combine(baseDirectory, "error.txt"), ex.ToString());
			}
		}
	}
}


