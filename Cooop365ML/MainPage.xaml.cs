namespace Cooop365ML;

using Cooop365ML.Models;

#if ANDROID
using Android.OS;
using Microsoft.Maui.Graphics.Platform;
#endif
using Cooop365ML.Services;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        #if ANDROID
        cameraView.Camera = cameraView.Cameras.First();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await cameraView.StopCameraAsync();
            await cameraView.StartCameraAsync();
        });
        #endif
    }

    private async void Button_Clicked(object sender, EventArgs e)
	{
        string imageName = "coop365image.png";
        await cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.PNG, @$"{FileSystem.Current.CacheDirectory}/{imageName}");
        Root data = JsonConvert.DeserializeObject<Root>(RoboFlowService.GetPrediction(imageName));
        //Root data = JsonConvert.DeserializeObject<Root>("{\"time\":0.4760886690000916,\"image\":{\"width\":2848,\"height\":4272},\"predictions\":[{\"x\":1454.5,\"y\":2437.0,\"width\":2647.0,\"height\":2686.0,\"confidence\":0.8973177671432495,\"class\":\"Apple\"}]}");
        GraphicsDrawable.GetData(data);
        var graphicsView = this.pictureGraphicsView;

        graphicsView.Invalidate();
    }

}