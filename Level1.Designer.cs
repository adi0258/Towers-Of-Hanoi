namespace Towers_Of_Hanoi
{
    partial class Level1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.CheckBox checkBoxRed;
        private System.Windows.Forms.CheckBox checkBoxGreen;
        private System.Windows.Forms.CheckBox checkBoxBlue;
        private System.Windows.Forms.CheckBox checkBoxYellow;
        private System.Windows.Forms.CheckBox checkBoxOrange;
        private System.Windows.Forms.CheckBox checkBoxPurple;
        private System.Windows.Forms.CheckBox checkBoxBrown;
        private System.Windows.Forms.CheckBox checkBoxPink;
        private System.Windows.Forms.CheckBox checkBoxTurquoise;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.confirmButton = new System.Windows.Forms.Button();
            this.checkBoxRed = new System.Windows.Forms.CheckBox();
            this.checkBoxGreen = new System.Windows.Forms.CheckBox();
            this.checkBoxBlue = new System.Windows.Forms.CheckBox();
            this.checkBoxYellow = new System.Windows.Forms.CheckBox();
            this.checkBoxOrange = new System.Windows.Forms.CheckBox();
            this.checkBoxPurple = new System.Windows.Forms.CheckBox();
            this.checkBoxBrown = new System.Windows.Forms.CheckBox();
            this.checkBoxPink = new System.Windows.Forms.CheckBox();
            this.checkBoxTurquoise = new System.Windows.Forms.CheckBox();

            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(90, 240);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(100, 30);
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;

            // Position and setup checkboxes
            int startX = 30, startY = 20, gapY = 25;
            void SetupCheckBox(System.Windows.Forms.CheckBox cb, string text, int idx)
            {
                cb.Text = text;
                cb.AutoSize = true;
                cb.Location = new System.Drawing.Point(startX, startY + gapY * idx);
            }

            SetupCheckBox(checkBoxRed, "Red", 0);
            SetupCheckBox(checkBoxGreen, "Green", 1);
            SetupCheckBox(checkBoxBlue, "Blue", 2);
            SetupCheckBox(checkBoxYellow, "Yellow", 3);
            SetupCheckBox(checkBoxOrange, "Orange", 4);
            SetupCheckBox(checkBoxPurple, "Purple", 5);
            SetupCheckBox(checkBoxBrown, "Brown", 6);
            SetupCheckBox(checkBoxPink, "Pink", 7);
            SetupCheckBox(checkBoxTurquoise, "Turquoise", 8);

            // Add controls to the form
            this.ClientSize = new System.Drawing.Size(250, 300);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                confirmButton,
                checkBoxRed, checkBoxGreen, checkBoxBlue, checkBoxYellow, checkBoxOrange,
                checkBoxPurple, checkBoxBrown, checkBoxPink, checkBoxTurquoise
            });

            this.Name = "Level1";
            this.Text = "Select Colors";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
