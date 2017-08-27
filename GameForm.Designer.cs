namespace life
{
    partial class GameForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
    private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Tools = new System.Windows.Forms.ToolStrip();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.btnStepForward = new System.Windows.Forms.ToolStripButton();
            this.btnStepBackward = new System.Windows.Forms.ToolStripButton();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.Population = new System.Windows.Forms.ToolStripLabel();
            this.StepLabel = new System.Windows.Forms.ToolStripLabel();
            this.CurrentStep = new System.Windows.Forms.ToolStripLabel();
            this.Picture = new System.Windows.Forms.PictureBox();
            this.Tools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // Tools
            // 
            this.Tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStart,
            this.btnStepForward,
            this.btnStepBackward,
            this.btnClear,
            this.toolStripSeparator4,
            this.Population,
            this.StepLabel,
            this.CurrentStep});
            this.Tools.Location = new System.Drawing.Point(0, 0);
            this.Tools.Name = "Tools";
            this.Tools.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.Tools.Size = new System.Drawing.Size(1105, 25);
            this.Tools.TabIndex = 3;
            this.Tools.Text = "toolStrip1";
            // 
            // btnStart
            // 
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Indigo;
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(42, 22);
            this.btnStart.Text = "Старт";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStepForward
            // 
            this.btnStepForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStepForward.Name = "btnStepForward";
            this.btnStepForward.Size = new System.Drawing.Size(74, 22);
            this.btnStepForward.Text = "Шаг вперед";
            this.btnStepForward.Click += new System.EventHandler(this.btnStepForward_Click);
            // 
            // btnStepBackward
            // 
            this.btnStepBackward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStepBackward.Name = "btnStepBackward";
            this.btnStepBackward.Size = new System.Drawing.Size(66, 22);
            this.btnStepBackward.Text = "Шаг назад";
            this.btnStepBackward.Click += new System.EventHandler(this.btnStepBackward_Click);
            // 
            // btnClear
            // 
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(63, 22);
            this.btnClear.Text = "Очистить";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // Population
            // 
            this.Population.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Population.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.Population.ForeColor = System.Drawing.Color.Black;
            this.Population.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.Population.Name = "Population";
            this.Population.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Population.Size = new System.Drawing.Size(15, 22);
            this.Population.Text = "0";
            // 
            // StepLabel
            // 
            this.StepLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.StepLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StepLabel.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.StepLabel.Name = "StepLabel";
            this.StepLabel.Size = new System.Drawing.Size(15, 22);
            this.StepLabel.Text = "0";
            this.StepLabel.ToolTipText = "Поколений";
            // 
            // CurrentStep
            // 
            this.CurrentStep.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CurrentStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurrentStep.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.CurrentStep.Name = "CurrentStep";
            this.CurrentStep.Size = new System.Drawing.Size(15, 22);
            this.CurrentStep.Text = "0";
            this.CurrentStep.ToolTipText = "Текущее поколение";
            // 
            // Picture
            // 
            this.Picture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Picture.BackColor = System.Drawing.Color.White;
            this.Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Picture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Picture.Location = new System.Drawing.Point(162, 28);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(637, 557);
            this.Picture.TabIndex = 2;
            this.Picture.TabStop = false;
            this.Picture.Paint += new System.Windows.Forms.PaintEventHandler(this.Picture_Paint);
            this.Picture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Picture_MouseClick);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 583);
            this.Controls.Add(this.Tools);
            this.Controls.Add(this.Picture);
            this.Name = "GameForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.Tools.ResumeLayout(false);
            this.Tools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Picture;
        private System.Windows.Forms.ToolStrip Tools;
        private System.Windows.Forms.ToolStripButton btnStart;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel Population;
        private System.Windows.Forms.ToolStripLabel StepLabel;
        private System.Windows.Forms.ToolStripButton btnStepForward;
        private System.Windows.Forms.ToolStripButton btnStepBackward;
        private System.Windows.Forms.ToolStripLabel CurrentStep;
    }
}

