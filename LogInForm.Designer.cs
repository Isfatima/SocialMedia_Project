namespace socialmedia
{
    partial class LogInForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.UsernameTextBoxLogIn = new System.Windows.Forms.TextBox();
            this.PasswordTextBoxLogIn = new System.Windows.Forms.TextBox();
            this.LogIn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Sign_in_link_lable = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(247, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Username:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(249, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Password:";
            // 
            // UsernameTextBoxLogIn
            // 
            this.UsernameTextBoxLogIn.Location = new System.Drawing.Point(330, 81);
            this.UsernameTextBoxLogIn.Name = "UsernameTextBoxLogIn";
            this.UsernameTextBoxLogIn.Size = new System.Drawing.Size(183, 20);
            this.UsernameTextBoxLogIn.TabIndex = 2;
            // 
            // PasswordTextBoxLogIn
            // 
            this.PasswordTextBoxLogIn.Location = new System.Drawing.Point(330, 124);
            this.PasswordTextBoxLogIn.Name = "PasswordTextBoxLogIn";
            this.PasswordTextBoxLogIn.Size = new System.Drawing.Size(183, 20);
            this.PasswordTextBoxLogIn.TabIndex = 3;
            this.PasswordTextBoxLogIn.UseSystemPasswordChar = true;
            // 
            // LogIn
            // 
            this.LogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogIn.Location = new System.Drawing.Point(363, 179);
            this.LogIn.Name = "LogIn";
            this.LogIn.Size = new System.Drawing.Size(110, 33);
            this.LogIn.TabIndex = 4;
            this.LogIn.Text = "Log in ";
            this.LogIn.UseVisualStyleBackColor = true;
            this.LogIn.Click += new System.EventHandler(this.LogIn_Click_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(311, 243);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Don\'t have an account? ";
            // 
            // Sign_in_link_lable
            // 
            this.Sign_in_link_lable.AutoSize = true;
            this.Sign_in_link_lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sign_in_link_lable.Location = new System.Drawing.Point(466, 243);
            this.Sign_in_link_lable.Name = "Sign_in_link_lable";
            this.Sign_in_link_lable.Size = new System.Drawing.Size(47, 16);
            this.Sign_in_link_lable.TabIndex = 6;
            this.Sign_in_link_lable.TabStop = true;
            this.Sign_in_link_lable.Text = "Sign in";
            this.Sign_in_link_lable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::socialmedia.Properties.Resources._26fab505c68205a74af7681b21335ea3_computer_isometric;
            this.pictureBox1.Location = new System.Drawing.Point(41, 81);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(182, 206);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // LogInForm
            // 
            this.ClientSize = new System.Drawing.Size(661, 428);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Sign_in_link_lable);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LogIn);
            this.Controls.Add(this.PasswordTextBoxLogIn);
            this.Controls.Add(this.UsernameTextBoxLogIn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Name = "LogInForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log in";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox UsernameTextBoxLogIn;
        private System.Windows.Forms.TextBox PasswordTextBoxLogIn;
        private System.Windows.Forms.Button LogIn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel Sign_in_link_lable;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

