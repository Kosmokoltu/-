using System;

namespace HockeyTodoApp.Exceptions
{
    /// <summary>
    /// Исключение, выбрасываемое при превышении максимальной длины задачи
    /// </summary>
    public class TaskLengthLimitException : Exception
    {
        /// <summary>
        /// Конструктор исключения
        /// </summary>
        /// <param name="taskLength">Текущая длина задачи</param>
        /// <param name="taskLengthLimit">Максимально допустимая длина</param>
        public TaskLengthLimitException(int taskLength, int taskLengthLimit)
            : base($"Длина задачи '{taskLength}' превышает максимально допустимое значение {taskLengthLimit}") { }
    }
}
