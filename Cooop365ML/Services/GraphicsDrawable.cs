using Cooop365ML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
#if ANDROID
using Microsoft.Maui.Graphics.Platform;
#endif

namespace Cooop365ML.Services
{
    public class GraphicsDrawable : IDrawable
    {
        public static Root Data { get; set; }
        public static void GetData(Root data)
        {
            Data = data;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            Microsoft.Maui.Graphics.IImage image;
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("Cooop365ML.Resources.Images.able.jpg"))
            {
                image = PlatformImage.FromStream(stream);
            }

            if (image != null)
            {
                Microsoft.Maui.Graphics.IImage newImage = image.Resize(300, 300, ResizeMode.Bleed);
                //canvas.DrawImage(newImage, 100, 0, newImage.Width, newImage.Height);
                canvas.DrawImage(image, 100, 0, image.Width / 10, image.Height / 10);
                if(Data != null)
                {
                    foreach (var item in Data.predictions)
                    {
                        int xPos = Convert.ToInt16(100 + (item.x - (item.width / 2)) / 10);
                        int yPos = Convert.ToInt16((item.y - (item.height / 2)) / 10);
                        int id = 0;
                        switch (item.@class)
                        {
                            case "Apple":
                                canvas.StrokeColor = Colors.Red;
                                canvas.FontColor = Colors.Red;
                                id = Convert.ToInt16(Fruits.FruitID.Apple);
                                break;
                            case "Pear":
                                canvas.StrokeColor = Colors.Blue;
                                canvas.FontColor = Colors.Blue;
                                id = Convert.ToInt16(Fruits.FruitID.Pear);
                                break;
                            case "Banana":
                                canvas.StrokeColor = Colors.Yellow;
                                canvas.FontColor = Colors.Yellow;
                                id = Convert.ToInt16(Fruits.FruitID.Banana);
                                break;
                            case "Kiwi":
                                canvas.StrokeColor = Colors.DarkGreen;
                                canvas.FontColor = Colors.DarkGreen;
                                id = Convert.ToInt16(Fruits.FruitID.Kiwi);
                                break; 
                            case "Cutted_Kiwi":
                                canvas.StrokeColor = Colors.Green;
                                canvas.FontColor = Colors.Green;
                                id = Convert.ToInt16(Fruits.FruitID.Cutted_Kiwi);
                                break;
                            default:
                                canvas.StrokeColor = null;
                                break;
                        }
                        canvas.StrokeSize = 6;
                        canvas.DrawRectangle(xPos, yPos, Convert.ToInt16(item.width / 10), Convert.ToInt16(item.height / 10));

                        canvas.FontSize = 16;
                        canvas.DrawString(item.@class + " - " + id, xPos, yPos - 5, HorizontalAlignment.Left);
                    }
                }
            }
        }
    }
}
