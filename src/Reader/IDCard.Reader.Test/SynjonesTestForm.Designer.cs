﻿namespace IDCard.Reader.Test
{
    partial class SynjonesTestForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnReadTextPhotoInfo = new System.Windows.Forms.Button();
            this.tbxResultInfo = new System.Windows.Forms.TextBox();
            this.btnReadNewAddressInfo = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReadTextPhotoInfo
            // 
            this.btnReadTextPhotoInfo.Location = new System.Drawing.Point(12, 12);
            this.btnReadTextPhotoInfo.Name = "btnReadTextPhotoInfo";
            this.btnReadTextPhotoInfo.Size = new System.Drawing.Size(104, 23);
            this.btnReadTextPhotoInfo.TabIndex = 0;
            this.btnReadTextPhotoInfo.Text = "读文字照片信息";
            this.btnReadTextPhotoInfo.UseVisualStyleBackColor = true;
            this.btnReadTextPhotoInfo.Click += new System.EventHandler(this.btnReadTextPhotoInfo_Click);
            // 
            // tbxResultInfo
            // 
            this.tbxResultInfo.Location = new System.Drawing.Point(12, 41);
            this.tbxResultInfo.Multiline = true;
            this.tbxResultInfo.Name = "tbxResultInfo";
            this.tbxResultInfo.ReadOnly = true;
            this.tbxResultInfo.Size = new System.Drawing.Size(444, 234);
            this.tbxResultInfo.TabIndex = 1;
            // 
            // btnReadNewAddressInfo
            // 
            this.btnReadNewAddressInfo.Location = new System.Drawing.Point(122, 12);
            this.btnReadNewAddressInfo.Name = "btnReadNewAddressInfo";
            this.btnReadNewAddressInfo.Size = new System.Drawing.Size(104, 23);
            this.btnReadNewAddressInfo.TabIndex = 2;
            this.btnReadNewAddressInfo.Text = "读追加地址信息";
            this.btnReadNewAddressInfo.UseVisualStyleBackColor = true;
            this.btnReadNewAddressInfo.Click += new System.EventHandler(this.btnReadNewAddressInfo_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(381, 12);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 3;
            this.btnClearLog.Text = "清除日志";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // SynjonesTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 287);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.btnReadNewAddressInfo);
            this.Controls.Add(this.tbxResultInfo);
            this.Controls.Add(this.btnReadTextPhotoInfo);
            this.Name = "SynjonesTestForm";
            this.Text = "新中新身份证阅读器测试";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReadTextPhotoInfo;
        private System.Windows.Forms.TextBox tbxResultInfo;
        private System.Windows.Forms.Button btnReadNewAddressInfo;
        private System.Windows.Forms.Button btnClearLog;
    }
}

