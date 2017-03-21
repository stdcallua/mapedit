using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.IO;
using System.Xml.Serialization;

namespace mapedit
{
    public class MissionSettings: INotifyPropertyChanged
    {
        public MissionSettings()
        {
            Font = new SerializableFont();
        }

        private String _mapFileName = String.Empty;

        [EditorAttribute(typeof(FilteredFileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Карта")]
        [Description("Файл карты")]
        public String MapFileName
        {
            get { return _mapFileName; }
            set
            {
                if (!File.Exists(value)) return;
                _mapFileName = value;
                OnPropertyChanged("MapFileName");
            }
        }

        public int SourceWidth;
        public int SourceHeight;

        private double _widthMap = 1000;

        [Category("Grid params")]
        [Description("Ширина карты  в метрах")]
        public double WidthMap
        {
            get { return _widthMap; }
            set
            {
                _widthMap = value;
                RecalcStep();
                OnPropertyChanged("WidthMap");
            }
        }

        private double _heightMap = 1000;
        [Category("Grid params")]
        [Description("Высота карты  в метрах")]
        public double HeightMap
        {
            get { return _heightMap; }
            set
            {
                _heightMap = value;
                RecalcStep();
                OnPropertyChanged("WidthMap");
            }
        }

        private void RecalcStep()
        {
            _step = (int)((SourceWidth * _cellSize) / WidthMap);
        }

        private double _cellSize = 100;
        [Category("Grid params")]
        [Description("Размер сетки в метрах")]
        public double CellSize
        {
            get { return _cellSize; }
            set
            {
                _cellSize = value;
                RecalcStep();
                OnPropertyChanged("CellSize");
            }
        }

        private int _step = 100;
        [Category("Grid params")]
        [Description("Настройка шага сетки")]
        public int Step
        {
            get { return _step; }
            set
            {
                if (value != 0) 
                    _step = value;
                OnPropertyChanged("Step");
            }
        }

        private bool _whiteSpace = true;
        [Category("Grid params")]
        [Description("Настройка шага сетки")]
        public bool WhiteSpace
        {
            get { return _whiteSpace; }
            set
            {
                _whiteSpace = value;
                OnPropertyChanged("WhiteSpace");
            }
        }

        private Color _color = Color.Red;

        [Browsable(false)]
        [XmlElement("ClrGrid")]
        public string ClrGridHtml
        {
            get { return ColorTranslator.ToHtml(Color); }
            set { Color = ColorTranslator.FromHtml(value); }
        }

        [XmlIgnore]
        [Category("Grid params")]
        [Description("Цвет")]
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        private Color _colorFont = Color.Red;
        [XmlIgnore]
        [Category("Grid params")]
        [Description("Цвет текста")]
        public Color ColorFont
        {
            get { return _colorFont; }
            set
            {
                _colorFont = value;
                OnPropertyChanged("Color");
            }
        }

        private int _lineWidth = 1;
        [Category("Grid params")]
        [Description("Толшина линий")]
        public int LineWidth
        {
            get { return _lineWidth; }
            set
            {
                _lineWidth = value;
                OnPropertyChanged("LineWidth");
            }
        }

        private System.Drawing.Drawing2D.DashStyle _dashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
        [Category("Grid params")]
        [Description("Штриховка")]
        public System.Drawing.Drawing2D.DashStyle DashStyle
        {
            get { return _dashStyle; }
            set
            {
                _dashStyle = value;
                OnPropertyChanged("DashStyle");
            }
        }

        private String _dashPattern = @"2;2;2";
        [Category("Grid params")]
        [Description(@"Пользовательская штриховка")]
        public String DashPattern
        {
            get { return _dashPattern; }
            set
            {
                _dashPattern = value;
                OnPropertyChanged("DashPattern");
            }
        }

        private SerializableFont _font;
        [Category("Grid params")]
        [Description(@"Шрифт сетки")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SerializableFont Font
        {
            get { return _font; }
            set
            {
                _font = value;
                OnPropertyChanged("Font");
            }
        }

        #region Горизонтальная маркировка
        private Markers.MarkerTypes _horizontalMarkerType = Markers.MarkerTypes.Number;
        [Category("Маркировка")]
        [Description(@"Горизонтальная маркировка тип")]
        public Markers.MarkerTypes HorizontalMarkerType
        {
            get { return _horizontalMarkerType; }
            set
            {
                _horizontalMarkerType = value;
                OnPropertyChanged("MarkerType");
            }
        }

        private String _horizontalStart = "";
        [Category("Маркировка")]
        [Description(@"Горизонтальная маркировка первое значение")]
        public String HorizontalStart
        {
            get { return _horizontalStart; }
            set
            {
                _horizontalStart = value;
                OnPropertyChanged("HorizontalStart");
            }
        }

        private String _horizontalStep = "";
        [Category("Маркировка")]
        [Description(@"Горизонтальная маркировка шаг")]
        public String HorizontalStep
        {
            get { return _horizontalStep; }
            set
            {
                _horizontalStep = value;
                OnPropertyChanged("HorizontalStep");
            }
        }

        #endregion


        #region Вертикальная маркировка
        private Markers.MarkerTypes _verticalMarkerType = Markers.MarkerTypes.Number;
        [Category("Маркировка")]
        [Description(@"Вертикальная маркировка тип")]
        public Markers.MarkerTypes VerticalMarkerType
        {
            get { return _verticalMarkerType; }
            set
            {
                _verticalMarkerType = value;
                OnPropertyChanged("VerticalMarkerType");
            }
        }

        private String _verticalStart = "";
        [Category("Маркировка")]
        [Description(@"Вертикальная маркировка первое значение")]
        public String VerticalStart
        {
            get { return _verticalStart; }
            set
            {
                _verticalStart = value;
                OnPropertyChanged("HorizontalStart");
            }
        }

        private String _verticalStep = "";
        [Category("Маркировка")]
        [Description(@"Вертикальная маркировка шаг")]
        public String VerticalStep
        {
            get { return _verticalStep; }
            set
            {
                _verticalStep = value;
                OnPropertyChanged("HorizontalStep");
            }
        }
        #endregion

        public float[] GetDashPattern()
        {
            var patternstrs = DashPattern.Split(';');
            List<float> result = new List<float>();
            for (int i = 0; i < patternstrs.Length; i++)
            {
                float value = 0;
                if (float.TryParse(patternstrs[i], out value))
                    result.Add(value);
                else return new float[] {1};
            }
            return result.ToArray();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    internal class FilteredFileNameEditor : UITypeEditor
    {
        private OpenFileDialog ofd = new OpenFileDialog();
        public override UITypeEditorEditStyle GetEditStyle(
         ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(
         ITypeDescriptorContext context,
         IServiceProvider provider,
         object value)
        {
            ofd.FileName = value.ToString();
            ofd.Filter = "Map|*.jpg|All Files|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            return base.EditValue(context, provider, value);
        }
    }
}
