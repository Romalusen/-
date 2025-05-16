using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
namespace Луценко
{
    public partial class CreateOrderForm : Form
    {
        private List<ShoeStore> _availableStoresList;
        private ShoeStore _initiallySelectedStore; // <-- ПОЛЕ МАЄ БУТИ ОГОЛОШЕНО
        private ShoeStore _selectedStoreForOrder;
        private OrderNotificationBot _orderBotInstance;
        private List<Shoe> _shoppingCart = new List<Shoe>();

        public CreateOrderForm(List<ShoeStore> stores, ShoeStore initiallySelectedStore, OrderNotificationBot bot)
        {
            InitializeComponent();
            _availableStoresList = stores;
            _initiallySelectedStore = initiallySelectedStore;
            _orderBotInstance = bot;
            PopulateStoreComboBox();
            SetupDataGridViewColumns();

            if (_initiallySelectedStore != null && allStores.Items.Contains(_initiallySelectedStore.Name))
            {
                allStores.SelectedItem = _initiallySelectedStore.Name;
                _selectedStoreForOrder = _initiallySelectedStore;
                if (_selectedStoreForOrder != null) // Додаткова перевірка
                {
                    LoadAvailableShoes(_selectedStoreForOrder);
                }
            }
            else if (allStores.Items.Count > 0)
            {
                allStores.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Немає доступних магазинів для замовлення.", "Помилка");
            }
            UpdateTotalAmountLabel();
        }

        // ... (решта коду CreateOrderForm.cs, як я надавав раніше) ...
        // Особливо перевірте btnSubmitOrder_Click, щоб він використовував правильні імена
        // для ваших TextBox-ів (textBox1, textBox2, textBox3, Comments)
    

        private void PopulateStoreComboBox()
        {
            allStores.Items.Clear();
            if (_availableStoresList != null)
            {
                foreach (var store in _availableStoresList)
                {
                    allStores.Items.Add(store.Name);
                }
            }
        }

