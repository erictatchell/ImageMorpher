namespace ImageMorpher
{
    partial class Parent
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
            menu_strip = new MenuStrip();
            menu_file = new ToolStripMenuItem();
            morphingToolStripMenuItem = new ToolStripMenuItem();
            beginToolStripMenuItem = new ToolStripMenuItem();
            framesToolStripMenuItem = new ToolStripMenuItem();
            frames5 = new ToolStripMenuItem();
            frames10 = new ToolStripMenuItem();
            threadsToolStripMenuItem = new ToolStripMenuItem();
            t1 = new ToolStripMenuItem();
            t2 = new ToolStripMenuItem();
            t3 = new ToolStripMenuItem();
            t4 = new ToolStripMenuItem();
            t8 = new ToolStripMenuItem();
            menu_strip.SuspendLayout();
            SuspendLayout();
            // 
            // menu_strip
            // 
            menu_strip.Items.AddRange(new ToolStripItem[] { menu_file, morphingToolStripMenuItem, framesToolStripMenuItem, threadsToolStripMenuItem });
            menu_strip.Location = new Point(0, 0);
            menu_strip.Name = "menu_strip";
            menu_strip.Size = new Size(1079, 24);
            menu_strip.TabIndex = 1;
            menu_strip.Text = "menuStrip1";
            menu_strip.ItemClicked += menu_strip_ItemClicked;
            // 
            // menu_file
            // 
            menu_file.Name = "menu_file";
            menu_file.Size = new Size(37, 20);
            menu_file.Text = "File";
            // 
            // morphingToolStripMenuItem
            // 
            morphingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { beginToolStripMenuItem });
            morphingToolStripMenuItem.Name = "morphingToolStripMenuItem";
            morphingToolStripMenuItem.Size = new Size(72, 20);
            morphingToolStripMenuItem.Text = "Morphing";
            // 
            // beginToolStripMenuItem
            // 
            beginToolStripMenuItem.Name = "beginToolStripMenuItem";
            beginToolStripMenuItem.Size = new Size(104, 22);
            beginToolStripMenuItem.Text = "Begin";
            beginToolStripMenuItem.Click += beginToolStripMenuItem_Click;
            // 
            // framesToolStripMenuItem
            // 
            framesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { frames5, frames10 });
            framesToolStripMenuItem.Name = "framesToolStripMenuItem";
            framesToolStripMenuItem.Size = new Size(57, 20);
            framesToolStripMenuItem.Text = "Frames";
            // 
            // frames5
            // 
            frames5.Name = "frames5";
            frames5.Size = new Size(86, 22);
            frames5.Text = "5";
            frames5.Click += frames5_Click;
            // 
            // frames10
            // 
            frames10.Name = "frames10";
            frames10.Size = new Size(86, 22);
            frames10.Text = "10";
            frames10.Click += frames10_Click;
            // 
            // threadsToolStripMenuItem
            // 
            threadsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { t1, t2, t3, t4, t8 });
            threadsToolStripMenuItem.Name = "threadsToolStripMenuItem";
            threadsToolStripMenuItem.Size = new Size(60, 20);
            threadsToolStripMenuItem.Text = "Threads";
            // 
            // t1
            // 
            t1.Name = "t1";
            t1.Size = new Size(180, 22);
            t1.Text = "1";
            t1.Click += t1_Click;
            // 
            // t2
            // 
            t2.Name = "t2";
            t2.Size = new Size(180, 22);
            t2.Text = "2";
            t2.Click += t2_Click;
            // 
            // t3
            // 
            t3.Name = "t3";
            t3.Size = new Size(180, 22);
            t3.Text = "3";
            t3.Click += t3_Click;
            // 
            // t4
            // 
            t4.Name = "t4";
            t4.Size = new Size(180, 22);
            t4.Text = "4";
            t4.Click += t4_Click;
            // 
            // t8
            // 
            t8.Name = "t8";
            t8.Size = new Size(180, 22);
            t8.Text = "8";
            t8.Click += t8_Click;
            // 
            // Parent
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1079, 634);
            Controls.Add(menu_strip);
            IsMdiContainer = true;
            MainMenuStrip = menu_strip;
            Name = "Parent";
            Text = "Image Morpher";
            Load += Main_Load;
            menu_strip.ResumeLayout(false);
            menu_strip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menu_strip;
        private ToolStripMenuItem menu_file;
        private ToolStripMenuItem morphingToolStripMenuItem;
        private ToolStripMenuItem beginToolStripMenuItem;
        private ToolStripMenuItem framesToolStripMenuItem;
        private ToolStripMenuItem frames5;
        private ToolStripMenuItem frames10;
        private ToolStripMenuItem threadsToolStripMenuItem;
        private ToolStripMenuItem t1;
        private ToolStripMenuItem t2;
        private ToolStripMenuItem t3;
        private ToolStripMenuItem t4;
        private ToolStripMenuItem t8;
    }
}