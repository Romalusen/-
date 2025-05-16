namespace Луценко
{
    partial class CreateOrderForm
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
            allStores = new ComboBox();
            label1 = new Label();
            dgvAvailableShoes = new DataGridView();
            button1 = new Button();
            dgvOrderCart = new DataGridView();
            lblTotalAmount = new Label();
            txtCustomerName = new Label();
            txtCustomerPhone = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button2 = new Button();
            label2 = new Label();
            textBox3 = new TextBox();
            btnCancelOrder = new Button();
            label3 = new Label();
            Comments = new TextBox();
            btnSubmitOrder = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAvailableShoes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrderCart).BeginInit();
            SuspendLayout();
            // 
            // allStores
            // 
            allStores.FormattingEnabled = true;
            allStores.Location = new Point(164, 168);
            allStores.Name = "allStores";
            allStores.Size = new Size(100, 23);
            allStores.TabIndex = 0;
            allStores.SelectedIndexChanged += allStores_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(41, 168);
            label1.Name = "label1";
            label1.Size = new Size(92, 15);
            label1.TabIndex = 1;
            label1.Text = "Вибір магазину";
            label1.Click += label1_Click;
            // 
            // dgvAvailableShoes
            // 
            dgvAvailableShoes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAvailableShoes.Location = new Point(270, 22);
            dgvAvailableShoes.Name = "dgvAvailableShoes";
            dgvAvailableShoes.Size = new Size(687, 245);
            dgvAvailableShoes.TabIndex = 2;
            dgvAvailableShoes.CellContentClick += dgvAvailableShoes_CellContentClick;
            // 
            // button1
            // 
            button1.Location = new Point(515, 286);
            button1.Name = "button1";
            button1.Size = new Size(121, 23);
            button1.TabIndex = 3;
            button1.Text = "Додати до кошика";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dgvOrderCart
            // 
            dgvOrderCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrderCart.Location = new Point(963, 22);
            dgvOrderCart.Name = "dgvOrderCart";
            dgvOrderCart.Size = new Size(483, 245);
            dgvOrderCart.TabIndex = 4;
            dgvOrderCart.CellContentClick += dgvOrderCart_CellContentClick;
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.AutoSize = true;
            lblTotalAmount.Location = new Point(729, 539);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(139, 15);
            lblTotalAmount.TabIndex = 6;
            lblTotalAmount.Text = "Загальна сума : 0.00 грн";
            lblTotalAmount.Click += lblTotalAmount_Click;
            // 
            // txtCustomerName
            // 
            txtCustomerName.AutoSize = true;
            txtCustomerName.Location = new Point(41, 36);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(69, 15);
            txtCustomerName.TabIndex = 7;
            txtCustomerName.Text = "ПІБ клієнта";
            // 
            // txtCustomerPhone
            // 
            txtCustomerPhone.AutoSize = true;
            txtCustomerPhone.Location = new Point(41, 80);
            txtCustomerPhone.Name = "txtCustomerPhone";
            txtCustomerPhone.Size = new Size(101, 15);
            txtCustomerPhone.TabIndex = 8;
            txtCustomerPhone.Text = "Номер телефону";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(164, 37);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 9;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(164, 77);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 10;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // button2
            // 
            button2.Location = new Point(1155, 273);
            button2.Name = "button2";
            button2.Size = new Size(121, 49);
            button2.TabIndex = 11;
            button2.Text = "видалити з кошика";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(41, 125);
            label2.Name = "label2";
            label2.Size = new Size(92, 15);
            label2.TabIndex = 12;
            label2.Text = "Адрес доставки";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(164, 122);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 13;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // btnCancelOrder
            // 
            btnCancelOrder.DialogResult = DialogResult.Cancel;
            btnCancelOrder.Location = new Point(948, 497);
            btnCancelOrder.Name = "btnCancelOrder";
            btnCancelOrder.Size = new Size(79, 23);
            btnCancelOrder.TabIndex = 15;
            btnCancelOrder.Text = "Скасувати";
            btnCancelOrder.UseVisualStyleBackColor = true;
            btnCancelOrder.Click += btnCancelOrder_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 347);
            label3.Name = "label3";
            label3.Size = new Size(61, 15);
            label3.TabIndex = 16;
            label3.Text = "Коментар";
            // 
            // Comments
            // 
            Comments.Location = new Point(138, 347);
            Comments.Name = "Comments";
            Comments.Size = new Size(637, 23);
            Comments.TabIndex = 17;
            Comments.TextChanged += Comments_TextChanged;
            // 
            // btnSubmitOrder
            // 
            btnSubmitOrder.Location = new Point(561, 497);
            btnSubmitOrder.Name = "btnSubmitOrder";
            btnSubmitOrder.Size = new Size(75, 23);
            btnSubmitOrder.TabIndex = 18;
            btnSubmitOrder.Text = "Замовити";
            btnSubmitOrder.UseVisualStyleBackColor = true;
            btnSubmitOrder.Click += btnSubmitOrder_Click;
            // 
            // CreateOrderForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1458, 563);
            Controls.Add(btnSubmitOrder);
            Controls.Add(Comments);
            Controls.Add(label3);
            Controls.Add(btnCancelOrder);
            Controls.Add(textBox3);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(txtCustomerPhone);
            Controls.Add(txtCustomerName);
            Controls.Add(lblTotalAmount);
            Controls.Add(dgvOrderCart);
            Controls.Add(button1);
            Controls.Add(dgvAvailableShoes);
            Controls.Add(label1);
            Controls.Add(allStores);
            Name = "CreateOrderForm";
            Text = "CreateOrderForm";
            ((System.ComponentModel.ISupportInitialize)dgvAvailableShoes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrderCart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox allStores;
        private Label label1;
        private DataGridView dgvAvailableShoes;
        private Button button1;
        private DataGridView dgvOrderCart;
        private Label lblTotalAmount;
        private Label txtCustomerName;
        private Label txtCustomerPhone;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button2;
        private Label label2;
        private TextBox textBox3;
        private Button btnCancelOrder;
        private Label label3;
        private TextBox Comments;
        private Button btnSubmitOrder;
    }
}