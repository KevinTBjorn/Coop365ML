namespace Cooop365ML;

using Cooop365ML.Services;
using System.IO;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        cameraView.Camera = cameraView.Cameras.First();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await cameraView.StopCameraAsync();
            await cameraView.StartCameraAsync();
        });
    }

	private async void Button_Clicked(object sender, EventArgs e)
	{
        string imageName = "coop365image.png";

        /*var stream = await cameraView.TakePhotoAsync();
        if (stream != null)
        {
            //var result = ImageSource.FromStream(() => stream);
            //byte[] imageArray = await ConvertImageSourceToBytesAsync(result);

            ////Convert byte array to image
            //string bytesToString = Convert.ToBase64String(imageArray);
            //string stringToImage = string.Format("data:image/png;base64,{0}", bytesToString);
        }*/
        await cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.PNG, @$"{FileSystem.Current.CacheDirectory}/{imageName}");
        RoboFlowService.GetPrediction(imageName);
    }

    public async Task<byte[]> ConvertImageSourceToBytesAsync(ImageSource imageSource)
    {
        Stream stream = await ((StreamImageSource)imageSource).Stream(CancellationToken.None);
        byte[] bytesAvailable = new byte[stream.Length];
        stream.Read(bytesAvailable, 0, bytesAvailable.Length);

        return bytesAvailable;
    }
}