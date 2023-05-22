namespace Cooop365ML;

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
        var status = await Permissions.RequestAsync<Permissions.StorageWrite>();
        if (status != PermissionStatus.Granted)
        {
            // Handle permission denied
            await DisplayAlert("Permission Denied", "Storage permission is required to save the image.", "OK");
            return;
        }

        string fileName = "snapshot.png";

        string folderPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).AbsolutePath;
        string filePath = Path.Combine(folderPath, fileName);

        await cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.PNG, filePath);

        myImage.Source = ImageSource.FromFile(filePath);
    }
}