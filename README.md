# Мессенджер LiteCall

https://github.com/user-attachments/assets/6d1d5c71-e55d-4d84-a3bd-b223296f5e4f


## __Оглавление__

- [Описание](#Описание)
- [Установка](#Установка)
- [Возможности приложения](#Что-может-делать-приложение?)
- [Ссылки на авторов](#Ссылки-на-авторов)

---

## __Описание__

На данный момент мессенджер реализован на связке **WPF+SignalR Core+ASP.NET Core**.

На первом этапе работы был выбран фреймворк WCF, позже он был заменён на SignalR.

### __11.03.2022__ Первая  рабочая SignalR версия клиента
Выпущена первая рабочая версия SignalR сервера, на ней отсутствуют проблемы которые были на WCF.

<details>
<summary>Проблемы WCF</summary>
<br>

- При проверке в глобальной сети столкнулись с серьезными проблемами тайм-аута(когда вы хотите использовать дуплексный сервис в течение длительного времени). Нам пришлось периодически совершать сервисные вызовы, чтобы поддерживать клиентские соединения
- Нет автоматической обработки разрыва соединений, приходиться при каждом обратном вызове отлавливать ошибки через try catch{ } и удалять клиентов с которыми потеряно соединение (на котором выдало исключение)
- Сложная интеграция с клиентом т.к. VS 2022 не поддерживает нормальную генерацию proxy(прокси), а использовать SVCutil затратно по времени 

</details>


### __Окно авторизации__

![Project Image](https://github.com/user-attachments/assets/06520c9b-a20e-41d1-816d-690975db1578)

### __Главная страница__
![Project Image](https://github.com/user-attachments/assets/1b767ac7-a27f-4530-8890-39aaa15c110b)

### __Окно подключения к серверу__

![Project Image](https://github.com/user-attachments/assets/99cf3da8-788c-46f5-8708-533e69b35e4a)


### __Страница сервера__

![Project Image](https://github.com/user-attachments/assets/0091bd2d-00aa-4c1a-b899-7269cec0825f)


### __Окно сервера__

![Project Image](https://github.com/Nutr1k/LiteCall/blob/master/ReadmeAssets/ServerConsol.png)

#### Технологии

### Client
[![.Net][.Net-shield]][.Net-url]
[![SignalR][SignalR-shield]][SignalR-url]
[![ReactiveUI][ReactiveUI-shield]][ReactiveUI-url]
[![Naudio][NAudio-shield]][NAudio-url]
[![Newtonsoft][Newtonsoft-shield]][Newtonsoft-url]

### Authentication server (also main server)
[![Asp.Net][Asp.Net-shield]][Asp.Net-url]
[![SqlLite.Net][SqlLite-shield]][SqlLite-url]
[![EntityFramework.Net][EntityFramework-shield]][EntityFramework-url]
![JwtBearer][JwtBearer-shield]


### Chat-server
[![Asp.Net][Asp.Net-shield]][Asp.Net-url]
[![SignalR][SignalR-shield]][SignalR-url]

## __Серверная часть LiteCall__
Серверная часть чата реализована на ASP.NET Core SignalR

**Задачи которые реализованы Сервером чата([SignalRServer](https://github.com/Nutr1k/LiteCall/tree/master/SignalRServer)):**
- Текстовый групповой чат
- Голосовой чат(стабильная работа более чем с 2-мя пользователями)
- Создание комнат(групп) для общения
- Автоматическое удаление пустых групп при отключении пользователей
- Автоматическое удаление из групп при отключении
- Контроль имён пользователей(уникальность имён)
- Валидация JWT

****

**Задачи которые реализованы Сервером Авторизации([ServerAuthorization](https://github.com/Nutr1k/LiteCall/tree/master/ServerAuthorization)):**
- Генерация JWT
- Шифрование JWT токена с помощью RSA256
- Регистрация пользователей
- Получения списка серверов и информации о них

<details>
<summary>Подробнее про авторизацию</summary>
<br>
Пароль при передаче шифруются однонаправленным алгоритмом SHA-1, целостность передачи важных данных гарантирует Json Web Token. Json Web Token зашифрован с помощью алгоритма SHA-256

**Используемая JWT конструкция**

В полезной нагрузке расположена информация об имени пользователя,его роли, времени действия токена.
<br>
![Project Image](https://github.com/Nutr1k/LiteCall/blob/master/ReadmeAssets/JWT.png)

</details>


## __Установка__

[![Release](https://img.shields.io/github/v/release/Nutr1k/LiteCall?label=Latest%20Release)](https://github.com/Nutr1k/LiteCall/releases/tag/1.0.0)
``` bash
1.Установить клиент - LiteCall.exe
2.Запустить сервера - LC-servers.exe (\LT-servers\LiteCallServer\LC-servers.exe)
3.IP для подключения по стандарту будет localhost:43891
```
[![YouTube](https://img.shields.io/badge/LiteCall-YouTube-090909?style=for-the-badge&logo=YouTube&logoColor=FF0000)](https://www.youtube.com/watch?v=dQw4w9WgXcQ](https://youtu.be/GtL1huin9EE)

---
## __Недобавленные возможности/баги:__
- Невозможность подключения больше чем 2 человек в голосовой чат __[03.06.22 исправлено]__
- Отсутствие нормального отключения пользователя от сервера __[11.03.22 исправлено]__
- Отсутствие нормальной валидации __[10.03.22 исправлено]__
---
## __Ссылки на авторов__


Клиентская часть:

[![Костя](https://img.shields.io/badge/-Костя-1C1C22?style=for-the-badge&logo=vk&logoColor=blue)](https://vk.com/jessnjake)


Серверная часть:

[![Артём](https://img.shields.io/badge/-Артём-1C1C22?style=for-the-badge&logo=telegram)](https://t.me/artemK6484)


<!-- MARKDOWN LINKS & IMAGES -->
[contributors-shield]: https://img.shields.io/github/contributors/Nutr1k/LiteCall.svg?style=for-the-badge
[contributors-url]: https://github.com/Nutr1k/LiteCall/graphs/contributors

[forks-shield]: https://img.shields.io/github/forks/Nutr1k/LiteCall.svg?style=for-the-badge
[forks-url]: https://github.com/Nutr1k/LiteCall/network/members

[stars-shield]: https://img.shields.io/github/stars/Nutr1k/LiteCall.svg?style=for-the-badge
[stars-url]: https://github.com/Nutr1k/LiteCall/stargazers

[issues-shield]: https://img.shields.io/github/issues/Nutr1k/LiteCall.svg?style=for-the-badge
[issues-url]: https://github.com/othneildrew/Nutr1k/LiteCall

[license-shield]: https://img.shields.io/github/license/Nutr1k/LiteCall.svg?style=for-the-badge
[license-url]: https://github.com/Nutr1k/LiteCall/blob/master/LICENSE.txt

[NAudio-shield]: https://img.shields.io/badge/NAudio-090909?style=for-the-badge&logo=
[NAudio-url]: https://github.com/naudio/NAudio

[ReactiveUI-shield]: https://img.shields.io/badge/ReacctiveUI-090909?style=for-the-badge&logo=
[ReactiveUI-url]: https://www.reactiveui.net/

[Newtonsoft-shield]: https://img.shields.io/badge/Json.NET-090909?style=for-the-badge&logo=
[Newtonsoft-url]: https://www.newtonsoft.com/json

[SignalR-shield]: https://img.shields.io/badge/SignalR-090909?style=for-the-badge&logo=
[SignalR-url]: https://docs.microsoft.com/ru-ru/aspnet/core/tutorials/signalr?view=aspnetcore-6.0&tabs=visual-studio

[.Net-shield]: https://img.shields.io/badge/.Net-090909?style=for-the-badge&logo=
[.Net-url]: https://dotnet.microsoft.com/en-us/

[Asp.Net-shield]: https://img.shields.io/badge/Asp.Net_Core-090909?style=for-the-badge&logo=
[Asp.Net-url]: https://dotnet.microsoft.com/en-us/apps/aspnet

[JwtBearer-shield]: https://img.shields.io/badge/JwtBearer-090909?style=for-the-badge&logo=

[EntityFramework-shield]: https://img.shields.io/badge/EntityFramework-090909?style=for-the-badge&logo=
[EntityFramework-url]: https://docs.microsoft.com/ru-ru/ef/

[SqlLite-shield]: https://img.shields.io/badge/SqlLite-090909?style=for-the-badge&logo=
[SqlLite-url]: https://www.sqlite.org/index.html
