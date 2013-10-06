namespace TontineUtil.CountryAzureTableGenerator
{
    partial class MainForm
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
            this.btnGenerateCountries = new System.Windows.Forms.Button();
            this.btnGetCountriesInRegion = new System.Windows.Forms.Button();
            this.listBoxCountriesInRegion = new System.Windows.Forms.ListBox();
            this.txtBoxRegion = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGenerateCountries
            // 
            this.btnGenerateCountries.Location = new System.Drawing.Point(12, 12);
            this.btnGenerateCountries.Name = "btnGenerateCountries";
            this.btnGenerateCountries.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateCountries.TabIndex = 0;
            this.btnGenerateCountries.Text = "Generate";
            this.btnGenerateCountries.UseVisualStyleBackColor = true;
            this.btnGenerateCountries.Click += new System.EventHandler(this.btnGenerateCountries_Click);
            // 
            // btnGetCountriesInRegion
            // 
            this.btnGetCountriesInRegion.Location = new System.Drawing.Point(115, 12);
            this.btnGetCountriesInRegion.Name = "btnGetCountriesInRegion";
            this.btnGetCountriesInRegion.Size = new System.Drawing.Size(150, 23);
            this.btnGetCountriesInRegion.TabIndex = 2;
            this.btnGetCountriesInRegion.Text = "Get Countries in Region";
            this.btnGetCountriesInRegion.UseVisualStyleBackColor = true;
            this.btnGetCountriesInRegion.Click += new System.EventHandler(this.btnGetCountriesInRegion_Click);
            // 
            // listBoxCountriesInRegion
            // 
            this.listBoxCountriesInRegion.FormattingEnabled = true;
            this.listBoxCountriesInRegion.Location = new System.Drawing.Point(311, 12);
            this.listBoxCountriesInRegion.Name = "listBoxCountriesInRegion";
            this.listBoxCountriesInRegion.Size = new System.Drawing.Size(146, 95);
            this.listBoxCountriesInRegion.TabIndex = 3;
            // 
            // txtBoxRegion
            // 
            this.txtBoxRegion.Location = new System.Drawing.Point(115, 41);
            this.txtBoxRegion.Name = "txtBoxRegion";
            this.txtBoxRegion.Size = new System.Drawing.Size(75, 20);
            this.txtBoxRegion.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 186);
            this.Controls.Add(this.txtBoxRegion);
            this.Controls.Add(this.listBoxCountriesInRegion);
            this.Controls.Add(this.btnGetCountriesInRegion);
            this.Controls.Add(this.btnGenerateCountries);
            this.Name = "Form1";
            this.Text = "Country Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerateCountries;
        private System.Windows.Forms.Button btnGetCountriesInRegion;
        private System.Windows.Forms.ListBox listBoxCountriesInRegion;
        private System.Windows.Forms.TextBox txtBoxRegion;
    }
}

