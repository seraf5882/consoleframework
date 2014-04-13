﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ConsoleFramework;
using ConsoleFramework.Controls;
using ConsoleFramework.Core;
using ConsoleFramework.Rendering;
using JSIL;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //When choosing types for variables that are part of the DOM API,
            //You will want to use var when it's possible and dynamic when it's not.
            Console.WriteLine("Starting");

            WindowsHost windowsHost = new WindowsHost(  );
            Window window = new Window(  );
            window.Content = new Panel(  )
                {
                    Width = 30,
                    Height = 10
                };
            window.Title = "Hello, Web !";
            windowsHost.Show( window );

            runWindows( windowsHost );
            //ConsoleApplication.Instance.Run( windowsHost );
        }

        private static void runWindows(Control control) {
            ConsoleAdapter canvas = new ConsoleAdapter( 80, 25 );
            canvas.Initialize(  );
            
            Renderer renderer = ConsoleApplication.Instance.Renderer;
            renderer.Canvas = canvas;
            // Fill the canvas by default
            renderer.RootElementRect = new Rect( new Point( 0, 0 ), canvas.Size );
            renderer.RootElement = control;
            //
            control.Invalidate();
            renderer.UpdateLayout();
            renderer.FinallyApplyChangesToCanvas();

            Console.WriteLine("Applied to canvas");

            canvas.UserInputReceived += ( sender, args ) => {
                control.Invalidate();
                renderer.UpdateLayout();
                renderer.FinallyApplyChangesToCanvas();
            };
        }
    }
}