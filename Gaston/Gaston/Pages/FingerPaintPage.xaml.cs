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
        

        private SKPath _path = new SKPath();
        private List<SKPoint> _crossedBoxes = new List<SKPoint>();

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



            if (test.X < 350 && test.X > 299 && test.Y < 350 && test.Y > 299 && !_path.Contains(325, 325))
            {
                //DisplayAlert("Hit", "Hit", "ok");

                SKPoint center = new SKPoint(325, 325);
                if (_path.IsEmpty)
                {
                    _path.Reset();
                    _path.MoveTo(center);
                    CanvasView.InvalidateSurface();
                }
                if (!_path.IsEmpty)
                {
                    _path.LineTo(center);
                    CanvasView.InvalidateSurface();
                }

                _crossedBoxes.Add(center);
            }

            if (test.X < 650 && test.X > 599 && test.Y < 550 && test.Y > 499 && !_path.Contains(625, 525))
            {
                SKPoint center = new SKPoint(625, 525);
                //DisplayAlert("Hit", "Hit", "ok");

                if (_path.IsEmpty)
                {
                    _path.Reset();
                    _path.MoveTo(center);
                    CanvasView.InvalidateSurface();
                }
                if (!_path.IsEmpty)
                {
                    _path.LineTo(center);
                    CanvasView.InvalidateSurface();
                }
                _crossedBoxes.Add(center);
            }

            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    break;
                    
                case TouchActionType.Moved:
                    _path.Reset();
                    if (_crossedBoxes.Count != 0)
                    {
                        _path.MoveTo(_crossedBoxes[0]);

                        foreach (SKPoint point in _crossedBoxes)
                        {
                            _path.LineTo(point);
                        }
                        _path.LineTo(ConvertToPixel(args.Location));
                        CanvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Released:
                    if (!_path.IsEmpty)
                    {
                        _path.Reset();
                        CanvasView.InvalidateSurface();
                    }

                    break;

                case TouchActionType.Cancelled:
                    if (!_path.IsEmpty)
                    {
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

            canvas.DrawRect(600, 500, 50, 50, _paint);

            canvas.DrawPath(_path, _paint);
        }

        SKPoint ConvertToPixel(Point pt)
        {
            return new SKPoint((float) (CanvasView.CanvasSize.Width * pt.X / CanvasView.Width),
                (float) (CanvasView.CanvasSize.Height * pt.Y / CanvasView.Height));
        }
    }
}