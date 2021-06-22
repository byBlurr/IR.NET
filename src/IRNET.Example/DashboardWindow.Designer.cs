
namespace IRNET.Example
{
    partial class DashboardWindow
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
            this.components = new System.ComponentModel.Container();
            this.GearIndicator = new System.Windows.Forms.Label();
            this.RevsIndicator = new System.Windows.Forms.Label();
            this.SpeedIndicator = new System.Windows.Forms.Label();
            this.CurrentLap = new System.Windows.Forms.Label();
            this.BestTime = new System.Windows.Forms.Label();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.RevsBack = new System.Windows.Forms.PictureBox();
            this.SettingsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RevsFore = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.RevsBack)).BeginInit();
            this.SettingsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RevsFore)).BeginInit();
            this.SuspendLayout();
            // 
            // GearIndicator
            // 
            this.GearIndicator.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.GearIndicator.Location = new System.Drawing.Point(163, 22);
            this.GearIndicator.Name = "GearIndicator";
            this.GearIndicator.Size = new System.Drawing.Size(100, 104);
            this.GearIndicator.TabIndex = 0;
            this.GearIndicator.Text = "0";
            this.GearIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RevsIndicator
            // 
            this.RevsIndicator.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.RevsIndicator.Location = new System.Drawing.Point(12, 51);
            this.RevsIndicator.Name = "RevsIndicator";
            this.RevsIndicator.Size = new System.Drawing.Size(150, 28);
            this.RevsIndicator.TabIndex = 3;
            this.RevsIndicator.Text = "2000 rpm";
            this.RevsIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SpeedIndicator
            // 
            this.SpeedIndicator.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SpeedIndicator.Location = new System.Drawing.Point(12, 79);
            this.SpeedIndicator.Name = "SpeedIndicator";
            this.SpeedIndicator.Size = new System.Drawing.Size(150, 28);
            this.SpeedIndicator.TabIndex = 4;
            this.SpeedIndicator.Text = "130 mph";
            this.SpeedIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CurrentLap
            // 
            this.CurrentLap.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CurrentLap.Location = new System.Drawing.Point(248, 79);
            this.CurrentLap.Name = "CurrentLap";
            this.CurrentLap.Size = new System.Drawing.Size(150, 28);
            this.CurrentLap.TabIndex = 6;
            this.CurrentLap.Text = "1:42.820";
            this.CurrentLap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BestTime
            // 
            this.BestTime.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.BestTime.Location = new System.Drawing.Point(248, 51);
            this.BestTime.Name = "BestTime";
            this.BestTime.Size = new System.Drawing.Size(150, 28);
            this.BestTime.TabIndex = 5;
            this.BestTime.Text = "1:42.132";
            this.BestTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Enabled = true;
            this.UpdateTimer.Tick += new System.EventHandler(this.Update);
            // 
            // RevsBack
            // 
            this.RevsBack.Location = new System.Drawing.Point(12, 12);
            this.RevsBack.Name = "RevsBack";
            this.RevsBack.Size = new System.Drawing.Size(386, 36);
            this.RevsBack.TabIndex = 7;
            this.RevsBack.TabStop = false;
            // 
            // SettingsMenu
            // 
            this.SettingsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloseMenuItem});
            this.SettingsMenu.Name = "SettingsMenu";
            this.SettingsMenu.Size = new System.Drawing.Size(104, 26);
            this.SettingsMenu.TabStop = true;
            this.SettingsMenu.Text = "Options";
            this.SettingsMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.SettingsMenuItemClicked);
            // 
            // CloseMenuItem
            // 
            this.CloseMenuItem.Name = "CloseMenuItem";
            this.CloseMenuItem.Size = new System.Drawing.Size(103, 22);
            this.CloseMenuItem.Text = "Close";
            this.CloseMenuItem.ToolTipText = "Close the dashboard.";
            // 
            // RevsFore
            // 
            this.RevsFore.Location = new System.Drawing.Point(12, 12);
            this.RevsFore.Name = "RevsFore";
            this.RevsFore.Size = new System.Drawing.Size(386, 36);
            this.RevsFore.TabIndex = 8;
            this.RevsFore.TabStop = false;
            // 
            // DashboardWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(410, 127);
            this.ContextMenuStrip = this.SettingsMenu;
            this.Controls.Add(this.RevsFore);
            this.Controls.Add(this.RevsBack);
            this.Controls.Add(this.CurrentLap);
            this.Controls.Add(this.BestTime);
            this.Controls.Add(this.SpeedIndicator);
            this.Controls.Add(this.RevsIndicator);
            this.Controls.Add(this.GearIndicator);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(1260, 900);
            this.Name = "DashboardWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Example Dashboard";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.RevsBack)).EndInit();
            this.SettingsMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RevsFore)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label GearIndicator;
        private System.Windows.Forms.Label RevsIndicator;
        private System.Windows.Forms.Label SpeedIndicator;
        private System.Windows.Forms.Label CurrentLap;
        private System.Windows.Forms.Label BestTime;
        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.PictureBox RevsBack;
        private System.Windows.Forms.ContextMenuStrip SettingsMenu;
        private System.Windows.Forms.ToolStripMenuItem CloseMenuItem;
        private System.Windows.Forms.PictureBox RevsFore;
    }
}

