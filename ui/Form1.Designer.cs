using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BackupHeroApp.UI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.comboBoxDatabases = new System.Windows.Forms.ComboBox();
            this.buttonBackup = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.listBoxScheduledDatabases = new System.Windows.Forms.ListBox();
            this.buttonStopBackup = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dateTimePickerBackupTime = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownMaxCopies = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxCopies)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxDatabases
            // 
            this.comboBoxDatabases.DropDownHeight = 200;
            this.comboBoxDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDatabases.FormattingEnabled = true;
            this.comboBoxDatabases.IntegralHeight = false;
            this.comboBoxDatabases.ItemHeight = 13;
            this.comboBoxDatabases.Location = new System.Drawing.Point(12, 136);
            this.comboBoxDatabases.Name = "comboBoxDatabases";
            this.comboBoxDatabases.Size = new System.Drawing.Size(296, 21);
            this.comboBoxDatabases.TabIndex = 0;
            this.toolTip.SetToolTip(this.comboBoxDatabases, "Select a database to backup");
            // 
            // buttonBackup
            // 
            this.buttonBackup.Location = new System.Drawing.Point(12, 225);
            this.buttonBackup.Name = "buttonBackup";
            this.buttonBackup.Size = new System.Drawing.Size(296, 29);
            this.buttonBackup.TabIndex = 1;
            this.buttonBackup.Text = "Start Auto Backup";
            this.toolTip.SetToolTip(this.buttonBackup, "Start automatic backup for the selected database");
            this.buttonBackup.UseVisualStyleBackColor = true;
            this.buttonBackup.Click += new System.EventHandler(this.buttonBackup_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.ForeColor = System.Drawing.Color.Green;
            this.labelStatus.Location = new System.Drawing.Point(9, 271);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(74, 13);
            this.labelStatus.TabIndex = 2;
            this.labelStatus.Text = "Status: Ready";
            // 
            // listBoxScheduledDatabases
            // 
            this.listBoxScheduledDatabases.FormattingEnabled = true;
            this.listBoxScheduledDatabases.Location = new System.Drawing.Point(12, 310);
            this.listBoxScheduledDatabases.Name = "listBoxScheduledDatabases";
            this.listBoxScheduledDatabases.Size = new System.Drawing.Size(296, 69);
            this.listBoxScheduledDatabases.TabIndex = 4;
            this.toolTip.SetToolTip(this.listBoxScheduledDatabases, "List of databases with scheduled backups");
            // 
            // buttonStopBackup
            // 
            this.buttonStopBackup.Location = new System.Drawing.Point(12, 385);
            this.buttonStopBackup.Name = "buttonStopBackup";
            this.buttonStopBackup.Size = new System.Drawing.Size(296, 29);
            this.buttonStopBackup.TabIndex = 5;
            this.buttonStopBackup.Text = "Stop Auto Backup";
            this.toolTip.SetToolTip(this.buttonStopBackup, "Stop automatic backup for the selected database");
            this.buttonStopBackup.UseVisualStyleBackColor = true;
            this.buttonStopBackup.Click += new System.EventHandler(this.buttonStopBackup_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 294);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Running Auto Backups:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Select Database:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(89, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 29);
            this.label3.TabIndex = 9;
            this.label3.Text = "BACKUP HERO";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(296, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // dateTimePickerBackupTime
            // 
            this.dateTimePickerBackupTime.CustomFormat = "HH:mm";
            this.dateTimePickerBackupTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerBackupTime.Location = new System.Drawing.Point(63, 188);
            this.dateTimePickerBackupTime.Name = "dateTimePickerBackupTime";
            this.dateTimePickerBackupTime.ShowUpDown = true;
            this.dateTimePickerBackupTime.Size = new System.Drawing.Size(66, 20);
            this.dateTimePickerBackupTime.TabIndex = 10;
            this.toolTip.SetToolTip(this.dateTimePickerBackupTime, "Set the time for the daily backup");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Backup Time:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(165, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Max Copies";
            // 
            // numericUpDownMaxCopies
            // 
            this.numericUpDownMaxCopies.Location = new System.Drawing.Point(172, 188);
            this.numericUpDownMaxCopies.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDownMaxCopies.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxCopies.Name = "numericUpDownMaxCopies";
            this.numericUpDownMaxCopies.Size = new System.Drawing.Size(66, 20);
            this.numericUpDownMaxCopies.TabIndex = 15;
            this.numericUpDownMaxCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(320, 426);
            this.Controls.Add(this.numericUpDownMaxCopies);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePickerBackupTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonStopBackup);
            this.Controls.Add(this.listBoxScheduledDatabases);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonBackup);
            this.Controls.Add(this.comboBoxDatabases);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Backup Hero";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxCopies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxDatabases;
        private System.Windows.Forms.Button buttonBackup;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ListBox listBoxScheduledDatabases;
        private System.Windows.Forms.Button buttonStopBackup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerBackupTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxCopies;
    }
}