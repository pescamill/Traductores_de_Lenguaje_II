namespace Practica_1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Copiar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Token = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A1 = new System.Windows.Forms.RichTextBox();
            this.ListErrors = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Copiar
            // 
            this.Copiar.Location = new System.Drawing.Point(569, 11);
            this.Copiar.Margin = new System.Windows.Forms.Padding(2);
            this.Copiar.Name = "Copiar";
            this.Copiar.Size = new System.Drawing.Size(374, 36);
            this.Copiar.TabIndex = 2;
            this.Copiar.Text = "Analizar";
            this.Copiar.UseVisualStyleBackColor = true;
            this.Copiar.Click += new System.EventHandler(this.Copiar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Estado,
            this.Token});
            this.dataGridView1.Location = new System.Drawing.Point(569, 51);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(383, 357);
            this.dataGridView1.TabIndex = 3;
            // 
            // Estado
            // 
            this.Estado.HeaderText = "Estado";
            this.Estado.MinimumWidth = 6;
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            this.Estado.Width = 70;
            // 
            // Token
            // 
            this.Token.HeaderText = "Token";
            this.Token.MinimumWidth = 6;
            this.Token.Name = "Token";
            this.Token.ReadOnly = true;
            this.Token.Width = 160;
            // 
            // A1
            // 
            this.A1.Location = new System.Drawing.Point(26, 39);
            this.A1.Margin = new System.Windows.Forms.Padding(2);
            this.A1.Name = "A1";
            this.A1.Size = new System.Drawing.Size(515, 239);
            this.A1.TabIndex = 4;
            this.A1.Text = "";
            // 
            // ListErrors
            // 
            this.ListErrors.FormattingEnabled = true;
            this.ListErrors.HorizontalScrollbar = true;
            this.ListErrors.Location = new System.Drawing.Point(26, 300);
            this.ListErrors.Margin = new System.Windows.Forms.Padding(2);
            this.ListErrors.Name = "ListErrors";
            this.ListErrors.Size = new System.Drawing.Size(515, 108);
            this.ListErrors.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Código a analizar:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 419);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListErrors);
            this.Controls.Add(this.A1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Copiar);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Copiar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox A1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Token;
        private System.Windows.Forms.ListBox ListErrors;
        private System.Windows.Forms.Label label1;
    }
}

