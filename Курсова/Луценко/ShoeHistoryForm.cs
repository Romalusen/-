// ShoeHistoryForm.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Луценко
{
    public partial class ShoeHistoryForm : Form
    {
        private class OrderHistoryViewModel
        {
            public string OrderNumber { get; set; }
            public DateTime OrderDate { get; set; }
            public string StoreName { get; set; }
            public string CustomerFullNameProxy { get; set; }
            public string CustomerPhoneNumberProxy { get; set; }
            public string ItemsSummaryProxy { get; set; }
            public decimal TotalAmount { get; set; }
            public OrderStatus Status { get; set; }
            public string DeliveryAddress { get; set; }
            public string Comments { get; set; }

            public OrderHistoryViewModel(Order order)
            {
                // OriginalOrder = order;
                OrderNumber = order.OrderNumber;
                OrderDate = order.OrderDate;
                StoreName = order.StoreName;
                CustomerFullNameProxy = order.Customer?.FullName ?? "N/A";
                CustomerPhoneNumberProxy = order.Customer?.PhoneNumber ?? "N/A";

                if (order.Items != null && order.Items.Any())
                {
                    ItemsSummaryProxy = string.Join("; ", order.Items
                        .GroupBy(s => new { s.Model, s.Brand, s.Size }) // Групуємо, щоб показати кількість, якщо є дублікати
                        .Select(g => $"{g.Key.Brand} {g.Key.Model} (Розмір: {g.Key.Size}) - {g.Count()} шт."));
                }
                else
                {
                    ItemsSummaryProxy = "Товари не вказані";
                }

                TotalAmount = order.TotalAmount;
                Status = order.Status;
                DeliveryAddress = order.DeliveryAddress;
                Comments = order.Comments;
            }
        }

        public ShoeHistoryForm(List<Order> orders) // <--- КОНСТРУКТОР З ОДНИМ АРГУМЕНТОМ
        {
            InitializeComponent();
            SetupDataGridViewColumns();
            LoadOrderHistory(orders);
            this.Text = "Історія Замовлень";
        }

        private void SetupDataGridViewColumns()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrderNumber", HeaderText = "Номер Зам.", DataPropertyName = "OrderNumber" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrderDate", HeaderText = "Дата", DataPropertyName = "OrderDate", DefaultCellStyle = { Format = "dd.MM.yyyy HH:mm" } });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "StoreName", HeaderText = "Магазин", DataPropertyName = "StoreName" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "CustomerName", HeaderText = "ПІБ Клієнта", DataPropertyName = "CustomerFullNameProxy" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "CustomerPhone", HeaderText = "Телефон", DataPropertyName = "CustomerPhoneNumberProxy" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemsSummary", HeaderText = "Товари", DataPropertyName = "ItemsSummaryProxy", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalAmount", HeaderText = "Сума", DataPropertyName = "TotalAmount", DefaultCellStyle = { Format = "C2" } });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Статус", DataPropertyName = "Status" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "DeliveryAddress", HeaderText = "Адреса Доставки", DataPropertyName = "DeliveryAddress" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Comments", HeaderText = "Коментар", DataPropertyName = "Comments" });
        }

        private void LoadOrderHistory(List<Order> orders)
        {
            var lblInfo = this.Controls.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("Форма Історії Взуття відкрита"));
            if (lblInfo != null) this.Controls.Remove(lblInfo);

            if (orders == null || !orders.Any())
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("Історія замовлень порожня.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var displayList = orders
                                .OrderByDescending(o => o.OrderDate)
                                .Select(o => new OrderHistoryViewModel(o))
                                .ToList();

            dataGridView1.DataSource = new BindingList<OrderHistoryViewModel>(displayList);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Можлива логіка для деталей
        }

        private void button1_Click(object sender, EventArgs e) // Кнопка "ОК"
        {
            this.Close();
        }
    }
}