namespace Cooop365ML;

#if ANDROID  
using Android.OS;
using Microsoft.Maui.Graphics.Platform;
#endif
using Cooop365ML.Services;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Platform;
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
        RoboFlowService.GetPrediction(imageName);

        System.Diagnostics.Debug.WriteLine(RoboFlowService.GetPrediction(@"C:\Users\Kevin Bjørn\Documents\Coop365ML\aeble_2-4Q536WZvxhcrDoquO26eWQ.jpg")); 

    }
}
public class GraphicsDrawable : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        IImage image;
        Assembly assembly = GetType().GetTypeInfo().Assembly;
        using (Stream stream = assembly.GetManifestResourceStream("Cooop365ML.Resources.Images.able.jpg"))
        {
            image = PlatformImage.FromStream(stream);
        }

        if (image != null)
        {
            IImage newImage = image.Resize(300, 300, ResizeMode.Bleed);
            canvas.DrawImage(newImage, 100, 100, newImage.Width, newImage.Height);

            //canvas.DrawImage(image, 10, 15, 90, 90);

            //canvas.DrawImage(image, 10, 10, image.Width, image.Height);
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 6;
            canvas.DrawLine(60, 60, 800, 800);
        }
    }
}