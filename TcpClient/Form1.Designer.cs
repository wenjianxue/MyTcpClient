namespace MyTcpClient
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.targetIptextBox = new System.Windows.Forms.TextBox();
            this.targetPorttextBox = new System.Windows.Forms.TextBox();
            this.Conectbutton = new System.Windows.Forms.Button();
            this.Sendbutton = new System.Windows.Forms.Button();
            this.SendMsgtextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RecvMsgtextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.CloseConectbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "目标IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "端口:";
            // 
            // targetIptextBox
            // 
            this.targetIptextBox.Location = new System.Drawing.Point(67, 30);
            this.targetIptextBox.Name = "targetIptextBox";
            this.targetIptextBox.Size = new System.Drawing.Size(144, 21);
            this.targetIptextBox.TabIndex = 1;
            // 
            // targetPorttextBox
            // 
            this.targetPorttextBox.Location = new System.Drawing.Point(267, 30);
            this.targetPorttextBox.Name = "targetPorttextBox";
            this.targetPorttextBox.Size = new System.Drawing.Size(66, 21);
            this.targetPorttextBox.TabIndex = 2;
            // 
            // Conectbutton
            // 
            this.Conectbutton.Location = new System.Drawing.Point(350, 31);
            this.Conectbutton.Name = "Conectbutton";
            this.Conectbutton.Size = new System.Drawing.Size(73, 23);
            this.Conectbutton.TabIndex = 3;
            this.Conectbutton.Text = "连接";
            this.Conectbutton.UseVisualStyleBackColor = true;
            this.Conectbutton.Click += new System.EventHandler(this.Conectbutton_Click);
            // 
            // Sendbutton
            // 
            this.Sendbutton.Location = new System.Drawing.Point(16, 201);
            this.Sendbutton.Name = "Sendbutton";
            this.Sendbutton.Size = new System.Drawing.Size(75, 23);
            this.Sendbutton.TabIndex = 5;
            this.Sendbutton.Text = "发送";
            this.Sendbutton.UseVisualStyleBackColor = true;
            this.Sendbutton.Click += new System.EventHandler(this.Sendbutton_Click);
            // 
            // SendMsgtextBox
            // 
            this.SendMsgtextBox.AcceptsTab = true;
            this.SendMsgtextBox.Location = new System.Drawing.Point(16, 87);
            this.SendMsgtextBox.Multiline = true;
            this.SendMsgtextBox.Name = "SendMsgtextBox";
            this.SendMsgtextBox.Size = new System.Drawing.Size(449, 98);
            this.SendMsgtextBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "发送消息:";
            // 
            // RecvMsgtextBox
            // 
            this.RecvMsgtextBox.Location = new System.Drawing.Point(16, 255);
            this.RecvMsgtextBox.Multiline = true;
            this.RecvMsgtextBox.Name = "RecvMsgtextBox";
            this.RecvMsgtextBox.ReadOnly = true;
            this.RecvMsgtextBox.Size = new System.Drawing.Size(449, 98);
            this.RecvMsgtextBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "接受消息:";
            // 
            // CloseConectbutton
            // 
            this.CloseConectbutton.Location = new System.Drawing.Point(429, 31);
            this.CloseConectbutton.Name = "CloseConectbutton";
            this.CloseConectbutton.Size = new System.Drawing.Size(56, 23);
            this.CloseConectbutton.TabIndex = 11;
            this.CloseConectbutton.Text = "关闭";
            this.CloseConectbutton.UseVisualStyleBackColor = true;
            this.CloseConectbutton.Click += new System.EventHandler(this.CloseConectbutton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 365);
            this.Controls.Add(this.CloseConectbutton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RecvMsgtextBox);
            this.Controls.Add(this.SendMsgtextBox);
            this.Controls.Add(this.Sendbutton);
            this.Controls.Add(this.Conectbutton);
            this.Controls.Add(this.targetPorttextBox);
            this.Controls.Add(this.targetIptextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "TcpClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox targetIptextBox;
        private System.Windows.Forms.TextBox targetPorttextBox;
        private System.Windows.Forms.Button Conectbutton;
        private System.Windows.Forms.Button Sendbutton;
        private System.Windows.Forms.TextBox SendMsgtextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox RecvMsgtextBox;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button CloseConectbutton;
    }
}

