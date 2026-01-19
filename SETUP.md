# Инструкция: Как создать полноценный C# проект на GitHub

## 📋 Шаг 1: Подготовка файлов локально

### Вариант 1 - Через командную строку (Git Bash, PowerShell, Terminal)

```bash
# Создайте папку проекта
mkdir HockeyTodoApp
cd HockeyTodoApp

# Инициализируйте Git репозиторий
git init

# Создайте нужные папки
mkdir Models
mkdir Exceptions
mkdir Handlers

# Скопируйте все файлы в нужные места
# Program.cs - в корень
# ToDoUser.cs, ToDoItem.cs, ToDoItemState.cs - в папку Models
# Остальные файлы исключений - в папку Exceptions
```

## 📂 Шаг 2: Структура файлов

Скопируйте эти файлы в соответствующие папки:

### **Корневая папка:**
- `Program.cs` (основное приложение)
- `HockeyTodoApp.csproj` (конфигурация)
- `.gitignore` (исключения)
- `README.md` (документация)

### **Папка Models/:**
- `ToDoUser.cs`
- `ToDoItem.cs`
- `ToDoItemState.cs`

### **Папка Exceptions/:**
- `TaskCountLimitException.cs`
- `TaskLengthLimitException.cs`
- `DuplicateTaskException.cs`
- `TaskNotFoundException.cs`

## 🌐 Шаг 3: Создание репозитория на GitHub

1. Перейдите на **github.com** и войдите в аккаунт
2. Нажмите **"+"** в правом верхнем углу → **"New repository"**
3. Заполните:
   - **Repository name:** `HockeyTodoApp`
   - **Description:** `Console application for task management`
   - **Visibility:** Public (если хотите, чтобы все видели)
   - **Initialize this repository with:**
     - ☐ Add a README file (НЕ проверяйте - у нас уже есть!)
     - ☐ Add .gitignore (НЕ проверяйте - у нас уже есть!)
4. Нажмите **"Create repository"**

## 📤 Шаг 4: Загрузка проекта на GitHub

### Вариант 1: Через командную строку

```bash
# Перейдите в папку проекта
cd HockeyTodoApp

# Добавьте все файлы
git add .

# Создайте первый коммит
git commit -m "Initial commit: Add HockeyTodoApp project structure"

# Добавьте remote URL (замените YOUR_USERNAME и YOUR_REPO_NAME)
git branch -M main
git remote add origin https://github.com/YOUR_USERNAME/HockeyTodoApp.git

# Загрузите на GitHub
git push -u origin main
```

### Вариант 2: Через GitHub Desktop

1. Откройте **GitHub Desktop**
2. Нажмите **"File"** → **"Add Local Repository"**
3. Выберите папку `HockeyTodoApp`
4. Нажмите **"Create Repository"**
5. Нажмите **"Publish repository"**
6. Убедитесь, что **"Keep this code private"** НЕ проверена
7. Нажмите **"Publish Repository"**

### Вариант 3: Через Visual Studio

1. Откройте проект в Visual Studio
2. **File** → **Add to Source Control** → **Git**
3. **View** → **Team Explorer**
4. **Publish** → введите имя репозитория
5. **Publish**

## ✅ Шаг 5: Проверка

После загрузки на GitHub:

```bash
# Проверьте, что репозиторий связан
git remote -v

# Вывод должен быть:
# origin  https://github.com/YOUR_USERNAME/HockeyTodoApp.git (fetch)
# origin  https://github.com/YOUR_USERNAME/HockeyTodoApp.git (push)
```

Откройте ваш репозиторий на GitHub и проверьте, что все файлы загружены.

## 🚀 Шаг 6: Позволить другим скачать и запустить

Теперь люди смогут:

```bash
# Клонировать репозиторий
git clone https://github.com/YOUR_USERNAME/HockeyTodoApp.git

# Перейти в папку
cd HockeyTodoApp

# Запустить приложение
dotnet run
```

## 📝 Шаг 7: Дальнейшие обновления

Когда вы сделали изменения:

```bash
git add .
git commit -m "Описание изменений"
git push
```

## 🎯 Итоговая структура на GitHub

```
HockeyTodoApp/
├── .git/                          # Git папка (создается автоматически)
├── Models/
│   ├── ToDoUser.cs
│   ├── ToDoItem.cs
│   └── ToDoItemState.cs
├── Exceptions/
│   ├── TaskCountLimitException.cs
│   ├── TaskLengthLimitException.cs
│   ├── DuplicateTaskException.cs
│   └── TaskNotFoundException.cs
├── Program.cs
├── HockeyTodoApp.csproj
├── .gitignore
├── README.md
└── SETUP.md                       # Этот файл
```

## 🆘 Если что-то пошло не так

### Ошибка: "Permission denied (publickey)"
```bash
# Добавьте SSH ключ на GitHub
ssh-keygen -t ed25519 -C "your@email.com"
# Скопируйте содержимое ~/.ssh/id_ed25519.pub в GitHub Settings → SSH Keys
```

### Ошибка: "fatal: not a git repository"
```bash
# Инициализируйте Git
git init
```

### Хотите удалить remote и заново добавить
```bash
git remote remove origin
git remote add origin https://github.com/YOUR_USERNAME/HockeyTodoApp.git
git branch -M main
git push -u origin main
```

## ✨ Готово!

Ваш полноценный C# проект теперь на GitHub! 🎉

Люди смогут:
- ✅ Скачать ваш код
- ✅ Запустить его через `dotnet run`
- ✅ Видеть историю изменений
- ✅ Делать Pull Requests с улучшениями
