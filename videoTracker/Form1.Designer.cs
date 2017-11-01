namespace videoTracker
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tracksList = new System.Windows.Forms.ListBox();
            this.tick = new System.Windows.Forms.Timer(this.components);
            this.btLoad = new System.Windows.Forms.Button();
            this.btHeat = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.btAnimate = new System.Windows.Forms.Button();
            this.mplayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.btAnimateAll = new System.Windows.Forms.Button();
            this.btClean = new System.Windows.Forms.Button();
            this.btCalc = new System.Windows.Forms.Button();
            this.btDeleteTrack = new System.Windows.Forms.Button();
            this.btDraw = new System.Windows.Forms.Button();
            this.blLoadVideo = new System.Windows.Forms.Button();
            this.speed = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.mplayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speed)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tracksList
            // 
            this.tracksList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tracksList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.tracksList.FormattingEnabled = true;
            this.tracksList.Location = new System.Drawing.Point(657, 406);
            this.tracksList.Name = "tracksList";
            this.tracksList.Size = new System.Drawing.Size(110, 56);
            this.tracksList.TabIndex = 4;
            this.tracksList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tracksList_DrawItem);
            this.tracksList.SelectedIndexChanged += new System.EventHandler(this.tracksList_SelectedIndexChanged);
            this.tracksList.DoubleClick += new System.EventHandler(this.tracksList_DoubleClick);
            // 
            // tick
            // 
            this.tick.Tick += new System.EventHandler(this.tick_Tick);
            // 
            // btLoad
            // 
            this.btLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btLoad.Location = new System.Drawing.Point(658, 70);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(110, 27);
            this.btLoad.TabIndex = 7;
            this.btLoad.Text = "Load tracks from file";
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Click += new System.EventHandler(this.button2_Click);
            // 
            // btHeat
            // 
            this.btHeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btHeat.Location = new System.Drawing.Point(658, 99);
            this.btHeat.Name = "btHeat";
            this.btHeat.Size = new System.Drawing.Size(110, 40);
            this.btHeat.TabIndex = 8;
            this.btHeat.Text = "Heatmap (selected track)";
            this.btHeat.UseVisualStyleBackColor = true;
            this.btHeat.Click += new System.EventHandler(this.button3_Click);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Location = new System.Drawing.Point(658, 41);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(110, 27);
            this.btSave.TabIndex = 6;
            this.btSave.Text = "Save tracks to file";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // btAnimate
            // 
            this.btAnimate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btAnimate.Location = new System.Drawing.Point(658, 183);
            this.btAnimate.Name = "btAnimate";
            this.btAnimate.Size = new System.Drawing.Size(110, 40);
            this.btAnimate.TabIndex = 10;
            this.btAnimate.Text = "Animate\r\n(selected track)";
            this.btAnimate.UseVisualStyleBackColor = true;
            this.btAnimate.Click += new System.EventHandler(this.button4_Click);
            // 
            // mplayer
            // 
            this.mplayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mplayer.Enabled = true;
            this.mplayer.Location = new System.Drawing.Point(2, 2);
            this.mplayer.Name = "mplayer";
            this.mplayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mplayer.OcxState")));
            this.mplayer.Size = new System.Drawing.Size(649, 615);
            this.mplayer.TabIndex = 0;
            this.mplayer.Enter += new System.EventHandler(this.mplayer_Enter);
            // 
            // btAnimateAll
            // 
            this.btAnimateAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btAnimateAll.Location = new System.Drawing.Point(658, 225);
            this.btAnimateAll.Name = "btAnimateAll";
            this.btAnimateAll.Size = new System.Drawing.Size(110, 27);
            this.btAnimateAll.TabIndex = 11;
            this.btAnimateAll.Text = "Animate all tracks";
            this.btAnimateAll.UseVisualStyleBackColor = true;
            this.btAnimateAll.Click += new System.EventHandler(this.button5_Click);
            // 
            // btClean
            // 
            this.btClean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClean.Location = new System.Drawing.Point(658, 296);
            this.btClean.Name = "btClean";
            this.btClean.Size = new System.Drawing.Size(110, 27);
            this.btClean.TabIndex = 12;
            this.btClean.Text = "Clean drawings";
            this.btClean.UseVisualStyleBackColor = true;
            this.btClean.Click += new System.EventHandler(this.button6_Click);
            // 
            // btCalc
            // 
            this.btCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCalc.Location = new System.Drawing.Point(658, 254);
            this.btCalc.Name = "btCalc";
            this.btCalc.Size = new System.Drawing.Size(110, 40);
            this.btCalc.TabIndex = 12;
            this.btCalc.Text = "Calc distance (selected track)";
            this.btCalc.UseVisualStyleBackColor = true;
            this.btCalc.Click += new System.EventHandler(this.button7_Click);
            // 
            // btDeleteTrack
            // 
            this.btDeleteTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDeleteTrack.Location = new System.Drawing.Point(657, 325);
            this.btDeleteTrack.Name = "btDeleteTrack";
            this.btDeleteTrack.Size = new System.Drawing.Size(112, 27);
            this.btDeleteTrack.TabIndex = 11;
            this.btDeleteTrack.Text = "Delet selected track";
            this.btDeleteTrack.UseVisualStyleBackColor = true;
            this.btDeleteTrack.Click += new System.EventHandler(this.button8_Click);
            // 
            // btDraw
            // 
            this.btDraw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDraw.Location = new System.Drawing.Point(658, 141);
            this.btDraw.Name = "btDraw";
            this.btDraw.Size = new System.Drawing.Size(110, 40);
            this.btDraw.TabIndex = 8;
            this.btDraw.Text = "Draw track (selected track)";
            this.btDraw.UseVisualStyleBackColor = true;
            this.btDraw.Click += new System.EventHandler(this.button9_Click);
            // 
            // blLoadVideo
            // 
            this.blLoadVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.blLoadVideo.Location = new System.Drawing.Point(658, 12);
            this.blLoadVideo.Name = "blLoadVideo";
            this.blLoadVideo.Size = new System.Drawing.Size(110, 27);
            this.blLoadVideo.TabIndex = 6;
            this.blLoadVideo.Text = "Load new Video";
            this.blLoadVideo.UseVisualStyleBackColor = true;
            this.blLoadVideo.Click += new System.EventHandler(this.blLoadVideo_Click);
            // 
            // speed
            // 
            this.speed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.speed.Location = new System.Drawing.Point(657, 369);
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(110, 20);
            this.speed.TabIndex = 13;
            this.speed.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(655, 354);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Animation Speed";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(655, 391);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Tracks:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 91);
            this.label1.TabIndex = 10;
            this.label1.Text = "z - Open Video\r\nx - Play Video\r\nc - Pause Video\r\nv - Stop Video\r\ns - New track\r\nd" +
    " - Start tacking\r\nf - Stop tracking";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(658, 481);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(110, 136);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Keyboard Commands";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 617);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.speed);
            this.Controls.Add(this.btDeleteTrack);
            this.Controls.Add(this.btCalc);
            this.Controls.Add(this.btClean);
            this.Controls.Add(this.btAnimateAll);
            this.Controls.Add(this.btAnimate);
            this.Controls.Add(this.btDraw);
            this.Controls.Add(this.btHeat);
            this.Controls.Add(this.btLoad);
            this.Controls.Add(this.blLoadVideo);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.tracksList);
            this.Controls.Add(this.mplayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "VideoTracker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.mplayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speed)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer mplayer;
        private System.Windows.Forms.ListBox tracksList;
        private System.Windows.Forms.Timer tick;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.Button btHeat;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btAnimate;
        private System.Windows.Forms.Button btAnimateAll;
        private System.Windows.Forms.Button btClean;
        private System.Windows.Forms.Button btCalc;
        private System.Windows.Forms.Button btDeleteTrack;
        private System.Windows.Forms.Button btDraw;
        private System.Windows.Forms.Button blLoadVideo;
        private System.Windows.Forms.NumericUpDown speed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

