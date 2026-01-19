using System;

namespace HockeyTodoApp.Models
{
    /// <summary>
    /// Класс, представляющий задачу в ToDo приложении
    /// </summary>
    public class ToDoItem
    {
        /// <summary>
        /// Уникальный идентификатор задачи
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Пользователь, создавший задачу
        /// </summary>
        public ToDoUser User { get; private set; }

        /// <summary>
        /// Название/описание задачи
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Дата создания задачи
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Текущее состояние задачи (Активна или Завершена)
        /// </summary>
        public ToDoItemState State { get; private set; }

        /// <summary>
        /// Дата изменения состояния задачи (заполняется при завершении)
        /// </summary>
        public DateTime? StateChangedAt { get; private set; }

        /// <summary>
        /// Конструктор задачи
        /// </summary>
        /// <param name="user">Пользователь, создавший задачу</param>
        /// <param name="name">Название задачи</param>
        public ToDoItem(ToDoUser user, string name)
        {
            Id = Guid.NewGuid();
            User = user;
            Name = name;
            CreatedAt = DateTime.UtcNow;
            State = ToDoItemState.Active;
            StateChangedAt = null;
        }

        /// <summary>
        /// Отмечает задачу как завершённую
        /// </summary>
        public void CompleteTask()
        {
            State = ToDoItemState.Completed;
            StateChangedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Возвращает строковое представление задачи
        /// </summary>
        public override string ToString()
        {
            return $"{Name} - {CreatedAt:dd.MM.yyyy HH:mm:ss} - {Id}";
        }
    }
}
