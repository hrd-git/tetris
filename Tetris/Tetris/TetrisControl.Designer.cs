using System.Windows.Forms;
using Tetris;
using Tetris.Tetris;

namespace Tetris
{
    partial class TetrisControl
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Drop = new System.Windows.Forms.Timer(this.components);
            this.NextBlockField = new System.Windows.Forms.Label();
            this.TetrisField = new System.Windows.Forms.Label();
            this.Start = new System.Windows.Forms.Timer(this.components);
            this.GameOverText = new System.Windows.Forms.Label();
            this.StockBlockField = new System.Windows.Forms.Label();
            this.StartButton = new Tetris.ButtonEx();
            this.ResetButton = new Tetris.ButtonEx();
            this.SuspendLayout();
            // 
            // Drop
            // 
            this.Drop.Interval = 500;
            this.Drop.Tick += new System.EventHandler(this.DropDownTick);
            // 
            // NextBlockField
            // 
            this.NextBlockField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.NextBlockField.Location = new System.Drawing.Point(360, 100);
            this.NextBlockField.Margin = new System.Windows.Forms.Padding(0);
            this.NextBlockField.Name = "NextBlockField";
            this.NextBlockField.Size = new System.Drawing.Size(120, 440);
            this.NextBlockField.TabIndex = 0;
            // 
            // TetrisField
            // 
            this.TetrisField.BackColor = System.Drawing.Color.Black;
            this.TetrisField.Font = new System.Drawing.Font("ＭＳ ゴシック", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TetrisField.ForeColor = System.Drawing.Color.Black;
            this.TetrisField.Location = new System.Drawing.Point(140, 100);
            this.TetrisField.Margin = new System.Windows.Forms.Padding(0);
            this.TetrisField.Name = "TetrisField";
            this.TetrisField.Size = new System.Drawing.Size(240, 440);
            this.TetrisField.TabIndex = 1;
            // 
            // Start
            // 
            this.Start.Enabled = true;
            this.Start.Interval = 50;
            this.Start.Tick += new System.EventHandler(this.TetrisInit);
            // 
            // GameOverText
            // 
            this.GameOverText.BackColor = System.Drawing.Color.Black;
            this.GameOverText.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GameOverText.ForeColor = System.Drawing.Color.White;
            this.GameOverText.Location = new System.Drawing.Point(160, 180);
            this.GameOverText.Margin = new System.Windows.Forms.Padding(0);
            this.GameOverText.Name = "GameOverText";
            this.GameOverText.Size = new System.Drawing.Size(200, 50);
            this.GameOverText.TabIndex = 2;
            this.GameOverText.Text = "GameOver";
            this.GameOverText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.GameOverText.Visible = false;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(140, 30);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StockBlockField
            // 
            this.StockBlockField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.StockBlockField.Location = new System.Drawing.Point(40, 100);
            this.StockBlockField.Margin = new System.Windows.Forms.Padding(0);
            this.StockBlockField.Name = "StockBlockField";
            this.StockBlockField.Size = new System.Drawing.Size(120, 120);
            this.StockBlockField.TabIndex = 4;
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(300, 30);
            this.ResetButton.Margin = new System.Windows.Forms.Padding(0);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 18;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // TetrisControl
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(602, 633);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.StockBlockField);
            this.Controls.Add(this.GameOverText);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.NextBlockField);
            this.Controls.Add(this.TetrisField);
            this.KeyPreview = true;
            this.Name = "TetrisControl";
            this.Text = "Tetris";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TetrisMove);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer Drop;
        private System.Windows.Forms.Label NextBlockField;
        private System.Windows.Forms.Label TetrisField;
        private Timer Start;
        private Label GameOverText;
        private ButtonEx StartButton;
        private Label StockBlockField;
        private ButtonEx ResetButton;
    }
}

