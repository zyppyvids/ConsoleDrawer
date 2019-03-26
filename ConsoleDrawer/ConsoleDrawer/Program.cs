using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawer
{
    class Program
    {
        /// <summary>Defines the entry point of the application.</summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            ConsoleColor[,] firstImageColors = GetColors(@"..\..\images\testImage1.png");
            ConsoleColor[,] secondImageColors = GetColors(@"..\..\images\testImage2.png");
            ConsoleColor[,] thirdImageColors = GetColors(@"..\..\images\testImage3.png");
            ConsoleColor[,] fourthImageColors = GetColors(@"..\..\images\testImage4.png");

            DrawImage(firstImageColors);
            DrawImage(secondImageColors);
            DrawImage(thirdImageColors);
            DrawImage(fourthImageColors);
        }

        /// <summary>Gets the colors.</summary>
        /// <param name="imagePath">The image path.</param>
        static ConsoleColor[,] GetColors(string imagePath)
        {
            Bitmap imageBitmap = ResizeImage((Image)new Bitmap(imagePath), new Size(50, 25));
            
            Color[,] colors = new Color[imageBitmap.Height, imageBitmap.Width];


            for (int y = 0; y < imageBitmap.Height; y++)
            {
                for (int x = 0; x < imageBitmap.Width; x++)
                {
                    colors[y, x] = (imageBitmap.GetPixel(x, y));
                }
            }

            return ConvertToConsoleColor(colors);
        }

        /// <summary>Converts the color of to console.</summary>
        /// <param name="colors">The colors.</param>
        static ConsoleColor[,] ConvertToConsoleColor(Color[,] colors)
        {
            ConsoleColor[,] consoleColors = new ConsoleColor[colors.GetLength(0), colors.GetLength(1)];

            for (int y = 0; y < consoleColors.GetLength(0); y++)
            {
                for (int x = 0; x < consoleColors.GetLength(1); x++)
                {
                    Color color = colors[y, x];

                    int index = (color.R > 128 | color.G > 128 | color.B > 128) ? 8 : 0; // Bright bit
                    index |= (color.R > 64) ? 4 : 0; // Red bit
                    index |= (color.G > 64) ? 2 : 0; // Green bit
                    index |= (color.B > 64) ? 1 : 0; // Blue bit

                    consoleColors[y, x] = (ConsoleColor)index;
                }
            }

            return consoleColors;
        }

        /// <summary>Draws the image.</summary>
        /// <param name="colors">The colors.</param>
        static void DrawImage(ConsoleColor[,] colors)
        {
            for (int y = 0; y < colors.GetLength(0); y++)
            {
                for (int x = 0; x < colors.GetLength(1); x++)
                {
                    Console.BackgroundColor = colors[y, x];
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>Resizes the image.</summary>
        /// <param name="imgToResize">The img to resize.</param>
        /// <param name="size">The size.</param>
        static Bitmap ResizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }
    }
}
