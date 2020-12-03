namespace ChessGame
{
    partial class Form1
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
            this.boardPicture = new System.Windows.Forms.PictureBox();
            this.player2 = new System.Windows.Forms.Label();
            this.player1 = new System.Windows.Forms.Label();
            this.btnFlipBoard = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.boardPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // boardPicture
            // 
            this.boardPicture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.boardPicture.Location = new System.Drawing.Point(242, 110);
            this.boardPicture.Name = "boardPicture";
            this.boardPicture.Size = new System.Drawing.Size(401, 401);
            this.boardPicture.TabIndex = 0;
            this.boardPicture.TabStop = false;
            // 
            // player2
            // 
            this.player2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.player2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.player2.Location = new System.Drawing.Point(143, 84);
            this.player2.Name = "player2";
            this.player2.Size = new System.Drawing.Size(100, 23);
            this.player2.TabIndex = 1;
            this.player2.Text = "PLAYER 2";
            // 
            // player1
            // 
            this.player1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.player1.BackColor = System.Drawing.SystemColors.Control;
            this.player1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.player1.Location = new System.Drawing.Point(147, 514);
            this.player1.Name = "player1";
            this.player1.Size = new System.Drawing.Size(96, 25);
            this.player1.TabIndex = 1;
            this.player1.Text = "PLAYER 1";
            // 
            // btnFlipBoard
            // 
            this.btnFlipBoard.Location = new System.Drawing.Point(676, 110);
            this.btnFlipBoard.Name = "btnFlipBoard";
            this.btnFlipBoard.Size = new System.Drawing.Size(75, 23);
            this.btnFlipBoard.TabIndex = 2;
            this.btnFlipBoard.Text = "Fip Board";
            this.btnFlipBoard.UseVisualStyleBackColor = true;
            this.btnFlipBoard.Click += new System.EventHandler(this.btnFlipBoard_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 596);
            this.Controls.Add(this.btnFlipBoard);
            this.Controls.Add(this.player1);
            this.Controls.Add(this.player2);
            this.Controls.Add(this.boardPicture);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.boardPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox boardPicture;
        public System.Windows.Forms.Label player2;
        public System.Windows.Forms.Label player1;
        private System.Windows.Forms.Button btnFlipBoard;
    }
}

