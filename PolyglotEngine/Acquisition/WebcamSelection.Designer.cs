namespace PolyglotFramework.Acquisition
{
    partial class WebcamSelection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webCamComboBox = new System.Windows.Forms.ComboBox();
            this.confirmationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // webCamComboBox
            // 
            this.webCamComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.webCamComboBox.FormattingEnabled = true;
            this.webCamComboBox.Location = new System.Drawing.Point(13, 13);
            this.webCamComboBox.Name = "webCamComboBox";
            this.webCamComboBox.Size = new System.Drawing.Size(198, 21);
            this.webCamComboBox.TabIndex = 0;
            this.webCamComboBox.SelectedIndexChanged += new System.EventHandler(this.webCamComboBox_SelectedIndexChanged);
            // 
            // confirmationButton
            // 
            this.confirmationButton.Enabled = false;
            this.confirmationButton.Location = new System.Drawing.Point(13, 49);
            this.confirmationButton.Name = "confirmationButton";
            this.confirmationButton.Size = new System.Drawing.Size(198, 23);
            this.confirmationButton.TabIndex = 1;
            this.confirmationButton.Text = "Select";
            this.confirmationButton.UseVisualStyleBackColor = true;
            this.confirmationButton.Click += new System.EventHandler(this.confirmationButton_Click);
            // 
            // WebcamSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 84);
            this.Controls.Add(this.confirmationButton);
            this.Controls.Add(this.webCamComboBox);
            this.Name = "WebcamSelection";
            this.Text = "Webcam Selection";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox webCamComboBox;
        private System.Windows.Forms.Button confirmationButton;
    }
}