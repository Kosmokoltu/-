using System;
using System.Collections.Generic;
using System.Linq;
using HockeyTodoApp.Models;
using HockeyTodoApp.Exceptions;

namespace HockeyTodoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Введите максимально допустимое количество задач (1-100): ");
                int taskLimit = ParseAndValidateInt(Console.ReadLine(), 1, 100);

                Console.Write("Введите максимально допустимую длину задачи (1-100): ");
                int taskLengthLimit = ParseAndValidateInt(Console.ReadLine(), 1, 100);

                Run(taskLimit, taskLengthLimit);
            }
            catch (TaskCountLimitException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (TaskLengthLimitException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DuplicateTaskException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (TaskNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла непредвиденная ошибка:");
                Console.WriteLine($"Type: {ex.GetType().FullName}");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                Console.WriteLine($"InnerException: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Основной цикл приложения
        /// </summary>
        static void Run(int taskLimit, int taskLengthLimit)
        {
            ToDoUser currentUser = null;
            bool isRunning = true;
            string version = "2.0";
            string creationDate = "19.11.2025";
            List<ToDoItem> tasks = new List<ToDoItem>();

            Console.WriteLine("Добро пожаловать в меню хоккейного одевалкина!");
            Console.WriteLine("Доступные команды: /start, /help, /info, /echo, /addtask, /showtasks, /showalltasks, /completetask, /removetask, /exit");

            while (isRunning)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                if (input == null)
                {
                    Console.WriteLine("Пустой ввод. Введите /help для списка команд.");
                    continue;
                }

                if (input.StartsWith("/start"))
                {
                    HandleStart(ref currentUser);
                }
                else if (input.StartsWith("/help"))
                {
                    HandleHelp();
                }
                else if (input.StartsWith("/info"))
                {
                    HandleInfo(version, creationDate);
                }
                else if (input.StartsWith("/echo"))
                {
                    HandleEcho(input, currentUser);
                }
                else if (input.StartsWith("/addtask"))
                {
                    if (currentUser != null)
                    {
                        HandleAddTask(tasks, currentUser, taskLimit, taskLengthLimit);
                    }
                    else
                    {
                        Console.WriteLine("Сначала введите команду /start и имя.");
                    }
                }
                else if (input.StartsWith("/showtasks"))
                {
                    HandleShowTasks(tasks);
                }
                else if (input.StartsWith("/showalltasks"))
                {
                    HandleShowAllTasks(tasks);
                }
                else if (input.StartsWith("/completetask"))
                {
                    HandleCompleteTask(tasks, input);
                }
                else if (input.StartsWith("/removetask"))
                {
                    HandleRemoveTask(tasks);
                }
                else if (input.StartsWith("/exit"))
                {
                    Console.WriteLine("Программа завершена.");
                    isRunning = false;
                }
                else
                {
                    Console.WriteLine("Неизвестная команда. Введите /help для списка команд.");
                }
            }
        }

        // ===== Методы валидации =====

        /// <summary>
        /// Парсит и валидирует целое число в диапазоне
        /// </summary>
        static int ParseAndValidateInt(string? str, int min, int max)
        {
            if (!int.TryParse(str, out int value))
                throw new ArgumentException("Ожидается целое число.");

            if (value < min || value > max)
                throw new ArgumentException($"Число должно быть в диапазоне от {min} до {max}.");

            return value;
        }

        /// <summary>
        /// Валидирует строку на пустоту
        /// </summary>
        static void ValidateString(string? str)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentException("Строка не должна быть пустой и состоять только из пробелов.");
        }

        // ===== Обработчики команд =====

        /// <summary>
        /// Обрабатывает команду /start
        /// </summary>
        static void HandleStart(ref ToDoUser currentUser)
        {
            Console.Write("Пожалуйста, введите своё имя: ");
            string userName = Console.ReadLine();
            ValidateString(userName);
            currentUser = new ToDoUser(userName);
            Console.WriteLine($"Здравствуйте, {userName}! Теперь доступна команда /echo.");
        }

        /// <summary>
        /// Обрабатывает команду /help
        /// </summary>
        static void HandleHelp()
        {
            Console.WriteLine("\n=== Справка ===");
            Console.WriteLine("/start - начать работу и ввести имя");
            Console.WriteLine("/help - показать это сообщение");
            Console.WriteLine("/info - информация о программе");
            Console.WriteLine("/echo <текст> - повторить введённый текст (доступно после /start)");
            Console.WriteLine("/addtask - добавить новую задачу в список");
            Console.WriteLine("/showtasks - показать все активные задачи");
            Console.WriteLine("/showalltasks - показать все задачи со статусом");
            Console.WriteLine("/completetask <id> - завершить задачу по ID");
            Console.WriteLine("/removetask - удалить задачу по номеру");
            Console.WriteLine("/exit - выйти из программы\n");
        }

        /// <summary>
        /// Обрабатывает команду /info
        /// </summary>
        static void HandleInfo(string version, string creationDate)
        {
            Console.WriteLine($"Версия программы: {version}");
            Console.WriteLine($"Дата создания: {creationDate}");
        }

        /// <summary>
        /// Обрабатывает команду /echo
        /// </summary>
        static void HandleEcho(string input, ToDoUser currentUser)
        {
            if (currentUser != null)
            {
                string message = input.Length > 6 ? input.Substring(6) : "";
                Console.WriteLine($"{currentUser.TelegramUserName}, ваш текст: {message}");
            }
            else
            {
                Console.WriteLine("Сначала введите команду /start и имя.");
            }
        }

        /// <summary>
        /// Обрабатывает команду /addtask
        /// </summary>
        static void HandleAddTask(List<ToDoItem> tasks, ToDoUser currentUser, int taskLimit, int taskLengthLimit)
        {
            Console.Write("Введите описание задачи: ");
            string taskName = Console.ReadLine();

            ValidateString(taskName);

            if (tasks.Count >= taskLimit)
                throw new TaskCountLimitException(taskLimit);

            if (taskName.Length > taskLengthLimit)
                throw new TaskLengthLimitException(taskName.Length, taskLengthLimit);

            // Проверка дубликатов среди активных задач
            if (tasks.Any(t => t.Name == taskName && t.State == ToDoItemState.Active))
                throw new DuplicateTaskException(taskName);

            ToDoItem newTask = new ToDoItem(currentUser, taskName);
            tasks.Add(newTask);
            Console.WriteLine($"✓ Задача добавлена. ID: {newTask.Id}");
        }

        /// <summary>
        /// Обрабатывает команду /showtasks (показывает только активные)
        /// </summary>
        static void HandleShowTasks(List<ToDoItem> tasks)
        {
            var activeTasks = tasks.Where(t => t.State == ToDoItemState.Active).ToList();

            if (activeTasks.Count == 0)
            {
                Console.WriteLine("Список активных задач пуст.");
            }
            else
            {
                Console.WriteLine("\n=== Активные задачи ===");
                for (int i = 0; i < activeTasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {activeTasks[i]}");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Обрабатывает команду /showalltasks (показывает все со статусом)
        /// </summary>
        static void HandleShowAllTasks(List<ToDoItem> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("Список всех задач пуст.");
            }
            else
            {
                Console.WriteLine("\n=== Все задачи ===");
                for (int i = 0; i < tasks.Count; i++)
                {
                    string state = tasks[i].State == ToDoItemState.Active ? "✓ Активна" : "✗ Завершена";
                    string stateChangedInfo = tasks[i].StateChangedAt.HasValue 
                        ? $" (завершена: {tasks[i].StateChangedAt:dd.MM.yyyy HH:mm:ss})"
                        : "";
                    Console.WriteLine($"{i + 1}. [{state}] {tasks[i]}{stateChangedInfo}");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Обрабатывает команду /completetask
        /// </summary>
        static void HandleCompleteTask(List<ToDoItem> tasks, string input)
        {
            string[] parts = input.Split(' ');
            
            if (parts.Length < 2)
            {
                Console.WriteLine("Пожалуйста, укажите ID задачи. Пример: /completetask 73c7940a-ca8c-4327-8a15-9119bffd1d5e");
                return;
            }

            if (!Guid.TryParse(parts[1], out Guid taskId))
            {
                Console.WriteLine("Неверный формат ID. Используйте UUID.");
                return;
            }

            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
                throw new TaskNotFoundException(taskId);

            task.CompleteTask();
            Console.WriteLine($"✓ Задача '{task.Name}' завершена.");
        }

        /// <summary>
        /// Обрабатывает команду /removetask
        /// </summary>
        static void HandleRemoveTask(List<ToDoItem> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("Список задач пуст, удалять нечего.");
                return;
            }

            Console.WriteLine("\nТекущие задачи:");
            for (int i = 0; i < tasks.Count; i++)
            {
                string state = tasks[i].State == ToDoItemState.Active ? "✓ Активна" : "✗ Завершена";
                Console.WriteLine($"{i + 1}. [{state}] {tasks[i]}");
            }

            Console.Write("Введите номер задачи для удаления: ");
            string indexInput = Console.ReadLine();
            int index = ParseAndValidateInt(indexInput, 1, tasks.Count);

            tasks.RemoveAt(index - 1);
            Console.WriteLine("✓ Задача удалена.\n");
        }
    }
}
