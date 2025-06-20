using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Towers_Of_Hanoi
{
    public partial class Instructions : Form
    {
        public Instructions()
        {
            InitializeComponent();
            InitializeCuteInstructions();
        }

        private void InitializeCuteInstructions()
        {
            this.BackColor = Color.MistyRose;

            Label titleLabel = new Label
            {
                Text = "🌸 How to Play Towers of Hanoi 🌸",
                Font = new Font("Comic Sans MS", 18, FontStyle.Bold),
                ForeColor = Color.HotPink,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 50
            };
            this.Controls.Add(titleLabel);

            Label step1 = new Label
            {
                Text = "1. Move all the disks from the left peg to the right peg! 💖",
                Font = new Font("Comic Sans MS", 12, FontStyle.Regular),
                ForeColor = Color.MediumVioletRed,
                AutoSize = false,
                Width = 500,
                Height = 40,
                Top = 60,
                Left = 30
            };
            this.Controls.Add(step1);

            Label step2 = new Label
            {
                Text = "2. Only one disk can be moved at a time. ✨",
                Font = new Font("Comic Sans MS", 12, FontStyle.Regular),
                ForeColor = Color.MediumVioletRed,
                AutoSize = false,
                Width = 400,
                Height = 40,
                Top = 100,
                Left = 30
            };
            this.Controls.Add(step2);

            Label step3 = new Label
            {
                Text = "3. Never place a bigger disk on a smaller one! 🚫",
                Font = new Font("Comic Sans MS", 12, FontStyle.Regular),
                ForeColor = Color.MediumVioletRed,
                AutoSize = false,
                Width = 400,
                Height = 40,
                Top = 140,
                Left = 30
            };
            this.Controls.Add(step3);

            Label step4 = new Label
            {
                Text = "4. Try to solve it in the fewest moves possible. Good luck, princess! 👑",
                Font = new Font("Comic Sans MS", 12, FontStyle.Regular),
                ForeColor = Color.MediumVioletRed,
                AutoSize = false,
                Width = 500,
                Height = 40,
                Top = 180,
                Left = 30
            };
            this.Controls.Add(step4);
        }
    }
}
