using System;
using System.Collections.Generic;
using Gaston.TouchTracking;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Gaston.Pages
{
    public partial class FingerPaintPage : ContentPage
    {
        private readonly Dictionary<long, SKPath> _inProgressPaths = new Dictionary<long, SKPath>();
        private readonly List<SKPath> _completedPaths = new List<SKPath>();

        private readonly SKPaint _paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Blue,
            StrokeWidth = 10,
            StrokeCap = SKStrokeCap.Round,
            StrokeJoin = SKStrokeJoin.Round
        };

        public FingerPaintPage()
        {
            InitializeComponent();
        }

        public void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            Console.WriteLine("x: " + args.Location.X);
            Console.WriteLine("y: " + args.Location.Y);
            var test = ConvertToPixel(args.Location);
            if (test.X < 350 && test.X > 299 && test.Y < 350 && test.Y > 299)
            {
                DisplayAlert("Hit", "Hit", "ok");
            }

            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    if (!_inProgressPaths.ContainsKey(args.Id))
                    {
                        SKPath path = new SKPath();
                        path.MoveTo(ConvertToPixel(args.Location));
                        _inProgressPaths.Add(args.Id, path);
                        CanvasView.InvalidateSurface();
                    }

                    break;

                case TouchActionType.Moved:
                    if (_inProgressPaths.ContainsKey(args.Id))
                    {
                        SKPath path = _inProgressPaths[args.Id];
                        path.LineTo(ConvertToPixel(args.Location));
                        CanvasView.InvalidateSurface();
                    }

                    break;

                case TouchActionType.Released:
                    if (_inProgressPaths.ContainsKey(args.Id))
                    {
                        _completedPaths.Add(_inProgressPaths[args.Id]);
                        _inProgressPaths.Remove(args.Id);
                        CanvasView.InvalidateSurface();
                    }

                    break;

                case TouchActionType.Cancelled:
                    if (_inProgressPaths.ContainsKey(args.Id))
                    {
                        _inProgressPaths.Remove(args.Id);
                        CanvasView.InvalidateSurface();
                    }

                    break;
            }
        }

        public void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKCanvas canvas = args.Surface.Canvas;
            canvas.Clear();
            canvas.DrawRect(300, 300, 50, 50, _paint);
            foreach (SKPath path in _completedPaths)
            {
                canvas.DrawPath(path, _paint);
            }

            foreach (SKPath path in _inProgressPaths.Values)
            {
                canvas.DrawPath(path, _paint);
            }
        }

        SKPoint ConvertToPixel(Point pt)
        {
            return new SKPoint((float) (CanvasView.CanvasSize.Width * pt.X / CanvasView.Width),
                (float) (CanvasView.CanvasSize.Height * pt.Y / CanvasView.Height));
        }
    }
}