using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Переконайтеся, що класи Order, Customer, Shoe доступні (мають бути в namespace Луценко)
namespace Луценко // Переконайтесь, що цей namespace співпадає з іншими вашими файлами
{
    public class OrderNotificationBot
    {
        private static ITelegramBotClient _botClient; // Рекомендовано використовувати інтерфейс ITelegramBotClient
        private readonly ChatId _targetChatId; // Зберігаємо як ChatId

        public OrderNotificationBot(string botToken, string targetChatIdString)
        {
            if (string.IsNullOrWhiteSpace(botToken))
                throw new ArgumentNullException(nameof(botToken), "Bot token cannot be empty.");
            if (string.IsNullOrWhiteSpace(targetChatIdString))
                throw new ArgumentNullException(nameof(targetChatIdString), "Target Chat ID cannot be empty.");

            if (_botClient == null)
            {
                _botClient = new TelegramBotClient(botToken);
            }

            if (long.TryParse(targetChatIdString, out long id))
            {
                _targetChatId = new ChatId(id);
            }
            else
            {
                _targetChatId = new ChatId(targetChatIdString); 
            }
        }

        public async Task SendOrderNotificationAsync(Order order, Customer customer, List<Shoe> items, string storeName, string deliveryAddress, string comments)
        {
            if (order == null || customer == null || items == null || !items.Any())
            {
                Console.WriteLine("Attempted to send an empty or incomplete order notification.");
                return; // Або кинути виняток
            }

            var messageTextBuilder = new StringBuilder();
            messageTextBuilder.AppendLine("🚨 **Нове Замовлення!** 🚨");
            messageTextBuilder.AppendLine($"Магазин: *{storeName}*");
            messageTextBuilder.AppendLine($"Номер замовлення: `{order.OrderNumber}`");
            messageTextBuilder.AppendLine($"Дата: {order.OrderDate:dd.MM.yyyy HH:mm}");
            messageTextBuilder.AppendLine("---");
            messageTextBuilder.AppendLine("**Клієнт:**");
            messageTextBuilder.AppendLine($"ПІБ: {customer.FullName}");
            messageTextBuilder.AppendLine($"Телефон: {customer.PhoneNumber}");
            if (!string.IsNullOrWhiteSpace(deliveryAddress))
            {
                messageTextBuilder.AppendLine($"Адреса доставки: {deliveryAddress}");
            }
            messageTextBuilder.AppendLine("---");
            messageTextBuilder.AppendLine("**Товари:**");
            foreach (var itemGroup in items.GroupBy(s => new { s.Model, s.Brand, s.Size, s.Price }))
            {
                messageTextBuilder.AppendLine($"- {itemGroup.Key.Brand} {itemGroup.Key.Model} (Розмір: {itemGroup.Key.Size}, Ціна: {itemGroup.Key.Price:C}) - {itemGroup.Count()} шт.");
            }
            messageTextBuilder.AppendLine("---");
            messageTextBuilder.AppendLine($"**Загальна сума: {order.TotalAmount:C}**");
            if (!string.IsNullOrWhiteSpace(comments))
            {
                messageTextBuilder.AppendLine("---");
                messageTextBuilder.AppendLine($"**Коментар:** {comments}");
            }

            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "✅ Погодитися", callbackData: $"approve_{order.OrderNumber}"),
                    InlineKeyboardButton.WithCallbackData(text: "❌ Відмовити", callbackData: $"reject_{order.OrderNumber}")
                }
            });

            try
            {

                if (_botClient == null)
                {
                    Console.WriteLine("TelegramBotClient is not initialized.");
                    throw new InvalidOperationException("TelegramBotClient is not initialized.");
                }


                Telegram.Bot.Types.Message sentMessage = await _botClient.SendTextMessageAsync( 
     chatId: _targetChatId,
     text: messageTextBuilder.ToString(),
     parseMode: ParseMode.Markdown,
     replyMarkup: inlineKeyboard
 );
                Console.WriteLine($"Order {order.OrderNumber} notification sent to chat {_targetChatId}. Message ID: {sentMessage.MessageId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending Telegram notification for order {order.OrderNumber}: {ex.GetType().FullName} - {ex.Message}");
                // Додайте більше деталей про помилку, якщо потрібно
                // Console.WriteLine(ex.StackTrace); 
                throw; // Перекидаємо помилку далі, щоб CreateOrderForm міг її обробити
            }
        }
    }
}