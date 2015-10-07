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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Document childForm = new Document();
            childForm.MdiParent = this;
            childForm.Text = String.Empty;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Mission Files (*.mission)|*.mission|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                Document childForm = new Document();
                childForm.MdiParent = this;
                childForm.Text = FileName;
                childForm.Settings = (MissionSettings)Xml.Load(FileName, typeof(MissionSettings));
                childForm.Show();
                childForm.AfterLoad();
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Mission Files (*.mission)|*.mission|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                var document = ActiveMdiChild as Document;
                Xml.Save(FileName, document.Settings, typeof(MissionSettings));
                document.Text = FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void Main_MdiChildActivate(object sender, EventArgs e)
        {
            var document = ActiveMdiChild as Document;
            if (document == null)
            {
                propertyGrid.SelectedObject = null;
                return;
            }
            propertyGrid.SelectedObject = document.Settings;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var document = ActiveMdiChild as Document;
            if (document == null) return;
            if (saveFileDialogJPG.ShowDialog() != DialogResult.OK) return;
            document.Buffer.Save(saveFileDialogJPG.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var document = ActiveMdiChild as Document;
            if (document == null) return;
            if (document.Text == String.Empty)
            {
                SaveAsToolStripMenuItem_Click(sender, e);
                return;
            }
            Xml.Save(document.Text, document.Settings, typeof(MissionSettings));
        }
    }
}
