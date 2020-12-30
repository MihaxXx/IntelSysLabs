(defrule Салат-Обычное_меню
(declare(salience 40))
(element(param Салат) (confidence ?c0))
=>
(assert(element(param Обычное_меню) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Салат (" ?c0 ")" " -> Обычное_меню (" ?c0 ")")))
)

(defrule Кофе,_чай-Десерт-Обычное_меню
(declare(salience 40))
(element(param Кофе,_чай) (confidence ?c0))
(element(param Десерт) (confidence ?c1))
=>
(assert(element(param Обычное_меню) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Кофе,_чай (" ?c0 ")" " + " "Десерт (" ?c1 ")" " -> Обычное_меню (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Первое_блюдо-Обычное_меню
(declare(salience 40))
(element(param Первое_блюдо) (confidence ?c0))
=>
(assert(element(param Обычное_меню) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Первое_блюдо (" ?c0 ")" " -> Обычное_меню (" ?c0 ")")))
)

(defrule Второе_блюдо-Обычное_меню
(declare(salience 40))
(element(param Второе_блюдо) (confidence ?c0))
=>
(assert(element(param Обычное_меню) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Второе_блюдо (" ?c0 ")" " -> Обычное_меню (" ?c0 ")")))
)

(defrule Подходит_для_всей_семьи-Уютный_интерьер-Домашняя_атмосфера
(declare(salience 40))
(element(param Подходит_для_всей_семьи) (confidence ?c0))
(element(param Уютный_интерьер) (confidence ?c1))
=>
(assert(element(param Домашняя_атмосфера) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Подходит_для_всей_семьи (" ?c0 ")" " + " "Уютный_интерьер (" ?c1 ")" " -> Домашняя_атмосфера (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Детская_площадка-Подходит_для_всей_семьи-Пригодно_для_детей
(declare(salience 40))
(element(param Детская_площадка) (confidence ?c0))
(element(param Подходит_для_всей_семьи) (confidence ?c1))
=>
(assert(element(param Пригодно_для_детей) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Детская_площадка (" ?c0 ")" " + " "Подходит_для_всей_семьи (" ?c1 ")" " -> Пригодно_для_детей (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Горки,_качели-Доска_для_рисования-Детская_площадка
(declare(salience 40))
(element(param Горки,_качели) (confidence ?c0))
(element(param Доска_для_рисования) (confidence ?c1))
=>
(assert(element(param Детская_площадка) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Горки,_качели (" ?c0 ")" " + " "Доска_для_рисования (" ?c1 ")" " -> Детская_площадка (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Светлые_тона_в_итерьере-Наличие_декоративных_растений-Уютный_интерьер
(declare(salience 40))
(element(param Светлые_тона_в_итерьере) (confidence ?c0))
(element(param Наличие_декоративных_растений) (confidence ?c1))
=>
(assert(element(param Уютный_интерьер) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Светлые_тона_в_итерьере (" ?c0 ")" " + " "Наличие_декоративных_растений (" ?c1 ")" " -> Уютный_интерьер (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Пригодно_для_детей-Домашняя_атмосфера-Обычное_меню-Детское_кафе
(declare(salience 40))
(element(param Пригодно_для_детей) (confidence ?c0))
(element(param Домашняя_атмосфера) (confidence ?c1))
(element(param Обычное_меню) (confidence ?c2))
=>
(assert(element(param Детское_кафе) (confidence (/ (+ ?c0 ?c1 ?c2) 3)) ))
(assert(appendmessagehalt (str-cat "Пригодно_для_детей (" ?c0 ")" " + " "Домашняя_атмосфера (" ?c1 ")" " + " "Обычное_меню (" ?c2 ")" " -> Детское_кафе (" (/ (+ ?c0 ?c1 ?c2) 3) ")")))
)

(defrule Пригодно_для_детей-Интернет-Обычное_меню-Детское_кафе
(declare(salience 40))
(element(param Пригодно_для_детей) (confidence ?c0))
(element(param Интернет) (confidence ?c1))
(element(param Обычное_меню) (confidence ?c2))
=>
(assert(element(param Детское_кафе) (confidence (/ (+ ?c0 ?c1 ?c2) 3)) ))
(assert(appendmessagehalt (str-cat "Пригодно_для_детей (" ?c0 ")" " + " "Интернет (" ?c1 ")" " + " "Обычное_меню (" ?c2 ")" " -> Детское_кафе (" (/ (+ ?c0 ?c1 ?c2) 3) ")")))
)

(defrule Громкая_музыка-Алкоголь-Клубный_интерьер-Клуб
(declare(salience 40))
(element(param Громкая_музыка) (confidence ?c0))
(element(param Алкоголь) (confidence ?c1))
(element(param Клубный_интерьер) (confidence ?c2))
=>
(assert(element(param Клуб) (confidence (/ (+ ?c0 ?c1 ?c2) 3)) ))
(assert(appendmessagehalt (str-cat "Громкая_музыка (" ?c0 ")" " + " "Алкоголь (" ?c1 ")" " + " "Клубный_интерьер (" ?c2 ")" " -> Клуб (" (/ (+ ?c0 ?c1 ?c2) 3) ")")))
)

(defrule Громкая_музыка-Кальян-Клубный_интерьер-Клуб
(declare(salience 40))
(element(param Громкая_музыка) (confidence ?c0))
(element(param Кальян) (confidence ?c1))
(element(param Клубный_интерьер) (confidence ?c2))
=>
(assert(element(param Клуб) (confidence (/ (+ ?c0 ?c1 ?c2) 3)) ))
(assert(appendmessagehalt (str-cat "Громкая_музыка (" ?c0 ")" " + " "Кальян (" ?c1 ")" " + " "Клубный_интерьер (" ?c2 ")" " -> Клуб (" (/ (+ ?c0 ?c1 ?c2) 3) ")")))
)

(defrule Громкая_музыка-Интернет-Клубный_интерьер-Клуб
(declare(salience 40))
(element(param Громкая_музыка) (confidence ?c0))
(element(param Интернет) (confidence ?c1))
(element(param Клубный_интерьер) (confidence ?c2))
=>
(assert(element(param Клуб) (confidence (/ (+ ?c0 ?c1 ?c2) 3)) ))
(assert(appendmessagehalt (str-cat "Громкая_музыка (" ?c0 ")" " + " "Интернет (" ?c1 ")" " + " "Клубный_интерьер (" ?c2 ")" " -> Клуб (" (/ (+ ?c0 ?c1 ?c2) 3) ")")))
)

(defrule Электронная_музыка-Шумная_атмосфера-Громкая_музыка
(declare(salience 40))
(element(param Электронная_музыка) (confidence ?c0))
(element(param Шумная_атмосфера) (confidence ?c1))
=>
(assert(element(param Громкая_музыка) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Электронная_музыка (" ?c0 ")" " + " "Шумная_атмосфера (" ?c1 ")" " -> Громкая_музыка (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Музыка-Танцплощадка-Электронная_музыка
(declare(salience 40))
(element(param Музыка) (confidence ?c0))
(element(param Танцплощадка) (confidence ?c1))
=>
(assert(element(param Электронная_музыка) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Музыка (" ?c0 ")" " + " "Танцплощадка (" ?c1 ")" " -> Электронная_музыка (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Наличие_медиатехники-Транслирует_звук-Музыка
(declare(salience 40))
(element(param Наличие_медиатехники) (confidence ?c0))
(element(param Транслирует_звук) (confidence ?c1))
=>
(assert(element(param Музыка) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Наличие_медиатехники (" ?c0 ")" " + " "Транслирует_звук (" ?c1 ")" " -> Музыка (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Темные_тона_в_итерьере-Разноцветное_искусственное_освещение-Клубный_интерьер
(declare(salience 40))
(element(param Темные_тона_в_итерьере) (confidence ?c0))
(element(param Разноцветное_искусственное_освещение) (confidence ?c1))
=>
(assert(element(param Клубный_интерьер) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Темные_тона_в_итерьере (" ?c0 ")" " + " "Разноцветное_искусственное_освещение (" ?c1 ")" " -> Клубный_интерьер (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Шумный_отдых-Трансляция_матчей-Пиво-Спортбар
(declare(salience 40))
(element(param Шумный_отдых) (confidence ?c0))
(element(param Трансляция_матчей) (confidence ?c1))
(element(param Пиво) (confidence ?c2))
=>
(assert(element(param Спортбар) (confidence (/ (+ ?c0 ?c1 ?c2) 3)) ))
(assert(appendmessagehalt (str-cat "Шумный_отдых (" ?c0 ")" " + " "Трансляция_матчей (" ?c1 ")" " + " "Пиво (" ?c2 ")" " -> Спортбар (" (/ (+ ?c0 ?c1 ?c2) 3) ")")))
)

(defrule Шумный_отдых-Трансляция_матчей-Закуски-Спортбар
(declare(salience 40))
(element(param Шумный_отдых) (confidence ?c0))
(element(param Трансляция_матчей) (confidence ?c1))
(element(param Закуски) (confidence ?c2))
=>
(assert(element(param Спортбар) (confidence (/ (+ ?c0 ?c1 ?c2) 3)) ))
(assert(appendmessagehalt (str-cat "Шумный_отдых (" ?c0 ")" " + " "Трансляция_матчей (" ?c1 ")" " + " "Закуски (" ?c2 ")" " -> Спортбар (" (/ (+ ?c0 ?c1 ?c2) 3) ")")))
)

(defrule Спортивная_тематика-Алкоголь-Пиво
(declare(salience 40))
(element(param Спортивная_тематика) (confidence ?c0))
(element(param Алкоголь) (confidence ?c1))
=>
(assert(element(param Пиво) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Спортивная_тематика (" ?c0 ")" " + " "Алкоголь (" ?c1 ")" " -> Пиво (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Шумная_атмосфера-Любите_кричать-Шумный_отдых
(declare(salience 40))
(element(param Шумная_атмосфера) (confidence ?c0))
(element(param Любите_кричать) (confidence ?c1))
=>
(assert(element(param Шумный_отдых) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Шумная_атмосфера (" ?c0 ")" " + " "Любите_кричать (" ?c1 ")" " -> Шумный_отдых (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Наличие_телевизора-Спортивная_тематика-Трансляция_матчей
(declare(salience 40))
(element(param Наличие_телевизора) (confidence ?c0))
(element(param Спортивная_тематика) (confidence ?c1))
=>
(assert(element(param Трансляция_матчей) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Наличие_телевизора (" ?c0 ")" " + " "Спортивная_тематика (" ?c1 ")" " -> Трансляция_матчей (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Наличие_медиатехники-Транслирует_звук-Транслирует_видео-Наличие_телевизора
(declare(salience 40))
(element(param Наличие_медиатехники) (confidence ?c0))
(element(param Транслирует_звук) (confidence ?c1))
(element(param Транслирует_видео) (confidence ?c2))
=>
(assert(element(param Наличие_телевизора) (confidence (/ (+ ?c0 ?c1 ?c2) 3)) ))
(assert(appendmessagehalt (str-cat "Наличие_медиатехники (" ?c0 ")" " + " "Транслирует_звук (" ?c1 ")" " + " "Транслирует_видео (" ?c2 ")" " -> Наличие_телевизора (" (/ (+ ?c0 ?c1 ?c2) 3) ")")))
)

(defrule Обычные_официанты-Уютный_интерьер-Особое_меню-Кафе
(declare(salience 40))
(element(param Обычные_официанты) (confidence ?c0))
(element(param Уютный_интерьер) (confidence ?c1))
(element(param Особое_меню) (confidence ?c2))
=>
(assert(element(param Кафе) (confidence (/ (+ ?c0 ?c1 ?c2) 3)) ))
(assert(appendmessagehalt (str-cat "Обычные_официанты (" ?c0 ")" " + " "Уютный_интерьер (" ?c1 ")" " + " "Особое_меню (" ?c2 ")" " -> Кафе (" (/ (+ ?c0 ?c1 ?c2) 3) ")")))
)

(defrule Обычные_официанты-Уютный_интерьер-Обычное_меню-Кафе
(declare(salience 40))
(element(param Обычные_официанты) (confidence ?c0))
(element(param Уютный_интерьер) (confidence ?c1))
(element(param Обычное_меню) (confidence ?c2))
=>
(assert(element(param Кафе) (confidence (/ (+ ?c0 ?c1 ?c2) 3)) ))
(assert(appendmessagehalt (str-cat "Обычные_официанты (" ?c0 ")" " + " "Уютный_интерьер (" ?c1 ")" " + " "Обычное_меню (" ?c2 ")" " -> Кафе (" (/ (+ ?c0 ?c1 ?c2) 3) ")")))
)

(defrule Средняя_ценовая_категория-Наличие_официантов-Обычные_официанты
(declare(salience 40))
(element(param Средняя_ценовая_категория) (confidence ?c0))
(element(param Наличие_официантов) (confidence ?c1))
=>
(assert(element(param Обычные_официанты) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Средняя_ценовая_категория (" ?c0 ")" " + " "Наличие_официантов (" ?c1 ")" " -> Обычные_официанты (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Светлые_тона_в_итерьере-Удобные_диваны_/кресла_/стулья-Уютный_интерьер
(declare(salience 40))
(element(param Светлые_тона_в_итерьере) (confidence ?c0))
(element(param Удобные_диваны_/кресла_/стулья) (confidence ?c1))
=>
(assert(element(param Уютный_интерьер) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Светлые_тона_в_итерьере (" ?c0 ")" " + " "Удобные_диваны_/кресла_/стулья (" ?c1 ")" " -> Уютный_интерьер (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Веганское_меню-Особое_меню
(declare(salience 40))
(element(param Веганское_меню) (confidence ?c0))
=>
(assert(element(param Особое_меню) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Веганское_меню (" ?c0 ")" " -> Особое_меню (" ?c0 ")")))
)

(defrule Вегетарианское_меню-Особое_меню
(declare(salience 40))
(element(param Вегетарианское_меню) (confidence ?c0))
=>
(assert(element(param Особое_меню) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Вегетарианское_меню (" ?c0 ")" " -> Особое_меню (" ?c0 ")")))
)

(defrule Не_ем_мясо_и_птицу-Вегетарианское_меню
(declare(salience 40))
(element(param Не_ем_мясо_и_птицу) (confidence ?c0))
=>
(assert(element(param Вегетарианское_меню) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Не_ем_мясо_и_птицу (" ?c0 ")" " -> Вегетарианское_меню (" ?c0 ")")))
)

(defrule Не_ем_продукты_животного_происхождения-Не_ем_мясо_и_птицу-Веганское_меню
(declare(salience 40))
(element(param Не_ем_продукты_животного_происхождения) (confidence ?c0))
(element(param Не_ем_мясо_и_птицу) (confidence ?c1))
=>
(assert(element(param Веганское_меню) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Не_ем_продукты_животного_происхождения (" ?c0 ")" " + " "Не_ем_мясо_и_птицу (" ?c1 ")" " -> Веганское_меню (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Музыка-Приглашенные_певцы-Живая_музыка
(declare(salience 40))
(element(param Музыка) (confidence ?c0))
(element(param Приглашенные_певцы) (confidence ?c1))
=>
(assert(element(param Живая_музыка) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Музыка (" ?c0 ")" " + " "Приглашенные_певцы (" ?c1 ")" " -> Живая_музыка (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Оркестр-Живая_музыка
(declare(salience 40))
(element(param Оркестр) (confidence ?c0))
=>
(assert(element(param Живая_музыка) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Оркестр (" ?c0 ")" " -> Живая_музыка (" ?c0 ")")))
)

(defrule Профессиональные_повара-Качественные_натуральные_продукты-Блюда_от_шеф-повара
(declare(salience 40))
(element(param Профессиональные_повара) (confidence ?c0))
(element(param Качественные_натуральные_продукты) (confidence ?c1))
=>
(assert(element(param Блюда_от_шеф-повара) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Профессиональные_повара (" ?c0 ")" " + " "Качественные_натуральные_продукты (" ?c1 ")" " -> Блюда_от_шеф-повара (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Высокая_ценовая_категория-Наличие_официантов-Официанты_высокого_уровня
(declare(salience 40))
(element(param Высокая_ценовая_категория) (confidence ?c0))
(element(param Наличие_официантов) (confidence ?c1))
=>
(assert(element(param Официанты_высокого_уровня) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Высокая_ценовая_категория (" ?c0 ")" " + " "Наличие_официантов (" ?c1 ")" " -> Официанты_высокого_уровня (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Живая_музыка-Обычное_меню-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара) (confidence ?c0))
(element(param Официанты_высокого_уровня) (confidence ?c1))
(element(param Живая_музыка) (confidence ?c2))
(element(param Обычное_меню) (confidence ?c3))
=>
(assert(element(param Ресторан) (confidence (/ (+ ?c0 ?c1 ?c2 ?c3) 4)) ))
(assert(appendmessagehalt (str-cat "Блюда_от_шеф-повара (" ?c0 ")" " + " "Официанты_высокого_уровня (" ?c1 ")" " + " "Живая_музыка (" ?c2 ")" " + " "Обычное_меню (" ?c3 ")" " -> Ресторан (" (/ (+ ?c0 ?c1 ?c2 ?c3) 4) ")")))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Интернет-Обычное_меню-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара) (confidence ?c0))
(element(param Официанты_высокого_уровня) (confidence ?c1))
(element(param Интернет) (confidence ?c2))
(element(param Обычное_меню) (confidence ?c3))
=>
(assert(element(param Ресторан) (confidence (/ (+ ?c0 ?c1 ?c2 ?c3) 4)) ))
(assert(appendmessagehalt (str-cat "Блюда_от_шеф-повара (" ?c0 ")" " + " "Официанты_высокого_уровня (" ?c1 ")" " + " "Интернет (" ?c2 ")" " + " "Обычное_меню (" ?c3 ")" " -> Ресторан (" (/ (+ ?c0 ?c1 ?c2 ?c3) 4) ")")))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Живая_музыка-Особое_меню-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара) (confidence ?c0))
(element(param Официанты_высокого_уровня) (confidence ?c1))
(element(param Живая_музыка) (confidence ?c2))
(element(param Особое_меню) (confidence ?c3))
=>
(assert(element(param Ресторан) (confidence (/ (+ ?c0 ?c1 ?c2 ?c3) 4)) ))
(assert(appendmessagehalt (str-cat "Блюда_от_шеф-повара (" ?c0 ")" " + " "Официанты_высокого_уровня (" ?c1 ")" " + " "Живая_музыка (" ?c2 ")" " + " "Особое_меню (" ?c3 ")" " -> Ресторан (" (/ (+ ?c0 ?c1 ?c2 ?c3) 4) ")")))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Интернет-Особое_меню-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара) (confidence ?c0))
(element(param Официанты_высокого_уровня) (confidence ?c1))
(element(param Интернет) (confidence ?c2))
(element(param Особое_меню) (confidence ?c3))
=>
(assert(element(param Ресторан) (confidence (/ (+ ?c0 ?c1 ?c2 ?c3) 4)) ))
(assert(appendmessagehalt (str-cat "Блюда_от_шеф-повара (" ?c0 ")" " + " "Официанты_высокого_уровня (" ?c1 ")" " + " "Интернет (" ?c2 ")" " + " "Особое_меню (" ?c3 ")" " -> Ресторан (" (/ (+ ?c0 ?c1 ?c2 ?c3) 4) ")")))
)

(defrule Обычное_меню-Самообслуживание-Простенький_интерьер-Столовая
(declare(salience 40))
(element(param Обычное_меню) (confidence ?c0))
(element(param Самообслуживание) (confidence ?c1))
(element(param Простенький_интерьер) (confidence ?c2))
=>
(assert(element(param Столовая) (confidence (/ (+ ?c0 ?c1 ?c2) 3)) ))
(assert(appendmessagehalt (str-cat "Обычное_меню (" ?c0 ")" " + " "Самообслуживание (" ?c1 ")" " + " "Простенький_интерьер (" ?c2 ")" " -> Столовая (" (/ (+ ?c0 ?c1 ?c2) 3) ")")))
)

(defrule Нет_официантов-Самообслуживание
(declare(salience 40))
(element(param Нет_официантов) (confidence ?c0))
=>
(assert(element(param Самообслуживание) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Нет_официантов (" ?c0 ")" " -> Самообслуживание (" ?c0 ")")))
)

(defrule Низкая_ценовая_категория-Только_столы_и_стулья
(declare(salience 40))
(element(param Низкая_ценовая_категория) (confidence ?c0))
=>
(assert(element(param Только_столы_и_стулья) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Низкая_ценовая_категория (" ?c0 ")" " -> Только_столы_и_стулья (" ?c0 ")")))
)

(defrule Только_столы_и_стулья-Простенький_интерьер
(declare(salience 40))
(element(param Только_столы_и_стулья) (confidence ?c0))
=>
(assert(element(param Простенький_интерьер) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Только_столы_и_стулья (" ?c0 ")" " -> Простенький_интерьер (" ?c0 ")")))
)

(defrule Есть_коты-Возможность_поиграть_с_животными
(declare(salience 40))
(element(param Есть_коты) (confidence ?c0))
=>
(assert(element(param Возможность_поиграть_с_животными) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Есть_коты (" ?c0 ")" " -> Возможность_поиграть_с_животными (" ?c0 ")")))
)

(defrule Есть_попугайчики-Возможность_поиграть_с_животными
(declare(salience 40))
(element(param Есть_попугайчики) (confidence ?c0))
=>
(assert(element(param Возможность_поиграть_с_животными) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Есть_попугайчики (" ?c0 ")" " -> Возможность_поиграть_с_животными (" ?c0 ")")))
)

(defrule Есть_собаки-Возможность_поиграть_с_животными
(declare(salience 40))
(element(param Есть_собаки) (confidence ?c0))
=>
(assert(element(param Возможность_поиграть_с_животными) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Есть_собаки (" ?c0 ")" " -> Возможность_поиграть_с_животными (" ?c0 ")")))
)

(defrule WiFi-Интернет
(declare(salience 40))
(element(param WiFi) (confidence ?c0))
=>
(assert(element(param Интернет) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "WiFi (" ?c0 ")" " -> Интернет (" ?c0 ")")))
)

(defrule Настольные_игры-Интернет-Наличие_развлечений
(declare(salience 40))
(element(param Настольные_игры) (confidence ?c0))
(element(param Интернет) (confidence ?c1))
=>
(assert(element(param Наличие_развлечений) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Настольные_игры (" ?c0 ")" " + " "Интернет (" ?c1 ")" " -> Наличие_развлечений (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Наличие_развлечений-Кальян-Кофе,_чай-Антикафе
(declare(salience 40))
(element(param Наличие_развлечений) (confidence ?c0))
(element(param Кальян) (confidence ?c1))
(element(param Кофе,_чай) (confidence ?c2))
=>
(assert(element(param Антикафе) (confidence (/ (+ ?c0 ?c1 ?c2) 3)) ))
(assert(appendmessagehalt (str-cat "Наличие_развлечений (" ?c0 ")" " + " "Кальян (" ?c1 ")" " + " "Кофе,_чай (" ?c2 ")" " -> Антикафе (" (/ (+ ?c0 ?c1 ?c2) 3) ")")))
)

(defrule Наличие_развлечений-Возможность_поиграть_с_животными-Кофе,_чай-Антикафе
(declare(salience 40))
(element(param Наличие_развлечений) (confidence ?c0))
(element(param Возможность_поиграть_с_животными) (confidence ?c1))
(element(param Кофе,_чай) (confidence ?c2))
=>
(assert(element(param Антикафе) (confidence (/ (+ ?c0 ?c1 ?c2) 3)) ))
(assert(appendmessagehalt (str-cat "Наличие_развлечений (" ?c0 ")" " + " "Возможность_поиграть_с_животными (" ?c1 ")" " + " "Кофе,_чай (" ?c2 ")" " -> Антикафе (" (/ (+ ?c0 ?c1 ?c2) 3) ")")))
)

(defrule Наличие_развлечений-Кальян-Десерт-Антикафе
(declare(salience 40))
(element(param Наличие_развлечений) (confidence ?c0))
(element(param Кальян) (confidence ?c1))
(element(param Десерт) (confidence ?c2))
=>
(assert(element(param Антикафе) (confidence (/ (+ ?c0 ?c1 ?c2) 3)) ))
(assert(appendmessagehalt (str-cat "Наличие_развлечений (" ?c0 ")" " + " "Кальян (" ?c1 ")" " + " "Десерт (" ?c2 ")" " -> Антикафе (" (/ (+ ?c0 ?c1 ?c2) 3) ")")))
)

(defrule Наличие_развлечений-Возможность_поиграть_с_животными-Десерт-Антикафе
(declare(salience 40))
(element(param Наличие_развлечений) (confidence ?c0))
(element(param Возможность_поиграть_с_животными) (confidence ?c1))
(element(param Десерт) (confidence ?c2))
=>
(assert(element(param Антикафе) (confidence (/ (+ ?c0 ?c1 ?c2) 3)) ))
(assert(appendmessagehalt (str-cat "Наличие_развлечений (" ?c0 ")" " + " "Возможность_поиграть_с_животными (" ?c1 ")" " + " "Десерт (" ?c2 ")" " -> Антикафе (" (/ (+ ?c0 ?c1 ?c2) 3) ")")))
)

(defrule Подают_равиоли-Есть_паста
(declare(salience 40))
(element(param Подают_равиоли) (confidence ?c0))
=>
(assert(element(param Есть_паста) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Подают_равиоли (" ?c0 ")" " -> Есть_паста (" ?c0 ")")))
)

(defrule Подают_феттуччини-Есть_паста
(declare(salience 40))
(element(param Подают_феттуччини) (confidence ?c0))
=>
(assert(element(param Есть_паста) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Подают_феттуччини (" ?c0 ")" " -> Есть_паста (" ?c0 ")")))
)

(defrule Подают_спагетти-Есть_паста
(declare(salience 40))
(element(param Подают_спагетти) (confidence ?c0))
=>
(assert(element(param Есть_паста) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Подают_спагетти (" ?c0 ")" " -> Есть_паста (" ?c0 ")")))
)

(defrule Подают_карбонару-Есть_паста
(declare(salience 40))
(element(param Подают_карбонару) (confidence ?c0))
=>
(assert(element(param Есть_паста) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Подают_карбонару (" ?c0 ")" " -> Есть_паста (" ?c0 ")")))
)

(defrule Есть_паста-Итальянское_кафе-Итальянское_заведение
(declare(salience 40))
(element(param Есть_паста) (confidence ?c0))
(element(param Итальянское_кафе) (confidence ?c1))
=>
(assert(element(param Итальянское_заведение) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Есть_паста (" ?c0 ")" " + " "Итальянское_кафе (" ?c1 ")" " -> Итальянское_заведение (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Итальянское_заведение-Средняя_ценовая_категория-Итальянское_кафе
(declare(salience 40))
(element(param Итальянское_заведение) (confidence ?c0))
(element(param Средняя_ценовая_категория) (confidence ?c1))
=>
(assert(element(param Итальянское_кафе) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Итальянское_заведение (" ?c0 ")" " + " "Средняя_ценовая_категория (" ?c1 ")" " -> Итальянское_кафе (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Итальянское_заведение-Высокая_ценовая_категория-Ресторан_итальянской_кухни
(declare(salience 40))
(element(param Итальянское_заведение) (confidence ?c0))
(element(param Высокая_ценовая_категория) (confidence ?c1))
=>
(assert(element(param Ресторан_итальянской_кухни) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Итальянское_заведение (" ?c0 ")" " + " "Высокая_ценовая_категория (" ?c1 ")" " -> Ресторан_итальянской_кухни (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Есть_картошка_фри-Вредная_еда
(declare(salience 40))
(element(param Есть_картошка_фри) (confidence ?c0))
=>
(assert(element(param Вредная_еда) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Есть_картошка_фри (" ?c0 ")" " -> Вредная_еда (" ?c0 ")")))
)

(defrule Подают_бургеры-Вредная_еда
(declare(salience 40))
(element(param Подают_бургеры) (confidence ?c0))
=>
(assert(element(param Вредная_еда) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Подают_бургеры (" ?c0 ")" " -> Вредная_еда (" ?c0 ")")))
)

(defrule Есть_сладкие_газированные_напитки-Вредная_еда
(declare(salience 40))
(element(param Есть_сладкие_газированные_напитки) (confidence ?c0))
=>
(assert(element(param Вредная_еда) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Есть_сладкие_газированные_напитки (" ?c0 ")" " -> Вредная_еда (" ?c0 ")")))
)

(defrule Есть_наггетсы-Вредная_еда
(declare(salience 40))
(element(param Есть_наггетсы) (confidence ?c0))
=>
(assert(element(param Вредная_еда) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Есть_наггетсы (" ?c0 ")" " -> Вредная_еда (" ?c0 ")")))
)

(defrule Вредная_еда-Средняя_ценовая_категория-Бургерная
(declare(salience 40))
(element(param Вредная_еда) (confidence ?c0))
(element(param Средняя_ценовая_категория) (confidence ?c1))
=>
(assert(element(param Бургерная) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Вредная_еда (" ?c0 ")" " + " "Средняя_ценовая_категория (" ?c1 ")" " -> Бургерная (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Вредная_еда-Низкая_ценовая_категория-Ресторан_быстрого_питания
(declare(salience 40))
(element(param Вредная_еда) (confidence ?c0))
(element(param Низкая_ценовая_категория) (confidence ?c1))
=>
(assert(element(param Ресторан_быстрого_питания) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Вредная_еда (" ?c0 ")" " + " "Низкая_ценовая_категория (" ?c1 ")" " -> Ресторан_быстрого_питания (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Подают_роллы_Калифорния-Второе_блюдо-Подают_роллы
(declare(salience 40))
(element(param Подают_роллы_Калифорния) (confidence ?c0))
(element(param Второе_блюдо) (confidence ?c1))
=>
(assert(element(param Подают_роллы) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Подают_роллы_Калифорния (" ?c0 ")" " + " "Второе_блюдо (" ?c1 ")" " -> Подают_роллы (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Подают_роллы_Филадельфия-Второе_блюдо-Подают_роллы
(declare(salience 40))
(element(param Подают_роллы_Филадельфия) (confidence ?c0))
(element(param Второе_блюдо) (confidence ?c1))
=>
(assert(element(param Подают_роллы) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Подают_роллы_Филадельфия (" ?c0 ")" " + " "Второе_блюдо (" ?c1 ")" " -> Подают_роллы (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Подают_роллы_Аляска-Второе_блюдо-Подают_роллы
(declare(salience 40))
(element(param Подают_роллы_Аляска) (confidence ?c0))
(element(param Второе_блюдо) (confidence ?c1))
=>
(assert(element(param Подают_роллы) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Подают_роллы_Аляска (" ?c0 ")" " + " "Второе_блюдо (" ?c1 ")" " -> Подают_роллы (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Подают_роллы_Лава-Второе_блюдо-Подают_роллы
(declare(salience 40))
(element(param Подают_роллы_Лава) (confidence ?c0))
(element(param Второе_блюдо) (confidence ?c1))
=>
(assert(element(param Подают_роллы) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Подают_роллы_Лава (" ?c0 ")" " + " "Второе_блюдо (" ?c1 ")" " -> Подают_роллы (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Подают_лапшу_Удон-Второе_блюдо-Есть_классическая_японская_кухня
(declare(salience 40))
(element(param Подают_лапшу_Удон) (confidence ?c0))
(element(param Второе_блюдо) (confidence ?c1))
=>
(assert(element(param Есть_классическая_японская_кухня) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Подают_лапшу_Удон (" ?c0 ")" " + " "Второе_блюдо (" ?c1 ")" " -> Есть_классическая_японская_кухня (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Подают_суп_Мисо-Первое_блюдо-Есть_классическая_японская_кухня
(declare(salience 40))
(element(param Подают_суп_Мисо) (confidence ?c0))
(element(param Первое_блюдо) (confidence ?c1))
=>
(assert(element(param Есть_классическая_японская_кухня) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Подают_суп_Мисо (" ?c0 ")" " + " "Первое_блюдо (" ?c1 ")" " -> Есть_классическая_японская_кухня (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Подают_роллы-Японское_заведение
(declare(salience 40))
(element(param Подают_роллы) (confidence ?c0))
=>
(assert(element(param Японское_заведение) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Подают_роллы (" ?c0 ")" " -> Японское_заведение (" ?c0 ")")))
)

(defrule Есть_классическая_японская_кухня-Японское_заведение
(declare(salience 40))
(element(param Есть_классическая_японская_кухня) (confidence ?c0))
=>
(assert(element(param Японское_заведение) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Есть_классическая_японская_кухня (" ?c0 ")" " -> Японское_заведение (" ?c0 ")")))
)

(defrule Японское_заведение-Средняя_ценовая_категория-Японское_кафе
(declare(salience 40))
(element(param Японское_заведение) (confidence ?c0))
(element(param Средняя_ценовая_категория) (confidence ?c1))
=>
(assert(element(param Японское_кафе) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Японское_заведение (" ?c0 ")" " + " "Средняя_ценовая_категория (" ?c1 ")" " -> Японское_кафе (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Японское_заведение-Высокая_ценовая_категория-Ресторан_японской_кухни
(declare(salience 40))
(element(param Японское_заведение) (confidence ?c0))
(element(param Высокая_ценовая_категория) (confidence ?c1))
=>
(assert(element(param Ресторан_японской_кухни) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Японское_заведение (" ?c0 ")" " + " "Высокая_ценовая_категория (" ?c1 ")" " -> Ресторан_японской_кухни (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Первое_блюдо-Домашная_еда-Русское_заведение
(declare(salience 40))
(element(param Первое_блюдо) (confidence ?c0))
(element(param Домашная_еда) (confidence ?c1))
=>
(assert(element(param Русское_заведение) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Первое_блюдо (" ?c0 ")" " + " "Домашная_еда (" ?c1 ")" " -> Русское_заведение (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Подают_пироги-Домашная_еда-Русское_заведение
(declare(salience 40))
(element(param Подают_пироги) (confidence ?c0))
(element(param Домашная_еда) (confidence ?c1))
=>
(assert(element(param Русское_заведение) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Подают_пироги (" ?c0 ")" " + " "Домашная_еда (" ?c1 ")" " -> Русское_заведение (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Русское_заведение-Средняя_ценовая_категория-Русское_кафе
(declare(salience 40))
(element(param Русское_заведение) (confidence ?c0))
(element(param Средняя_ценовая_категория) (confidence ?c1))
=>
(assert(element(param Русское_кафе) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Русское_заведение (" ?c0 ")" " + " "Средняя_ценовая_категория (" ?c1 ")" " -> Русское_кафе (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Русское_заведение-Высокая_ценовая_категория-Ресторан_русской_кухни
(declare(salience 40))
(element(param Русское_заведение) (confidence ?c0))
(element(param Высокая_ценовая_категория) (confidence ?c1))
=>
(assert(element(param Ресторан_русской_кухни) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Русское_заведение (" ?c0 ")" " + " "Высокая_ценовая_категория (" ?c1 ")" " -> Ресторан_русской_кухни (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Подают_пироги-Есть_осетинские_национальные_блюда-Подают_осетинские_пироги
(declare(salience 40))
(element(param Подают_пироги) (confidence ?c0))
(element(param Есть_осетинские_национальные_блюда) (confidence ?c1))
=>
(assert(element(param Подают_осетинские_пироги) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Подают_пироги (" ?c0 ")" " + " "Есть_осетинские_национальные_блюда (" ?c1 ")" " -> Подают_осетинские_пироги (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Подают_осетинские_пироги-Кавказское_заведение
(declare(salience 40))
(element(param Подают_осетинские_пироги) (confidence ?c0))
=>
(assert(element(param Кавказское_заведение) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Подают_осетинские_пироги (" ?c0 ")" " -> Кавказское_заведение (" ?c0 ")")))
)

(defrule Кавказское_заведение-Средняя_ценовая_категория-Кавказское_кафе
(declare(salience 40))
(element(param Кавказское_заведение) (confidence ?c0))
(element(param Средняя_ценовая_категория) (confidence ?c1))
=>
(assert(element(param Кавказское_кафе) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Кавказское_заведение (" ?c0 ")" " + " "Средняя_ценовая_категория (" ?c1 ")" " -> Кавказское_кафе (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Кавказское_заведение-Высокая_ценовая_категория-Ресторан_кавказской_кухни
(declare(salience 40))
(element(param Кавказское_заведение) (confidence ?c0))
(element(param Высокая_ценовая_категория) (confidence ?c1))
=>
(assert(element(param Ресторан_кавказской_кухни) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Кавказское_заведение (" ?c0 ")" " + " "Высокая_ценовая_категория (" ?c1 ")" " -> Ресторан_кавказской_кухни (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Подают_Маргарита-Подают_пиццу
(declare(salience 40))
(element(param Подают_Маргарита) (confidence ?c0))
=>
(assert(element(param Подают_пиццу) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Подают_Маргарита (" ?c0 ")" " -> Подают_пиццу (" ?c0 ")")))
)

(defrule Подают_4_сыра-Подают_пиццу
(declare(salience 40))
(element(param Подают_4_сыра) (confidence ?c0))
=>
(assert(element(param Подают_пиццу) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Подают_4_сыра (" ?c0 ")" " -> Подают_пиццу (" ?c0 ")")))
)

(defrule Подают_Сицилия-Подают_пиццу
(declare(salience 40))
(element(param Подают_Сицилия) (confidence ?c0))
=>
(assert(element(param Подают_пиццу) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Подают_Сицилия (" ?c0 ")" " -> Подают_пиццу (" ?c0 ")")))
)

(defrule Подают_Кальцоне-Подают_пиццу
(declare(salience 40))
(element(param Подают_Кальцоне) (confidence ?c0))
=>
(assert(element(param Подают_пиццу) (confidence ?c0) ))
(assert(appendmessagehalt (str-cat "Подают_Кальцоне (" ?c0 ")" " -> Подают_пиццу (" ?c0 ")")))
)

(defrule Подают_пиццу-Средняя_ценовая_категория-Пиццерия
(declare(salience 40))
(element(param Подают_пиццу) (confidence ?c0))
(element(param Средняя_ценовая_категория) (confidence ?c1))
=>
(assert(element(param Пиццерия) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Подают_пиццу (" ?c0 ")" " + " "Средняя_ценовая_категория (" ?c1 ")" " -> Пиццерия (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Подают_пиццу-Высокая_ценовая_категория-Ресторан_итальянской_кухни
(declare(salience 40))
(element(param Подают_пиццу) (confidence ?c0))
(element(param Высокая_ценовая_категория) (confidence ?c1))
=>
(assert(element(param Ресторан_итальянской_кухни) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Подают_пиццу (" ?c0 ")" " + " "Высокая_ценовая_категория (" ?c1 ")" " -> Ресторан_итальянской_кухни (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Алкоголь-Музыка-Питейное_заведение
(declare(salience 40))
(element(param Алкоголь) (confidence ?c0))
(element(param Музыка) (confidence ?c1))
=>
(assert(element(param Питейное_заведение) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Алкоголь (" ?c0 ")" " + " "Музыка (" ?c1 ")" " -> Питейное_заведение (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Питейное_заведение-Высокая_ценовая_категория-Паб_с_крафтовым_пивом
(declare(salience 40))
(element(param Питейное_заведение) (confidence ?c0))
(element(param Высокая_ценовая_категория) (confidence ?c1))
=>
(assert(element(param Паб_с_крафтовым_пивом) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Питейное_заведение (" ?c0 ")" " + " "Высокая_ценовая_категория (" ?c1 ")" " -> Паб_с_крафтовым_пивом (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Питейное_заведение-Средняя_ценовая_категория-Бар
(declare(salience 40))
(element(param Питейное_заведение) (confidence ?c0))
(element(param Средняя_ценовая_категория) (confidence ?c1))
=>
(assert(element(param Бар) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Питейное_заведение (" ?c0 ")" " + " "Средняя_ценовая_категория (" ?c1 ")" " -> Бар (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Удобные_диваны_/кресла_/стулья-Питейное_заведение-Сидрерия
(declare(salience 40))
(element(param Удобные_диваны_/кресла_/стулья) (confidence ?c0))
(element(param Питейное_заведение) (confidence ?c1))
=>
(assert(element(param Сидрерия) (confidence (/ (+ ?c0 ?c1) 2)) ))
(assert(appendmessagehalt (str-cat "Удобные_диваны_/кресла_/стулья (" ?c0 ")" " + " "Питейное_заведение (" ?c1 ")" " -> Сидрерия (" (/ (+ ?c0 ?c1) 2) ")")))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Живая_музыка-Обычное_меню-Пригодно_для_детей-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара) (confidence ?c0))
(element(param Официанты_высокого_уровня) (confidence ?c1))
(element(param Живая_музыка) (confidence ?c2))
(element(param Обычное_меню) (confidence ?c3))
(element(param Пригодно_для_детей) (confidence ?c4))
=>
(assert(element(param Ресторан) (confidence (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5)) ))
(assert(appendmessagehalt (str-cat "Блюда_от_шеф-повара (" ?c0 ")" " + " "Официанты_высокого_уровня (" ?c1 ")" " + " "Живая_музыка (" ?c2 ")" " + " "Обычное_меню (" ?c3 ")" " + " "Пригодно_для_детей (" ?c4 ")" " -> Ресторан (" (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5) ")")))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Живая_музыка-Обычное_меню-Природа-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара) (confidence ?c0))
(element(param Официанты_высокого_уровня) (confidence ?c1))
(element(param Живая_музыка) (confidence ?c2))
(element(param Обычное_меню) (confidence ?c3))
(element(param Природа) (confidence ?c4))
=>
(assert(element(param Ресторан) (confidence (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5)) ))
(assert(appendmessagehalt (str-cat "Блюда_от_шеф-повара (" ?c0 ")" " + " "Официанты_высокого_уровня (" ?c1 ")" " + " "Живая_музыка (" ?c2 ")" " + " "Обычное_меню (" ?c3 ")" " + " "Природа (" ?c4 ")" " -> Ресторан (" (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5) ")")))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Интернет-Обычное_меню-Пригодно_для_детей-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара) (confidence ?c0))
(element(param Официанты_высокого_уровня) (confidence ?c1))
(element(param Интернет) (confidence ?c2))
(element(param Обычное_меню) (confidence ?c3))
(element(param Пригодно_для_детей) (confidence ?c4))
=>
(assert(element(param Ресторан) (confidence (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5)) ))
(assert(appendmessagehalt (str-cat "Блюда_от_шеф-повара (" ?c0 ")" " + " "Официанты_высокого_уровня (" ?c1 ")" " + " "Интернет (" ?c2 ")" " + " "Обычное_меню (" ?c3 ")" " + " "Пригодно_для_детей (" ?c4 ")" " -> Ресторан (" (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5) ")")))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Интернет-Обычное_меню-Природа-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара) (confidence ?c0))
(element(param Официанты_высокого_уровня) (confidence ?c1))
(element(param Интернет) (confidence ?c2))
(element(param Обычное_меню) (confidence ?c3))
(element(param Природа) (confidence ?c4))
=>
(assert(element(param Ресторан) (confidence (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5)) ))
(assert(appendmessagehalt (str-cat "Блюда_от_шеф-повара (" ?c0 ")" " + " "Официанты_высокого_уровня (" ?c1 ")" " + " "Интернет (" ?c2 ")" " + " "Обычное_меню (" ?c3 ")" " + " "Природа (" ?c4 ")" " -> Ресторан (" (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5) ")")))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Живая_музыка-Особое_меню-Пригодно_для_детей-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара) (confidence ?c0))
(element(param Официанты_высокого_уровня) (confidence ?c1))
(element(param Живая_музыка) (confidence ?c2))
(element(param Особое_меню) (confidence ?c3))
(element(param Пригодно_для_детей) (confidence ?c4))
=>
(assert(element(param Ресторан) (confidence (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5)) ))
(assert(appendmessagehalt (str-cat "Блюда_от_шеф-повара (" ?c0 ")" " + " "Официанты_высокого_уровня (" ?c1 ")" " + " "Живая_музыка (" ?c2 ")" " + " "Особое_меню (" ?c3 ")" " + " "Пригодно_для_детей (" ?c4 ")" " -> Ресторан (" (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5) ")")))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Живая_музыка-Особое_меню-Природа-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара) (confidence ?c0))
(element(param Официанты_высокого_уровня) (confidence ?c1))
(element(param Живая_музыка) (confidence ?c2))
(element(param Особое_меню) (confidence ?c3))
(element(param Природа) (confidence ?c4))
=>
(assert(element(param Ресторан) (confidence (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5)) ))
(assert(appendmessagehalt (str-cat "Блюда_от_шеф-повара (" ?c0 ")" " + " "Официанты_высокого_уровня (" ?c1 ")" " + " "Живая_музыка (" ?c2 ")" " + " "Особое_меню (" ?c3 ")" " + " "Природа (" ?c4 ")" " -> Ресторан (" (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5) ")")))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Интернет-Особое_меню-Пригодно_для_детей-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара) (confidence ?c0))
(element(param Официанты_высокого_уровня) (confidence ?c1))
(element(param Интернет) (confidence ?c2))
(element(param Особое_меню) (confidence ?c3))
(element(param Пригодно_для_детей) (confidence ?c4))
=>
(assert(element(param Ресторан) (confidence (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5)) ))
(assert(appendmessagehalt (str-cat "Блюда_от_шеф-повара (" ?c0 ")" " + " "Официанты_высокого_уровня (" ?c1 ")" " + " "Интернет (" ?c2 ")" " + " "Особое_меню (" ?c3 ")" " + " "Пригодно_для_детей (" ?c4 ")" " -> Ресторан (" (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5) ")")))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Интернет-Особое_меню-Природа-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара) (confidence ?c0))
(element(param Официанты_высокого_уровня) (confidence ?c1))
(element(param Интернет) (confidence ?c2))
(element(param Особое_меню) (confidence ?c3))
(element(param Природа) (confidence ?c4))
=>
(assert(element(param Ресторан) (confidence (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5)) ))
(assert(appendmessagehalt (str-cat "Блюда_от_шеф-повара (" ?c0 ")" " + " "Официанты_высокого_уровня (" ?c1 ")" " + " "Интернет (" ?c2 ")" " + " "Особое_меню (" ?c3 ")" " + " "Природа (" ?c4 ")" " -> Ресторан (" (/ (+ ?c0 ?c1 ?c2 ?c3 ?c4) 5) ")")))
)

