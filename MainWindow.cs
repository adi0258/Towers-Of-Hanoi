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
        private Label messageLabel;

        private const int k_DiskHeight = 20;
        private const int k_DiskMaxWidth = 160;
        private const int k_DiskMinWidth = 60;

        private readonly Dictionary<Panel, List<Panel>> r_PegDisks = new Dictionary<Panel, List<Panel>>();

        private readonly List<string> m_SelectedColors;

        public MainWindow(List<string> selectedColors)
        {
            this.Icon = new Icon("favicon.ico");
            // Validate input
            m_SelectedColors = selectedColors ?? throw new ArgumentNullException(nameof(selectedColors));

            InitializeComponent();
            InitializeGameUI();
        }

        private void InitializeGameUI()
        {
            basePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.MistyRose // Soft pink background
            };
            Controls.Add(basePanel);

            // Add instruction label at the top
            Label instructionLabel = new Label
            {
                Text = "Move all the disks to peg C",
                AutoSize = false,
                Height = 40,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Comic Sans MS", 16, FontStyle.Bold),
                ForeColor = Color.DeepPink,
                BackColor = Color.LavenderBlush
            };
            basePanel.Controls.Add(instructionLabel);

            peg1 = CreatePeg(150);
            peg2 = CreatePeg(350);
            peg3 = CreatePeg(550);

            basePanel.Controls.AddRange(new Control[] { peg1, peg2, peg3 });

            // Add cute labels under each peg
            AddPegLabel(peg1, "A", Color.HotPink);
            AddPegLabel(peg2, "B", Color.MediumVioletRed);
            AddPegLabel(peg3, "C", Color.DeepPink);

            messageLabel = new Label
            {
                AutoSize = false,
                Height = 40,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Comic Sans MS", 14, FontStyle.Bold),
                ForeColor = Color.HotPink,
                BackColor = Color.LavenderBlush
            };

            basePanel.Controls.Add(messageLabel);

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

        // Overload AddPegLabel for color
        private void AddPegLabel(Panel peg, string text, Color color)
        {
            Label label = new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font("Comic Sans MS", 18, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = color,
                BackColor = Color.Transparent
            };
            label.Left = peg.Left + peg.Width / 2 - label.PreferredWidth / 2;
            label.Top = peg.Top + peg.Height + 5;
            label.BringToFront();
            basePanel.Controls.Add(label);
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
                int width = k_DiskMinWidth + i * ((k_DiskMaxWidth - k_DiskMinWidth) / (count - 1));
                Panel disk = new Panel
                {
                    Height = k_DiskHeight,
                    Width = width,
                    BackColor = Color.FromName(selectedColors[i]),
                    BorderStyle = BorderStyle.None,
                    Tag = width,
                    Cursor = Cursors.Hand
                };

                // Add a cute border and shadow effect
                disk.Paint += (s, e) =>
                {
                    var panel = (Panel)s;
                    int radius = panel.Height;
                    using (var shadowBrush = new SolidBrush(Color.FromArgb(60, 255, 182, 193)))
                    {
                        e.Graphics.FillEllipse(shadowBrush, 4, 4, panel.Width - 8, panel.Height - 4);
                    }
                    using (var brush = new SolidBrush(panel.BackColor))
                    using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        path.AddArc(0, 0, radius, radius, 90, 180);
                        path.AddArc(panel.Width - radius, 0, radius, radius, 270, 180);
                        path.CloseFigure();
                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        e.Graphics.FillPath(brush, path);
                        using (var pen = new Pen(Color.Black, 6))
                        {
                            e.Graphics.DrawPath(pen, path);
                        }
                    }
                    //// Add a cute heart emoji in the center
                    //using (var font = new Font("Segoe UI Emoji", 10))
                    //using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    //{
                    //    e.Graphics.DrawString("💗", font, Brushes.DeepPink, panel.Width / 2, panel.Height / 2, format);
                    //}
                };

                disk.MouseDown += Disk_MouseDown;

                r_PegDisks[peg1].Insert(0, disk);
                basePanel.Controls.Add(disk);
                disk.BringToFront();
            }
            UpdateDiskPositions(peg1);
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
                    showMessage("Cannot place a larger disk on a smaller one ❌");
                    return;
                }
            }

            r_PegDisks[sourcePeg].Remove(disk);
            r_PegDisks[targetPeg].Add(disk);

            UpdateDiskPositions(sourcePeg);
            UpdateDiskPositions(targetPeg);

            // Show move label after each move
            string from = GetPegName(sourcePeg);
            string to = GetPegName(targetPeg);
            showMessage($"Moved disk from peg {from} to peg {to}");

            if (r_PegDisks[peg3].Count == m_SelectedColors.Count)
            {
                showMessage("You solved the puzzle!🎉");
            }
        }

        // Helper to get peg name
        private string GetPegName(Panel peg)
        {
            if (peg == peg1) return "A";
            if (peg == peg2) return "B";
            if (peg == peg3) return "C";
            return "?";
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

        private void showMessage(string text, int durationMs = 3000)
        {
            messageLabel.Text = text;
            Timer timer = new Timer { Interval = durationMs };
            timer.Tick += (s, e) =>
            {
                messageLabel.Text = string.Empty;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }
    }
}
