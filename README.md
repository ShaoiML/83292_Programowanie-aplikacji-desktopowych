# 83292_Programowanie-aplikacji-desktopowych
TREŚCI ZADAŃ

ZADANIE_1
Zbudować aplikację z oknem, w którym rozmieszczone będą trzy główne elementy:
-górny poziomy panel z elementami (mogą być kolorowe puste bordery z nadaną szerokością). Panel ma być albo przewijany poziomo, albo zawijany i przewijany pionowo
-lewy pionowy panel z elementami (jak wyżej) przewijany pionowo
-pusty border lub scrollviewer
Pomiędzy elementami ma być przerwa 5-10px


ZADANIE_2
Zdefiniować okno aplikacji, składające się z umieszczonych na siatce:
●	górnej belki, zawierające przyciski
●	sub-siatki, wyróżniającej
○	lewą belkę
○	dolną belkę
○	przestrzeń na zawartość (mogą być puste komórki siatki
W aplikacji należy zdefiniować wzorzec przycisków, w którym należy zagwarantować zmianę koloru obramowania i tła (na wybrane przez nas inne niż domyślne) przy najechaniu oraz kliknięciu. Wszystkie przyciski powinny mieć domyślnie obramowanie na 4-6px oraz margines na 2-4px.
W sub-siatce zdefiniować styl przycisków znajdującej się w niej - wystarczy zmienić kolor tła przycisku.
W aplikacji zdefiniować alternatywny styl przycisków (jeszcze inny kolor) identyfikowany po kluczu. W oknie w każdej belce umieścić co najmniej jeden taki przycisk.


ZADANIE_3
Napisać program z oknem zawierającym dwa pola tekstowe, w których użytkownik może wpisać imię i nazwisko oraz datę urodzin. Pole te mają być powiązane z właściwościami imięNazwisko oraz dataUrodzin. Ponadto poniżej mamy mieć bloki tekstowe wyświetlające osobno: pierwsze imię, nazwisko oraz wiek osoby.

Seter imięNazwisko ma rozbijać wprowadzony napis względem spacji i zapisać pierwszy ostatni wyraz jako nazwisko a pierwszy jako pierwszeImię (lub razem z resztą w tablicy imiona). Ustawienie tych właściwości powinno aktualizować pola tekstowe (implementujemy interfejs INotifyPropertyChanged) z nimi powiązane.

Seter dataUrodzin ma obliczać i aktualizować wiek. Seter wieku powinien aktualizować pole tekstowe z nim powiązane. Właściwość daty urodzin może być zrobiona jako napis lub data - w drugim wypadku implementujemy interfejs IValueConverter.


ZADANIE_4
Napisać kalkulator z działaniami dodawania, mnożenia, odejmowania, dzielenia, odpowiadającymi operacjami z procentem, działaniami pierwiastkowania, odwracania liczby, potęgowania. Kalkulator powinien mieć bufor skonstruowany tak, by ponowne wykonanie operacji bez nowych danych używało poprzednio wprowadzanej liczby jako prawego (lub jedynego w wypadku np pierwiastkowania) argumentu, a ostatniego wyniku jako lewego. Ponadto resetowanie powinno wyzerować wszystkie bufory.
