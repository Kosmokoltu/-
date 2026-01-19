using System;

namespace HockeyTodoApp.Exceptions
{
    /// <summary>
    /// Исключение, выбрасываемое при попытке найти задачу, которая не существует
    /// </summary>
    public class TaskNotFoundException : Exception
    {
        /// <summary>
        /// Конструктор исключения
        /// </summary>
        /// <param name="taskId">ID задачи, которая не найдена</param>
        public TaskNotFoundException(Guid taskId)
            : base($"Задача с ID '{taskId}' не найдена") { }
    }
}
