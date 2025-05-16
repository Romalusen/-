using System;
using System.Windows.Forms;

namespace Луценко // Переконайтесь, що namespace той самий, що і в MainForm та Designer
{
    public partial class AddShoeForm : Form
    {
        private Shoe shoeToEdit; // Взуття, яке редагується (null, якщо додається нове)
        public Shoe AddedShoe { get; private set; } // Для повернення нового взуття в MainForm

        // Конструктор для режиму ДОДАВАННЯ нового взуття
        public AddShoeForm()
        {
            InitializeComponent(); // Цей метод ініціалізує всі ваші textBox1, numericUpDown1 і т.д.
            this.Text = "Додати нове взуття";
            // ... (код ініціалізації інших компонентів) ...

            this.btnSave = new System.Windows.Forms.Button();
            // ... (інші властивості btnSave, як Text, Location, Name, Size, etc.) ...
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 13; // Наприклад
            this.btnSave.Text = "Зберегти";
            this.btnSave.UseVisualStyleBackColor = true;
            // Ось цей важливий рядок:
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        }

        // Конструктор для режиму РЕДАГУВАННЯ існуючого взуття
        public AddShoeForm(Shoe shoeToEdit) : this() // Викликаємо конструктор без параметрів для InitializeComponent()
        {
            this.shoeToEdit = shoeToEdit;
            this.Text = "Редагувати інформацію про взуття";
            btnSave.Text = "Зберегти зміни"; // Змінюємо текст кнопки для режиму редагування

            // Заповнюємо поля форми даними з об'єкта shoeToEdit
            if (this.shoeToEdit != null)
            {
                // Використовуємо імена контролів з вашого AddShoeForm.Designer.cs
                textBox1.Text = this.shoeToEdit.Model;    // Модель
                textBox2.Text = this.shoeToEdit.Brand;    // Бренд
                textBox3.Text = this.shoeToEdit.Type;     // Тип взуття

                // Для NumericUpDown важливо перевіряти, чи значення в межах Min/Max
                if (this.shoeToEdit.Size >= numericUpDown1.Minimum && this.shoeToEdit.Size <= numericUpDown1.Maximum)
                    numericUpDown1.Value = this.shoeToEdit.Size; // Розмір
                else numericUpDown1.Value = numericUpDown1.Minimum; // Або якесь значення за замовчуванням

                if (this.shoeToEdit.Price >= numericUpDown2.Minimum && this.shoeToEdit.Price <= numericUpDown2.Maximum)
                    numericUpDown2.Value = this.shoeToEdit.Price; // Ціна
                else numericUpDown2.Value = numericUpDown2.Minimum;

                if (this.shoeToEdit.Stock >= numericUpDown3.Minimum && this.shoeToEdit.Stock <= numericUpDown3.Maximum)
                    numericUpDown3.Value = this.shoeToEdit.Stock; // Кількість
                else numericUpDown3.Value = numericUpDown3.Minimum;
            }
        }

        // Обробник події для кнопки "Зберегти" (або "Додати")
        // Переконайтесь, що в AddShoeForm.Designer.cs для btnSave підключено цей обробник:
        // this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        // Якщо Visual Studio згенерував іншу назву, замініть btnSave_Click на неї тут і в Designer.cs
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Отримуємо дані з полів форми
                string model = textBox1.Text.Trim();
                string brand = textBox2.Text.Trim();
                int size = Convert.ToInt32(numericUpDown1.Value);
                decimal price = numericUpDown2.Value;
                int stock = Convert.ToInt32(numericUpDown3.Value);
                string type = textBox3.Text.Trim();

