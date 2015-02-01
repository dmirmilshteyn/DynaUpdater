using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
        }

        private void ResetCommandCallback() {

        }

        private void PerformUpdateCommandCallback() {

        }

        private void ClearCacheCommandCallback() {

        }
    }
}
