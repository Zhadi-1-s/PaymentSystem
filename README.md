# PaymentSystem
PaymentSystem
Система онлайн-платежей, состоящая из клиентской части на Angular и серверной части на .NET.

🚀 Стек технологий
Frontend
Framework: Angular 15.2.0

Language: TypeScript 4.9.4

State Management & Async: RxJS 7.8.0

Testing: Jasmine & Karma

Backend
Framework: .NET 10 (Preview/Current)

Environment: ASP.NET Core Development

API URL: http://localhost:5213 или https://localhost:7214

🛠️ Установка и запуск
Для работы проекта вам понадобятся:

Node.js (рекомендуется LTS версия)

Angular CLI (npm install -g @angular/cli)

.NET SDK 10

1. Запуск Backend
Перейдите в директорию с бэкенд-проектом:

Bash
dotnet restore
dotnet run --launch-profile https
Бэкенд будет доступен по адресу: https://localhost:7214

2. Запуск Frontend
Перейдите в папку frontend:

Bash
# Установка зависимостей
npm install

# Запуск сервера разработки
npm start
Приложение откроется по адресу http://localhost:4200/.

📂 Структура скриптов фронтенда
В папке фронтенда доступны следующие команды:

npm start — запуск приложения в режиме разработки (ng serve).

npm run build — сборка проекта для продакшена.

npm run watch — сборка с автоматическим обновлением при изменениях.

npm test — запуск юнит-тестов через Karma.

⚙️ Конфигурация среды
Backend (launchSettings.json)
Проект настроен на запуск в среде Development. Основные порты:

HTTP: 5213

HTTPS: 7214

Frontend (package.json)
Используется Angular 15 с поддержкой анимаций, форм и роутинга. Конфигурация по умолчанию находится в angular.json.

📝 О проекте
Проект представляет собой систему управления платежами (PaymentSystem).

Репозиторий: GitHub: Zhadi-1-s/PaymentSystem

Generated for PaymentSystem Project
