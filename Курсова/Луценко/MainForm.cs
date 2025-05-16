    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Text.Json;
    using System.Text.Json.Serialization;
using System.IO;
namespace Луценко
    {
    public partial class MainForm : Form
    {
        // Допоміжний внутрішній клас для зберігання взуття з назвою його магазину
        private class ShoeWithStoreInfo
        {
            public Shoe ActualShoe { get; set; }
            public string StoreName { get; set; }
        }
        private int currentRowIndexForContextMenu = -1;
        private List<ShoeStore> allStores;
        private ShoeStore currentDisplayStore; // Якщо null, значить показуємо всі магазини
        private ContextMenuStrip storesContextMenu;

        private OrderNotificationBot _orderBot;
        public void ProcessOrderApproval(string orderNumber)
        {
            Order orderToUpdate = FindOrder(orderNumber);
            if (orderToUpdate != null)
            {
                ShoeStore store = FindStoreContainingOrder(orderNumber); 
                if (store != null)
                {
                    orderToUpdate.UpdateStatus(OrderStatus.Processing); 
                    SaveStoresToJson();
                    RefreshCurrentView(); // Оновити DataGridView
                    Console.WriteLine($"Order {orderNumber} approved and stock updated.");
                }
            }
        }

        public void ProcessOrderRejection(string orderNumber)
        {
            Order orderToUpdate = FindOrder(orderNumber);
            if (orderToUpdate != null)
            {

                ShoeStore store = FindStoreContainingOrder(orderNumber);

                orderToUpdate.UpdateStatus(OrderStatus.Cancelled);
                SaveStoresToJson();
                RefreshCurrentView();
                Console.WriteLine($"Order {orderNumber} rejected.");
            }
        }

        private Order FindOrder(string orderNumber)
        {
            return allStores?.SelectMany(s => s.Orders ?? new List<Order>())
                            .FirstOrDefault(o => o.OrderNumber == orderNumber);
        }
        private ShoeStore FindStoreContainingOrder(string orderNumber)
        {
            return allStores?.FirstOrDefault(s => s.Orders?.Any(o => o.OrderNumber == orderNumber) ?? false);
        }


        private void RefreshCurrentView()
        {
            if (currentDisplayStore != null)
            {
                LoadShoesToGrid(currentDisplayStore.ViewInventory(), currentDisplayStore.Name);
            }
            else
            {
                DisplayShoesWithStoreInfo(GetAllShoesWithStoreInfo());
            }
        }
        public MainForm()
        {
            InitializeComponent();

            // Ініціалізація бота ТУТ, всередині конструктора
            string botToken = "7714005472:AAEGMJcHgrdTirJBmu9WEoI6Y-qFiXcaOuE"; // токен
            string chatIdForOrders = "1293474566"; // Chat ID
            _orderBot = new OrderNotificationBot(botToken, chatIdForOrders); // Ініціалізуємо поле

            InitializeStoresAndDataFromJson();
            InitializeStoresContextMenu();

            if (allStores != null && allStores.Any())
            {
                currentDisplayStore = null;
            }
            else
            {
                currentDisplayStore = null;
            }
        }
        private void оформитиЗамовленняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (allStores == null || !allStores.Any())
            {
                MessageBox.Show("Немає доступних магазинів для оформлення замовлення.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Виправлено тип MessageBoxIcon
                return;
            }

            // Передаємо список магазинів та поточний обраний магазин (якщо є) у форму замовлення
            // Також передаємо екземпляр _orderBot
            CreateOrderForm orderForm = new CreateOrderForm(allStores, currentDisplayStore, _orderBot);
            DialogResult result = orderForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (currentDisplayStore != null)
                {
                    LoadShoesToGrid(currentDisplayStore.ViewInventory(), currentDisplayStore.Name);
                }
                else
                {
                    DisplayShoesWithStoreInfo(GetAllShoesWithStoreInfo());
                }
                SaveStoresToJson();
                MessageBox.Show("Запит на замовлення надіслано! Очікуйте підтвердження.", "Замовлення", MessageBoxButtons.OK, MessageBoxIcon.Information); // Виправлено тип MessageBoxIcon
            }
        }

        private void InitializeStoresAndDataFromJson()
        {
            allStores = new List<ShoeStore>();
            string filePath = "stores.json";

            try
            {
                if (File.Exists(filePath))
                {
                    string jsonData = File.ReadAllText(filePath);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve // <--- Встановлюємо тут
                    };
                    allStores = JsonSerializer.Deserialize<List<ShoeStore>>(jsonData, options);
                    // ...
                
                if (allStores != null)
                    {
                        foreach (var store in allStores)
                        {
                            store.Inventory ??= new List<Shoe>();
                            store.Orders ??= new List<Order>();
                            store.Customers ??= new List<Customer>();
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"Файл даних '{filePath}' не знайдено. Буде завантажено запасні дані.", "Помилка завантаження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadFallbackStores();
                }
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show($"Помилка читання JSON файлу: {jsonEx.Message}\nШлях: {jsonEx.Path}\nРядок: {jsonEx.LineNumber}, Позиція: {jsonEx.BytePositionInLine}", "Помилка JSON", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadFallbackStores();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Загальна помилка при завантаженні даних: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadFallbackStores();
            }

            if (allStores == null || !allStores.Any())
            {
                allStores = new List<ShoeStore>();
                LoadFallbackStores();
            }
        }

        private void LoadFallbackStores()
        {
           
        }

        private void InitializeStoresContextMenu()
        {
            storesContextMenu = new ContextMenuStrip();
            UpdateStoresContextMenu();
            if (allStores != null)
            {
                foreach (ShoeStore store in allStores)
                {
                    ToolStripMenuItem storeItem = new ToolStripMenuItem(store.Name);
                    storeItem.Tag = store;
                    storeItem.Click += StoreMenuItem_Click;
                    storesContextMenu.Items.Add(storeItem);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // За замовчуванням показуємо все з усіх магазинів
            DisplayShoesWithStoreInfo(GetAllShoesWithStoreInfo());
        }

        // Показує товари одного магазину
        private void LoadShoesToGrid(List<Shoe> shoes, string storeName)
        {
            dataGridView1.Rows.Clear();
            if (shoes != null)
            {
                foreach (var shoe in shoes)
                {
                    dataGridView1.Rows.Add(storeName, shoe.Brand, shoe.Model, shoe.Size, shoe.Price, shoe.Type, shoe.Stock);
                }
            }
        }

        private List<ShoeWithStoreInfo> GetAllShoesWithStoreInfo()
        {
            var list = new List<ShoeWithStoreInfo>();
            if (allStores != null)
            {
                foreach (var store in allStores)
                {
                    if (store.Inventory != null)
                    {
                        foreach (var shoe in store.Inventory)
                        {
                            list.Add(new ShoeWithStoreInfo { ActualShoe = shoe, StoreName = store.Name });
                        }
                    }
                }
            }
            return list;
        }

        private void DisplayShoesWithStoreInfo(List<ShoeWithStoreInfo> shoesToShow)
        {
            dataGridView1.Rows.Clear();
            if (shoesToShow != null)
            {
                foreach (var item in shoesToShow)
                {
                    dataGridView1.Rows.Add(
                        item.StoreName,
                        item.ActualShoe.Brand,
                        item.ActualShoe.Model,
                        item.ActualShoe.Size,
                        item.ActualShoe.Price,
                        item.ActualShoe.Type,
                        item.ActualShoe.Stock
                    );
                }
            }
        }

        // --- Обробники подій для кнопок ---

        private void toolStripButton8_Click(object sender, EventArgs e) // Пошук по магазинам
        {
            // Перебудовуємо меню кожен раз, якщо список магазинів може динамічно змінюватися
            storesContextMenu.Items.Clear();
            if (allStores != null && allStores.Any())
            {
                foreach (ShoeStore store in allStores)
                {
                    ToolStripMenuItem storeItem = new ToolStripMenuItem(store.Name);
                    storeItem.Tag = store;
                    storeItem.Click += StoreMenuItem_Click;
                    storesContextMenu.Items.Add(storeItem);
                }
            }

            if (storesContextMenu.Items.Count > 0)
            {
                storesContextMenu.Show(toolStrip1, toolStripButton8.Bounds.Location + new Size(0, toolStripButton8.Bounds.Height));
            }
            else
            {
                MessageBox.Show("Немає доступних магазинів для вибору.", "Повідомлення");
            }
        }

        private void StoreMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            if (clickedItem != null)
            {
                ShoeStore selectedStore = clickedItem.Tag as ShoeStore;
                if (selectedStore != null)
                {
                    currentDisplayStore = selectedStore;
                    LoadShoesToGrid(currentDisplayStore.ViewInventory(), currentDisplayStore.Name);
                }
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e) // Показати все
        {
            currentDisplayStore = null;
            DisplayShoesWithStoreInfo(GetAllShoesWithStoreInfo());
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddShopForm addStoreForm = new AddShopForm();
            if (addStoreForm.ShowDialog() == DialogResult.OK)
            {
                ShoeStore newStore = addStoreForm.NewStore;
                if (newStore != null)
                {
                    if (allStores == null)
                    {
                        allStores = new List<ShoeStore>();
                    }

                    // Перевірка, чи магазин з такою назвою вже існує (опціонально, але бажано)
                    if (allStores.Any(s => s.Name.Equals(newStore.Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show($"Магазин з назвою '{newStore.Name}' вже існує.", "Помилка додавання", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    allStores.Add(newStore);
                    UpdateStoresContextMenu();
                    SaveStoresToJson();


                    currentDisplayStore = newStore;
                    LoadShoesToGrid(currentDisplayStore.ViewInventory(), currentDisplayStore.Name);

                    MessageBox.Show($"Магазин '{newStore.Name}' успішно додано!", "Новий магазин", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void btnAddSpecificShoe_Click(object sender, EventArgs e) // Назва методу має відповідати тому, що згенерував дизайнер
        {
            if (currentDisplayStore == null)
            {
                MessageBox.Show("Будь ласка, спочатку виберіть магазин (через кнопку 'Пошук по магазинам'), до якого хочете додати взуття.", "Оберіть магазин", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            AddShoeForm addShoeForm = new AddShoeForm();
            if (addShoeForm.ShowDialog() == DialogResult.OK)
            {
                Shoe newShoe = addShoeForm.AddedShoe; // Отримуємо дані з форми AddShoeForm
                if (newShoe != null)
                {
                    currentDisplayStore.AddShoe(newShoe); // Додаємо взуття до поточного магазину
                    LoadShoesToGrid(currentDisplayStore.ViewInventory(), currentDisplayStore.Name); // Оновлюємо таблицю
                    SaveStoresToJson(); // Зберігаємо зміни в JSON
                }
            }
        }
        private void UpdateStoresContextMenu()
        {
            storesContextMenu.Items.Clear();
            if (allStores != null)
            {
                foreach (ShoeStore store in allStores)
                {
                    ToolStripMenuItem storeItem = new ToolStripMenuItem(store.Name);
                    storeItem.Tag = store;
                    storeItem.Click += StoreMenuItem_Click;
                    storesContextMenu.Items.Add(storeItem);
                }
            }
        }


        private void toolStripButton2_Click(object sender, EventArgs e) // Сортування А-Я
        {
            if (currentDisplayStore != null)
            {
                LoadShoesToGrid(currentDisplayStore.SortShoesByNameAscending(), currentDisplayStore.Name);
            }
            else
            {
                var allShoes = GetAllShoesWithStoreInfo();
                var sortedShoes = allShoes.OrderBy(s => s.ActualShoe.Model).ToList();
                DisplayShoesWithStoreInfo(sortedShoes);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e) // Сортування Я-А
        {
            if (currentDisplayStore != null)
            {
                LoadShoesToGrid(currentDisplayStore.SortShoesByNameDescending(), currentDisplayStore.Name);
            }
            else
            {
                var allShoes = GetAllShoesWithStoreInfo();
                var sortedShoes = allShoes.OrderByDescending(s => s.ActualShoe.Model).ToList();
                DisplayShoesWithStoreInfo(sortedShoes);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e) // Історія
        {
            List<Order> ordersToShow;

            if (currentDisplayStore != null) // Якщо обрано конкретний магазин, показуємо його історію
            {
                ordersToShow = currentDisplayStore.Orders ?? new List<Order>();
            }
            else // Інакше показуємо історію з усіх магазинів
            {
                ordersToShow = allStores?.SelectMany(s => s.Orders ?? new List<Order>()).ToList() ?? new List<Order>();
                if (!ordersToShow.Any())
                {
                    MessageBox.Show("Історія замовлень порожня для всіх магазинів.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Можливо, тут не варто відкривати порожню форму історії, або ShoeHistoryForm сама це обробить
                }
            }

            // Створюємо форму, передаючи ТІЛЬКИ список замовлень
            ShoeHistoryForm shoeHistoryForm = new ShoeHistoryForm(ordersToShow);
            shoeHistoryForm.ShowDialog(); // Або Show()
        }

        private void toolStripButton5_Click(object sender, EventArgs e) // В наявності
        {
            if (currentDisplayStore != null)
            {
                LoadShoesToGrid(currentDisplayStore.FilterShoesInStock(), currentDisplayStore.Name);
            }
            else
            {
                var allShoes = GetAllShoesWithStoreInfo();
                var filteredShoes = allShoes.Where(s => s.ActualShoe.Stock > 0).ToList();
                DisplayShoesWithStoreInfo(filteredShoes);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e) // Не в наявності
        {
            if (currentDisplayStore != null)
            {
                LoadShoesToGrid(currentDisplayStore.FilterShoesOutOfStock(), currentDisplayStore.Name);
            }
            else
            {
                var allShoes = GetAllShoesWithStoreInfo();
                var filteredShoes = allShoes.Where(s => s.ActualShoe.Stock == 0).ToList();
                DisplayShoesWithStoreInfo(filteredShoes);
            }
        }

        private void вийтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        // Обробник для пункту меню "Бренди", якщо він використовується окремо
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Обробник для пункту меню 'Бренди' (toolStripMenuItem1) ще не реалізовано повністю.", "Інформація");
            // Тут може бути логіка фільтрації за брендами, яка викликається з меню,
            // наприклад, відкриття діалогового вікна для вибору бренду.
        }
        private void відМеньшогоДоБільшогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentDisplayStore != null) // Якщо обрано конкретний магазин
            {
                LoadShoesToGrid(currentDisplayStore.SortShoesBySizeAscending(), currentDisplayStore.Name);
            }
            else // Якщо режим "всі магазини"
            {
                var allShoes = GetAllShoesWithStoreInfo();
                // Сортуємо список об'єктів ShoeWithStoreInfo за розміром взуття
                var sortedShoes = allShoes.OrderBy(s => s.ActualShoe.Size).ToList();
                DisplayShoesWithStoreInfo(sortedShoes);
            }
        }


        // Обробник для пункту меню "Від більшого до меньшого" (Сортування за розміром за спаданням)
        private void відБільшогоДоМеньшогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentDisplayStore != null) // Якщо обрано конкретний магазин
            {
                LoadShoesToGrid(currentDisplayStore.SortShoesBySizeDescending(), currentDisplayStore.Name);
            }
            else // Якщо режим "всі магазини"
            {
                var allShoes = GetAllShoesWithStoreInfo();
                // Сортуємо список об'єктів ShoeWithStoreInfo за розміром взуття за спаданням
                var sortedShoes = allShoes.OrderByDescending(s => s.ActualShoe.Size).ToList();
                DisplayShoesWithStoreInfo(sortedShoes);
            }
        }

        private void рToolStripMenuItem_Click(object sender, EventArgs e) // Редагувати з контекстного меню
        {
            if (currentRowIndexForContextMenu >= 0 && currentRowIndexForContextMenu < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[currentRowIndexForContextMenu];
                if (selectedRow.IsNewRow) return;

                Shoe shoeToEdit = null;
                ShoeStore storeOfShoe = null;
                string storeNameFromGrid = selectedRow.Cells["Магазин"].Value?.ToString();

                if (currentDisplayStore != null && currentDisplayStore.Name == storeNameFromGrid)
                {
                    storeOfShoe = currentDisplayStore;
                    string model = selectedRow.Cells["Модель"].Value?.ToString();
                    string brand = selectedRow.Cells["Бренд"].Value?.ToString();
                    int.TryParse(selectedRow.Cells["Розмір"].Value?.ToString(), out int size);
                    shoeToEdit = storeOfShoe.Inventory.FirstOrDefault(s => s.Model == model && s.Brand == brand && s.Size == size);
                }
                else
                {
                    string model = selectedRow.Cells["Модель"].Value?.ToString();
                    string brand = selectedRow.Cells["Бренд"].Value?.ToString();
                    int.TryParse(selectedRow.Cells["Розмір"].Value?.ToString(), out int size);
                    storeOfShoe = allStores.FirstOrDefault(s => s.Name == storeNameFromGrid);
                    if (storeOfShoe != null)
                    {
                        shoeToEdit = storeOfShoe.Inventory.FirstOrDefault(s => s.Model == model && s.Brand == brand && s.Size == size);
                    }
                }

                if (shoeToEdit != null && storeOfShoe != null)
                {
                    AddShoeForm editShoeForm = new AddShoeForm(shoeToEdit); // Передаємо взуття для редагування
                    if (editShoeForm.ShowDialog() == DialogResult.OK)
                    {
                        // Об'єкт shoeToEdit вже був змінений всередині AddShoeForm,
                        // оскільки він передавався за посиланням.
                        // Тепер оновлюємо DataGridView:
                        if (currentDisplayStore != null)
                        {
                            LoadShoesToGrid(currentDisplayStore.ViewInventory(), currentDisplayStore.Name);
                        }
                        else
                        {
                            DisplayShoesWithStoreInfo(GetAllShoesWithStoreInfo());
                        }

                        SaveStoresToJson(); // <<< --- ОСЬ ТУТ ВИКЛИКАЄМО ЗБЕРЕЖЕННЯ ПІСЛЯ РЕДАГУВАННЯ
                    }
                }
                else
                {
                    MessageBox.Show("Не вдалося знайти взуття для редагування.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void видалитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentRowIndexForContextMenu >= 0 && currentRowIndexForContextMenu < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[currentRowIndexForContextMenu];
                if (selectedRow.IsNewRow) return;

                if (MessageBox.Show("Ви впевнені, що хочете видалити цей запис?", "Підтвердження видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // ... (логіка пошуку shoeToDelete та storeOfShoe, як у вас вже є) ...
                    Shoe shoeToDelete = null;
                    ShoeStore storeOfShoe = null;
                    string storeNameFromGrid = selectedRow.Cells["Магазин"].Value?.ToString();

                    if (currentDisplayStore != null && currentDisplayStore.Name == storeNameFromGrid)
                    {
                        storeOfShoe = currentDisplayStore;
                        string model = selectedRow.Cells["Модель"].Value?.ToString();
                        string brand = selectedRow.Cells["Бренд"].Value?.ToString();
                        int.TryParse(selectedRow.Cells["Розмір"].Value?.ToString(), out int size);
                        shoeToDelete = storeOfShoe.Inventory.FirstOrDefault(s => s.Model == model && s.Brand == brand && s.Size == size);
                    }
                    else
                    {
                        string model = selectedRow.Cells["Модель"].Value?.ToString();
                        string brand = selectedRow.Cells["Бренд"].Value?.ToString();
                        int.TryParse(selectedRow.Cells["Розмір"].Value?.ToString(), out int size);
                        storeOfShoe = allStores.FirstOrDefault(s => s.Name == storeNameFromGrid);
                        if (storeOfShoe != null)
                        {
                            shoeToDelete = storeOfShoe.Inventory.FirstOrDefault(s => s.Model == model && s.Brand == brand && s.Size == size);
                        }
                    }

                    if (shoeToDelete != null && storeOfShoe != null)
                    {
                        storeOfShoe.Inventory.Remove(shoeToDelete); // Видаляємо з колекції в пам'яті

                        // Оновлюємо DataGridView
                        if (currentDisplayStore != null)
                        {
                            LoadShoesToGrid(currentDisplayStore.ViewInventory(), currentDisplayStore.Name);
                        }
                        else
                        {
                            DisplayShoesWithStoreInfo(GetAllShoesWithStoreInfo());
                        }

                        SaveStoresToJson(); // <<< --- ОСЬ ТУТ ВИКЛИКАЄМО ЗБЕРЕЖЕННЯ ПІСЛЯ ВИДАЛЕННЯ
                    }
                    else
                    {
                        MessageBox.Show("Не вдалося знайти взуття для видалення.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void SaveStoresToJson()
        {
            string filePath = "stores.json";
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    ReferenceHandler = ReferenceHandler.Preserve // <--- Встановлюємо властивість тут
                };
                string jsonData = JsonSerializer.Serialize(allStores, options);
                File.WriteAllText(filePath, jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при збереженні даних у JSON: {ex.Message}", "Помилка збереження", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void toolStripButton_Click(object sender, EventArgs e) // Додати
        {
            if (currentDisplayStore == null)
            {
                MessageBox.Show("Будь ласка, виберіть конкретний магазин для додавання взуття (через кнопку 'Пошук по магазинам').", "Дія неможлива", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AddShoeForm addShoeForm = new AddShoeForm(); // Створюємо форму для додавання
            if (addShoeForm.ShowDialog() == DialogResult.OK)
            {
                Shoe newShoe = addShoeForm.AddedShoe; // Отримуємо нове взуття з форми
                if (newShoe != null)
                {
                    currentDisplayStore.AddShoe(newShoe); // Додаємо взуття до інвентарю поточного магазину
                    LoadShoesToGrid(currentDisplayStore.ViewInventory(), currentDisplayStore.Name); // Оновлюємо таблицю

                    SaveStoresToJson(); // <<< --- ОСЬ ТУТ ВИКЛИКАЄМО ЗБЕРЕЖЕННЯ
                }
            }
        }



        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = dataGridView1.HitTest(e.X, e.Y);
                if (hitTestInfo.RowIndex >= 0 && hitTestInfo.RowIndex < dataGridView1.Rows.Count && !dataGridView1.Rows[hitTestInfo.RowIndex].IsNewRow) // Перевірка, що це не новий порожній рядок
                {
                    dataGridView1.ClearSelection(); // Очистити попереднє виділення
                    dataGridView1.Rows[hitTestInfo.RowIndex].Selected = true; // Виділити рядок, по якому клацнули
                    currentRowIndexForContextMenu = hitTestInfo.RowIndex; // Зберегти індекс рядка
                }
                else
                {
                    currentRowIndexForContextMenu = -1; // Якщо клацнули не по дійсному рядку
                }
            }
        }

        private void removeCurrentStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentDisplayStore == null)
            {
                MessageBox.Show("Не обрано жодного магазину для видалення.\nБудь ласка, спочатку виберіть магазин за допомогою кнопки 'Пошук по магазинам'.",
                                "Магазин не обрано", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string storeNameToDelete = currentDisplayStore.Name; // Зберігаємо ім'я магазину ДО видалення

            DialogResult confirmation = MessageBox.Show($"Ви впевнені, що хочете видалити магазин '{storeNameToDelete}' та всі його товари?",
                                                       "Підтвердження видалення магазину",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                allStores.Remove(currentDisplayStore);

                UpdateStoresContextMenu();
                SaveStoresToJson();
                if (allStores.Any())
                {
                    currentDisplayStore = allStores.First();
                    LoadShoesToGrid(currentDisplayStore.ViewInventory(), currentDisplayStore.Name);
                }
                else
                {
                    currentDisplayStore = null;
                    dataGridView1.Rows.Clear();
                }


                MessageBox.Show($"Магазин '{storeNameToDelete}' успішно видалено.", "Видалення успішне", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (currentDisplayStore == null && !allStores.Any())
                {
                    MessageBox.Show("Всі магазини було видалено.", "Список порожній", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void PopulateBrandFilterMenu()
        {
            // Очищуємо попередні динамічно створені пункти
            фільтруванняЗаБрендомToolStripMenuItem.DropDownItems.Clear();

            HashSet<string> uniqueBrands = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (allStores != null)
            {
                foreach (var store in allStores)
                {
                    if (store.Inventory != null)
                    {
                        foreach (var shoe in store.Inventory)
                        {
                            if (!string.IsNullOrEmpty(shoe.Brand))
                            {
                                uniqueBrands.Add(shoe.Brand);
                            }
                        }
                    }
                }
            }

            if (uniqueBrands.Any())
            {
                // Додаємо пункт "Всі бренди" (для скасування фільтра)
                ToolStripMenuItem allBrandsItem = new ToolStripMenuItem("Всі бренди (скинути фільтр)");
                allBrandsItem.Click += AllBrandsMenuItem_Click;
                фільтруванняЗаБрендомToolStripMenuItem.DropDownItems.Add(allBrandsItem);
                фільтруванняЗаБрендомToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());

                // Створюємо пункти меню для кожного унікального бренду
                foreach (string brand in uniqueBrands.OrderBy(b => b))
                {
                    ToolStripMenuItem brandItem = new ToolStripMenuItem(brand);
                    brandItem.Tag = brand;
                    brandItem.Click += BrandMenuItem_Click;
                    фільтруванняЗаБрендомToolStripMenuItem.DropDownItems.Add(brandItem);
                }
            }
            else
            {
                ToolStripMenuItem noBrandsItem = new ToolStripMenuItem("(Немає брендів для фільтрації)");
                noBrandsItem.Enabled = false;
                фільтруванняЗаБрендомToolStripMenuItem.DropDownItems.Add(noBrandsItem);
            }
        }
        private void фільтруванняЗаБрендомToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            // Якщо ви хочете, щоб список брендів оновлювався при кожному відкритті меню,
            // розкоментуйте наступний рядок:
            PopulateBrandFilterMenu();
            // В іншому випадку, якщо заповнення відбувається один раз при завантаженні/зміні даних,
            // цей метод може бути порожнім або його підписку можна видалити.
            // Для "відразу прогружене" - залиште його порожнім або видаліть підписку.
        }
        private void BrandMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedBrandItem = sender as ToolStripMenuItem;
            if (clickedBrandItem != null)
            {
                string selectedBrand = clickedBrandItem.Tag as string;

                if (!string.IsNullOrEmpty(selectedBrand))
                {
                    if (currentDisplayStore != null) // Фільтруємо інвентар поточного магазину
                    {
                        var filteredShoes = currentDisplayStore.Inventory
                                            .Where(shoe => shoe.Brand.Equals(selectedBrand, StringComparison.OrdinalIgnoreCase))
                                            .ToList();
                        // Оновлюємо заголовок або статус, щоб показати активний фільтр
                        LoadShoesToGrid(filteredShoes, currentDisplayStore.Name + $" (Бренд: {selectedBrand})");
                    }
                    else // Фільтруємо загальний список товарів з усіх магазинів
                    {
                        var allShoesWithInfo = GetAllShoesWithStoreInfo();
                        var filteredShoesWithInfo = allShoesWithInfo
                                                    .Where(s => s.ActualShoe.Brand.Equals(selectedBrand, StringComparison.OrdinalIgnoreCase))
                                                    .ToList();
                        DisplayShoesWithStoreInfo(filteredShoesWithInfo);
                    }
                }
            }
        }
        // MainForm.cs
        private void AllBrandsMenuItem_Click(object sender, EventArgs e)
        {
            // Скидаємо фільтр, показуючи або всі товари поточного магазину, або всі товари з усіх магазинів
            if (currentDisplayStore != null)
            {
                LoadShoesToGrid(currentDisplayStore.ViewInventory(), currentDisplayStore.Name);
            }
            else
            {
                DisplayShoesWithStoreInfo(GetAllShoesWithStoreInfo());
            }
        }

        private void фільтруванняЗаТипомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            фільтруванняЗаТипомToolStripMenuItem.DropDownItems.Clear(); // Очищуємо попередні пункти

            HashSet<string> uniqueTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (currentDisplayStore != null) // Якщо обрано конкретний магазин
            {
                // Якщо використовуєте метод з ShoeStore.cs:
                // currentDisplayStore.GetUniqueTypes().ForEach(type => uniqueTypes.Add(type));
                // Або без нього:
                if (currentDisplayStore.Inventory != null)
                {
                    foreach (var shoe in currentDisplayStore.Inventory)
                    {
                        if (!string.IsNullOrEmpty(shoe.Type)) { uniqueTypes.Add(shoe.Type); }
                    }
                }
            }
            else // Якщо режим "всі магазини"
            {
                if (allStores != null)
                {
                    foreach (var store in allStores)
                    {
                        // Якщо використовуєте метод з ShoeStore.cs:
                        // store.GetUniqueTypes().ForEach(type => uniqueTypes.Add(type));
                        // Або без нього:
                        if (store.Inventory != null)
                        {
                            foreach (var shoe in store.Inventory)
                            {
                                if (!string.IsNullOrEmpty(shoe.Type)) { uniqueTypes.Add(shoe.Type); }
                            }
                        }
                    }
                }
            }

            if (uniqueTypes.Any())
            {
                ToolStripMenuItem allTypesItem = new ToolStripMenuItem("Всі типи (скинути фільтр)");
                allTypesItem.Click += AllTypesMenuItem_Click; // Обробник для скидання фільтру за типом
                фільтруванняЗаТипомToolStripMenuItem.DropDownItems.Add(allTypesItem);
                фільтруванняЗаТипомToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());

                foreach (string type in uniqueTypes.OrderBy(t => t))
                {
                    ToolStripMenuItem typeItem = new ToolStripMenuItem(type);
                    typeItem.Tag = type; // Зберігаємо тип
                    typeItem.Click += TypeMenuItem_Click; // Обробник для вибору типу
                    фільтруванняЗаТипомToolStripMenuItem.DropDownItems.Add(typeItem);
                }
            }
            else
            {
                ToolStripMenuItem noTypesItem = new ToolStripMenuItem("(Немає типів для фільтрації)");
                noTypesItem.Enabled = false;
                фільтруванняЗаТипомToolStripMenuItem.DropDownItems.Add(noTypesItem);
            }
        }
        private void TypeMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedTypeItem = sender as ToolStripMenuItem;
            if (clickedTypeItem != null)
            {
                string selectedType = clickedTypeItem.Tag as string;

                if (!string.IsNullOrEmpty(selectedType))
                {
                    if (currentDisplayStore != null) // Фільтруємо інвентар поточного магазину
                    {
                        var filteredShoes = currentDisplayStore.Inventory
                                            .Where(shoe => shoe.Type.Equals(selectedType, StringComparison.OrdinalIgnoreCase))
                                            .ToList();
                        LoadShoesToGrid(filteredShoes, currentDisplayStore.Name + $" (Тип: {selectedType})");
                    }
                    else // Фільтруємо загальний список товарів з усіх магазинів
                    {
                        var allShoesWithInfo = GetAllShoesWithStoreInfo();
                        var filteredShoesWithInfo = allShoesWithInfo
                                                    .Where(s => s.ActualShoe.Type.Equals(selectedType, StringComparison.OrdinalIgnoreCase))
                                                    .ToList();
                        DisplayShoesWithStoreInfo(filteredShoesWithInfo);
                    }
                }
            }
        }
        private void AllTypesMenuItem_Click(object sender, EventArgs e)
        {
            if (currentDisplayStore != null)
            {
                LoadShoesToGrid(currentDisplayStore.ViewInventory(), currentDisplayStore.Name);
            }
            else
            {
                DisplayShoesWithStoreInfo(GetAllShoesWithStoreInfo());
            }
        }

        private void оформитиЗамовленняToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (allStores == null || !allStores.Any())
            {
                MessageBox.Show("Немає доступних магазинів для оформлення замовлення.",
                                "Помилка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning); // Використовуйте MessageBoxIcon.Warning
                return;
            }

            // Передаємо список усіх магазинів, поточний обраний магазин (якщо є, для початкового вибору),
            // та екземпляр _orderBot у конструктор CreateOrderForm.
            // Переконайтеся, що клас CreateOrderForm має відповідний конструктор.
            CreateOrderForm orderForm = new CreateOrderForm(allStores, currentDisplayStore, _orderBot);
            DialogResult result = orderForm.ShowDialog(); // Показуємо форму як модальне вікно

            if (result == DialogResult.OK)
            {
                // Цей блок виконається, якщо користувач успішно "оформив замовлення"
                // на формі CreateOrderForm (тобто натиснув кнопку, яка встановлює DialogResult.OK)

                // Оновлюємо DataGridView, оскільки залишки товарів могли змінитися
                if (currentDisplayStore != null)
                {
                    LoadShoesToGrid(currentDisplayStore.ViewInventory(), currentDisplayStore.Name);
                }
                else // Якщо був режим "всі магазини", оновлюємо його
                {
                    DisplayShoesWithStoreInfo(GetAllShoesWithStoreInfo());
                }

                SaveStoresToJson(); // Зберігаємо можливі зміни в залишках або нове замовлення (якщо логіка ShoeStore це робить)

                MessageBox.Show("Запит на замовлення надіслано! Очікуйте підтвердження від оператора.",
                                "Замовлення",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information); // Використовуйте MessageBoxIcon.Information
            }

        }

    }
}