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
            this.btnGetCountry = new System.Windows.Forms.Button();
            this.txtBoxCountryName = new System.Windows.Forms.TextBox();
            this.grpBoxCountry = new System.Windows.Forms.GroupBox();
            this.picBoxFlag = new System.Windows.Forms.PictureBox();
            this.lblCurrencyAlpha3Code = new System.Windows.Forms.Label();
            this.lblCapital = new System.Windows.Forms.Label();
            this.lblNumberCode = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.lblCountryName = new System.Windows.Forms.Label();
            this.lblAlpha3Code = new System.Windows.Forms.Label();
            this.lblAlpha2Code = new System.Windows.Forms.Label();
            this.btnAddCountry = new System.Windows.Forms.Button();
            this.txtBoxNewCountryName = new System.Windows.Forms.TextBox();
            this.txtBoxNewRegion = new System.Windows.Forms.TextBox();
            this.txtBoxNewNumberCode = new System.Windows.Forms.TextBox();
            this.txtBoxNewCurrecyAlpha3Code = new System.Windows.Forms.TextBox();
            this.txtBoxNewAlpha3Code = new System.Windows.Forms.TextBox();
            this.txtBoxNewAlpha2Code = new System.Windows.Forms.TextBox();
            this.txtBoxNewCapital = new System.Windows.Forms.TextBox();
            this.grpBoxCountry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxFlag)).BeginInit();
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
            this.btnGetCountriesInRegion.Location = new System.Drawing.Point(306, 12);
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
            this.listBoxCountriesInRegion.Location = new System.Drawing.Point(462, 12);
            this.listBoxCountriesInRegion.Name = "listBoxCountriesInRegion";
            this.listBoxCountriesInRegion.Size = new System.Drawing.Size(146, 95);
            this.listBoxCountriesInRegion.TabIndex = 3;
            // 
            // txtBoxRegion
            // 
            this.txtBoxRegion.Location = new System.Drawing.Point(381, 41);
            this.txtBoxRegion.Name = "txtBoxRegion";
            this.txtBoxRegion.Size = new System.Drawing.Size(75, 20);
            this.txtBoxRegion.TabIndex = 4;
            // 
            // btnGetCountry
            // 
            this.btnGetCountry.Location = new System.Drawing.Point(8, 84);
            this.btnGetCountry.Name = "btnGetCountry";
            this.btnGetCountry.Size = new System.Drawing.Size(75, 23);
            this.btnGetCountry.TabIndex = 5;
            this.btnGetCountry.Text = "Get Country";
            this.btnGetCountry.UseVisualStyleBackColor = true;
            this.btnGetCountry.Click += new System.EventHandler(this.btnGetCountry_Click);
            // 
            // txtBoxCountryName
            // 
            this.txtBoxCountryName.Location = new System.Drawing.Point(8, 113);
            this.txtBoxCountryName.Name = "txtBoxCountryName";
            this.txtBoxCountryName.Size = new System.Drawing.Size(75, 20);
            this.txtBoxCountryName.TabIndex = 6;
            this.txtBoxCountryName.Text = "Austria";
            // 
            // grpBoxCountry
            // 
            this.grpBoxCountry.Controls.Add(this.picBoxFlag);
            this.grpBoxCountry.Controls.Add(this.lblCurrencyAlpha3Code);
            this.grpBoxCountry.Controls.Add(this.lblCapital);
            this.grpBoxCountry.Controls.Add(this.lblNumberCode);
            this.grpBoxCountry.Controls.Add(this.lblRegion);
            this.grpBoxCountry.Controls.Add(this.lblCountryName);
            this.grpBoxCountry.Controls.Add(this.lblAlpha3Code);
            this.grpBoxCountry.Controls.Add(this.lblAlpha2Code);
            this.grpBoxCountry.Location = new System.Drawing.Point(89, 84);
            this.grpBoxCountry.Name = "grpBoxCountry";
            this.grpBoxCountry.Size = new System.Drawing.Size(295, 118);
            this.grpBoxCountry.TabIndex = 7;
            this.grpBoxCountry.TabStop = false;
            this.grpBoxCountry.Text = "Country";
            // 
            // picBoxFlag
            // 
            this.picBoxFlag.Location = new System.Drawing.Point(133, 16);
            this.picBoxFlag.Name = "picBoxFlag";
            this.picBoxFlag.Size = new System.Drawing.Size(150, 91);
            this.picBoxFlag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxFlag.TabIndex = 7;
            this.picBoxFlag.TabStop = false;
            // 
            // lblCurrencyAlpha3Code
            // 
            this.lblCurrencyAlpha3Code.AutoSize = true;
            this.lblCurrencyAlpha3Code.Location = new System.Drawing.Point(8, 94);
            this.lblCurrencyAlpha3Code.Name = "lblCurrencyAlpha3Code";
            this.lblCurrencyAlpha3Code.Size = new System.Drawing.Size(119, 13);
            this.lblCurrencyAlpha3Code.TabIndex = 6;
            this.lblCurrencyAlpha3Code.Text = "<CurrencyAlpha3Code>";
            // 
            // lblCapital
            // 
            this.lblCapital.AutoSize = true;
            this.lblCapital.Location = new System.Drawing.Point(8, 55);
            this.lblCapital.Name = "lblCapital";
            this.lblCapital.Size = new System.Drawing.Size(51, 13);
            this.lblCapital.TabIndex = 5;
            this.lblCapital.Text = "<Capital>";
            // 
            // lblNumberCode
            // 
            this.lblNumberCode.AutoSize = true;
            this.lblNumberCode.Location = new System.Drawing.Point(8, 42);
            this.lblNumberCode.Name = "lblNumberCode";
            this.lblNumberCode.Size = new System.Drawing.Size(81, 13);
            this.lblNumberCode.TabIndex = 4;
            this.lblNumberCode.Text = "<NumberCode>";
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(8, 29);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(53, 13);
            this.lblRegion.TabIndex = 3;
            this.lblRegion.Text = "<Region>";
            // 
            // lblCountryName
            // 
            this.lblCountryName.AutoSize = true;
            this.lblCountryName.Location = new System.Drawing.Point(8, 16);
            this.lblCountryName.Name = "lblCountryName";
            this.lblCountryName.Size = new System.Drawing.Size(83, 13);
            this.lblCountryName.TabIndex = 2;
            this.lblCountryName.Text = "<CountryName>";
            // 
            // lblAlpha3Code
            // 
            this.lblAlpha3Code.AutoSize = true;
            this.lblAlpha3Code.Location = new System.Drawing.Point(8, 81);
            this.lblAlpha3Code.Name = "lblAlpha3Code";
            this.lblAlpha3Code.Size = new System.Drawing.Size(77, 13);
            this.lblAlpha3Code.TabIndex = 1;
            this.lblAlpha3Code.Text = "<Alpha3Code>";
            // 
            // lblAlpha2Code
            // 
            this.lblAlpha2Code.AutoSize = true;
            this.lblAlpha2Code.Location = new System.Drawing.Point(8, 68);
            this.lblAlpha2Code.Name = "lblAlpha2Code";
            this.lblAlpha2Code.Size = new System.Drawing.Size(77, 13);
            this.lblAlpha2Code.TabIndex = 0;
            this.lblAlpha2Code.Text = "<Alpha2Code>";
            // 
            // btnAddCountry
            // 
            this.btnAddCountry.Location = new System.Drawing.Point(12, 238);
            this.btnAddCountry.Name = "btnAddCountry";
            this.btnAddCountry.Size = new System.Drawing.Size(75, 23);
            this.btnAddCountry.TabIndex = 8;
            this.btnAddCountry.Text = "Add Country";
            this.btnAddCountry.UseVisualStyleBackColor = true;
            this.btnAddCountry.Click += new System.EventHandler(this.btnAddCountry_Click);
            // 
            // txtBoxNewCountryName
            // 
            this.txtBoxNewCountryName.Location = new System.Drawing.Point(100, 240);
            this.txtBoxNewCountryName.Name = "txtBoxNewCountryName";
            this.txtBoxNewCountryName.Size = new System.Drawing.Size(100, 20);
            this.txtBoxNewCountryName.TabIndex = 9;
            this.txtBoxNewCountryName.Text = "Zamunda";
            // 
            // txtBoxNewRegion
            // 
            this.txtBoxNewRegion.Location = new System.Drawing.Point(100, 265);
            this.txtBoxNewRegion.Name = "txtBoxNewRegion";
            this.txtBoxNewRegion.Size = new System.Drawing.Size(100, 20);
            this.txtBoxNewRegion.TabIndex = 10;
            this.txtBoxNewRegion.Text = "Africa";
            // 
            // txtBoxNewNumberCode
            // 
            this.txtBoxNewNumberCode.Location = new System.Drawing.Point(100, 290);
            this.txtBoxNewNumberCode.Name = "txtBoxNewNumberCode";
            this.txtBoxNewNumberCode.Size = new System.Drawing.Size(100, 20);
            this.txtBoxNewNumberCode.TabIndex = 11;
            this.txtBoxNewNumberCode.Text = "999";
            // 
            // txtBoxNewCurrecyAlpha3Code
            // 
            this.txtBoxNewCurrecyAlpha3Code.Location = new System.Drawing.Point(100, 390);
            this.txtBoxNewCurrecyAlpha3Code.Name = "txtBoxNewCurrecyAlpha3Code";
            this.txtBoxNewCurrecyAlpha3Code.Size = new System.Drawing.Size(100, 20);
            this.txtBoxNewCurrecyAlpha3Code.TabIndex = 14;
            this.txtBoxNewCurrecyAlpha3Code.Text = "ZDL";
            // 
            // txtBoxNewAlpha3Code
            // 
            this.txtBoxNewAlpha3Code.Location = new System.Drawing.Point(100, 365);
            this.txtBoxNewAlpha3Code.Name = "txtBoxNewAlpha3Code";
            this.txtBoxNewAlpha3Code.Size = new System.Drawing.Size(100, 20);
            this.txtBoxNewAlpha3Code.TabIndex = 13;
            this.txtBoxNewAlpha3Code.Text = "ZMD";
            // 
            // txtBoxNewAlpha2Code
            // 
            this.txtBoxNewAlpha2Code.Location = new System.Drawing.Point(100, 340);
            this.txtBoxNewAlpha2Code.Name = "txtBoxNewAlpha2Code";
            this.txtBoxNewAlpha2Code.Size = new System.Drawing.Size(100, 20);
            this.txtBoxNewAlpha2Code.TabIndex = 12;
            this.txtBoxNewAlpha2Code.Text = "ZM";
            // 
            // txtBoxNewCapital
            // 
            this.txtBoxNewCapital.Location = new System.Drawing.Point(100, 315);
            this.txtBoxNewCapital.Name = "txtBoxNewCapital";
            this.txtBoxNewCapital.Size = new System.Drawing.Size(100, 20);
            this.txtBoxNewCapital.TabIndex = 15;
            this.txtBoxNewCapital.Text = "Zamunda City";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 461);
            this.Controls.Add(this.txtBoxNewCapital);
            this.Controls.Add(this.txtBoxNewCurrecyAlpha3Code);
            this.Controls.Add(this.txtBoxNewAlpha3Code);
            this.Controls.Add(this.txtBoxNewAlpha2Code);
            this.Controls.Add(this.txtBoxNewNumberCode);
            this.Controls.Add(this.txtBoxNewRegion);
            this.Controls.Add(this.txtBoxNewCountryName);
            this.Controls.Add(this.btnAddCountry);
            this.Controls.Add(this.grpBoxCountry);
            this.Controls.Add(this.txtBoxCountryName);
            this.Controls.Add(this.btnGetCountry);
            this.Controls.Add(this.txtBoxRegion);
            this.Controls.Add(this.listBoxCountriesInRegion);
            this.Controls.Add(this.btnGetCountriesInRegion);
            this.Controls.Add(this.btnGenerateCountries);
            this.Name = "MainForm";
            this.Text = "Country Generator";
            this.grpBoxCountry.ResumeLayout(false);
            this.grpBoxCountry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxFlag)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerateCountries;
        private System.Windows.Forms.Button btnGetCountriesInRegion;
        private System.Windows.Forms.ListBox listBoxCountriesInRegion;
        private System.Windows.Forms.TextBox txtBoxRegion;
        private System.Windows.Forms.Button btnGetCountry;
        private System.Windows.Forms.TextBox txtBoxCountryName;
        private System.Windows.Forms.GroupBox grpBoxCountry;
        private System.Windows.Forms.Label lblAlpha2Code;
        private System.Windows.Forms.Label lblAlpha3Code;
        private System.Windows.Forms.Label lblCountryName;
        private System.Windows.Forms.Label lblCurrencyAlpha3Code;
        private System.Windows.Forms.Label lblCapital;
        private System.Windows.Forms.Label lblNumberCode;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.PictureBox picBoxFlag;
        private System.Windows.Forms.Button btnAddCountry;
        private System.Windows.Forms.TextBox txtBoxNewCountryName;
        private System.Windows.Forms.TextBox txtBoxNewRegion;
        private System.Windows.Forms.TextBox txtBoxNewNumberCode;
        private System.Windows.Forms.TextBox txtBoxNewCurrecyAlpha3Code;
        private System.Windows.Forms.TextBox txtBoxNewAlpha3Code;
        private System.Windows.Forms.TextBox txtBoxNewAlpha2Code;
        private System.Windows.Forms.TextBox txtBoxNewCapital;
    }
}

