using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolyglotFramework
{
    public partial class ModuleForm : Form
    {

        public EventHandler<ModuleFormClosedEventArgs> ModuleFormClosed;

        protected bool closing = false;

        public ModuleForm()
        {
            InitializeComponent();
        }

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

        private void ModuleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.closing = true;
            this.ModuleFormClosed(this, new ModuleFormClosedEventArgs());
        }

        new public void Close()
        {
            if(!this.closing) base.Close();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ModuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "ModuleForm";
            this.Text = "ModuleForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ModuleForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

    }
}
