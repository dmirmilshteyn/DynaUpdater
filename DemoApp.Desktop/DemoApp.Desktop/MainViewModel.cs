using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml;
using Updater;
using Updater.Desktop;
using Updater.Installation;
using Updater.Storage;

namespace DemoApp.Desktop
{
    class MainViewModel : ObservableObject
    {
        public ICommand ResetCommand { get; private set; }
        public ICommand PerformUpdateCommand { get; private set; }
        public ICommand ClearCacheCommand { get; private set; }

        string status;
        public string Status {
            get { return status; }
            set {
                status = value;
                RaisePropertyChanged();
            }
        }

        BitmapSource image;
        public BitmapSource Image {
            get { return image; }
            set {
                image = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel() {
            this.ResetCommand = new Command(ResetCommandCallback);
            this.PerformUpdateCommand = new Command(PerformUpdateCommandCallback);
            this.ClearCacheCommand = new Command(ClearCacheCommandCallback);

            this.Status = "Waiting for input...";

            // Make a copy of the image to allow for reseting
            File.Copy(Path.Combine("Data", "Image.png"), Path.Combine(Path.Combine("Data", "Image.png.bak")), true);

            RefreshImage();
        }

        private void ResetCommandCallback() {
            File.Copy(Path.Combine(Path.Combine("Data", "Image.png.bak")), Path.Combine("Data", "Image.png"), true);

            ClearCacheCommandCallback();
            RefreshImage();
        }

        private async void PerformUpdateCommandCallback() {
            Status = "Downloading update...";
            Uri updateSite = new Uri(@"https://onedrive.live.com/download?resid=D332F531B200D073%218103");
            Uri remotePackageStorageDirectory = new Uri(updateSite, "Packages/");

            string baseDirectory = Path.GetFullPath("Data");

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

            RefreshImage();
            Status = "Image updated!";
        }

        private void ClearCacheCommandCallback() {
            string baseDirectory = Path.GetFullPath(Path.Combine("Data", "Cache"));

            // Clean the old test environment if it already exist
            if (Directory.Exists(baseDirectory)) {
                Directory.Delete(baseDirectory, true);
            }

            Status = "Update cache cleared!";
        }

        private void RefreshImage() {
            this.Image = LoadBitmapFromFile(Path.Combine("Data", "Image.png"));
        }

        private BitmapSource LoadBitmapFromFile(string file) {
            using (FileStream fileStream = new FileStream(file, FileMode.Open)) {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = fileStream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
