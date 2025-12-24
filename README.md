# youngAPI

## О проекте
youngAPI - это сервис для публикации и прослушивания музыки, разработанный с использованием **ASP.NET CORE Web API**

## Функционал
- Регистрация и аунтефикация пользователей
- Публикация, прослушивание, обновление и удаление музыки

## Технологии
- C#
- ASP.NET CORE Web API
- Entity Framework Core
- Microsoft SQL Server
- Identity

## Как запустить проект
### Склонируем репозиторий
```bash
   git clone https://github.com/akanelovw/TaskTracker.git
```
- Настраивем подключение к базе данных (В appsettings.json установить корректную строку подключения к SQL Server.)
### Применим миграции
```bash
   dotnet ef database update
```
- Запускаем проект