                // Валідація даних (базова)
                if (string.IsNullOrWhiteSpace(model))
                {
                    MessageBox.Show("Поле 'Модель' не може бути порожнім.", "Помилка вводу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(brand))
                {
                    MessageBox.Show("Поле 'Бренд' не може бути порожнім.", "Помилка вводу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(type))
                {
                    MessageBox.Show("Поле 'Тип взуття' не може бути порожнім.", "Помилка вводу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox3.Focus();
                    return;
                }
                // Додаткові перевірки для числових значень, якщо Minimum/Maximum не достатньо
                if (size <= 0)
                {
                    MessageBox.Show("Розмір має бути більше нуля.", "Помилка вводу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    numericUpDown1.Focus();
                    return;
                }
                if (price < 0)
                {
                    MessageBox.Show("Ціна не може бути від'ємною.", "Помилка вводу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    numericUpDown2.Focus();
                    return;
                }
                if (stock < 0)
                {
                    MessageBox.Show("Кількість не може бути від'ємною.", "Помилка вводу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    numericUpDown3.Focus();
                    return;
                }


                if (shoeToEdit != null) // Якщо це режим редагування
                {
                    // Оновлюємо властивості існуючого об'єкта shoeToEdit
                    shoeToEdit.Model = model;
                    shoeToEdit.Brand = brand;
                    shoeToEdit.Size = size;
                    shoeToEdit.Price = price;
                    shoeToEdit.Stock = stock;
                    shoeToEdit.Type = type;
                    // AddedShoe тут не потрібне, бо ми модифікували переданий об'єкт
                }
                else // Якщо це режим додавання нового взуття
                {
                    AddedShoe = new Shoe(model, brand, size, price, stock, type);
                }

                this.DialogResult = DialogResult.OK; // Встановлюємо результат діалогу на OK
                this.Close(); // Закриваємо форму
            }
            catch (ArgumentException ex) // Для помилок валідації з конструктора Shoe, якщо він використовується
            {
                MessageBox.Show($"Помилка валідації: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Будь ласка, введіть коректні числові значення для розміру, ціни та кількості.", "Помилка формату", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) // Інші можливі помилки
            {
                MessageBox.Show($"Сталася непередбачена помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обробник події для кнопки "Скасувати"
        // Переконайтесь, що в AddShoeForm.Designer.cs для btnCancel підключено цей обробник:
        // this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // DialogResult вже встановлено в Designer.cs, але для ясності
            this.Close();
        }

        // Порожні обробники, які Visual Studio міг згенерувати, якщо ви на них клацали.
        // Якщо вони вам не потрібні, їх можна видалити, АЛЕ ТАКОЖ ВИДАЛІТЬ ПІДПИСКУ НА НИХ
        // У ФАЙЛІ AddShoeForm.Designer.cs (наприклад, this.label1.Click -= new System.EventHandler(this.label1_Click);)
        // Або просто залиште їх порожніми.
        private void label1_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        // textBox3_TextChanged у вас не було в попередньому AddShoeForm.cs
        private void label4_Click(object sender, EventArgs e) { }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Отримуємо дані з полів форми (textBox1, numericUpDown1 і т.д.)
                string model = textBox1.Text.Trim();
                string brand = textBox2.Text.Trim();
                int size = Convert.ToInt32(numericUpDown1.Value);
                decimal price = numericUpDown2.Value;
                int stock = Convert.ToInt32(numericUpDown3.Value);
                string type = textBox3.Text.Trim();

                // Валідація ... (як у попередньому прикладі)
                if (string.IsNullOrWhiteSpace(model) /* ... і так далі ... */)
                {
                    MessageBox.Show("Поле 'Модель' не може бути порожнім.");
                    return;
                }
                // ... інша валідація ...

                if (shoeToEdit != null) // Режим редагування
                {
                    shoeToEdit.Model = model;
                    shoeToEdit.Brand = brand;
                    shoeToEdit.Size = size;
                    shoeToEdit.Price = price;
                    shoeToEdit.Stock = stock;
                    shoeToEdit.Type = type;
                }
                else // Режим додавання
                {
                    AddedShoe = new Shoe(model, brand, size, price, stock, type);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        // comboBox1_SelectedIndexChanged - цього елемента немає у вашому Designer.cs, тому цей метод не потрібен.
        // Якщо він є у вашому AddShoeForm.cs, видаліть його.
    }
}