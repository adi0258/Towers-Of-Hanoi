using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Towers_Of_Hanoi
{
    public partial class MainWindow : Form
    {
        private Panel peg1, peg2, peg3;
        private Panel basePanel;

        private const int k_DiskHeight = 20;
        private const int k_DiskMaxWidth = 160;
        private const int k_DiskMinWidth = 60;

        private readonly Dictionary<Panel, List<Panel>> r_PegDisks = new Dictionary<Panel, List<Panel>>();

        private readonly List<string> m_SelectedColors;

        public MainWindow(List<string> selectedColors)
        {
            m_SelectedColors = selectedColors ?? throw new ArgumentNullException(nameof(selectedColors));

            InitializeComponent();
            InitializeGameUI();
        }

        private void InitializeGameUI()
        {
            basePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.LightGray
            };
            Controls.Add(basePanel);

            peg1 = CreatePeg(150);
            peg2 = CreatePeg(350);
            peg3 = CreatePeg(550);

            basePanel.Controls.AddRange(new Control[] { peg1, peg2, peg3 });

            r_PegDisks[peg1] = new List<Panel>();
            r_PegDisks[peg2] = new List<Panel>();
            r_PegDisks[peg3] = new List<Panel>();

            CreateDisks(m_SelectedColors);

            foreach (Panel peg in new[] { peg1, peg2, peg3 })
            {
                peg.AllowDrop = true;
                peg.DragEnter += Peg_DragEnter;
                peg.DragDrop += Peg_DragDrop;
            }
        }

        private Panel CreatePeg(int centerX)
        {
            return new Panel
            {
                Width = 10,
                Height = 180,
                BackColor = Color.SaddleBrown,
                Left = centerX - 5,
                Top = 150
            };
        }

        private void CreateDisks(List<string> selectedColors)
        {
            int count = selectedColors.Count;

            for (int i = 0; i < count; i++)
            {
                int width = k_DiskMaxWidth - i * ((k_DiskMaxWidth - k_DiskMinWidth) / (count - 1));
                Panel disk = new Panel
                {
                    Height = k_DiskHeight,
                    Width = width,
                    BackColor = Color.FromName(selectedColors[i]),
                    BorderStyle = BorderStyle.FixedSingle,
                    Tag = width,
                    Cursor = Cursors.Hand
                };

                disk.MouseDown += Disk_MouseDown;

                r_PegDisks[peg1].Insert(0, disk);
                basePanel.Controls.Add(disk);
                UpdateDiskPositions(peg1);
            }
        }

        private void Disk_MouseDown(object sender, MouseEventArgs e)
        {
            Panel disk = sender as Panel;

            Panel peg = r_PegDisks.First(p => p.Value.Contains(disk)).Key;
            if (r_PegDisks[peg][r_PegDisks[peg].Count - 1] != disk)
                return;

            DoDragDrop(disk, DragDropEffects.Move);
        }

        private void Peg_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Panel)))
                e.Effect = DragDropEffects.Move;
        }

        private void Peg_DragDrop(object sender, DragEventArgs e)
        {
            Panel targetPeg = sender as Panel;
            Panel disk = (Panel)e.Data.GetData(typeof(Panel));

            Panel sourcePeg = r_PegDisks.First(p => p.Value.Contains(disk)).Key;

            // Validation: Only smaller disk can be placed on a larger one
            if (r_PegDisks[targetPeg].Count > 0)
            {
                int topWidth = (int)r_PegDisks[targetPeg][r_PegDisks[targetPeg].Count - 1].Tag;
                if ((int)disk.Tag > topWidth)
                {
                    MessageBox.Show("Cannot place a larger disk on a smaller one.", "Invalid Move", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            r_PegDisks[sourcePeg].Remove(disk);
            r_PegDisks[targetPeg].Add(disk);

            UpdateDiskPositions(sourcePeg);
            UpdateDiskPositions(targetPeg);

            if (r_PegDisks[peg3].Count == m_SelectedColors.Count)
            {
                MessageBox.Show("🎉 You solved the puzzle!", "Victory", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateDiskPositions(Panel peg)
        {
            int centerX = peg.Left + peg.Width / 2;
            int baseY = peg.Top + peg.Height;

            for (int i = 0; i < r_PegDisks[peg].Count; i++)
            {
                Panel disk = r_PegDisks[peg][i];
                disk.Top = baseY - (i + 1) * (k_DiskHeight + 2);
                disk.Left = centerX - disk.Width / 2;
            }
        }
    }
}
