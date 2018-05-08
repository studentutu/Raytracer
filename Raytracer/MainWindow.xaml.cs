﻿using System;
using System.Windows;
using System.Windows.Input;
using Raytracer.Types;


namespace Raytracer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private RenderEngine raytracer;

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += GenerateImage_Event;
            this.SizeChanged += GenerateImage_Event;


            this.KeyDown += MainWindow_KeyDown;

            raytracer = new RenderEngine(Scene.BuildExampleScene());
        }

        private void GenerateImage_Event(object sender, RoutedEventArgs e)
        {
            GenerateImage();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            double stepSize = 0.3;
            var cam = raytracer.Scene.Camera;

            var vec = cam.Position;
            switch (e.Key)
            {
                case Key.W:
                    vec.Z += stepSize;
                    break;
                case Key.S:
                    vec.Z -= stepSize;
                    break;
                case Key.D:
                    vec.X += stepSize;
                    break;
                case Key.A:
                    vec.X -= stepSize;
                    break;
            }

            if (cam.Position != vec)
            {
                cam.UpdatePosition(vec);
                GenerateImage();
            }
            
        }

        private void GenerateImage()
        {
            var size = new Size2D(Viewbox.ActualWidth, Viewbox.ActualHeight);
            if (size == Size2D.Zero)
                    size = new Size2D(1, 1);

            var renderOutput = raytracer.Render(size);

            RenderOutput.Source = renderOutput;
        }

    }
}
