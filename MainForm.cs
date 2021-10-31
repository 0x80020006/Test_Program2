using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Test_Program2
{
    class MainForm : Form
    {
        static readonly int IMAGE_BUTTON_WIDTH  = 100;
        static readonly int IMAGE_BUTTON_HEIGHT = 100;


        List<string> filesList;
        public int loadFileNum;
        MenuStrip menuStrip;
        OpenFileDialog ofd;

        public MainForm()
        {
            Load += new EventHandler(MainForm_Load);
            menuStrip = new MenuStrip();
            Controls.Add(menuStrip);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ToolStripMenuItem menuFile = new ToolStripMenuItem();
            menuFile.Text = "ファイル(&F)";
            menuStrip.Items.Add(menuFile);
          
            ToolStripMenuItem menuFileOpen = new ToolStripMenuItem();

            menuFileOpen.Text = "開く(&O)";
            menuFileOpen.Click += new EventHandler(Open_Click);
            menuFile.DropDownItems.Add(menuFileOpen);
            ToolStripMenuItem menuFileEnd = new ToolStripMenuItem();

            menuFileEnd.Text = "終了(&X)";
            menuFileEnd.Click += new EventHandler(Close_Click);
            menuFile.DropDownItems.Add(menuFileEnd);
        }

        private void Open_Click(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog(); 
            ofd.Filter = "Image File(*.bmp,*.jpg,*.png)|*.bmp;*.jpg;*.png|Bitmap(*.bmp)|*.bmp|Jpeg(*.jpg)|*.jpg|PNG(*.png)|*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine($"読み込みファイル:{ofd.FileName}");
                string folderPath = Path.GetDirectoryName(ofd.FileName);
                IEnumerable<string> files = Directory.EnumerateFiles(folderPath).Where(str => str.EndsWith(".bmp") || str.EndsWith(".jpg") || str.EndsWith(".png"));
                filesList = files.ToList();
                Console.WriteLine($"{filesList.Count}");

                loadFileNum = filesList.IndexOf(ofd.FileName);

                Button[] iButton = new Button[filesList.Count];
                for (int i = 0; i < iButton.Length; i++)
                {
                    iButton[i] = new Button();
                    iButton[i].Location = new Point(ClientSize.Width / 2 - iButton[i].Size.Width / 2 + 100 * (i - loadFileNum), ClientSize.Height / 2);
                    iButton[i].Text = i.ToString();
                    Controls.Add(iButton[i]);
                }

                //ZOrderでコントロールのZ位置を調整する(参考：https://docs.microsoft.com/ja-jp/office/vba/language/reference/user-interface-help/zorder-method)
                iButton[loadFileNum].BringToFront();
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
