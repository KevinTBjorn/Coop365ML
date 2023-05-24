namespace Cooop365ML;

using Cooop365ML.Services;
using System.IO;

public partial class MainPage : ContentPage
{
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
        await cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.PNG, @$"{FileSystem.Current.CacheDirectory}/{imageName}");
        RoboFlowService.GetPrediction(imageName);
    }
}