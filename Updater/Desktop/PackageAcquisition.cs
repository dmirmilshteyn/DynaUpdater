using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Updater.Desktop
{
    public class PackageAcquisition : IPackageAcquisition
    {
        Uri remotePackageStorageDirectory;
        IUpdaterCacheStorageProvider storageProvider;

        public event EventHandler<PackageAcquisitionUpdateEventArgs> AcquisitionUpdate;

        public PackageAcquisition(Uri remotePackageStorageDirectory, IUpdaterCacheStorageProvider storageProvider) {
            this.remotePackageStorageDirectory = remotePackageStorageDirectory;
            this.storageProvider = storageProvider;
        }

        public async Task<ZipArchive> AcquirePackageArchive(IPackageMetadata packageMetadata) {
            using (HttpClient httpClient = new HttpClient()) {
                Uri targetUri = new Uri(remotePackageStorageDirectory, packageMetadata.Id + ".zip");
                using (Stream packageStream = await httpClient.GetStreamAsync(new Uri(remotePackageStorageDirectory, packageMetadata.Id + ".zip"))) {
                    // Intentionally left open - will be closed by the caller
                    Stream temporaryFileStream = storageProvider.CreateTemporaryFile(packageMetadata.Id + ".zip.temp");
                    long length = packageMetadata.Size;

                    Progress<long> progress = new Progress<long>((totalBytesRead) =>
                    {
                        // Calculate percentage
                        int percent = (int)(totalBytesRead / length);

                        // Report the download update
                        if (AcquisitionUpdate != null) {
                            AcquisitionUpdate(this, new PackageAcquisitionUpdateEventArgs(percent));
                        }
                    });

                    // Async file download
                    await Task.Run(() => CopyStreams(packageStream, temporaryFileStream, progress));

                    return new ZipArchive(temporaryFileStream);
                }
            }
        }

        private void ReportProgress(int percent) {

        }

        private void CopyStreams(Stream inputStream, Stream outputStream, Progress<long> progress) {
            byte[] downloadBuffer = new byte[4096];
            int bytesRead = 0;
            int totalBytesRead = 0;
            while ((bytesRead = inputStream.Read(downloadBuffer, 0, downloadBuffer.Length)) > 0) {
                outputStream.Write(downloadBuffer, 0, bytesRead);

                totalBytesRead += bytesRead;
            }
        }
    }
}
