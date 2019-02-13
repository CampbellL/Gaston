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
        private List<SKPoint> _boxes = new List<SKPoint>();
        private List<SKPoint> _crossedBoxes = new List<SKPoint>();
        private int _boxSize = 0;

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
            //Console.WriteLine("x: " + args.Location.X);
            //Console.WriteLine("y: " + args.Location.Y);
            var test = ConvertToPixel(args.Location);

            for (int i = 0; i < 4 ; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int checkX = (int) _boxes[i + j * 4].X;
                    int checkY = (int)_boxes[i + j * 4].Y;
                    SKPoint center = new SKPoint(checkX + _boxSize/2, checkY + _boxSize/2);

                    if (test.X < checkX + _boxSize * 7/8 && 
                        test.X > checkX + _boxSize * 1/8 && 
                        test.Y < checkY + _boxSize * 7/8 && 
                        test.Y > checkY + _boxSize * 1/8 && 
                        !_path.Contains(center.X, center.Y))
                    {
                        if (_path.IsEmpty)
                        {
                            _path.Reset();
                            _path.MoveTo(center);
                            CanvasView.InvalidateSurface();
                        }
                        if (!_path.IsEmpty)
                        {
                            Console.WriteLine("CenterX: " + center.X);
                            Console.WriteLine("CenterY: " + center.Y);
                            _path.LineTo(center);
                            CanvasView.InvalidateSurface();
                        }
                        _crossedBoxes.Add(center);
                    }
                }
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
                        _crossedBoxes.Clear();
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

            SKPoint ConvertToPixel(Point pt)
            {
                return new SKPoint((float)(CanvasView.CanvasSize.Width * pt.X / CanvasView.Width),
                    (float)(CanvasView.CanvasSize.Height * pt.Y / CanvasView.Height));
            }

        
        }


        public void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKCanvas canvas = args.Surface.Canvas;
            int screenWidth = info.Width;
            int screenHeight = info.Height;
            _boxSize = 20*screenWidth/100;

            canvas.Clear();

            int startX = 10 * (screenWidth / 100);
            int startY = 50 * (screenHeight / 100);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                        createBox(startX + i * _boxSize, startY + j * _boxSize, canvas);
                }
            }

            canvas.DrawPath(_path, _paint);

            void createBox(int x, int y, SKCanvas c)
            {
                 canvas.DrawRect(x, y, _boxSize, _boxSize, _paint);
                 SKPoint topLeft = new SKPoint(x, y);
                 _boxes.Add(topLeft);
            }
        }
    }
}
