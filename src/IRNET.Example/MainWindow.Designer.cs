
namespace IRNET.Example
{
    partial class MainWindow
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
            this.DashboardGroup = new System.Windows.Forms.GroupBox();
            this.UpdateDashboardButton = new System.Windows.Forms.Button();
            this.RedlineLabel = new System.Windows.Forms.Label();
            this.OptShiftLabel = new System.Windows.Forms.Label();
            this.MaxRPMLabel = new System.Windows.Forms.Label();
            this.RedlineRevsBox = new System.Windows.Forms.TextBox();
            this.OptimumRevsBox = new System.Windows.Forms.TextBox();
            this.MaxRevsBox = new System.Windows.Forms.TextBox();
            this.DashboardToggle = new System.Windows.Forms.CheckBox();
            this.FuelCalcGroup = new System.Windows.Forms.GroupBox();
            this.LapsLeftToggle = new System.Windows.Forms.CheckBox();
            this.FuelCalculatorToggle = new System.Windows.Forms.CheckBox();
            this.FuelBarToggle = new System.Windows.Forms.CheckBox();
            this.AvgLapFuelToggle = new System.Windows.Forms.CheckBox();
            this.FastLapFuelToggle = new System.Windows.Forms.CheckBox();
            this.RefuelToggle = new System.Windows.Forms.CheckBox();
            this.DashboardGroup.SuspendLayout();
            this.FuelCalcGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // DashboardGroup
            // 
            this.DashboardGroup.Controls.Add(this.UpdateDashboardButton);
            this.DashboardGroup.Controls.Add(this.RedlineLabel);
            this.DashboardGroup.Controls.Add(this.OptShiftLabel);
            this.DashboardGroup.Controls.Add(this.MaxRPMLabel);
            this.DashboardGroup.Controls.Add(this.RedlineRevsBox);
            this.DashboardGroup.Controls.Add(this.OptimumRevsBox);
            this.DashboardGroup.Controls.Add(this.MaxRevsBox);
            this.DashboardGroup.Controls.Add(this.DashboardToggle);
            this.DashboardGroup.Location = new System.Drawing.Point(13, 13);
            this.DashboardGroup.Name = "DashboardGroup";
            this.DashboardGroup.Size = new System.Drawing.Size(215, 136);
            this.DashboardGroup.TabIndex = 0;
            this.DashboardGroup.TabStop = false;
            this.DashboardGroup.Text = "Dashboard";
            // 
            // UpdateDashboardButton
            // 
            this.UpdateDashboardButton.Location = new System.Drawing.Point(132, 18);
            this.UpdateDashboardButton.Name = "UpdateDashboardButton";
            this.UpdateDashboardButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateDashboardButton.TabIndex = 8;
            this.UpdateDashboardButton.Text = "Update";
            this.UpdateDashboardButton.UseVisualStyleBackColor = true;
            this.UpdateDashboardButton.Click += new System.EventHandler(this.RevsChanged);
            // 
            // RedlineLabel
            // 
            this.RedlineLabel.AutoSize = true;
            this.RedlineLabel.Location = new System.Drawing.Point(93, 110);
            this.RedlineLabel.Name = "RedlineLabel";
            this.RedlineLabel.Size = new System.Drawing.Size(74, 15);
            this.RedlineLabel.TabIndex = 7;
            this.RedlineLabel.Text = "Redline RPM";
            // 
            // OptShiftLabel
            // 
            this.OptShiftLabel.AutoSize = true;
            this.OptShiftLabel.Location = new System.Drawing.Point(93, 81);
            this.OptShiftLabel.Name = "OptShiftLabel";
            this.OptShiftLabel.Size = new System.Drawing.Size(114, 15);
            this.OptShiftLabel.TabIndex = 5;
            this.OptShiftLabel.Text = "Optimum Shift RPM";
            // 
            // MaxRPMLabel
            // 
            this.MaxRPMLabel.AutoSize = true;
            this.MaxRPMLabel.Location = new System.Drawing.Point(93, 51);
            this.MaxRPMLabel.Name = "MaxRPMLabel";
            this.MaxRPMLabel.Size = new System.Drawing.Size(90, 15);
            this.MaxRPMLabel.TabIndex = 4;
            this.MaxRPMLabel.Text = "Maximum RPM";
            // 
            // RedlineRevsBox
            // 
            this.RedlineRevsBox.Location = new System.Drawing.Point(7, 107);
            this.RedlineRevsBox.MaxLength = 5;
            this.RedlineRevsBox.Name = "RedlineRevsBox";
            this.RedlineRevsBox.Size = new System.Drawing.Size(80, 23);
            this.RedlineRevsBox.TabIndex = 3;
            // 
            // OptimumRevsBox
            // 
            this.OptimumRevsBox.Location = new System.Drawing.Point(7, 78);
            this.OptimumRevsBox.MaxLength = 5;
            this.OptimumRevsBox.Name = "OptimumRevsBox";
            this.OptimumRevsBox.Size = new System.Drawing.Size(80, 23);
            this.OptimumRevsBox.TabIndex = 2;
            // 
            // MaxRevsBox
            // 
            this.MaxRevsBox.Location = new System.Drawing.Point(7, 48);
            this.MaxRevsBox.MaxLength = 5;
            this.MaxRevsBox.Name = "MaxRevsBox";
            this.MaxRevsBox.Size = new System.Drawing.Size(80, 23);
            this.MaxRevsBox.TabIndex = 1;
            // 
            // DashboardToggle
            // 
            this.DashboardToggle.AutoSize = true;
            this.DashboardToggle.Location = new System.Drawing.Point(9, 22);
            this.DashboardToggle.Name = "DashboardToggle";
            this.DashboardToggle.Size = new System.Drawing.Size(61, 19);
            this.DashboardToggle.TabIndex = 0;
            this.DashboardToggle.Text = "Enable";
            this.DashboardToggle.UseVisualStyleBackColor = true;
            this.DashboardToggle.CheckedChanged += new System.EventHandler(this.ToggleOverlays);
            // 
            // FuelCalcGroup
            // 
            this.FuelCalcGroup.Controls.Add(this.RefuelToggle);
            this.FuelCalcGroup.Controls.Add(this.FastLapFuelToggle);
            this.FuelCalcGroup.Controls.Add(this.AvgLapFuelToggle);
            this.FuelCalcGroup.Controls.Add(this.FuelBarToggle);
            this.FuelCalcGroup.Controls.Add(this.LapsLeftToggle);
            this.FuelCalcGroup.Controls.Add(this.FuelCalculatorToggle);
            this.FuelCalcGroup.Location = new System.Drawing.Point(257, 13);
            this.FuelCalcGroup.Name = "FuelCalcGroup";
            this.FuelCalcGroup.Size = new System.Drawing.Size(215, 136);
            this.FuelCalcGroup.TabIndex = 1;
            this.FuelCalcGroup.TabStop = false;
            this.FuelCalcGroup.Text = "Fuel Calculator";
            // 
            // LapsLeftToggle
            // 
            this.LapsLeftToggle.AutoSize = true;
            this.LapsLeftToggle.Location = new System.Drawing.Point(6, 43);
            this.LapsLeftToggle.Name = "LapsLeftToggle";
            this.LapsLeftToggle.Size = new System.Drawing.Size(105, 19);
            this.LapsLeftToggle.TabIndex = 10;
            this.LapsLeftToggle.Text = "Show Laps Left";
            this.LapsLeftToggle.UseVisualStyleBackColor = true;
            this.LapsLeftToggle.Click += new System.EventHandler(this.FuelCalcChanged);
            // 
            // FuelCalculatorToggle
            // 
            this.FuelCalculatorToggle.AutoSize = true;
            this.FuelCalculatorToggle.Location = new System.Drawing.Point(6, 22);
            this.FuelCalculatorToggle.Name = "FuelCalculatorToggle";
            this.FuelCalculatorToggle.Size = new System.Drawing.Size(61, 19);
            this.FuelCalculatorToggle.TabIndex = 9;
            this.FuelCalculatorToggle.Text = "Enable";
            this.FuelCalculatorToggle.UseVisualStyleBackColor = true;
            this.FuelCalculatorToggle.CheckedChanged += new System.EventHandler(this.ToggleOverlays);
            // 
            // FuelBarToggle
            // 
            this.FuelBarToggle.AutoSize = true;
            this.FuelBarToggle.BackColor = System.Drawing.Color.Transparent;
            this.FuelBarToggle.Location = new System.Drawing.Point(6, 60);
            this.FuelBarToggle.Name = "FuelBarToggle";
            this.FuelBarToggle.Size = new System.Drawing.Size(100, 19);
            this.FuelBarToggle.TabIndex = 11;
            this.FuelBarToggle.Text = "Show Fuel Bar";
            this.FuelBarToggle.UseVisualStyleBackColor = false;
            // 
            // AvgLapFuelToggle
            // 
            this.AvgLapFuelToggle.AutoSize = true;
            this.AvgLapFuelToggle.BackColor = System.Drawing.Color.Transparent;
            this.AvgLapFuelToggle.Location = new System.Drawing.Point(6, 77);
            this.AvgLapFuelToggle.Name = "AvgLapFuelToggle";
            this.AvgLapFuelToggle.Size = new System.Drawing.Size(126, 19);
            this.AvgLapFuelToggle.TabIndex = 12;
            this.AvgLapFuelToggle.Text = "Show Avg Lap Fuel";
            this.AvgLapFuelToggle.UseVisualStyleBackColor = false;
            // 
            // FastLapFuelToggle
            // 
            this.FastLapFuelToggle.AutoSize = true;
            this.FastLapFuelToggle.BackColor = System.Drawing.Color.Transparent;
            this.FastLapFuelToggle.Location = new System.Drawing.Point(6, 94);
            this.FastLapFuelToggle.Name = "FastLapFuelToggle";
            this.FastLapFuelToggle.Size = new System.Drawing.Size(126, 19);
            this.FastLapFuelToggle.TabIndex = 13;
            this.FastLapFuelToggle.Text = "Show Fast Lap Fuel";
            this.FastLapFuelToggle.UseVisualStyleBackColor = false;
            // 
            // RefuelToggle
            // 
            this.RefuelToggle.AutoSize = true;
            this.RefuelToggle.BackColor = System.Drawing.Color.Transparent;
            this.RefuelToggle.Location = new System.Drawing.Point(6, 111);
            this.RefuelToggle.Name = "RefuelToggle";
            this.RefuelToggle.Size = new System.Drawing.Size(138, 19);
            this.RefuelToggle.TabIndex = 14;
            this.RefuelToggle.Text = "Show Refuel Amount";
            this.RefuelToggle.UseVisualStyleBackColor = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 161);
            this.Controls.Add(this.FuelCalcGroup);
            this.Controls.Add(this.DashboardGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainWindow";
            this.Text = "iRacing | Blurr Overlays";
            this.DashboardGroup.ResumeLayout(false);
            this.DashboardGroup.PerformLayout();
            this.FuelCalcGroup.ResumeLayout(false);
            this.FuelCalcGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox DashboardGroup;
        private System.Windows.Forms.TextBox OptimumRevsBox;
        private System.Windows.Forms.TextBox MaxRevsBox;
        private System.Windows.Forms.CheckBox DashboardToggle;
        private System.Windows.Forms.Label RedlineLabel;
        private System.Windows.Forms.Label OptShiftLabel;
        private System.Windows.Forms.Label MaxRPMLabel;
        private System.Windows.Forms.TextBox RedlineRevsBox;
        private System.Windows.Forms.Button UpdateDashboardButton;
        private System.Windows.Forms.GroupBox FuelCalcGroup;
        private System.Windows.Forms.CheckBox FuelCalculatorToggle;
        private System.Windows.Forms.CheckBox eft;
        private System.Windows.Forms.CheckBox LapsLeftToggle;
        private System.Windows.Forms.CheckBox AvgLapFuelToggle;
        private System.Windows.Forms.CheckBox FuelBarToggle;
        private System.Windows.Forms.CheckBox FastLapFuelToggle;
        private System.Windows.Forms.CheckBox RefuelToggle;
    }
}