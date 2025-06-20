using System;
using System.Drawing;
using System.Windows.Forms;

namespace Towers_Of_Hanoi
{
    internal partial class GameSetupWindow : Form
    {
        private Button m_StartBtn;
        private Button m_CounterBtn;
        private Button m_InstructionsBtn; 
        private int m_NumberOfLevels = 1;

        public int NumberOfLevels
        {
            get => m_NumberOfLevels;
            set
            {
                if (value < 1 || value > 3)
                    m_NumberOfLevels = 1;
                else
                    m_NumberOfLevels = value;
                UpdateCounterButtonText();
            }
        }

        public GameSetupWindow()
        {
            InitializeComponentSetup();

            // Cute & girlish style
            this.BackColor = Color.MistyRose;
            this.Font = new Font("Comic Sans MS", 11, FontStyle.Bold);

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Choose Your Level! ✨";

            // Add a cute title label
            Label titleLabel = new Label
            {
                Text = "🌸 Towers of Hanoi 🌸",
                AutoSize = false,
                Width = 359,
                Height = 40,
                Top = 5,
                Left = 0,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Comic Sans MS", 18, FontStyle.Bold),
                ForeColor = Color.HotPink,
                BackColor = Color.Transparent
            };
            Controls.Add(titleLabel);
            titleLabel.BringToFront();

            UpdateCounterButtonText();

            m_CounterBtn.Font = new Font("Comic Sans MS", 10, FontStyle.Bold);
            m_CounterBtn.ForeColor = Color.MediumVioletRed;
            m_CounterBtn.BackColor = Color.LavenderBlush;

            m_StartBtn.Font = new Font("Comic Sans MS", 12, FontStyle.Bold);
            m_StartBtn.ForeColor = Color.White;
            m_StartBtn.BackColor = Color.HotPink;
            m_StartBtn.FlatStyle = FlatStyle.Flat;
            m_StartBtn.FlatAppearance.BorderColor = Color.DeepPink;
            m_StartBtn.FlatAppearance.BorderSize = 2;

            m_InstructionsBtn.Font = new Font("Comic Sans MS", 12, FontStyle.Bold);
            m_InstructionsBtn.ForeColor = Color.White;
            m_InstructionsBtn.BackColor = Color.MediumVioletRed;
            m_InstructionsBtn.FlatStyle = FlatStyle.Flat;
            m_InstructionsBtn.FlatAppearance.BorderColor = Color.HotPink;
            m_InstructionsBtn.FlatAppearance.BorderSize = 2;
        }

        private void InitializeComponentSetup()
        {
            m_StartBtn = new Button();
            m_CounterBtn = new Button();
            m_InstructionsBtn = new Button();

            SuspendLayout();

            // Start Button
            m_StartBtn.Location = new Point(124, 134);
            m_StartBtn.Name = "m_StartBtn";
            m_StartBtn.Size = new Size(108, 37);
            m_StartBtn.Text = "Start";
            m_StartBtn.UseVisualStyleBackColor = true;
            m_StartBtn.Click += M_StartBtn_Click;

            // Counter Button
            m_CounterBtn.Location = new Point(28, 51);
            m_CounterBtn.Name = "m_CounterBtn";
            m_CounterBtn.Size = new Size(300, 23);
            m_CounterBtn.UseVisualStyleBackColor = true;
            m_CounterBtn.Click += M_CounterBtn_Click;

            // Instructions Button
            m_InstructionsBtn.Location = new Point(124, 180);
            m_InstructionsBtn.Name = "m_InstructionsBtn";
            m_InstructionsBtn.Size = new Size(108, 30);
            m_InstructionsBtn.Text = "Instructions";
            m_InstructionsBtn.UseVisualStyleBackColor = true;
            m_InstructionsBtn.Click += M_InstructionsBtn_Click;

            // Form
            ClientSize = new Size(359, 230);
            Controls.Add(m_StartBtn);
            Controls.Add(m_CounterBtn);
            Controls.Add(m_InstructionsBtn);

            ResumeLayout(false);
        }

        private void M_StartBtn_Click(object sender, EventArgs e)
        {
            int diskCount = 3; // Default for level 1
            if (NumberOfLevels == 2)
                diskCount = 6;
            else if (NumberOfLevels == 3)
                diskCount = 8;

            using (ColorChoiceForm colorForm = new ColorChoiceForm(diskCount))
            {
                var result = colorForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    MainWindow mainWindow = new MainWindow(colorForm.SelectedColors);
                    Hide();
                    mainWindow.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show($"You must select exactly {diskCount} colors to start the game.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void M_CounterBtn_Click(object sender, EventArgs e)
        {
            NumberOfLevels++;
        }

        private void M_InstructionsBtn_Click(object sender, EventArgs e)
        {
            using (Instructions instructions = new Instructions())
            {
                instructions.ShowDialog();
            }
        }

        private void UpdateCounterButtonText()
        {
            if (m_CounterBtn != null)
                m_CounterBtn.Text = $"Number of level: {NumberOfLevels}";
        }
    }
}
