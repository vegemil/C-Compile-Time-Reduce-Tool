namespace Compile_Time_Reduce_Tool
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.FileBrowseButton = new System.Windows.Forms.Button();
            this.FilePathTextBox = new System.Windows.Forms.TextBox();
            this.ResultTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // FileBrowseButton
            // 
            this.FileBrowseButton.Location = new System.Drawing.Point(13, 13);
            this.FileBrowseButton.Name = "FileBrowseButton";
            this.FileBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.FileBrowseButton.TabIndex = 0;
            this.FileBrowseButton.Text = "파일 열기";
            this.FileBrowseButton.UseVisualStyleBackColor = true;
            this.FileBrowseButton.Click += new System.EventHandler(this.FileBrowseButton_Click);
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.Location = new System.Drawing.Point(95, 14);
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.Size = new System.Drawing.Size(531, 21);
            this.FilePathTextBox.TabIndex = 1;
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.Location = new System.Drawing.Point(15, 46);
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.Size = new System.Drawing.Size(610, 273);
            this.ResultTextBox.TabIndex = 2;
            this.ResultTextBox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 335);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.FilePathTextBox);
            this.Controls.Add(this.FileBrowseButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FileBrowseButton;
        private System.Windows.Forms.TextBox FilePathTextBox;
        private System.Windows.Forms.RichTextBox ResultTextBox;
    }
}

