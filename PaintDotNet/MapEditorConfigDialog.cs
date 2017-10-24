using PaintDotNet.Effects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEdit
{
    public partial class MapEditorConfigDialog : EffectConfigDialog
    {
        protected override void InitialInitToken()
        {
            theEffectToken = new MapEditorConfigToken();
        }

        public MapEditorConfigDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FinishTokenUpdate();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
