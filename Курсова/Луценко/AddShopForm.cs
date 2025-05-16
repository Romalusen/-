using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Луценко
{
    public partial class AddShopForm : Form
    {
        public ShoeStore NewStore { get; private set; }
        public AddShopForm()
        {
            InitializeComponent();
            this.Text = "Додати новий магазин";

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Name_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveStore_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string contactInfo = txtContactInfo.Text.Trim();

            // Валідація для назви магазину
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Назва магазину не може бути порожньою.", "Помилка вводу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus(); // Встановлюємо фокус на поле з помилкою
                return;         // <-- Ось цей return ПЕРЕРИВАЄ подальше виконання методу
            }

            // Валідація для контактної інформації (наприклад, на порожнечу)
            if (string.IsNullOrWhiteSpace(contactInfo))
            {
                MessageBox.Show("Контактна інформація (ПІБ) не може бути порожньою.", "Помилка вводу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContactInfo.Focus();
                return;         // <-- І тут теж, якщо ця умова спрацювала
            }

            // Ваша перевірка на кількість пробілів/слів для contactInfo
            int spaceCount = contactInfo.Count(c => c == ' ');
            string[] words = contactInfo.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (spaceCount < 2 || words.Length < 3)
            {
                MessageBox.Show("Контактна інформація (ПІБ) має містити принаймні три слова, розділені пробілами (наприклад, Прізвище Ім'я По-батькові).", "Помилка формату ПІБ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContactInfo.Focus();
                return;         // <-- І тут теж
            }

            // Якщо всі перевірки вище не спрацювали (тобто return не було викликано),
            // то код продовжує виконуватися тут:
            try
            {
                NewStore = new ShoeStore(name, address, contactInfo);
                this.DialogResult = DialogResult.OK; // Встановлюємо, що все гаразд
                this.Close(); // Закриваємо форму ТІЛЬКИ ЯКЩО ВСЕ ДОБРЕ
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Тут форма НЕ закривається, якщо конструктор ShoeStore кинув помилку
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сталася непередбачена помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // І тут форма НЕ закривається
            }
        }


        private void btnCancelStore_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
