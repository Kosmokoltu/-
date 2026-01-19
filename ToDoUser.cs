using System;

namespace HockeyTodoApp.Models
{
    /// <summary>
    /// Класс, представляющий пользователя ToDo приложения
    /// Готовится к интеграции с Telegram ботом
    /// </summary>
    public class ToDoUser
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Имя пользователя (будет использоваться для Telegram)
        /// </summary>
        public string TelegramUserName { get; private set; }

        /// <summary>
        /// Дата регистрации пользователя
        /// </summary>
        public DateTime RegisteredAt { get; private set; }

        /// <summary>
        /// Конструктор пользователя
        /// </summary>
        /// <param name="telegramUserName">Имя пользователя</param>
        public ToDoUser(string telegramUserName)
        {
            UserId = Guid.NewGuid();
            TelegramUserName = telegramUserName;
            RegisteredAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Возвращает строковое представление пользователя
        /// </summary>
        public override string ToString()
        {
            return $"User: {TelegramUserName} (ID: {UserId})";
        }
    }
}
