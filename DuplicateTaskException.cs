using System;

namespace HockeyTodoApp.Exceptions
{
    /// <summary>
    /// Исключение, выбрасываемое при попытке добавить дубликат задачи
    /// </summary>
    public class DuplicateTaskException : Exception
    {
        /// <summary>
        /// Конструктор исключения
        /// </summary>
        /// <param name="task">Название задачи, которая уже существует</param>
        public DuplicateTaskException(string task)
            : base($"Задача '{task}' уже существует") { }
    }
}
