using System;
using Updater.Storage;
using System.IO;

namespace Updater.Android
{
	public class StorageProvider : IStorageProvider
	{
		string storageDirectory;

		public StorageProvider(string storageDirectory) {
			this.storageDirectory = storageDirectory;
		}

		public Stream CreateFile(string relativePath) {
			string fullPath = Path.Combine(this.storageDirectory, relativePath);
			string targetDirectory = Path.GetDirectoryName(fullPath);

			if (Directory.Exists(targetDirectory) == false) {
				Directory.CreateDirectory(targetDirectory);
			}

			return new FileStream(fullPath, FileMode.Create);
		}

		public Stream OpenFile(string relativePath) {
			return new FileStream(Path.Combine(this.storageDirectory, relativePath), FileMode.Open);
		}

		public void DeleteFile(string relativePath) {
			File.Delete(Path.Combine(this.storageDirectory, relativePath));
		}

		public void DeleteDirectory(string relativePath) {
			Directory.Delete(Path.Combine(this.storageDirectory, relativePath));
		}
	}
}

