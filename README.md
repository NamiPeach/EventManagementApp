
# EventManagementApp - Система управління подіями (Organizers, Events, Registrations)

Система для організації подій (концерти, конференції) з MVC-адмін-панеллю та MAUI-застосунком.

## Структура проєкту

- `Server/ServerApp` — ASP.NET Core застосунок (MVC адмін-панель + REST API)
- `Client/ClientApp` — .NET MAUI застосунок (MVVM, мобільний/десктопний клієнт)
- `Database` — SQL-скрипти для ініціалізації бази даних

## Встановлення та запуск

### Вимоги
- Visual Studio 2022 з .NET 8 SDK
- Робочі навантаження: ASP.NET and web development, .NET MAUI

### Запуск сервера (ServerApp)
```bash
cd Server/ServerApp
dotnet run
```

### Запуск клієнта (ClientApp)
Відкрити `Client/ClientApp` у Visual Studio та запустити на емуляторі Android/iOS або Windows.