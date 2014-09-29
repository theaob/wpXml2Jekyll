namespace wpXml2Jekyll
{
    partial class UIForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBoxExtractImages = new System.Windows.Forms.CheckBox();
            this.checkBoxConvertToMarkdown = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 133);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Select XML";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 133);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Save Posts";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBoxExtractImages
            // 
            this.checkBoxExtractImages.AutoSize = true;
            this.checkBoxExtractImages.Location = new System.Drawing.Point(12, 12);
            this.checkBoxExtractImages.Name = "checkBoxExtractImages";
            this.checkBoxExtractImages.Size = new System.Drawing.Size(95, 17);
            this.checkBoxExtractImages.TabIndex = 3;
            this.checkBoxExtractImages.Text = "Extract images";
            this.checkBoxExtractImages.UseVisualStyleBackColor = true;
            // 
            // checkBoxConvertToMarkdown
            // 
            this.checkBoxConvertToMarkdown.AutoSize = true;
            this.checkBoxConvertToMarkdown.Location = new System.Drawing.Point(12, 35);
            this.checkBoxConvertToMarkdown.Name = "checkBoxConvertToMarkdown";
            this.checkBoxConvertToMarkdown.Size = new System.Drawing.Size(128, 17);
            this.checkBoxConvertToMarkdown.TabIndex = 4;
            this.checkBoxConvertToMarkdown.Text = "Convert to Markdown";
            this.checkBoxConvertToMarkdown.UseVisualStyleBackColor = true;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 168);
            this.Controls.Add(this.checkBoxConvertToMarkdown);
            this.Controls.Add(this.checkBoxExtractImages);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "UIForm";
            this.Text = "Wordpress XML to Jekyll";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private readonly PostImporter _postImporter = new PostImporter();
        private System.Windows.Forms.CheckBox checkBoxExtractImages;
        private System.Windows.Forms.CheckBox checkBoxConvertToMarkdown;
    }
}

