namespace Луценко
{
    partial class AddShopForm
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
            Name = new Label();
            Address = new Label();
            ContactInfo = new Label();
            txtName = new TextBox();
            txtAddress = new TextBox();
            txtContactInfo = new TextBox();
            btnSaveStore = new Button();
            btnCancelStore = new Button();
            SuspendLayout();
            // 
            // Name
            // 
            Name.AutoSize = true;
            Name.Location = new Point(228, 55);
            Name.Name = "Name";
            Name.Size = new Size(93, 15);
            Name.TabIndex = 0;
            Name.Text = "Назва магазину";
            Name.Click += Name_Click;
            // 
            // Address
            // 
            Address.AutoSize = true;
            Address.Location = new Point(228, 97);
            Address.Name = "Address";
            Address.Size = new Size(49, 15);
            Address.TabIndex = 1;
            Address.Text = "Адреса:";
            // 
            // ContactInfo
            // 
            ContactInfo.AutoSize = true;
            ContactInfo.Location = new Point(228, 141);
            ContactInfo.Name = "ContactInfo";
            ContactInfo.Size = new Size(133, 15);
            ContactInfo.TabIndex = 2;
            ContactInfo.Text = "Контактна інформація:";
            // 
            // txtName
            // 
            txtName.Location = new Point(428, 47);
            txtName.Name = "txtName";
            txtName.Size = new Size(100, 23);
            txtName.TabIndex = 3;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(428, 89);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(100, 23);
            txtAddress.TabIndex = 4;
            txtAddress.TextChanged += txtAddress_TextChanged;
            // 
            // txtContactInfo
            // 
            txtContactInfo.Location = new Point(428, 133);
            txtContactInfo.Name = "txtContactInfo";
            txtContactInfo.Size = new Size(100, 23);
            txtContactInfo.TabIndex = 5;
            txtContactInfo.TextChanged += textBox3_TextChanged;
            // 
            // btnSaveStore
            // 
            btnSaveStore.Location = new Point(247, 207);
            btnSaveStore.Name = "btnSaveStore";
            btnSaveStore.Size = new Size(75, 23);
            btnSaveStore.TabIndex = 6;
            btnSaveStore.Text = "Зберегти";
            btnSaveStore.UseVisualStyleBackColor = true;
            btnSaveStore.Click += btnSaveStore_Click;
            // 
            // btnCancelStore
            // 
            btnCancelStore.DialogResult = DialogResult.Cancel;
            btnCancelStore.Location = new Point(414, 207);
            btnCancelStore.Name = "btnCancelStore";
            btnCancelStore.Size = new Size(75, 23);
            btnCancelStore.TabIndex = 7;
            btnCancelStore.Text = "Скасувати";
            btnCancelStore.UseVisualStyleBackColor = true;
            btnCancelStore.Click += btnCancelStore_Click;
            // 
            // AddShopForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCancelStore);
            Controls.Add(btnSaveStore);
            Controls.Add(txtContactInfo);
            Controls.Add(txtAddress);
            Controls.Add(txtName);
            Controls.Add(ContactInfo);
            Controls.Add(Address);
            Controls.Add(Name);
            Text = "AddShopForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Name;
        private Label Address;
        private Label ContactInfo;
        private TextBox txtName;
        private TextBox txtAddress;
        private TextBox txtContactInfo;
        private Button btnSaveStore;
        private Button btnCancelStore;
    }
}