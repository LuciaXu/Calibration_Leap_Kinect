namespace KinectFingerTracking
{
    partial class Adjust
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private int rotate_x = 0;
        private int rotate_y = 0;
        private int rotate_z = 0;
        private int scale_x = 0;
        private int scale_y = 0;
        private int scale_z = 0;
        public delegate void OK_Dele(int r_x, int r_y, int r_z, int s_x, int s_y, int s_z);
        public event OK_Dele my_ok;

        public delegate void Comp_Dele();
        public event Comp_Dele my_comp;

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
            this.OK = new System.Windows.Forms.Button();
            this.Complect = new System.Windows.Forms.Button();
            this.Rotatex_UpDown = new System.Windows.Forms.NumericUpDown();
            this.Rotatey_UpDown = new System.Windows.Forms.NumericUpDown();
            this.Rotatez_UpDown = new System.Windows.Forms.NumericUpDown();
            this.Scalex_UpDown = new System.Windows.Forms.NumericUpDown();
            this.Scaley_UpDown = new System.Windows.Forms.NumericUpDown();
            this.Scalez_UpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.Rotatex_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rotatey_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rotatez_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scalex_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scaley_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scalez_UpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(12, 198);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(137, 44);
            this.OK.TabIndex = 0;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Complect
            // 
            this.Complect.Location = new System.Drawing.Point(344, 198);
            this.Complect.Name = "Complect";
            this.Complect.Size = new System.Drawing.Size(130, 44);
            this.Complect.TabIndex = 1;
            this.Complect.Text = "Complect";
            this.Complect.UseVisualStyleBackColor = true;
            this.Complect.Click += new System.EventHandler(this.Complect_Click);
            // 
            // Rotatex_UpDown
            // 
            this.Rotatex_UpDown.Location = new System.Drawing.Point(81, 37);
            this.Rotatex_UpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.Rotatex_UpDown.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.Rotatex_UpDown.Name = "Rotatex_UpDown";
            this.Rotatex_UpDown.Size = new System.Drawing.Size(68, 25);
            this.Rotatex_UpDown.TabIndex = 2;
            this.Rotatex_UpDown.ValueChanged += new System.EventHandler(this.Rotatex_UpDown_ValueChanged);
            // 
            // Rotatey_UpDown
            // 
            this.Rotatey_UpDown.Location = new System.Drawing.Point(81, 87);
            this.Rotatey_UpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.Rotatey_UpDown.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.Rotatey_UpDown.Name = "Rotatey_UpDown";
            this.Rotatey_UpDown.Size = new System.Drawing.Size(68, 25);
            this.Rotatey_UpDown.TabIndex = 3;
            this.Rotatey_UpDown.ValueChanged += new System.EventHandler(this.Rotatey_UpDown_ValueChanged);
            // 
            // Rotatez_UpDown
            // 
            this.Rotatez_UpDown.Location = new System.Drawing.Point(81, 139);
            this.Rotatez_UpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.Rotatez_UpDown.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.Rotatez_UpDown.Name = "Rotatez_UpDown";
            this.Rotatez_UpDown.Size = new System.Drawing.Size(68, 25);
            this.Rotatez_UpDown.TabIndex = 4;
            this.Rotatez_UpDown.ValueChanged += new System.EventHandler(this.Rotatez_UpDown_ValueChanged);
            // 
            // Scalex_UpDown
            // 
            this.Scalex_UpDown.Location = new System.Drawing.Point(334, 37);
            this.Scalex_UpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Scalex_UpDown.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.Scalex_UpDown.Name = "Scalex_UpDown";
            this.Scalex_UpDown.Size = new System.Drawing.Size(68, 25);
            this.Scalex_UpDown.TabIndex = 5;
            this.Scalex_UpDown.ValueChanged += new System.EventHandler(this.Scalex_UpDown_ValueChanged);
            // 
            // Scaley_UpDown
            // 
            this.Scaley_UpDown.Location = new System.Drawing.Point(334, 87);
            this.Scaley_UpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Scaley_UpDown.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.Scaley_UpDown.Name = "Scaley_UpDown";
            this.Scaley_UpDown.Size = new System.Drawing.Size(68, 25);
            this.Scaley_UpDown.TabIndex = 6;
            this.Scaley_UpDown.ValueChanged += new System.EventHandler(this.Scaley_UpDown_ValueChanged);
            // 
            // Scalez_UpDown
            // 
            this.Scalez_UpDown.Location = new System.Drawing.Point(334, 139);
            this.Scalez_UpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Scalez_UpDown.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.Scalez_UpDown.Name = "Scalez_UpDown";
            this.Scalez_UpDown.Size = new System.Drawing.Size(68, 25);
            this.Scalez_UpDown.TabIndex = 7;
            this.Scalez_UpDown.ValueChanged += new System.EventHandler(this.Scalez_UpDown_ValueChanged);
            // 
            // Adjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 254);
            this.Controls.Add(this.Scalez_UpDown);
            this.Controls.Add(this.Scaley_UpDown);
            this.Controls.Add(this.Scalex_UpDown);
            this.Controls.Add(this.Rotatez_UpDown);
            this.Controls.Add(this.Rotatey_UpDown);
            this.Controls.Add(this.Rotatex_UpDown);
            this.Controls.Add(this.Complect);
            this.Controls.Add(this.OK);
            this.Name = "Adjust";
            this.Text = "Adjust";
            ((System.ComponentModel.ISupportInitialize)(this.Rotatex_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rotatey_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rotatez_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scalex_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scaley_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scalez_UpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Complect;
        private System.Windows.Forms.NumericUpDown Rotatex_UpDown;
        private System.Windows.Forms.NumericUpDown Rotatey_UpDown;
        private System.Windows.Forms.NumericUpDown Rotatez_UpDown;
        private System.Windows.Forms.NumericUpDown Scalex_UpDown;
        private System.Windows.Forms.NumericUpDown Scaley_UpDown;
        private System.Windows.Forms.NumericUpDown Scalez_UpDown;

    }
}