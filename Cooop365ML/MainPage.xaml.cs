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
        List<string> combinedPredictions = new List<string>();
        fruitsList.ItemsSource = null;
        string imageName = "coop365image.png";
        await cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.PNG, @$"{FileSystem.Current.CacheDirectory}/{imageName}");
        Root data = JsonConvert.DeserializeObject<Root>(RoboFlowService.GetPrediction(imageName));
        foreach (var pred in data.predictions)
        {
            int id = 0;
            switch (pred.@class)
            {
                case "Apple":
                    id = Convert.ToInt16(Fruits.FruitID.Apple);
                    break;
                case "Pear":
                    id = Convert.ToInt16(Fruits.FruitID.Pear);
                    break;
                case "Banana":
                    id = Convert.ToInt16(Fruits.FruitID.Banana);
                    break;
                case "Kiwi":
                    id = Convert.ToInt16(Fruits.FruitID.Kiwi);
                    break;
                case "Cutted_Kiwi":
                    id = Convert.ToInt16(Fruits.FruitID.Cutted_Kiwi);
                    break;
                default:
                    break;
            }
            if (!combinedPredictions.Contains(pred.@class))
            {
                combinedPredictions.Add($"{pred.@class}[{id}]");
            }
        }
        fruitsList.ItemsSource = combinedPredictions;
    }
}