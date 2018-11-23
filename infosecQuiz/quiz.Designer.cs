namespace infosecQuiz
{
    partial class quiz
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
            this.questionNumber = new System.Windows.Forms.Label();
            this.questionLabel = new System.Windows.Forms.TextBox();
            this.answerAText = new System.Windows.Forms.Button();
            this.answerBText = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.reputationBar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.verticleProgressBar1 = new infosecQuiz.verticleProgressBar();
            this.SuspendLayout();
            // 
            // questionNumber
            // 
            this.questionNumber.AutoSize = true;
            this.questionNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionNumber.Location = new System.Drawing.Point(43, 40);
            this.questionNumber.Name = "questionNumber";
            this.questionNumber.Size = new System.Drawing.Size(199, 37);
            this.questionNumber.TabIndex = 0;
            this.questionNumber.Text = "Question n/n";
            // 
            // questionLabel
            // 
            this.questionLabel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.questionLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.questionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionLabel.Location = new System.Drawing.Point(53, 115);
            this.questionLabel.Multiline = true;
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(600, 200);
            this.questionLabel.TabIndex = 1;
            this.questionLabel.TabStop = false;
            this.questionLabel.Text = "example question lorem ipsum";
            // 
            // answerAText
            // 
            this.answerAText.Location = new System.Drawing.Point(50, 479);
            this.answerAText.Name = "answerAText";
            this.answerAText.Size = new System.Drawing.Size(300, 150);
            this.answerAText.TabIndex = 2;
            this.answerAText.Text = "exampleanswer1";
            this.answerAText.UseVisualStyleBackColor = true;
            this.answerAText.Click += new System.EventHandler(this.answerA_Click);
            // 
            // answerBText
            // 
            this.answerBText.Location = new System.Drawing.Point(350, 479);
            this.answerBText.Name = "answerBText";
            this.answerBText.Size = new System.Drawing.Size(300, 150);
            this.answerBText.TabIndex = 3;
            this.answerBText.Text = "exampleanswer2";
            this.answerBText.UseVisualStyleBackColor = true;
            this.answerBText.Click += new System.EventHandler(this.answerB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 439);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 37);
            this.label1.TabIndex = 4;
            this.label1.Text = "Answer 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(343, 439);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 37);
            this.label2.TabIndex = 5;
            this.label2.Text = "Answer 2";
            // 
            // reputationBar
            // 
            this.reputationBar.Location = new System.Drawing.Point(934, 80);
            this.reputationBar.Name = "reputationBar";
            this.reputationBar.Size = new System.Drawing.Size(45, 549);
            this.reputationBar.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(757, 592);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 37);
            this.label3.TabIndex = 8;
            this.label3.Text = "Reputation";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1019, 592);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 37);
            this.label4.TabIndex = 9;
            this.label4.Text = "Threat";
            // 
            // verticleProgressBar1
            // 
            this.verticleProgressBar1.Location = new System.Drawing.Point(1135, 80);
            this.verticleProgressBar1.Name = "verticleProgressBar1";
            this.verticleProgressBar1.Size = new System.Drawing.Size(45, 549);
            this.verticleProgressBar1.TabIndex = 10;
            this.verticleProgressBar1.Click += new System.EventHandler(this.verticleProgressBar1_Click);
            // 
            // quiz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.verticleProgressBar1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.reputationBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.answerBText);
            this.Controls.Add(this.answerAText);
            this.Controls.Add(this.questionLabel);
            this.Controls.Add(this.questionNumber);
            this.Name = "quiz";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.quiz_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label questionNumber;
        private System.Windows.Forms.TextBox questionLabel;
        private System.Windows.Forms.Button answerAText;
        private System.Windows.Forms.Button answerBText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar reputationBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private verticleProgressBar verticleProgressBar1;
    }
}

