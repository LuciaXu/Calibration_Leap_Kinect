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
            ((System.ComponentModel.ISupportInitialize)(this.Rotatex_UpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rotatey_UpDown)).BeginInit();
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
            this.Rotatex_UpDown.Name = "Rotatex_UpDown";
            this.Rotatex_UpDown.Size = new System.Drawing.Size(68, 25);
            this.Rotatex_UpDown.TabIndex = 2;
            this.Rotatex_UpDown.ValueChanged += new System.EventHandler(this.Rotatex_UpDown_ValueChanged);
            // 
            // Rotatey_UpDown
            // 
            this.Rotatey_UpDown.Location = new System.Drawing.Point(81, 87);
            this.Rotatey_UpDown.Name = "Rotatey_UpDown";
            this.Rotatey_UpDown.Size = new System.Drawing.Size(68, 25);
            this.Rotatey_UpDown.TabIndex = 3;
            this.Rotatey_UpDown.ValueChanged += new System.EventHandler(this.Rotatey_UpDown_ValueChanged);
            // 
            // Adjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 254);
            this.Controls.Add(this.Rotatey_UpDown);
            this.Controls.Add(this.Rotatex_UpDown);
            this.Controls.Add(this.Complect);
            this.Controls.Add(this.OK);
            this.Name = "Adjust";
            this.Text = "Adjust";
            ((System.ComponentModel.ISupportInitialize)(this.Rotatex_UpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rotatey_UpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Complect;
        private System.Windows.Forms.NumericUpDown Rotatex_UpDown;
        private System.Windows.Forms.NumericUpDown Rotatey_UpDown;

    }
}