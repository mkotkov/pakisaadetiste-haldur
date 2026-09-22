# Pakisaadetiste haldur

WPF-i töölauarakendus pakisaadetiste haldamiseks. Rakendus võimaldab kasutajal lisada, muuta ja kustutada saadetisi ning arvutab saadetise hinna vastavalt valitud parameetritele.

## Projekti eesmärk

Projekti eesmärk on luua lihtne pakisaadetiste haldamise rakendus, kus on kasutatud C#-i, WPF-i ning eraldatud äriloogikat ja kasutajaliidest.

Rakendus võimaldab:

* lisada uusi saadetisi;
* muuta olemasolevaid saadetisi;
* kustutada saadetisi;
* valideerida kasutaja sisestatud andmeid;
* arvutada saadetise hinna;
* kuvada saadetiste nimekirja;
* jälgida saadetise staatust, mis muutub automaatselt iga 10 sekundi järel.

## Kasutatud tehnoloogiad

* **C#**
* **.NET**
* **WPF (Windows Presentation Foundation)**
* **XAML**
* **Git / GitHub**

## Funktsionaalsus

### Saadetise lisamine

Kasutaja saab määrata:

* saadetise liigi;
* kaalu;
* linna;
* tarneviisi;
* aadressi.

Kõigi vajalike andmete sisestamisel arvutatakse saadetise hind automaatselt.

### Saadetise muutmine

ListView's oleva saadetise valimisel täidetakse vorm olemasolevate andmetega. Kasutaja saab andmeid muuta ja salvestada muudatused.

### Saadetise kustutamine

Valitud saadetise saab kustutada. Enne kustutamist küsitakse kasutajalt kinnitust.

### Sisendite valideerimine

Rakendus kontrollib kasutaja sisestatud andmeid ja kuvab vigade korral arusaadava veateate.

Näiteks:

* kaal peab olema suurem kui 0;
* kaal ei tohi ületada 100 kg;
* aadress peab olema sisestatud;
* aadressi pikkus peab jääma lubatud piiridesse.

### Hinno arvestamine

Saadetise hind arvutatakse eraldi staatilises klassis `HinnaKalkulaator`.

Hinna arvutamisel võetakse arvesse:

* linna baashinda;
* saadetise kaalu;
* saadetise liigi hinnakordajat;
* tarneviisi lisatasu.

### Saadetise staatus

Igal saadetisel on staatus, mis on määratud `enum`-tüübiga.

Staatused liiguvad automaatselt järgmises järjekorras:

```text
Registreeritud
        ↓
Töötlemisel
        ↓
Teel
        ↓
Kohale toimetatud
```

Staatus muutub iga 10 sekundi järel.

## Arhitektuur

Projektis on kasutajaliidese ja äriloogika vastutused eraldatud.

**`pakisaadetiste_haldur.core`**

Sisaldab rakenduse põhiandmeid, enum-tüüpe, valideerimist ja hinna arvutamise loogikat.

**`pakisaadetiste_haldur.wpf`**

Sisaldab WPF-i kasutajaliidest ja kasutaja tegevuste käsitlemist.

Näiteks `HinnaKalkulaator` ei sõltu WPF-ist ning vastutab ainult hinna arvutamise eest.

## Ressursifailid

Kasutajale nähtavad tekstid ja veateated on salvestatud `Resources.resx` faili.

See võimaldab vältida kasutajaliidese koodi sisse kirjutatud tekstide liigset kordamist ning muudab tekstide haldamise lihtsamaks.

## Nõuete täitmine

| Nõue                                       | Teostus                                                  |
| ------------------------------------------ | -------------------------------------------------------- |
| Kirjete kuvamine DataGridis või ListView's | `ListView`                                               |
| Kirje lisamine                             | Jah                                                      |
| Kirje muutmine                             | Jah                                                      |
| Kirje kustutamine                          | Jah                                                      |
| Sisendite kontroll                         | `SisendiValideerija`                                     |
| Enum-tüüp                                  | `SaadetiseLiik`, `Linn`, `Tarneviis`, `SaadetiseStaatus` |
| If/switch-laused                           | Kasutatud valideerimisel ja staatuse muutmisel           |
| Eraldi staatiline arvutusklass             | `HinnaKalkulaator`                                       |
| Ressursifailid                             | `Resources.resx`                                         |
| Programm kompileerub ja käivitub           | Jah                                                      |

## Käivitamine

1. Klooni repositoorium.
2. Ava lahendus Visual Studios.
3. Taasta NuGet paketid, kui neid kasutatakse.
4. Käivita projekt `pakisaadetiste_haldur.wpf`.

## Autor

**Maksym Kotkov**

Projekt on loodud õppetöö raames.