        private void SetupDataGridViewColumns()
        {
            // Колонки для dgvAvailableShoes (Доступні товари)
            dgvAvailableShoes.AutoGenerateColumns = false;
            dgvAvailableShoes.Columns.Clear();
            dgvAvailableShoes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Brand", HeaderText = "Бренд", DataPropertyName = "Brand", ReadOnly = true });
            dgvAvailableShoes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Model", HeaderText = "Модель", DataPropertyName = "Model", ReadOnly = true });
            dgvAvailableShoes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Size", HeaderText = "Розмір", DataPropertyName = "Size", ReadOnly = true });
            dgvAvailableShoes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Type", HeaderText = "Тип", DataPropertyName = "Type", ReadOnly = true });
            dgvAvailableShoes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Price", HeaderText = "Ціна", DataPropertyName = "Price", ReadOnly = true, DefaultCellStyle = { Format = "N2" } });
            dgvAvailableShoes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Stock", HeaderText = "Залишок", DataPropertyName = "Stock", ReadOnly = true });
            dgvAvailableShoes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAvailableShoes.MultiSelect = false;


            // Колонки для dgvOrderCart (Кошик)
            dgvOrderCart.AutoGenerateColumns = false;
            dgvOrderCart.Columns.Clear();
            dgvOrderCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "CartBrand", HeaderText = "Бренд", DataPropertyName = "Brand", ReadOnly = true });
            dgvOrderCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "CartModel", HeaderText = "Модель", DataPropertyName = "Model", ReadOnly = true });
            dgvOrderCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "CartSize", HeaderText = "Розмір", DataPropertyName = "Size", ReadOnly = true });
            dgvOrderCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "CartPrice", HeaderText = "Ціна", DataPropertyName = "Price", ReadOnly = true, DefaultCellStyle = { Format = "N2" } });
            // Можна додати колонку для кількості, якщо одне й те саме взуття можна додати кілька разів
            dgvOrderCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrderCart.MultiSelect = false;
        }


        private void LoadAvailableShoes(ShoeStore store)
        {
            if (store != null && store.Inventory != null)
            {
                // Показуємо тільки ті, що є в наявності
                dgvAvailableShoes.DataSource = store.Inventory.Where(s => s.Stock > 0).ToList();
            }
            else
            {
                dgvAvailableShoes.DataSource = null;
            }
        }

        private void allStores_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStoreName = allStores.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedStoreName))
            {
                _selectedStoreForOrder = _availableStoresList.FirstOrDefault(s => s.Name == selectedStoreName);
                if (_selectedStoreForOrder != null)
                {
                    LoadAvailableShoes(_selectedStoreForOrder);
                    // Якщо кошик залежить від магазину, його тут можна очистити
                    // _shoppingCart.Clear();
                    // RefreshCartDataGridView();
                    // UpdateTotalAmountLabel();
                }
            }
        }

        // Кнопка "Додати до кошика" (button1)
        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvAvailableShoes.CurrentRow != null && dgvAvailableShoes.CurrentRow.DataBoundItem is Shoe selectedShoe)
            {
                if (selectedShoe.Stock > 0)
                {
                    // Для простоти додаємо як є. Для кількох однакових товарів логіка була б складнішою.
                    _shoppingCart.Add(selectedShoe); // Додаємо копію або сам об'єкт
                    RefreshCartDataGridView();
                    UpdateTotalAmountLabel();
                }
                else
                {
                    MessageBox.Show("Цього товару немає в наявності.", "Немає в наявності", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Кнопка "Видалити з кошика" (button2)
        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvOrderCart.CurrentRow != null && dgvOrderCart.CurrentRow.DataBoundItem is Shoe selectedCartItem)
            {
                _shoppingCart.Remove(selectedCartItem); // Потребує коректного порівняння об'єктів або пошуку за індексом
                RefreshCartDataGridView();
                UpdateTotalAmountLabel();
            }
        }

        private void RefreshCartDataGridView()
        {
            // Потрібно створити новий список для прив'язки, щоб DataGridView оновився
            dgvOrderCart.DataSource = null;
            if (_shoppingCart.Any())
            {
                dgvOrderCart.DataSource = new BindingList<Shoe>(_shoppingCart);
            }
        }

        private void UpdateTotalAmountLabel()
        {
            decimal totalAmount = _shoppingCart.Sum(shoe => shoe.Price);

            // Створюємо об'єкт культури для України
            CultureInfo ukrainianCulture = new CultureInfo("uk-UA");

            // Форматуємо з використанням української культури
            lblTotalAmount.Text = $"Загальна сума: {totalAmount.ToString("C", ukrainianCulture)}";
     
        }

        // Кнопка "Замовити" (btnSubmitOrder)
        // Переконайтесь, що в Designer.cs підписано саме btnSubmitOrder.Click += btnSubmitOrder_Click;
        private async void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            string customerName = textBox1.Text.Trim(); // textBox1 - для ПІБ
            string customerPhone = textBox2.Text.Trim(); // textBox2 - для телефону
            string deliveryAddress = textBox3.Text.Trim(); // textBox3 - для адреси доставки
            string orderComments = Comments.Text.Trim();

            if (_selectedStoreForOrder == null)
            {
                MessageBox.Show("Будь ласка, оберіть магазин.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!_shoppingCart.Any())
            {
                MessageBox.Show("Кошик порожній.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(customerPhone))
            {
                MessageBox.Show("Будь ласка, введіть ПІБ та номер телефону клієнта.", "Помилка вводу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Customer customer = new Customer(customerName, customerPhone);

            // Викликаємо оновлений CreateOrder
            Order newOrder = _selectedStoreForOrder.CreateOrder(customer,
                                                                 new List<Shoe>(_shoppingCart),
                                                                 deliveryAddress,
                                                                 orderComments);

            if (newOrder != null)
            {
                if (_orderBotInstance != null)
                {
                    try
                    {
                        // newOrder вже містить StoreName, DeliveryAddress, Comments
                        await _orderBotInstance.SendOrderNotificationAsync(newOrder, customer, _shoppingCart,
                                                                            newOrder.StoreName,
                                                                            newOrder.DeliveryAddress,
                                                                            newOrder.Comments);
                        MessageBox.Show("Замовлення успішно оформлено та надіслано на обробку!", "Замовлення прийнято", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка надсилання сповіщення: {ex.Message}\nЗамовлення створено, але не надіслано.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                // ... (інша логіка) ...
            }
            else
            {
                MessageBox.Show("Не вдалося створити замовлення. Можливо, деяких товарів вже немає в наявності або сталася інша помилка.", "Помилка створення замовлення", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (_selectedStoreForOrder != null) LoadAvailableShoes(_selectedStoreForOrder);
            }
        }
        private class OrderHistoryViewModel
        {
            public string OrderNumber { get; set; }
            public DateTime OrderDate { get; set; }
            public string StoreName { get; set; } // З Order.StoreName
            public string CustomerFullNameProxy { get; set; }
            public string CustomerPhoneNumberProxy { get; set; }
            public string ItemsSummaryProxy { get; set; }
            public decimal TotalAmount { get; set; }
            public OrderStatus Status { get; set; }
            public string DeliveryAddress { get; set; } // З Order.DeliveryAddress
            public string Comments { get; set; }        // З Order.Comments

            public OrderHistoryViewModel(Order order)
            {
                OrderNumber = order.OrderNumber;
                OrderDate = order.OrderDate;
                StoreName = order.StoreName; // <--- Перевірте це
                CustomerFullNameProxy = order.Customer?.FullName ?? "N/A";
                CustomerPhoneNumberProxy = order.Customer?.PhoneNumber ?? "N/A";

                if (order.Items != null && order.Items.Any())
                {
                    ItemsSummaryProxy = string.Join("; ", order.Items
                        .GroupBy(s => new { s.Model, s.Brand, s.Size, s.Price })
                        .Select(g => $"{g.Key.Brand} {g.Key.Model} (Розмір: {g.Key.Size}, Ціна: {g.Key.Price:C}) - {g.Count()} шт."));
                }
                else { ItemsSummaryProxy = "Товари не вказані"; }

                TotalAmount = order.TotalAmount;
                Status = order.Status;
                DeliveryAddress = order.DeliveryAddress; // <--- Перевірте це
                Comments = order.Comments;             // <--- Перевірте це
            }
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Порожні обробники, які ви маєте. Їх можна видалити, якщо вони не використовуються,
        // АЛЕ ТАКОЖ видаліть підписки на них у Designer.cs файлі!
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void lblTotalAmount_Click(object sender, EventArgs e) { }
        private void dgvOrderCart_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvAvailableShoes_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { } 
        private void textBox2_TextChanged(object sender, EventArgs e) { } 
        private void textBox3_TextChanged(object sender, EventArgs e) { } 
        private void Comments_TextChanged(object sender, EventArgs e) { } 
    }
}