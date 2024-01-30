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
            menu_strip.SuspendLayout();
            SuspendLayout();
            // 
            // menu_strip
            // 
            menu_strip.Items.AddRange(new ToolStripItem[] { menu_file, morphingToolStripMenuItem });
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
            beginToolStripMenuItem.Size = new Size(180, 22);
            beginToolStripMenuItem.Text = "Begin";
            beginToolStripMenuItem.Click += beginToolStripMenuItem_Click;
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
    }
}