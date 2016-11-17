using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Media;

namespace TodoAzure
{
    public partial class PrepMedia : ContentPage
    {
        public PrepMedia()
        {
            InitializeComponent();
        }

        public async void TakePhoto(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {

                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        public async void PickPhoto(object sender, EventArgs e)
        {

              if (!CrossMedia.Current.IsPickPhotoSupported)
              {
                  await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                  return;
              }
              var file = await CrossMedia.Current.PickPhotoAsync();


              if (file == null)
                  return;

              image.Source = ImageSource.FromStream(() =>
          {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

    }
}
