namespace Луценко
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            btnAddSpecificShoe = new ToolStripMenuItem();
            removeCurrentStoreToolStripMenuItem = new ToolStripMenuItem();
            вийтиToolStripMenuItem = new ToolStripMenuItem();
            замовленняToolStripMenuItem = new ToolStripMenuItem();
            оформитиЗамовленняToolStripMenuItem = new ToolStripMenuItem();
            діїToolStripMenuItem = new ToolStripMenuItem();
            фільтруванняЗаБрендомToolStripMenuItem = new ToolStripMenuItem();
            фільтруванняЗаТипомToolStripMenuItem = new ToolStripMenuItem();
            фільтруванняЗаРозміромToolStripMenuItem = new ToolStripMenuItem();
            відБільшогоДоМеньшогоToolStripMenuItem = new ToolStripMenuItem();
            відМеньшогоДоБільшогоToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            toolStripButton7 = new ToolStripButton();
            toolStripButton1 = new ToolStripButton();
            toolStripButton2 = new ToolStripButton();
            toolStripButton3 = new ToolStripButton();
            toolStripButton8 = new ToolStripButton();
            toolStripButton5 = new ToolStripButton();
            toolStripButton6 = new ToolStripButton();
            toolStripButton4 = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            dataGridView1 = new DataGridView();
            Магазин = new DataGridViewTextBoxColumn();
            Бренд = new DataGridViewTextBoxColumn();
            Модель = new DataGridViewTextBoxColumn();
            Розмір = new DataGridViewTextBoxColumn();
            Ціна = new DataGridViewTextBoxColumn();
            Тип = new DataGridViewTextBoxColumn();
            Кількість = new DataGridViewTextBoxColumn();
            contextMenuStrip1 = new ContextMenuStrip(components);
            рToolStripMenuItem = new ToolStripMenuItem();
            видалитиToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, замовленняToolStripMenuItem, діїToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(739, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { btnAddSpecificShoe, removeCurrentStoreToolStripMenuItem, вийтиToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(48, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // btnAddSpecificShoe
            // 
            btnAddSpecificShoe.Name = "btnAddSpecificShoe";
            btnAddSpecificShoe.Size = new Size(178, 22);
            btnAddSpecificShoe.Text = "Додати нове взуття";
            btnAddSpecificShoe.Click += btnAddSpecificShoe_Click;
            // 
            // removeCurrentStoreToolStripMenuItem
            // 
            removeCurrentStoreToolStripMenuItem.Name = "removeCurrentStoreToolStripMenuItem";
            removeCurrentStoreToolStripMenuItem.Size = new Size(178, 22);
            removeCurrentStoreToolStripMenuItem.Text = "Видалити магазин";
            removeCurrentStoreToolStripMenuItem.Click += removeCurrentStoreToolStripMenuItem_Click;
            // 
            // вийтиToolStripMenuItem
            // 
            вийтиToolStripMenuItem.Name = "вийтиToolStripMenuItem";
            вийтиToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            вийтиToolStripMenuItem.Size = new Size(178, 22);
            вийтиToolStripMenuItem.Text = "Вийти";
            вийтиToolStripMenuItem.Click += вийтиToolStripMenuItem_Click;
            // 
            // замовленняToolStripMenuItem
            // 
            замовленняToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { оформитиЗамовленняToolStripMenuItem });
            замовленняToolStripMenuItem.Name = "замовленняToolStripMenuItem";
            замовленняToolStripMenuItem.Size = new Size(87, 20);
            замовленняToolStripMenuItem.Text = "Замовлення";
            // 
            // оформитиЗамовленняToolStripMenuItem
            // 
            оформитиЗамовленняToolStripMenuItem.Name = "оформитиЗамовленняToolStripMenuItem";
            оформитиЗамовленняToolStripMenuItem.Size = new Size(203, 22);
            оформитиЗамовленняToolStripMenuItem.Text = "Оформити замовлення";
            оформитиЗамовленняToolStripMenuItem.Click += оформитиЗамовленняToolStripMenuItem_Click_1;
            // 
            // діїToolStripMenuItem
            // 
            діїToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { фільтруванняЗаБрендомToolStripMenuItem, фільтруванняЗаТипомToolStripMenuItem, фільтруванняЗаРозміромToolStripMenuItem });
            діїToolStripMenuItem.Name = "діїToolStripMenuItem";
            діїToolStripMenuItem.Size = new Size(33, 20);
            діїToolStripMenuItem.Text = "Дії";
            // 
            // фільтруванняЗаБрендомToolStripMenuItem
            // 
            фільтруванняЗаБрендомToolStripMenuItem.Name = "фільтруванняЗаБрендомToolStripMenuItem";
            фільтруванняЗаБрендомToolStripMenuItem.Size = new Size(220, 22);
            фільтруванняЗаБрендомToolStripMenuItem.Text = "фільтрування за брендом";
            фільтруванняЗаБрендомToolStripMenuItem.Click += фільтруванняЗаБрендомToolStripMenuItem_DropDownOpening;
            // 
            // фільтруванняЗаТипомToolStripMenuItem
            // 
            фільтруванняЗаТипомToolStripMenuItem.Name = "фільтруванняЗаТипомToolStripMenuItem";
            фільтруванняЗаТипомToolStripMenuItem.Size = new Size(220, 22);
            фільтруванняЗаТипомToolStripMenuItem.Text = "фільтрування за типом";
            фільтруванняЗаТипомToolStripMenuItem.Click += фільтруванняЗаТипомToolStripMenuItem_Click;
            // 
            // фільтруванняЗаРозміромToolStripMenuItem
            // 
            фільтруванняЗаРозміромToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { відБільшогоДоМеньшогоToolStripMenuItem, відМеньшогоДоБільшогоToolStripMenuItem });
            фільтруванняЗаРозміромToolStripMenuItem.Name = "фільтруванняЗаРозміромToolStripMenuItem";
            фільтруванняЗаРозміромToolStripMenuItem.Size = new Size(220, 22);
            фільтруванняЗаРозміромToolStripMenuItem.Text = "фільтрування за розміром";
            // 
            // відБільшогоДоМеньшогоToolStripMenuItem
            // 
            відБільшогоДоМеньшогоToolStripMenuItem.Name = "відБільшогоДоМеньшогоToolStripMenuItem";
            відБільшогоДоМеньшогоToolStripMenuItem.Size = new Size(223, 22);
            відБільшогоДоМеньшогоToolStripMenuItem.Text = "Від більшого до меньшого";
            відБільшогоДоМеньшогоToolStripMenuItem.Click += відБільшогоДоМеньшогоToolStripMenuItem_Click;
            // 
            // відМеньшогоДоБільшогоToolStripMenuItem
            // 
            відМеньшогоДоБільшогоToolStripMenuItem.Name = "відМеньшогоДоБільшогоToolStripMenuItem";
            відМеньшогоДоБільшогоToolStripMenuItem.Size = new Size(223, 22);
            відМеньшогоДоБільшогоToolStripMenuItem.Text = "Від меньшого до більшого";
            відМеньшогоДоБільшогоToolStripMenuItem.Click += відМеньшогоДоБільшогоToolStripMenuItem_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton7, toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton8, toolStripButton5, toolStripButton6, toolStripButton4 });
            toolStrip1.Location = new Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(739, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton7
            // 
            toolStripButton7.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton7.Image = (Image)resources.GetObject("toolStripButton7.Image");
            toolStripButton7.ImageTransparentColor = Color.Magenta;
            toolStripButton7.Name = "toolStripButton7";
            toolStripButton7.Size = new Size(23, 22);
            toolStripButton7.Text = "Показати все";
            toolStripButton7.Click += toolStripButton7_Click;
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(23, 22);
            toolStripButton1.Text = "Додати новий магазин";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(23, 22);
            toolStripButton2.Text = "Сортувати від А до Я";
            toolStripButton2.Click += toolStripButton2_Click;
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(23, 22);
            toolStripButton3.Text = "Сортувати від Я до А";
            toolStripButton3.Click += toolStripButton3_Click;
            // 
            // toolStripButton8
            // 
            toolStripButton8.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton8.Image = (Image)resources.GetObject("toolStripButton8.Image");
            toolStripButton8.ImageTransparentColor = Color.Magenta;
            toolStripButton8.Name = "toolStripButton8";
            toolStripButton8.Size = new Size(23, 22);
            toolStripButton8.Text = "Пошук по магазинам";
            toolStripButton8.Click += toolStripButton8_Click;
            // 
            // toolStripButton5
            // 
            toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton5.Image = (Image)resources.GetObject("toolStripButton5.Image");
            toolStripButton5.ImageTransparentColor = Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new Size(23, 22);
            toolStripButton5.Text = "В наявності";
            toolStripButton5.ToolTipText = "В наявності ";
            toolStripButton5.Click += toolStripButton5_Click;
            // 
            // toolStripButton6
            // 
            toolStripButton6.BackgroundImageLayout = ImageLayout.None;
            toolStripButton6.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton6.Image = (Image)resources.GetObject("toolStripButton6.Image");
            toolStripButton6.ImageTransparentColor = Color.Magenta;
            toolStripButton6.Name = "toolStripButton6";
            toolStripButton6.Size = new Size(23, 22);
            toolStripButton6.Text = "Не в наявності";
            toolStripButton6.ToolTipText = "Не в наявності ";
            toolStripButton6.Click += toolStripButton6_Click;
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton4.Image = (Image)resources.GetObject("toolStripButton4.Image");
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(23, 22);
            toolStripButton4.Text = "Переглянути історію покупок";
            toolStripButton4.Click += toolStripButton4_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new Point(0, 388);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(739, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Магазин, Бренд, Модель, Розмір, Ціна, Тип, Кількість });
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Location = new Point(0, 52);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(727, 367);
            dataGridView1.TabIndex = 3;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.MouseDown += dataGridView1_MouseDown;
            // 
            // Магазин
            // 
            Магазин.HeaderText = "Назва магазину";
            Магазин.Name = "Магазин";
            Магазин.ReadOnly = true;
            // 
            // Бренд
            // 
            Бренд.HeaderText = "Бренд";
            Бренд.Name = "Бренд";
            Бренд.ReadOnly = true;
            // 
            // Модель
            // 
            Модель.HeaderText = "Модель";
            Модель.Name = "Модель";
            Модель.ReadOnly = true;
            // 
            // Розмір
            // 
            Розмір.HeaderText = "Розмір";
            Розмір.Name = "Розмір";
            Розмір.ReadOnly = true;
            // 
            // Ціна
            // 
            Ціна.HeaderText = "Ціна";
            Ціна.Name = "Ціна";
            Ціна.ReadOnly = true;
            // 
            // Тип
            // 
            Тип.HeaderText = "Тип Взуття";
            Тип.Name = "Тип";
            Тип.ReadOnly = true;
            // 
            // Кількість
            // 
            Кількість.HeaderText = "Кількість пар";
            Кількість.Name = "Кількість";
            Кількість.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { рToolStripMenuItem, видалитиToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(135, 48);
            // 
            // рToolStripMenuItem
            // 
            рToolStripMenuItem.Name = "рToolStripMenuItem";
            рToolStripMenuItem.Size = new Size(134, 22);
            рToolStripMenuItem.Text = "Редагувати";
            рToolStripMenuItem.Click += рToolStripMenuItem_Click;
            // 
            // видалитиToolStripMenuItem
            // 
            видалитиToolStripMenuItem.Name = "видалитиToolStripMenuItem";
            видалитиToolStripMenuItem.Size = new Size(134, 22);
            видалитиToolStripMenuItem.Text = "Видалити";
            видалитиToolStripMenuItem.Click += видалитиToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(739, 410);
            Controls.Add(dataGridView1);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem btnAddSpecificShoe;
        private ToolStripMenuItem замовленняToolStripMenuItem;
        private ToolStripMenuItem оформитиЗамовленняToolStripMenuItem;
        private ToolStripMenuItem діїToolStripMenuItem;
        private ToolStripMenuItem фільтруванняЗаБрендомToolStripMenuItem;
        private ToolStripMenuItem фільтруванняЗаТипомToolStripMenuItem;
        private ToolStripMenuItem фільтруванняЗаРозміромToolStripMenuItem;
        private ToolStripMenuItem вийтиToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton4;
        private StatusStrip statusStrip1;
        private DataGridView dataGridView1;
        private ToolStripButton toolStripButton5;
        private ToolStripButton toolStripButton6;
        private ToolStripButton toolStripButton7;
        private ToolStripButton toolStripButton8;
        private ToolStripMenuItem відБільшогоДоМеньшогоToolStripMenuItem;
        private ToolStripMenuItem відМеньшогоДоБільшогоToolStripMenuItem;
        private DataGridViewTextBoxColumn Магазин;
        private DataGridViewTextBoxColumn Бренд;
        private DataGridViewTextBoxColumn Модель;
        private DataGridViewTextBoxColumn Розмір;
        private DataGridViewTextBoxColumn Ціна;
        private DataGridViewTextBoxColumn Тип;
        private DataGridViewTextBoxColumn Кількість;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem рToolStripMenuItem;
        private ToolStripMenuItem видалитиToolStripMenuItem;
        private ToolStripMenuItem removeCurrentStoreToolStripMenuItem;
    }
}