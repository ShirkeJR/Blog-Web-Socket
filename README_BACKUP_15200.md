# Blog #

### Założenia ###
* Klienci posiadają konto na serwerze.
* Każdy klient ma własnego bloga, na który może dodawać posty.
* Serwer zarządza blogami klientów

### ToDo ###
wszystko

### Opis protokołu ###
Komunikaty pomiędzy serwerem a klientem wymieniane są w formie tekstowej. Pojedynczy pakiet ma następującą budowę:
`X Y ZZZZZZZZZ...`
gdzie:
* `X` - całkowita długość prawidłowo odebranego pakietu liczona od pierwszego znaku Y do końca komunikatu
* `Y` - komenda określająca żądanie
* `ZZZZZZZZ...` - dane składające się na zapytanie (w przypadku klienta) bądź odpowiedź (w przypadku serwera)
<<<<<<< HEAD
=======
* `END` - ciąg znaków oznaczający koniec komunikatu, /rn/rn/rn$$
>>>>>>> 1a4142a... Update README.md

Pola te są rozdzielone tabulatorem.

W odniesieniu do przykładu:
`39 LOGIN janbonkowski@umcs.pl TestTest123#`
* 39 to liczba znaków licząc od `L` do `#`.
* `LOGIN` to żądanie wysyłane do serwera
* `janbonkowski@umcs.pl TestTest123#` to parametry żądania (tutaj login i hasło)

### Komunikaty ###
---
#### Serwer ####
W przypadku wystąpienia błędu serwer powinien odpowiedzieć:
* `4 QUE?` - jeśli występuje niezgodność rozmiaru pakietu odebranego z zadeklarowaną, komunikat był za duży lub żądanie nie zostało rozpoznane
* `12 IDENTIFY_PLS` - jeśli żądanie wymaga autentykacji ze strony użytkownika, a do niej nie doszło

Serwer po przetworzeniu każdego żądania odpowiada tym samym komunikatem, jednak przesłane parametry zostają zastąpione odpowiedzią na żądanie. W odniesieniu do przykładu:
`39 LOGIN janbonkowski@umcs.pl TestTest123#`
odpowiedź na żądanie może przyjąć jedną z dwóch postaci:
* `11 LOGIN OK 14` - gdy logowanie się powiedzie, 14 to przykładowe ID bloga zalogowanego użytkownika
* `12 LOGIN FAILED` - gdy logowanie się nie powiedzie

---
#### Klient ####
Ogólny format opisu pakietu:
* __Nazwa__: 
* __Treść pakietu__:
* __Ilość parametrów__: 
* __Opis parametru 1__:
* __Opis parametru 2__:
* __Opis parametru 3__:
* ...
* __Odpowiedź serwera__:
* __Opis odpowiedzi__:
* __Ilość parametrów odpowiedzi__:
* __Opis parametru 1__:
* __Opis parametru 2__:
* __Opis parametru 3__:
* ...

---
* __Nazwa__: Pakiet tworzenia konta 
* __Treść pakietu__: `REGISTER janbonkowski@umcs.pl TestTest123#`
* __Ilość parametrów__: 2
* __Opis parametru 1__: string zawierający login tworzonego konta
* __Opis parametru 2__: string zawierający hasło tworzonego konta
* __Odpowiedź serwera__: `REGISTER OK`
* __Opis odpowiedzi__: status utworzenia konta
* __Ilość parametrów odpowiedzi__: 1
* __Opis parametru 1__: string określający czy konto zostało utworzone pomyślnie (_OK_) czy wystąpił błąd (_INVALID_)

