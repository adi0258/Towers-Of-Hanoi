using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Towers_Of_Hanoi
{
    public partial class Level1 : Form
    {
        private readonly List<CheckBox> r_ColorCheckBoxes = new List<CheckBox>();
        private const int k_MaxColors = 3;

        public List<string> SelectedColors { get; private set; } = new List<string>();

        public Level1()
        {
            InitializeComponent(); // Initialize UI components

            confirmButton.Click += ConfirmButton_Click;

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Level 1 - Color Selection";

            InitializeColorCheckboxes();
        }

        private void InitializeColorCheckboxes()
        {
            r_ColorCheckBoxes.AddRange(new[]
            {
                checkBoxRed, checkBoxGreen, checkBoxBlue,
                checkBoxYellow, checkBoxOrange, checkBoxPurple,
                checkBoxBrown, checkBoxPink, checkBoxTurquoise
            });

            foreach (CheckBox cb in r_ColorCheckBoxes)
            {
                cb.CheckedChanged += OnColorCheckedChanged;
            }
        }

        private void OnColorCheckedChanged(object sender, EventArgs e)
        {
            int checkedCount = r_ColorCheckBoxes.Count(cb => cb.Checked);
            if (checkedCount > k_MaxColors)
            {
                MessageBox.Show($"You can only select {k_MaxColors} colors!", "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ((CheckBox)sender).Checked = false;
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            SelectedColors.Clear();

            foreach (CheckBox cb in r_ColorCheckBoxes)
            {
                if (cb.Checked)
                {
                    SelectedColors.Add(cb.Text);
                }
            }

            if (SelectedColors.Count != k_MaxColors)
            {
                MessageBox.Show($"Please select exactly {k_MaxColors} colors.", "Selection Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

