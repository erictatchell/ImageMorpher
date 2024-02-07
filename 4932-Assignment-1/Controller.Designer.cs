namespace ImageMorpher
{
    partial class Controller
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
            button1 = new Button();
            Previous = new Button();
            Play = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(130, 12);
            button1.Name = "button1";
            button1.Size = new Size(116, 23);
            button1.TabIndex = 0;
            button1.Text = "Next";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Previous
            // 
            Previous.Location = new Point(12, 12);
            Previous.Name = "Previous";
            Previous.Size = new Size(112, 23);
            Previous.TabIndex = 1;
            Previous.Text = "Previous";
            Previous.UseVisualStyleBackColor = true;
            Previous.Click += Previous_Click;
            // 
            // Play
            // 
            Play.Location = new Point(69, 57);
            Play.Name = "Play";
            Play.Size = new Size(116, 23);
            Play.TabIndex = 2;
            Play.Text = "Play";
            Play.UseVisualStyleBackColor = true;
            Play.Click += Play_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 89);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 3;
            label1.Text = "label1";
            // 
            // Controller
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(258, 138);
            Controls.Add(label1);
            Controls.Add(Play);
            Controls.Add(Previous);
            Controls.Add(button1);
            Name = "Controller";
            StartPosition = FormStartPosition.Manual;
            Text = "Controller";
            Load += Controller_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button Previous;
        private Button Play;
        private Label label1;
    }
}