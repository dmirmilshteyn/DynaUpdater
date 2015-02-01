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

namespace Android.IntegrationTestRunner
{
	[Activity(Label = "Android.IntegrationTestRunner", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle bundle) {
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);
			
			button.Click += delegate {
				Task updaterTask = HandleUpdater();
				updaterTask.Wait();

				button.Text = "Updated!";
			};
		}

		private async Task HandleUpdater() { 
			try {
				string updateSiteString = "??";

				Uri updateSite = new Uri(updateSiteString);
				Uri remotePackageStorageDirectory = new Uri(updateSite, "Packages/");

				string baseDirectory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Updater");

				// Clean the old test environment if it already exist
				if (Directory.Exists(baseDirectory)) {
					Directory.Delete(baseDirectory, true);
				}
				// Setup the test environment
				Directory.CreateDirectory(baseDirectory);

				IStorageProvider storageProvider = new StorageProvider(baseDirectory);
				using (ICacheStorageProvider cacheStorageProvider = new CacheStorageProvider(Path.Combine(baseDirectory, "Cache"))) {
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
				string baseDirectory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Updater");

				File.WriteAllText(Path.Combine(baseDirectory, "error.txt"), ex.ToString());
			}
		}
	}
}


