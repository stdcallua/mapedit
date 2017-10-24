using System;
using System.Collections;
using System.Drawing;
using PaintDotNet;
using PaintDotNet.Effects;

namespace MapEdit
{
    public class MapEditor
        : PaintDotNet.Effects.Effect
    {
        public static string StaticName
        {
            get
            {
                return "Map edit";
            }
        }

        public static Bitmap StaticIcon
        {
            get
            {
                return new Bitmap(typeof(MapEditor), "MapEditorIcon.png");
            }
        }

        public MapEditor()
            : base(MapEditor.StaticName, MapEditor.StaticIcon, true)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            return new MapEditorConfigDialog();
        }

        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            var g = dstArgs.Graphics;
            //g.Clear(Color.Transparent);
            var pen = new Pen(Color.Red, 10);
            var widthCount = dstArgs.Width/ 200;
            var heightCount = dstArgs.Height / 200;
            for (int i = 1; i < widthCount; i++)
            {
                for (int j = 1; j < dstArgs.Height; j++)
                {
                    int ii = i * 200;
                    dstArgs.Surface[ii, j] = ColorBgra.Red;
                }
                //g.DrawLine(pen, i * 200, 0, i * 200, dstArgs.Height);
                //var text = horizontal.Next();
                //var sizeText = draw.MeasureString(text, Settings.Font.FontValue);
                //Point textpoint = new Point(i * Settings.Step + (Settings.Step - (int)sizeText.Width) / 2, (Settings.Step - (int)sizeText.Height) / 2);
                //draw.DrawString(text, Settings.Font.FontValue, new SolidBrush(Settings.ColorFont), textpoint);
            }
        }
    }
}