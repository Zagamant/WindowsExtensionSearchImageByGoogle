namespace WindowsExtensionSearchImageByGoogle
{
    partial class InstallerForm
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
            this.contextMenuTitleLabel = new System.Windows.Forms.Label();
            this.menuTitleTextBox = new System.Windows.Forms.TextBox();
            this.includeFileNameCheckBox = new System.Windows.Forms.CheckBox();
            this.resizeOnUploadCheckbox = new System.Windows.Forms.CheckBox();
            this.installForAllUsersCheckBox = new System.Windows.Forms.CheckBox();
            this.fileTypesLabel = new System.Windows.Forms.Label();
            this.installButton = new System.Windows.Forms.Button();
            this.uninstallButton = new System.Windows.Forms.Button();
            this.fileTypesListBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // contextMenuTitleLabel
            // 
            this.contextMenuTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.contextMenuTitleLabel.Location = new System.Drawing.Point(14, 35);
            this.contextMenuTitleLabel.Name = "contextMenuTitleLabel";
            this.contextMenuTitleLabel.Size = new System.Drawing.Size(109, 14);
            this.contextMenuTitleLabel.TabIndex = 0;
            this.contextMenuTitleLabel.Text = "Context menu title";
            // 
            // menuTitleTextBox
            // 
            this.menuTitleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.menuTitleTextBox.Location = new System.Drawing.Point(129, 32);
            this.menuTitleTextBox.Name = "menuTitleTextBox";
            this.menuTitleTextBox.Size = new System.Drawing.Size(241, 20);
            this.menuTitleTextBox.TabIndex = 1;
            this.menuTitleTextBox.Text = "Search on Google Images";
            // 
            // includeFileNameCheckBox
            // 
            this.includeFileNameCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.includeFileNameCheckBox.Location = new System.Drawing.Point(14, 58);
            this.includeFileNameCheckBox.Name = "includeFileNameCheckBox";
            this.includeFileNameCheckBox.Size = new System.Drawing.Size(356, 25);
            this.includeFileNameCheckBox.TabIndex = 5;
            this.includeFileNameCheckBox.Text = "Include file name in search";
            this.includeFileNameCheckBox.UseVisualStyleBackColor = true;
            // 
            // resizeOnUploadCheckbox
            // 
            this.resizeOnUploadCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.resizeOnUploadCheckbox.Location = new System.Drawing.Point(14, 89);
            this.resizeOnUploadCheckbox.Name = "resizeOnUploadCheckbox";
            this.resizeOnUploadCheckbox.Size = new System.Drawing.Size(356, 25);
            this.resizeOnUploadCheckbox.TabIndex = 6;
            this.resizeOnUploadCheckbox.Text = "Resize large images before uploading";
            this.resizeOnUploadCheckbox.UseVisualStyleBackColor = true;
            // 
            // installForAllUsersCheckBox
            // 
            this.installForAllUsersCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.installForAllUsersCheckBox.Location = new System.Drawing.Point(14, 120);
            this.installForAllUsersCheckBox.Name = "installForAllUsersCheckBox";
            this.installForAllUsersCheckBox.Size = new System.Drawing.Size(356, 35);
            this.installForAllUsersCheckBox.TabIndex = 7;
            this.installForAllUsersCheckBox.Text = "Install/uninstall for all users (requires administrator privileges)";
            this.installForAllUsersCheckBox.UseVisualStyleBackColor = true;
            // 
            // fileTypesLabel
            // 
            this.fileTypesLabel.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.fileTypesLabel.Location = new System.Drawing.Point(14, 158);
            this.fileTypesLabel.Name = "fileTypesLabel";
            this.fileTypesLabel.Size = new System.Drawing.Size(356, 18);
            this.fileTypesLabel.TabIndex = 8;
            this.fileTypesLabel.Text = "Install/uninstall for these file types:";
            // 
            // installButton
            // 
            this.installButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.installButton.Location = new System.Drawing.Point(12, 254);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(175, 40);
            this.installButton.TabIndex = 10;
            this.installButton.Text = "Install";
            this.installButton.UseVisualStyleBackColor = true;
            this.installButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // uninstallButton
            // 
            this.uninstallButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uninstallButton.Location = new System.Drawing.Point(195, 254);
            this.uninstallButton.Name = "uninstallButton";
            this.uninstallButton.Size = new System.Drawing.Size(175, 40);
            this.uninstallButton.TabIndex = 11;
            this.uninstallButton.Text = "Uninstall";
            this.uninstallButton.UseVisualStyleBackColor = true;
            this.uninstallButton.Click += new System.EventHandler(this.uninstallButton_Click);
            // 
            // fileTypesListBox
            // 
            this.fileTypesListBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.fileTypesListBox.CheckOnClick = true;
            this.fileTypesListBox.FormattingEnabled = true;
            this.fileTypesListBox.Location = new System.Drawing.Point(14, 179);
            this.fileTypesListBox.Name = "fileTypesListBox";
            this.fileTypesListBox.Size = new System.Drawing.Size(356, 64);
            this.fileTypesListBox.TabIndex = 12;
            // 
            // InstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 306);
            this.Controls.Add(this.fileTypesListBox);
            this.Controls.Add(this.uninstallButton);
            this.Controls.Add(this.installButton);
            this.Controls.Add(this.fileTypesLabel);
            this.Controls.Add(this.installForAllUsersCheckBox);
            this.Controls.Add(this.resizeOnUploadCheckbox);
            this.Controls.Add(this.includeFileNameCheckBox);
            this.Controls.Add(this.menuTitleTextBox);
            this.Controls.Add(this.contextMenuTitleLabel);
            this.Name = "InstallerForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.InstallerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.CheckedListBox fileTypesListBox;

        private System.Windows.Forms.TextBox menuTitleTextBox;

        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.Button uninstallButton;

        private System.Windows.Forms.Label fileTypesLabel;

        private System.Windows.Forms.CheckBox installForAllUsersCheckBox;

        private System.Windows.Forms.CheckBox resizeOnUploadCheckbox;

        private System.Windows.Forms.CheckBox includeFileNameCheckBox;

        private System.Windows.Forms.Label contextMenuTitleLabel;

        #endregion
    }
}