---
* __Nazwa__: Pakiet logowania
* __Treść pakietu__: `LOGIN janbonkowski@umcs.pl TestTest123#`
* __Ilość parametrów__: 2
* __Opis parametru 1__: string zawierający login konta
* __Opis parametru 2__: string zawierający hasło konta
* __Odpowiedź serwera__: `LOGIN OK 14`
* __Opis odpowiedzi__: zalogowano pomyślnie i przesłano ID bloga
* __Ilość parametrów odpowiedzi__: 2
* __Opis parametru 1__: string informujący o pomyślnym logowaniu
* __Opis parametru 2__: int będący ID bloga w systemie
* __Odpowiedź serwera__: `LOGIN FAILED INVALID`
* __Opis odpowiedzi__: logowanie się nie powiodło z powodu niepoprawnych danych
* __Ilość parametrów odpowiedzi__: 2
* __Opis parametru 1__: string informujący o statusie logowania
* __Opis parametru 2__: string informujący o nieprawidłowych danych
* __Odpowiedź serwera__: `LOGIN FAILED LOCKED`
* __Opis odpowiedzi__: logowanie się nie powiodło z powodu zablokowania konta
* __Ilość parametrów odpowiedzi__: 2
* __Opis parametru 1__: string informujący o statusie logowania
* __Opis parametru 2__: string informujący o zablokowaniu konta

---
* __Nazwa__: Pakiet pobierania listy blogów
* __Treść pakietu__: `DISPLAY_BLOGS`
* __Ilość parametrów__: 0
* __Odpowiedź serwera__: `DISPLAY_BLOGS 1|Testowy Blog 2|Blog Programistyczny 3|Elektronika dla każdego ...`
* __Opis odpowiedzi__: lista blogów na serwerze
* __Ilość parametrów odpowiedzi__: n
* __Opis parametru 1__: int określający ID bloga + string określający nazwę bloga. Nazwa bloga nie może zawierać tabulacji w nazwie i separatora pionowego |!

---
* __Nazwa__: Pakiet pobierania listy wpisów na blogu
* __Treść pakietu__: `DISPLAY_BLOG X`
* __Ilość parametrów__: 1
* __Opis parametru 1__: int zawierający id bloga
* __Odpowiedź serwera__: `DISPLAY_BLOG 1|Powitanie 2|Kurs programowania 3|Czym są zmienne 4|Wyświetlanie danych w konsoli ...`
* __Opis odpowiedzi__: lista wpisów na blogu
* __Ilość parametrów odpowiedzi__: n
* __Opis parametru 1__: int określający ID wpisu|string określający tytuł wpisu na blogu.
* __Odpowiedź serwera__: `DISPLAY_BLOG FAILED`
* __Opis odpowiedzi__: nie można było pobrać listy wpisów na blogu, bo np. nie istnieje
* __Ilość parametrów odpowiedzi__: 1
* __Opis parametru 1__: string informujący o niepowodzeniu przy pobieraniu listy

---
* __Nazwa__: Pakiet dodawania wpisu do bloga
* __Treść pakietu__: `ADD_ENTRY To jest tytuł wpisu To jest treść wpisu`
* __Ilość parametrów__: 2
* __Opis parametru 1__: tytuł wpisu
* __Opis parametru 2__: treść wpisu
* __Odpowiedź serwera__: `ADD_ENTRY OK 15`
* __Opis odpowiedzi__: komunikat informujący o sukcesie dodawania wpisu i zawierający ID utworzonego wpisu
* __Ilość parametrów odpowiedzi__: 2
* __Opis parametru 1__: string określający sukces przy dodawaniu wpisu
* __Opis parametru 2__: int określający ID dodanego wpisu
* __Odpowiedź serwera__: `ADD_ENTRY ERR_TITLE`
* __Opis odpowiedzi__: Tytuł był zbyt długi lub zawierał niedozwolone znaki
* __Ilość parametrów odpowiedzi__: 1
* __Opis parametru 1__: string informujący o niepowodzeniu przy dodawaniu treści na bloga ze względu na błąd w tytule
* __Odpowiedź serwera__: `ADD_ENTRY ERR_CONTENT`
* __Opis odpowiedzi__: Treść była zbyt długa lub zawierała niedozwolone znaki
* __Ilość parametrów odpowiedzi__: 1
* __Opis parametru 1__: string informujący o niepowodzeniu przy dodawaniu treści na bloga ze względu na błąd w treści
* __Odpowiedź serwera__: `ADD_ENTRY ERR_OWNER`
* __Opis odpowiedzi__: Użytkownik nie jest właścicielem bloga, na który próbuje dodać notatkę
* __Ilość parametrów odpowiedzi__: 1
* __Opis parametru 1__: string informujący o niepowodzeniu przy dodawaniu treści na nie swojego bloga

