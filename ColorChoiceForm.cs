using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Towers_Of_Hanoi
{
    public partial class ColorChoiceForm : Form
    {
        private readonly List<CheckBox> r_ColorCheckBoxes = new List<CheckBox>();
        private int k_MaxColors;
        private Label labelSelectedCount;

        public List<string> SelectedColors { get; private set; } = new List<string>();

        public ColorChoiceForm(int maxColors)
        {
            this.ShowInTaskbar= false;
            k_MaxColors = maxColors;
            InitializeComponent();

            labelSelectedCount = new Label
            {
                AutoSize = true,
                Font = new System.Drawing.Font("Comic Sans MS", 10, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.MediumVioletRed,
                Top = 10,
                Left = 10,
                Text = $"Selected: 0 / {k_MaxColors}"
            };
            Controls.Add(labelSelectedCount);

            confirmButton.Click += ConfirmButton_Click;

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = $"Level - Color Selection ({k_MaxColors} colors)";

            InitializeColorCheckboxes();
            UpdateSelectedCountLabel();
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
            UpdateSelectedCountLabel();
        }

        private void UpdateSelectedCountLabel()
        {
            int checkedCount = r_ColorCheckBoxes.Count(cb => cb.Checked);
            labelSelectedCount.Text = $"Selected: {checkedCount} / {k_MaxColors}";
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

