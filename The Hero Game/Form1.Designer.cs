
namespace The_Hero_Game
{
    partial class Form1
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
            this.Down = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.Attack = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.Up = new System.Windows.Forms.Button();
            this.Left = new System.Windows.Forms.Button();
            this.Right = new System.Windows.Forms.Button();
            this.mapLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Down
            // 
            this.Down.Location = new System.Drawing.Point(606, 454);
            this.Down.Name = "Down";
            this.Down.Size = new System.Drawing.Size(83, 31);
            this.Down.TabIndex = 0;
            this.Down.Text = "Down";
            this.Down.UseVisualStyleBackColor = true;
            this.Down.Click += new System.EventHandler(this.DownButton);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(492, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(305, 73);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkOrchid;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.richTextBox3);
            this.panel1.Controls.Add(this.Attack);
            this.panel1.Controls.Add(this.richTextBox2);
            this.panel1.Location = new System.Drawing.Point(492, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 299);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(0, 26);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(305, 23);
            this.comboBox1.TabIndex = 4;
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(9, 65);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(287, 90);
            this.richTextBox3.TabIndex = 3;
            this.richTextBox3.Text = "";
            // 
            // Attack
            // 
            this.Attack.Location = new System.Drawing.Point(0, 173);
            this.Attack.Name = "Attack";
            this.Attack.Size = new System.Drawing.Size(305, 31);
            this.Attack.TabIndex = 3;
            this.Attack.Text = "Attack";
            this.Attack.UseVisualStyleBackColor = true;
            this.Attack.Click += new System.EventHandler(this.Attack_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(9, 220);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(287, 58);
            this.richTextBox2.TabIndex = 3;
            this.richTextBox2.Text = "";
            // 
            // Up
            // 
            this.Up.Location = new System.Drawing.Point(606, 396);
            this.Up.Name = "Up";
            this.Up.Size = new System.Drawing.Size(83, 31);
            this.Up.TabIndex = 3;
            this.Up.Text = "Up";
            this.Up.UseVisualStyleBackColor = true;
            this.Up.Click += new System.EventHandler(this.Upbutton);
            // 
            // Left
            // 
            this.Left.Location = new System.Drawing.Point(492, 418);
            this.Left.Name = "Left";
            this.Left.Size = new System.Drawing.Size(83, 31);
            this.Left.TabIndex = 4;
            this.Left.Text = "Left";
            this.Left.UseVisualStyleBackColor = true;
            this.Left.Click += new System.EventHandler(this.LeftButton);
            // 
            // Right
            // 
            this.Right.Location = new System.Drawing.Point(714, 418);
            this.Right.Name = "Right";
            this.Right.Size = new System.Drawing.Size(83, 31);
            this.Right.TabIndex = 5;
            this.Right.Text = "Right";
            this.Right.UseVisualStyleBackColor = true;
            this.Right.Click += new System.EventHandler(this.RightButton);
            // 
            // mapLabel
            // 
            this.mapLabel.AutoSize = true;
            this.mapLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mapLabel.Location = new System.Drawing.Point(12, 45);
            this.mapLabel.Name = "mapLabel";
            this.mapLabel.Size = new System.Drawing.Size(0, 15);
            this.mapLabel.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 497);
            this.Controls.Add(this.mapLabel);
            this.Controls.Add(this.Right);
            this.Controls.Add(this.Left);
            this.Controls.Add(this.Up);
            this.Controls.Add(this.Down);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Down;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.Button Attack;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button Up;
        private System.Windows.Forms.Button Left;
        private System.Windows.Forms.Button Right;
        public System.Windows.Forms.Label mapLabel;
    }
}

