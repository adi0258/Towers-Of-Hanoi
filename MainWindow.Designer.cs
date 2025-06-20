namespace Towers_Of_Hanoi
{
    partial class MainWindow
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainWindow
            // 
            this.ClientSize = new System.Drawing.Size(700, 450);
            this.Name = "MainWindow";
            this.Text = "Towers of Hanoi - Game";
            this.ResumeLayout(false);
        }
    }
}
