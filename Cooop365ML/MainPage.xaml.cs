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

	private void Button_Clicked(object sender, EventArgs e)
	{
		string snapFilePath = @"../Cooop365ML/SnapShot";
		string fileName = "snapshot.png";
		string filePath = Path.Combine(snapFilePath, fileName);
        cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.PNG, filePath); //Virker ikke

        //myImage.Source = ImageSource.FromFile(filePath);
        myImage.Source = cameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG); //Skal (når den er klar) hentes fra ML modellen
	}
}

