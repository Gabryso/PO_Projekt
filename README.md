# Sklep internetowy

## Opis programu

Aplikacja konsolowa w języku **C#**, symulująca sklep internetowy z obsługą magazynu, koszyka oraz wystawianiem paragonu. Umożliwia zarządzanie asortymentem (dodawanie, usuwanie produktów), a także robienie zakupów z podziałem na tryb administratora oraz użytkownika.

---

## Instrukcja uruchomienia

1. Otwórz projekt w środowisku programistycznym obsługującym C# (np. Visual Studio, VS Code + .NET).
2. Upewnij się, że masz zainstalowane środowisko **.NET SDK** (co najmniej .NET 6).
3. Uruchom program (np. poprzez `Run Project` albo `dotnet run` w terminalu w katalogu projektu).
4. Postępuj zgodnie z komunikatami w konsoli.

---

## Instrukcja obsługi programu

### Główne menu

Po uruchomieniu programu będzie widoczne menu główne z następującymi opcjami:

1. **Administrator** – przechodzi dalej w trybie Administratora.  
2. **Użytkownik** – przechodzi dalej w trybie Użytkownika.  
0. **Zakończ program** – kończy działanie programu.

---

### Tryb Administratora

**Wymaga podania hasła: `qwerty`**  
Po poprawnym logowaniu możliwe są następujące opcje:

1. **Pokaż katalog** – wyświetla wszystkie produkty w magazynie.  
2. **Dodaj produkt** – dodaje produkt do magazynu (z podziałem na typy: zabawka, napój, jedzenie, inny).  
3. **Usuń produkt całkowicie** – usuwa wszystkie sztuki produktu o podanej nazwie.  
4. **Sprawdź stan produktu** – pokazuje, ile sztuk danego produktu jest dostępnych.  
0. **Powrót** – wraca do menu głównego.

---

### Tryb Użytkownika

W trybie użytkownika nie trzeba podawać żadnego hasła.  
Po zalogowaniu się jako użytkownik możliwe są następujące opcje:

1. **Pokaż katalog** – jak wyżej, przeglądanie dostępnych produktów.  
2. **Dodaj do koszyka** – przenosi określoną liczbę sztuk produktu z magazynu do koszyka.  
3. **Zwróć do magazynu** – oddaje produkty z koszyka z powrotem do magazynu.  
4. **Pokaż koszyk** – pokazuje zawartość koszyka i łączną wartość.  
5. **Wystaw paragon** – tworzy paragon na podstawie koszyka, z opcją zapisu do pliku.  
0. **Powrót** – wraca do menu głównego.

---

## Wyjaśnienie programu

### Struktura klas

Główne klasy programu:

- **Program** – klasa główna `main`, zawiera interfejs menu, przełączanie trybów i interakcje.  
- **Product** – zawiera główne informacje o produkcie. Posiada klasy dziedziczące:
  - **FoodProduct**
  - **DrinkProduct**
  - **ToyProduct**
- **WareHouse** – magazyn produktów; umożliwia dodawanie, usuwanie, katalogowanie, sprawdzanie stanu.  
- **Cart** – koszyk użytkownika; dodawanie/zwracanie produktów, generowanie paragonu.

---

### Szczegóły klas

#### 1. Klasa **Product**

Bazowa klasa dla każdego produktu. Zawiera:

- `Nazwa`
- `Cena`
- `Marka` (opcjonalna)
- `Opis` (opcjonalny)

Klasa produkt zawiera również:

- `DisplayInfo()` - funkcja wyświetlająca informacje o produkcie

#### 2. Klasy dziedziczące `Product`

- **ToyProduct** – zawiera klasyfikację wiekową i informację o potencjalnym zagrożeniu.  
- **DrinkProduct** – zawiera informacje o gazowaniu i wersji zero.  
- **FoodProduct** – zawiera informacje o tym, czy produkt jest organiczny lub wegański.

Każda klasa dziedzicząca implementuje własną wersję metody **DisplayInfo()**, która wyświetla dodatkowe informacje.

#### 3. Klasa **WareHouse**

Zarządza wszystkimi produktami. Metody:

- `AddProduct(Product, int)` – dodaje określoną liczbę produktów  
- `RemoveProduct(string)` – usuwa wszystkie sztuki o podanej nazwie  
- `Stock(string)` – zwraca liczbę sztuk produktu o danej nazwie  
- `Catalogue()` – wypisuje wszystkie produkty podzielone na kategorie
- `RemoveSingleProduct(Product product)` - usuwa jedną konkretną instancję produktu z magazynu

#### 4. Klasa **Cart**

Zawiera produkty dodane przez użytkownika. Metody:

- `AddProductsFromWareHouse(WareHouse, string, int)` – przenosi produkty z magazynu do koszyka  
- `ReturnProductsToWareHouse(WareHouse, string, int)` – zwraca produkty do magazynu  
- `ShowCart()` – wypisuje zawartość koszyka  
- `Receipt()` – generuje i ewentualnie zapisuje paragon

---

### 5. Klasa **Program**

Stanowi punkt wejścia do aplikacji oraz główny kontroler logiki interakcji z użytkownikiem.

#### Główne zadania klasy `Program`:

- Wyświetlanie menu głównego oraz obsługa wyboru trybu: Administrator lub Użytkownik  
- Obsługa menu kontekstowych w zależności od roli:
  - menu administratora umożliwia modyfikację magazynu
  - menu użytkownika umożliwia zakupy i obsługę koszyka
- Zarządzanie przepływem programu poprzez pętle `while` i konstrukcje `switch`

#### Struktura główna:

- Inicjalizuje główne obiekty:
  - `WareHouse` – centralne repozytorium produktów
  - `Cart` – tymczasowy koszyk użytkownika

#### Funkcje:

- `AdminMenu(WareHouse)` – logika interfejsu administratora  
- `UserMenu(WareHouse, Cart)` – logika interfejsu użytkownika 
- `AddProductInteractive(WareHouse magazyn)` - dodaje produkty do magazyna z trybu administratora
- `Main()` – funkcja główna: wywołuje menu, obsługuje przełączanie między trybami i zakończenie programu

---

## Obsługa błędów

Program waliduje dane wejściowe:

- nie pozwala dodać produktu z pustą nazwą  
- cena musi być większa niż `0.01` i mieć maksymalnie 2 miejsca po przecinku  
- ilości muszą być dodatnie
