namespace GlassBlowMould
{
    partial class PartGlobalVariables
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            openFileDialog = new OpenFileDialog();
            OpenButton = new Button();
            rebuildButton = new Button();
            filePathTextBox = new TextBox();
            SuspendLayout();
            // 
            // openFileDialog
            // 
            openFileDialog.Filter = "SW Part(*.SLDPRT)|*.SLDPRT";
            openFileDialog.Title = "Open SolidWorks Part File";
            // 
            // OpenButton
            // 
            OpenButton.Location = new Point(457, 89);
            OpenButton.Name = "OpenButton";
            OpenButton.Size = new Size(101, 32);
            OpenButton.TabIndex = 0;
            OpenButton.Text = "Open Part File";
            OpenButton.UseMnemonic = false;
            OpenButton.UseVisualStyleBackColor = true;
            OpenButton.Click += OpenSolidWorksPartFile_Click;
            // 
            // rebuildButton
            // 
            rebuildButton.Location = new Point(616, 89);
            rebuildButton.Name = "rebuildButton";
            rebuildButton.Size = new Size(101, 32);
            rebuildButton.TabIndex = 1;
            rebuildButton.Text = "Rebuild";
            rebuildButton.UseVisualStyleBackColor = true;
            rebuildButton.Click += RebuildButton_Click;
            // 
            // filePathTextBox
            // 
            filePathTextBox.Location = new Point(354, 34);
            filePathTextBox.Name = "filePathTextBox";
            filePathTextBox.Size = new Size(434, 23);
            filePathTextBox.TabIndex = 2;
            // 
            // PartGlobalVariables
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(filePathTextBox);
            Controls.Add(rebuildButton);
            Controls.Add(OpenButton);
            Name = "PartGlobalVariables";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Part Global Variables";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog;
        private Button OpenButton;
        private Button rebuildButton;
        private TextBox filePathTextBox;
    }
}
