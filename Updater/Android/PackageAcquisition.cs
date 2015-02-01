using System;
using Updater.Storage;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net;

namespace Updater.Android
{
	public class PackageAcquisition : IPackageAcquisition
	{
		Uri remotePackageStorageDirectory;
		ICacheStorageProvider storageProvider;

		public event EventHandler<PackageAcquisitionUpdateEventArgs> AcquisitionUpdate;

		public PackageAcquisition(Uri remotePackageStorageDirectory, ICacheStorageProvider storageProvider) {
			this.remotePackageStorageDirectory = remotePackageStorageDirectory;
			this.storageProvider = storageProvider;
		}

		public async Task<ZipArchive> AcquirePackageArchive(IPackageMetadata packageMetadata) {
			using (WebClient webClient = new WebClient()) {
				Uri targetUri;
				if (string.IsNullOrEmpty(packageMetadata.Source)) {
					targetUri = new Uri(remotePackageStorageDirectory, packageMetadata.Id + ".zip");
				} else {
					targetUri = new Uri(packageMetadata.Source);
				}

				using (Stream packageStream = webClient.OpenRead(targetUri)) {
					// Intentionally left open - will be closed by the caller
					Stream temporaryFileStream = storageProvider.CreateTemporaryFile(packageMetadata.Id + ".temp");
					long length = packageMetadata.Size;

					IProgress<long> progress = new Progress<long>((totalBytesRead) =>
							{
								// Calculate percentage
								int percent;
								if (length > 0) {
									percent = (int)(totalBytesRead / length);
								} else {
									percent = 0;
								}

								// Report the download update
								if (AcquisitionUpdate != null) {
									AcquisitionUpdate(this, new PackageAcquisitionUpdateEventArgs(percent));
								}
							});

					// Async file download
					CopyStreams(packageStream, temporaryFileStream, progress);

					return new ZipArchive(temporaryFileStream);
				}
			}
		}

		private void ReportProgress(int percent) {

		}

		private void CopyStreams(Stream inputStream, Stream outputStream, IProgress<long> progress) {
			byte[] downloadBuffer = new byte[4096];
			int bytesRead = 0;
			int totalBytesRead = 0;
			while ((bytesRead = inputStream.Read(downloadBuffer, 0, downloadBuffer.Length)) > 0) {
				outputStream.Write(downloadBuffer, 0, bytesRead);

				totalBytesRead += bytesRead;
				progress.Report(totalBytesRead);
			}
		}
	}
}