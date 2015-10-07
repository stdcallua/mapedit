using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mapedit
{
    public partial class Document : Form
    {
        private Image _buffer;

        private Image _source = null;

        private void LockBuffer()
        {
            if (_buffer != null) _buffer.Dispose();
            _buffer = null;
            GC.Collect();
            if (Settings.WhiteSpace)
                _buffer = new Bitmap(_source.Width + Settings.Step, _source.Height + Settings.Step);
            else
                _buffer = new Bitmap(_source.Width, _source.Height);
            DrawBuffer();
        }

        public Image Source
        {
            get { return _source; }
            set
            {
                _source = value;
                LockBuffer();
            }
        } 

        public Image Buffer
        {
            get { return _buffer; }
        }

        public Document()
        {
            InitializeComponent();
            Settings = new MissionSettings();
            this.MouseWheel += Document_MouseWheel;
        }

        private void Document_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) Zoom--; else Zoom++;
        }

        private MissionSettings _settings = new MissionSettings();

        public MissionSettings Settings
        {
            get { return _settings; }
            set {
                _settings = value;
                _settings.PropertyChanged += _settings_PropertyChanged;
            }
        }

        public void AfterLoad()
        {
            if (!System.IO.File.Exists(Settings.MapFileName)) return;
            Source = Image.FromFile(Settings.MapFileName);
        }

        private void _settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WhiteSpace")
            {
                LockBuffer();
                return;
            }
            if (e.PropertyName == "MapFileName")
            {
                AfterLoad();
                return;
            }
            DrawBuffer();
        }

        private Point CameraPosition { get; set; }

        private int _zoom = 1;

        private int Zoom
        {
            get { return _zoom; }
            set {
                    if (value <= 1) _zoom = 1;
                        else _zoom = value;
                    Invalidate();
                }
        } 

        private Size CameraSize
        {
            get {
                return new Size(Zoom*Width, Zoom*Height);
            }
        }

        public void DrawBuffer()
        {
            Graphics draw = Graphics.FromImage(_buffer);
            //Draw grid
            draw.Clear(Color.White);
            if (Settings.WhiteSpace)
                draw.DrawImage(Source, new Point(Settings.Step, Settings.Step));
            else
                draw.DrawImage(Source, new Point(0, 0));
            var widthCount = Source.Width / Settings.Step;
            var heightCount = Source.Height / Settings.Step;
            if (Settings.WhiteSpace)
            {
                widthCount++; heightCount++;
            }
            var pen = new Pen(Settings.Color, Settings.LineWidth);
            pen.DashPattern = Settings.GetDashPattern();
            pen.DashStyle = Settings.DashStyle;
            Markers.IMarker horizontal = new Markers.NumberMarker(); 
            switch (Settings.HorizontalMarkerType)
            {
                case Markers.MarkerTypes.String:
                    horizontal = new Markers.StringMarker(); break;
            }
            horizontal.Refresh(Settings.HorizontalStart, Settings.HorizontalStep);
            for (int i = 1; i < widthCount; i++)
            {
                draw.DrawLine(pen, i * Settings.Step, 0, i * Settings.Step, _buffer.Height);
                var text = horizontal.Next();
                var sizeText = draw.MeasureString(text, Settings.Font.FontValue);
                Point textpoint = new Point(i * Settings.Step + (Settings.Step - (int)sizeText.Width) / 2, (Settings.Step- (int)sizeText.Height)/2);
                draw.DrawString(text, Settings.Font.FontValue, new SolidBrush(Settings.Color), textpoint);
            }

            Markers.IMarker vertical = new Markers.NumberMarker();
            switch (Settings.VerticalMarkerType)
            {
                case Markers.MarkerTypes.String:
                    vertical = new Markers.StringMarker(); break;
            }
            vertical.Refresh(Settings.VerticalStart, Settings.VerticalStep);
            for (int i = 1; i < widthCount; i++)
            {
                draw.DrawLine(pen, 0, i * Settings.Step, _buffer.Width, i * Settings.Step);
                var text = vertical.Next();
                var sizeText = draw.MeasureString(text, Settings.Font.FontValue);
                Point textpoint = new Point((Settings.Step - (int)sizeText.Width) / 2, i * Settings.Step + (Settings.Step - (int)sizeText.Height) / 2);
                draw.DrawString(text, Settings.Font.FontValue, new SolidBrush(Settings.Color), textpoint);

            }
            Invalidate();
        }

        private void Document_Paint(object sender, PaintEventArgs e)
        {
            if (_buffer == null) return;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
           
            e.Graphics.DrawImage(_buffer, this.ClientRectangle, new Rectangle(CameraPosition, CameraSize), GraphicsUnit.Pixel);
        }

        private int _prevX, _prevY;

        private void Document_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Document_MouseEnter(object sender, EventArgs e)
        {
            this.ActiveControl = panel1;
        }

        private void Document_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CameraPosition = new Point(CameraPosition.X +(_prevX - e.X)*Zoom, CameraPosition.Y + (_prevY - e.Y)*Zoom);
                Invalidate();
            }
            _prevX = e.X;
            _prevY = e.Y;
        }
    }
}
