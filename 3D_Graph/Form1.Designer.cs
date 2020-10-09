namespace _3D_Graph
{
    partial class Form1
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
            this.CreateGraph = new System.Windows.Forms.Button();
            this.CloseGraph = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LoadFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // CreateGraph
            // 
            this.CreateGraph.Location = new System.Drawing.Point(134, 359);
            this.CreateGraph.Name = "CreateGraph";
            this.CreateGraph.Size = new System.Drawing.Size(132, 50);
            this.CreateGraph.TabIndex = 0;
            this.CreateGraph.Text = "Сгенерировать 3D-график";
            this.CreateGraph.UseVisualStyleBackColor = true;
            this.CreateGraph.Click += new System.EventHandler(this.createGraph_Click);
            // 
            // CloseGraph
            // 
            this.CloseGraph.Location = new System.Drawing.Point(13, 359);
            this.CloseGraph.Name = "CloseGraph";
            this.CloseGraph.Size = new System.Drawing.Size(115, 50);
            this.CloseGraph.TabIndex = 1;
            this.CloseGraph.Text = "Закрыть график";
            this.CloseGraph.UseVisualStyleBackColor = true;
            this.CloseGraph.Click += new System.EventHandler(this.CloseGraph_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(374, 324);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // LoadFile
            // 
            this.LoadFile.Location = new System.Drawing.Point(272, 359);
            this.LoadFile.Name = "LoadFile";
            this.LoadFile.Size = new System.Drawing.Size(115, 50);
            this.LoadFile.TabIndex = 3;
            this.LoadFile.Text = "Загрузить изображение";
            this.LoadFile.UseVisualStyleBackColor = true;
            this.LoadFile.Click += new System.EventHandler(this.LoadFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 429);
            this.Controls.Add(this.LoadFile);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.CloseGraph);
            this.Controls.Add(this.CreateGraph);
            this.Name = "Form1";
            this.Text = "3D Graph";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CreateGraph;
        private System.Windows.Forms.Button CloseGraph;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button LoadFile;
    }
}

