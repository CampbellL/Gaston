using System;
using System.Collections.Generic;
using Gaston.TouchTracking;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Gaston.Pages
{
    public class Box
    {
        public SKPoint position;
        public readonly char c;

        public Box(int x, int y, Char c)
        {
            position = new SKPoint(x, y);
            this.c = c;
        }
    }

    public partial class FingerPaintPage : ContentPage
    {
        //current path being drawn onto canvas
        private SKPath _path = new SKPath();
        //list of all the boxes
        private List<Box> _boxes = new List<Box>();
        //boxes already chosen 
        private List<SKPoint> _crossedBoxes = new List<SKPoint>();
        //length of the side of one box, defined later
        private int _boxSize = 0;
        //list with all the letters in the grid
        private List<char> _letters = new List<char>();
        //list of possible answers
        private List<string> _answers = new List<string>();
        //currently "created" word
        private string _selection = "";

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
            initLetters(
                'e','g','n','a', 
                't','r','m','o',
                'i','o', 'e','n',
                'd','e','r','t');
            List<string>  a = new List<string>();
            a.Add("dormens");
            a.Add("mange");
            a.Add("montre");
            a.Add("dit");

            initAnswers(a);

        }

        public void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            //Console.WriteLine("x: " + args.Location.X);
            //Console.WriteLine("y: " + args.Location.Y);
            var test = ConvertToPixel(args.Location);
            

            for (int col = 0; col < 4; col++)
            {
                for (int row = 0; row < 4; row++)
                {
                    int checkX = (int)_boxes[row + col * 4].position.X;
                    int checkY = (int)_boxes[row + col * 4].position.Y;
                    SKPoint center = new SKPoint(checkX + _boxSize / 2, checkY + _boxSize / 2);

                    if (_path.IsEmpty ||
                        test.X < _crossedBoxes[_crossedBoxes.Count - 1].X + 15.0 / 8 * _boxSize &&
                        test.X > _crossedBoxes[_crossedBoxes.Count - 1].X - 9.0 / 8 * _boxSize &&
                        test.Y < _crossedBoxes[_crossedBoxes.Count - 1].Y + 15.0 / 8 * _boxSize &&
                        test.Y > _crossedBoxes[_crossedBoxes.Count - 1].Y - 9.0 / 8 * _boxSize)
                    {

                        if (test.X < checkX + _boxSize * 7.0 / 8 &&
                        test.X > checkX + _boxSize * 1.0 / 8 &&
                        test.Y < checkY + _boxSize * 7.0 / 8 &&
                        test.Y > checkY + _boxSize * 1.0 / 8 &&
                        !_path.Contains(center.X, center.Y))
                        {
                            if (_path.IsEmpty)
                            {
                                _path.Reset();
                                _path.MoveTo(center);
                                CanvasView.InvalidateSurface();

                            }
                            else
                            {
                                Console.WriteLine("CenterX: " + center.X);
                                Console.WriteLine("CenterY: " + center.Y);
                                _path.LineTo(center);

                                for(int k = 0; k < 16; k++)
                                {
                                    if(_boxes[k].position + new SKPoint(_boxSize/2, _boxSize / 2) == center)
                                    {
                                        _selection += _boxes[k].c;
                                        Console.WriteLine(_boxes[k].c);
                                        if (_answers.Contains(_selection))
                                        {
                                            DisplayAlert("Congratulations", "You have found an answer, press ok to ok", "ok");
                                        }
                                    }
                                }
                                CanvasView.InvalidateSurface();
                            }
                            _crossedBoxes.Add(center);
                        }
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
                        _selection = "";
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
            _boxSize = 20 * screenWidth / 100;

            canvas.Clear();

            int startX = 10 * (screenWidth / 100);
            int startY = 50 * (screenHeight / 100);

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    //init letters must be called before this
                    createBox(startX + col * _boxSize, startY + row * _boxSize, _letters[row + col*4], canvas);
                }
            }


            _paint.TextSize = 120;
            _paint.IsAntialias = true;
            _paint.IsStroke = false;
            _paint.Typeface = SKTypeface.FromFamilyName(
                  "Arial",
                  SKFontStyleWeight.Bold,
                  SKFontStyleWidth.Normal,
                  SKFontStyleSlant.Upright);

            for(int count = 0; count < 16; count++) {
                Box b = _boxes[count];
                String s = b.c.ToString();                
                canvas.DrawText(s, (float) b.position.X + _boxSize/2, (float) b.position.Y + _boxSize / 2, _paint);
            }

            _paint.IsStroke = true;
            canvas.DrawPath(_path, _paint);

            void createBox(int x, int y, char cha, SKCanvas can)
            {
                canvas.DrawRect(x, y, _boxSize, _boxSize, _paint);
                _boxes.Add(new Box(x, y, cha));
            }

            canvas.DrawText(_selection, 100, 200, _paint);
        }

        public void initLetters(char c0, char c1, char c2, char c3, char c4, char c5, char c6, char c7, char c8, char c9, char c10, char c11, char c12, char c13, char c14, char c15)
        {
            _letters.Add(c0);
            _letters.Add(c1);
            _letters.Add(c2);
            _letters.Add(c3);
            _letters.Add(c4);
            _letters.Add(c5);
            _letters.Add(c6);
            _letters.Add(c7);
            _letters.Add(c8);
            _letters.Add(c9);
            _letters.Add(c10);
            _letters.Add(c11);
            _letters.Add(c12);
            _letters.Add(c13);
            _letters.Add(c14);
            _letters.Add(c15);
        }
        
        public void initAnswers(List<string> answers)
        {
            _answers = answers;
        }
    }
}
