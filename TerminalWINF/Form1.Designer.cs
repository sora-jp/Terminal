namespace Terminal
{
    partial class Form1
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
            this.console = new System.Windows.Forms.RichTextBox();
            this.padding = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // console
            // 
            this.console.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.console.BackColor = System.Drawing.Color.Black;
            this.console.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.console.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.console.ForeColor = System.Drawing.Color.White;
            this.console.Location = new System.Drawing.Point(10, 27);
            this.console.Name = "console";
            this.console.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.console.Size = new System.Drawing.Size(741, 499);
            this.console.TabIndex = 0;
            this.console.Text = "";
            // 
            // padding
            // 
            this.padding.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.padding.BackColor = System.Drawing.Color.Black;
            this.padding.Location = new System.Drawing.Point(7, 27);
            this.padding.Name = "padding";
            this.padding.Size = new System.Drawing.Size(31, 499);
            this.padding.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 533);
            this.Controls.Add(this.console);
            this.Controls.Add(this.padding);
            this.Name = "Form1";
            this.Style = MetroFramework.MetroColorStyle.Black;
            this.Text = "Terminal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox console;
        private System.Windows.Forms.Panel padding;
    }
}

