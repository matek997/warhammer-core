# Warhammer
Projekt wykonany na zajęcia z **Programowanie w środowisko ASP.NET**.

Aplikacja ma za zadanie wspomagać fanów gier fabularnych w cieszeniu się ich hobby, szczególnie w obecnej sytuacji, gdy klasyczne spotkania wokół stołu nie wchodzą w grę.
W swoim przeznaczeniu i działaniu stawia sobie za wzór platformę Roll20 (https://roll20.net/), jednak skupia się tylko na na drugiej edycji Warhammer RPG.
Aplikacja ułatwia proces tworzenia postaci oraz budowania świata przez graczy lub mistrza gry, oraz komfortową rozgrywkę i redukcję czasu spędzonego na obliczanie atrybutów i innych rzeczy, które są mniej lubianą częścią doświadczenia.

## Skład
Grzegorz Sobociński <br/>
Mateusz Hanzel

## Technologie
Projekt składa się z trzech głównych części:
- front-end - aplikacja napisana za pomocą React (TypeScript). [Link do repozytorium](https://github.com/matek997/warhammer).
- back-end - aplikacja napisana za pomocą .NET Core. Solucja podzielona jest na 6 projektów.
    - WarhammerCore.Abstract - zawiera interfejsy oraz modele użyte w biznesowej logice solucji.
    - WarhammerCore.Business - zawiera implementację interfejsów w postaci serwisów. Znajduje się pomiędzy WebAPI a bazą danych.
    - WarhammerCore.Data - implementacja Entity Framework 6. Łączy się z bazą danych oraz wyciąga z niej dane.
    - WarhammerCore.WebApi - zawiera implementację ASP.NET. Posiada kontrollery oraz waliduje zapytania użytkownika. 
    - WarhammerCore.Tests.Integration - zawiera testy integracyjne. Używa bazy danych SQLite w celu zamockowania prawdziwej bazy. Testuje poprawne wyciąganie danych oraz ich formatowanie. Napisane przy użyciu xUnit.
    - WarhammerCore.Tests.Unit - zawiera testy jednostkowe kontrollerów. Napisane przy użyciu xUnit.
- baza danych - relacyjna baza danych stworzona przy użyciu Microsoft SQL Server. Dostęp do bazy za pomocą Microsoft SQL Server Management Studio 18. [Link do kopii bazy danych](https://github.com/matek997/warhammer-core/wiki/Setup).

Więcej informacji o projekcie można znaleźć w [zakładce Wiki](https://github.com/matek997/warhammer-core/wiki) .

## Wymaganie projektowe
- [x] UI, back-end, przechowywanie danych (wykorzystanie bazy danych)
- [x] Widok / komponent komunikujący się z serwerem za pomocą AJAX
- [x] Wstrzykiwanie zależności
- [x] Obsługa błędów w sposób przyjazny dla użytkownika
- [x] Bezpieczeństwo aplikacji, walidacja danych wejściowych
- [x] Dobre praktyki

## Wymagania projektowe (opcjonalne)
- [x] Logowanie do pliku ("obserwowalność" aplikacji)
- [ ] Wielojęzyczność
- [x] Web sockets
- [x] TypeScript / Single Page App
- [ ] SASS / SCSS
- [x] Konfigurowalność palikacji na różne środowiska (testowe, produkcyjne)
- [x] Continuous Integration / Continuous Delivery
- [ ] Hostowanie w Azure / inny dostawca
- [x] Docker
