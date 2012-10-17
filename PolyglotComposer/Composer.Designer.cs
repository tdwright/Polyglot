namespace PolyglotFramework
{
    partial class PolyglotComposer
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
            this.ModuleSelectionGroup = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.outputModuleLabel = new System.Windows.Forms.Label();
            this.outputComboBox = new System.Windows.Forms.ComboBox();
            this.pointerModuleLabel = new System.Windows.Forms.Label();
            this.transformationComboBox = new System.Windows.Forms.ComboBox();
            this.transformationModuleLabel = new System.Windows.Forms.Label();
            this.pointerComboBox = new System.Windows.Forms.ComboBox();
            this.acquisitionComboBox = new System.Windows.Forms.ComboBox();
            this.acquisitionModuleLabel = new System.Windows.Forms.Label();
            this.UseTheseButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getinstallPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModuleSelectionGroup.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ModuleSelectionGroup
            // 
            this.ModuleSelectionGroup.Controls.Add(this.tableLayoutPanel1);
            this.ModuleSelectionGroup.Controls.Add(this.UseTheseButton);
            this.ModuleSelectionGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModuleSelectionGroup.Location = new System.Drawing.Point(0, 24);
            this.ModuleSelectionGroup.Margin = new System.Windows.Forms.Padding(10);
            this.ModuleSelectionGroup.Name = "ModuleSelectionGroup";
            this.ModuleSelectionGroup.Size = new System.Drawing.Size(284, 238);
            this.ModuleSelectionGroup.TabIndex = 5;
            this.ModuleSelectionGroup.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.outputModuleLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.outputComboBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.pointerModuleLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.transformationComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.transformationModuleLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pointerComboBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.acquisitionComboBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.acquisitionModuleLabel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(278, 196);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // outputModuleLabel
            // 
            this.outputModuleLabel.AutoSize = true;
            this.outputModuleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputModuleLabel.Location = new System.Drawing.Point(3, 147);
            this.outputModuleLabel.Name = "outputModuleLabel";
            this.outputModuleLabel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.outputModuleLabel.Size = new System.Drawing.Size(133, 49);
            this.outputModuleLabel.TabIndex = 7;
            this.outputModuleLabel.Text = "Output module";
            this.outputModuleLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // outputComboBox
            // 
            this.outputComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputComboBox.FormattingEnabled = true;
            this.outputComboBox.Location = new System.Drawing.Point(142, 155);
            this.outputComboBox.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.outputComboBox.Name = "outputComboBox";
            this.outputComboBox.Size = new System.Drawing.Size(133, 21);
            this.outputComboBox.TabIndex = 12;
            this.outputComboBox.SelectedIndexChanged += new System.EventHandler(this.RecheckStuff);
            // 
            // pointerModuleLabel
            // 
            this.pointerModuleLabel.AutoSize = true;
            this.pointerModuleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pointerModuleLabel.Location = new System.Drawing.Point(3, 49);
            this.pointerModuleLabel.Name = "pointerModuleLabel";
            this.pointerModuleLabel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pointerModuleLabel.Size = new System.Drawing.Size(133, 49);
            this.pointerModuleLabel.TabIndex = 9;
            this.pointerModuleLabel.Text = "Pointer module";
            this.pointerModuleLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // transformationComboBox
            // 
            this.transformationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transformationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.transformationComboBox.FormattingEnabled = true;
            this.transformationComboBox.Location = new System.Drawing.Point(142, 106);
            this.transformationComboBox.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.transformationComboBox.Name = "transformationComboBox";
            this.transformationComboBox.Size = new System.Drawing.Size(133, 21);
            this.transformationComboBox.TabIndex = 11;
            this.transformationComboBox.SelectedIndexChanged += new System.EventHandler(this.RecheckStuff);
            // 
            // transformationModuleLabel
            // 
            this.transformationModuleLabel.AutoSize = true;
            this.transformationModuleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transformationModuleLabel.Location = new System.Drawing.Point(3, 98);
            this.transformationModuleLabel.Name = "transformationModuleLabel";
            this.transformationModuleLabel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.transformationModuleLabel.Size = new System.Drawing.Size(133, 49);
            this.transformationModuleLabel.TabIndex = 8;
            this.transformationModuleLabel.Text = "Transformation module";
            this.transformationModuleLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pointerComboBox
            // 
            this.pointerComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pointerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pointerComboBox.FormattingEnabled = true;
            this.pointerComboBox.Location = new System.Drawing.Point(142, 57);
            this.pointerComboBox.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.pointerComboBox.Name = "pointerComboBox";
            this.pointerComboBox.Size = new System.Drawing.Size(133, 21);
            this.pointerComboBox.TabIndex = 6;
            this.pointerComboBox.SelectedIndexChanged += new System.EventHandler(this.RecheckStuff);
            // 
            // acquisitionComboBox
            // 
            this.acquisitionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.acquisitionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.acquisitionComboBox.FormattingEnabled = true;
            this.acquisitionComboBox.Location = new System.Drawing.Point(142, 8);
            this.acquisitionComboBox.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.acquisitionComboBox.Name = "acquisitionComboBox";
            this.acquisitionComboBox.Size = new System.Drawing.Size(133, 21);
            this.acquisitionComboBox.TabIndex = 5;
            this.acquisitionComboBox.SelectedIndexChanged += new System.EventHandler(this.RecheckStuff);
            // 
            // acquisitionModuleLabel
            // 
            this.acquisitionModuleLabel.AutoSize = true;
            this.acquisitionModuleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acquisitionModuleLabel.Location = new System.Drawing.Point(3, 0);
            this.acquisitionModuleLabel.Name = "acquisitionModuleLabel";
            this.acquisitionModuleLabel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.acquisitionModuleLabel.Size = new System.Drawing.Size(133, 49);
            this.acquisitionModuleLabel.TabIndex = 10;
            this.acquisitionModuleLabel.Text = "Acquisition module";
            this.acquisitionModuleLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // UseTheseButton
            // 
            this.UseTheseButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.UseTheseButton.Enabled = false;
            this.UseTheseButton.Location = new System.Drawing.Point(3, 212);
            this.UseTheseButton.Name = "UseTheseButton";
            this.UseTheseButton.Size = new System.Drawing.Size(278, 23);
            this.UseTheseButton.TabIndex = 13;
            this.UseTheseButton.Text = "Use these modules";
            this.UseTheseButton.UseVisualStyleBackColor = true;
            this.UseTheseButton.Click += new System.EventHandler(this.UseTheseButton_click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.resetToolStripMenuItem.Text = "&Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+L";
            this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.loadToolStripMenuItem.Text = "&Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "Alt+F4";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.getinstallPathToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // getinstallPathToolStripMenuItem
            // 
            this.getinstallPathToolStripMenuItem.Name = "getinstallPathToolStripMenuItem";
            this.getinstallPathToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.getinstallPathToolStripMenuItem.Text = "Get &install path";
            // 
            // PolyglotComposer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.ModuleSelectionGroup);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(240, 235);
            this.Name = "PolyglotComposer";
            this.Text = "Polyglot Composer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PolyglotComposer_FormClosing);
            this.Load += new System.EventHandler(this.PolyglotComposer_Load);
            this.ModuleSelectionGroup.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox ModuleSelectionGroup;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label outputModuleLabel;
        private System.Windows.Forms.ComboBox outputComboBox;
        private System.Windows.Forms.Label pointerModuleLabel;
        private System.Windows.Forms.ComboBox transformationComboBox;
        private System.Windows.Forms.Label transformationModuleLabel;
        private System.Windows.Forms.ComboBox pointerComboBox;
        private System.Windows.Forms.ComboBox acquisitionComboBox;
        private System.Windows.Forms.Label acquisitionModuleLabel;
        private System.Windows.Forms.Button UseTheseButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getinstallPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;


    }
}

