using System;

namespace HockeyTodoApp.Exceptions
{
    /// <summary>
    /// Исключение, выбрасываемое при превышении лимита количества задач
    /// </summary>
    public class TaskCountLimitException : Exception
    {
        /// <summary>
        /// Конструктор исключения
        /// </summary>
        /// <param name="taskCountLimit">Максимальное допустимое количество задач</param>
        public TaskCountLimitException(int taskCountLimit)
            : base($"Превышено максимальное количество задач равное {taskCountLimit}") { }
    }
}