---
* __Nazwa__: Pakiet pobierania wpisu na blogu
* __Treść pakietu__: `DISPLAY_ENTRY 1`
* __Ilość parametrów__: 1
* __Odpowiedź serwera__: `DISPLAY_ENTRY 15 Tytuł notatki Treść notatki`
* __Opis odpowiedzi__: odpowiedź zawierająca ID notatki, jej tytuł i treść
* __Ilość parametrów odpowiedzi__: 3
* __Opis parametru 1__: int określający ID wpisu
* __Opis parametru 2__: string określający tytuł wpisu
* __Opis parametru 3__: string określający treść wpisu

---
* __Nazwa__: Pakiet usuwania wpisu z bloga
* __Treść pakietu__: `DELETE_ENTRY 15`
* __Ilość parametrów__: 1
* __Opis parametru 1__: ID usuwanego wpisu
* __Odpowiedź serwera__: `DELETE_ENTRY OK 15`
* __Opis odpowiedzi__: komunikat informujący o sukcesie usuwania wpisu i zawierający ID usuniętego wpisu
* __Ilość parametrów odpowiedzi__: 2
* __Opis parametru 1__: string określający sukces przy usuwaniu wpisu
* __Opis parametru 2__: int określający ID usuniętego wpisu
* __Odpowiedź serwera__: `DELETE_ENTRY FAILED NOTEXIST`
* __Opis odpowiedzi__: wpis o podanym ID nie istnieje
* __Ilość parametrów odpowiedzi__: 1
* __Opis parametru 1__: string informujący o niepowodzeniu przy usuwaniu wpisu
* __Odpowiedź serwera__: `DELETE_ENTRY FAILED NOTOWNER`
* __Opis odpowiedzi__: Użytkownik nie jest właścicielem bloga, z którego próbuje usunąć wpis
* __Ilość parametrów odpowiedzi__: 1
* __Opis parametru 1__: string informujący o niepowodzeniu przy usuwaniu treści z nie swojego bloga

---
* __Nazwa__: Pakiet wylogowania
* __Treść pakietu__: `THX_BYE`
* __Ilość parametrów__: 0
* __Odpowiedź serwera__: `THX_BYE`
* __Opis odpowiedzi__: komunikat informujący o pomyślnym wylogowaniu
* __Ilość parametrów odpowiedzi__: 0

---
* __Nazwa__: Pakiet GTFO
* __Treść pakietu__: `Co kolwiek`
* __Ilość parametrów__: 0
* __Odpowiedź serwera__: `GTFO`
* __Opis odpowiedzi__: komunikat informujący o nie istniejącym komunikacie
* __Ilość parametrów odpowiedzi__: 0

---
* __Nazwa__: Pakiet zmiany nazwy bloga
* __Treść pakietu__: `CHANGE_BLOG_NAME Id Nowa nazwa bloga`
* __Ilość parametrów__: 2
* __Opis parametru 1__: id bloga którego nazwa ma zostać zmieniona
* __Opis parametru 2__: string zawierający nową nazwę bloga
* __Odpowiedź serwera__: `CHANGE_BLOG_NAME OK`
* __Opis odpowiedzi__: komunikat informujący o pomyślnej zmianie nazwy bloga
* __Ilość parametrów odpowiedzi__: 1
* __Odpowiedź serwera__: `CHANGE_BLOG_NAME FAILED`
* __Opis odpowiedzi__: komunikat informujący o niepomyślnej zmianie nazwy bloga (np. próbowano zmienić nazwę nie swojego bloga)
* __Ilość parametrów odpowiedzi__: 1
* __Odpowiedź serwera__: `CHANGE_BLOG_NAME NOTOWNER`
* __Opis odpowiedzi__: Użytkownik nie jest właścicielem bloga, z którego próbuje usunąć wpis
* __Ilość parametrów odpowiedzi__: 1
* __Opis parametru 1__: string informujący o niepowodzeniu przy usuwaniu treści z nie swojego bloga
