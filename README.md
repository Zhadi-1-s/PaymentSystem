# PaymentSystem
# PaymentSystem

Система управления платежами. Проект состоит из клиентской части на Angular и серверной части на .NET 10.

## 🛠 Технологический стек

### Frontend
* **Framework:** Angular 15.2.0
* **Language:** TypeScript 4.9.4
* **State Management:** RxJS 7.8.0
* **UI & Animations:** @angular/animations
* **Testing:** Jasmine & Karma

### Backend
* **Runtime:** .NET 10
* **Environment:** ASP.NET Core (Development)
* **API URL:** * HTTPS: `https://localhost:7214`
    * HTTP: `http://localhost:5213`

---

## 🚀 Инструкция по запуску

### 1. Требования
Убедитесь, что у вас установлены:
* [Node.js](https://nodejs.org/) (версия 18+)
* [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`)
* [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### 2. Запуск Backend
Перейдите в корневую директорию бэкенд-проекта и выполните:
```bash
dotnet restore
dotnet run --launch-profile https
