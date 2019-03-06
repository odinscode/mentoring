namespace GreetingsSolution
{
    partial class GreetingForm
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
            this.GreetingButton = new System.Windows.Forms.Button();
            this.EnterNameLabel = new System.Windows.Forms.Label();
            this.EnterNameTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // GreetingButton
            // 
            this.GreetingButton.Location = new System.Drawing.Point(87, 67);
            this.GreetingButton.Name = "GreetingButton";
            this.GreetingButton.Size = new System.Drawing.Size(98, 23);
            this.GreetingButton.TabIndex = 0;
            this.GreetingButton.Text = "Welcome me!";
            this.GreetingButton.UseVisualStyleBackColor = true;
            this.GreetingButton.Click += new System.EventHandler(this.GreetingButton_Click);
            // 
            // EnterNameLabel
            // 
            this.EnterNameLabel.AutoSize = true;
            this.EnterNameLabel.Location = new System.Drawing.Point(13, 13);
            this.EnterNameLabel.Name = "EnterNameLabel";
            this.EnterNameLabel.Size = new System.Drawing.Size(89, 13);
            this.EnterNameLabel.TabIndex = 1;
            this.EnterNameLabel.Text = "Enter your Name:";
            // 
            // EnterNameTextbox
            // 
            this.EnterNameTextbox.Location = new System.Drawing.Point(13, 30);
            this.EnterNameTextbox.Name = "EnterNameTextbox";
            this.EnterNameTextbox.Size = new System.Drawing.Size(259, 20);
            this.EnterNameTextbox.TabIndex = 2;
            // 
            // GreetingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 102);
            this.Controls.Add(this.EnterNameTextbox);
            this.Controls.Add(this.EnterNameLabel);
            this.Controls.Add(this.GreetingButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GreetingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Greetings Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GreetingButton;
        private System.Windows.Forms.Label EnterNameLabel;
        private System.Windows.Forms.TextBox EnterNameTextbox;
    }
}

