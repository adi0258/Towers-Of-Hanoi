using System;
using System.Drawing;
using System.Windows.Forms;

namespace Towers_Of_Hanoi
{
    internal partial class GameSetupWindow : Form
    {
        private Button m_StartBtn;
        private Button m_CounterBtn;
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

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Choose Levels";

            UpdateCounterButtonText();

            m_CounterBtn.Font = new Font("Arial", 10, FontStyle.Bold);
            m_StartBtn.Font = new Font("Arial", 10, FontStyle.Bold);
        }

        private void InitializeComponentSetup()
        {
            m_StartBtn = new Button();
            m_CounterBtn = new Button();

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

            // Form
            ClientSize = new Size(359, 210);
            Controls.Add(m_StartBtn);
            Controls.Add(m_CounterBtn);

            ResumeLayout(false);
        }

        private void M_StartBtn_Click(object sender, EventArgs e)
        {
            using (Level1 level1 = new Level1())
            {
                var result = level1.ShowDialog();

                if (result == DialogResult.OK)
                {
                    MainWindow mainWindow = new MainWindow(level1.SelectedColors);
                    Hide();
                    mainWindow.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("You must select exactly 3 colors to start the game.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void M_CounterBtn_Click(object sender, EventArgs e)
        {
            NumberOfLevels++;
        }

        private void UpdateCounterButtonText()
        {
            if (m_CounterBtn != null)
                m_CounterBtn.Text = $"Number of levels: {NumberOfLevels}";
        }
    }
}
