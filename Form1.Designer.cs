namespace IpseityLauncher
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_LaunchIpseity = new System.Windows.Forms.Button();
            this.textBox_RootFolder = new System.Windows.Forms.TextBox();
            this.textBox_PrologFolder = new System.Windows.Forms.TextBox();
            this.label_IpseityRootFolder = new System.Windows.Forms.Label();
            this.label_PrologFolder = new System.Windows.Forms.Label();
            this.textBox_Status = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_LaunchIpseity
            // 
            this.button_LaunchIpseity.Location = new System.Drawing.Point(387, 65);
            this.button_LaunchIpseity.Name = "button_LaunchIpseity";
            this.button_LaunchIpseity.Size = new System.Drawing.Size(103, 23);
            this.button_LaunchIpseity.TabIndex = 0;
            this.button_LaunchIpseity.Text = "Launch Ipseity";
            this.button_LaunchIpseity.UseVisualStyleBackColor = true;
            this.button_LaunchIpseity.Click += new System.EventHandler(this.button_LaunchIpseity_Click);
            // 
            // textBox_RootFolder
            // 
            this.textBox_RootFolder.Location = new System.Drawing.Point(114, 10);
            this.textBox_RootFolder.Name = "textBox_RootFolder";
            this.textBox_RootFolder.Size = new System.Drawing.Size(376, 20);
            this.textBox_RootFolder.TabIndex = 1;
            this.textBox_RootFolder.Text = "C:\\Program Files (x86)\\ipseity_1-2-2_kernel_bin_win";
            // 
            // textBox_PrologFolder
            // 
            this.textBox_PrologFolder.Location = new System.Drawing.Point(114, 39);
            this.textBox_PrologFolder.Name = "textBox_PrologFolder";
            this.textBox_PrologFolder.Size = new System.Drawing.Size(376, 20);
            this.textBox_PrologFolder.TabIndex = 2;
            this.textBox_PrologFolder.Text = "C:\\Program Files (x86)\\pl";
            // 
            // label_IpseityRootFolder
            // 
            this.label_IpseityRootFolder.AutoSize = true;
            this.label_IpseityRootFolder.Location = new System.Drawing.Point(12, 10);
            this.label_IpseityRootFolder.Name = "label_IpseityRootFolder";
            this.label_IpseityRootFolder.Size = new System.Drawing.Size(101, 13);
            this.label_IpseityRootFolder.TabIndex = 3;
            this.label_IpseityRootFolder.Text = "Ipseity Root Folder :";
            // 
            // label_PrologFolder
            // 
            this.label_PrologFolder.AutoSize = true;
            this.label_PrologFolder.Location = new System.Drawing.Point(12, 39);
            this.label_PrologFolder.Name = "label_PrologFolder";
            this.label_PrologFolder.Size = new System.Drawing.Size(75, 13);
            this.label_PrologFolder.TabIndex = 4;
            this.label_PrologFolder.Text = "Prolog Folder :";
            // 
            // textBox_Status
            // 
            this.textBox_Status.Location = new System.Drawing.Point(114, 68);
            this.textBox_Status.Name = "textBox_Status";
            this.textBox_Status.ReadOnly = true;
            this.textBox_Status.Size = new System.Drawing.Size(267, 20);
            this.textBox_Status.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Status :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 100);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Status);
            this.Controls.Add(this.label_PrologFolder);
            this.Controls.Add(this.label_IpseityRootFolder);
            this.Controls.Add(this.textBox_PrologFolder);
            this.Controls.Add(this.textBox_RootFolder);
            this.Controls.Add(this.button_LaunchIpseity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "IpseityLauncher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_LaunchIpseity;
        private System.Windows.Forms.TextBox textBox_RootFolder;
        private System.Windows.Forms.TextBox textBox_PrologFolder;
        private System.Windows.Forms.Label label_IpseityRootFolder;
        private System.Windows.Forms.Label label_PrologFolder;
        private System.Windows.Forms.TextBox textBox_Status;
        private System.Windows.Forms.Label label1;
    }
}

