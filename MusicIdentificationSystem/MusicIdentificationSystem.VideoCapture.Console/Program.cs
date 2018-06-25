using System;

namespace MusicIdentificationSystem.VideoCapture.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            VideoCapture capture = new VideoCapture();
            //Console.WriteLine("Hello World!");
            while (true)
            {
                capture.Capture();
            }
        }
    }
}
