; Этот блок реализует логику обмена информацией с графической оболочкой,
; а также механизм остановки и повторного пуска машины вывода
; Русский текст в комментариях разрешён!
(deftemplate ioproxy		; шаблон факта-посредника для обмена информацией с GUI
	(slot fact-id)		; теоретически тут id факта для изменения
	(multislot answers)	; возможные ответы
	(multislot messages)	; исходящие сообщения
	(slot reaction)		; возможные ответы пользователя
	(slot value)		; выбор пользователя
	(slot restore)		; забыл зачем это поле
)

; Собственно экземпляр факта ioproxy
(deffacts proxy-fact
	(ioproxy
		(fact-id 0112)	; это поле пока что не задействовано
		(value none)	; значение пустое
		(messages)	; мультислот messages изначально пуст
	)
)

(defrule clear-messages
	(declare (salience 90))
	?clear-msg-flg <- (clearmessage)
	?proxy <- (ioproxy)
	=>
	(modify ?proxy (messages))
	(retract ?clear-msg-flg)
	(printout t "Messages cleared ..." crlf)
)

(defrule set-output-and-halt
	(declare (salience 99))
	?current-message <- (sendmessagehalt ?new-msg)
	?proxy <- (ioproxy (messages $?msg-list))
	=>
	(printout t "Message set : " ?new-msg " ... halting ... " crlf)
	(modify ?proxy (messages ?new-msg))
	(retract ?current-message)
	(halt)
)

(defrule append-output-and-halt
	(declare (salience 99))
	?current-message <- (appendmessagehalt $?new-msg)
	?proxy <- (ioproxy (messages $?msg-list))
	=>
	(printout t "Messages appended : " $?new-msg " ... halting ... " crlf) 
	(modify ?proxy (messages $?msg-list $?new-msg))
	(retract ?current-message)
	(halt)
)

(defrule set-output-and-proceed
	(declare (salience 99))
	?current-message <- (sendmessage ?new-msg)
	?proxy <- (ioproxy)
	=>
	(printout t "Message set : " ?new-msg " ... proceed ... " crlf)
	(modify ?proxy (messages ?new-msg))
	(retract ?current-message)
)

(defrule append-output-and-proceed
	(declare (salience 99))
	?current-message <- (appendmessage ?new-msg)
	?proxy <- (ioproxy (messages $?msg-list))
	=>
	(printout t "Message appended : " ?new-msg " ... proceed ... " crlf)
	(modify ?proxy (messages $?msg-list ?new-msg))
	(retract ?current-message)
)

; это правило не работает - исправить (тут нужна печать списка)
(defrule print-messages
	(declare (salience 99))
	?proxy <- (ioproxy (messages ?msg-list))
	?update-key <- (updated True)
	=>
	(retract ?update-key)
	(printout t "Messages received : " ?msg-list crlf)
)

;==================================================================================

(deftemplate element
	(slot param)
)

(defrule greeting
	(declare (salience 100))
	=>
	(assert (element (param Максим_Валентинович)))
	(assert (element (param Clips)))
	(assert (appendmessagehalt "Добро пожаловать в очередной deprecated кошмар разработчика!"))
)

(defrule НеСпроситьаПоинтересоваться
	(declare (salience 20))
	(element (param Максим_Валентинович))
	=>
	(assert (appendmessagehalt "[S]"))
)

(defrule ПолучиласьБеда
	(declare (salience 80))
	(element (param Максим_Валентинович))
	(element (param Clips))
	(element (param Удачный_выбор))
	=>
	(assert (element (param Победа)))
	(assert (appendmessagehalt "Максим_Валентинович, Clips, Удачный_выбор -> Победа"))
)
(defrule ПолучиласьБеда
	(declare (salience 80))
	(element (param Максим_Валентинович))
	(element (param Clips))
	(element (param Неудачный_выбор))
	=>
	(assert (element (param Беда)))
	(assert (appendmessagehalt "Максим_Валентинович, Clips, Неудачный_выбор -> Беда"))
)

(defrule Беда_не_приходит_одна
	(declare (salience 80))
	(element (param Беда))
	=>
	(assert (appendmessagehalt "[4:20] Guess I'll die("))
)
;==================================================================================

(defrule Салат-Обычное_меню
(declare(salience 40))
(element(param Салат))=>
(assert(element(param Обычное_меню)))
(assert(appendmessagehalt "Салат -> Обычное_меню"))
)

(defrule Кофе,_чай-Десерт-Обычное_меню
(declare(salience 40))
(element(param Кофе,_чай))
(element(param Десерт))=>
(assert(element(param Обычное_меню)))
(assert(appendmessagehalt "Кофе,_чай + Десерт -> Обычное_меню"))
)

(defrule Первое_блюдо-Обычное_меню
(declare(salience 40))
(element(param Первое_блюдо))=>
(assert(element(param Обычное_меню)))
(assert(appendmessagehalt "Первое_блюдо -> Обычное_меню"))
)

(defrule Второе_блюдо-Обычное_меню
(declare(salience 40))
(element(param Второе_блюдо))=>
(assert(element(param Обычное_меню)))
(assert(appendmessagehalt "Второе_блюдо -> Обычное_меню"))
)

(defrule Подходит_для_всей_семьи-Уютный_интерьер-Домашняя_атмосфера
(declare(salience 40))
(element(param Подходит_для_всей_семьи))
(element(param Уютный_интерьер))=>
(assert(element(param Домашняя_атмосфера)))
(assert(appendmessagehalt "Подходит_для_всей_семьи + Уютный_интерьер -> Домашняя_атмосфера"))
)

(defrule Детская_площадка-Подходит_для_всей_семьи-Пригодно_для_детей
(declare(salience 40))
(element(param Детская_площадка))
(element(param Подходит_для_всей_семьи))=>
(assert(element(param Пригодно_для_детей)))
(assert(appendmessagehalt "Детская_площадка + Подходит_для_всей_семьи -> Пригодно_для_детей"))
)

(defrule Горки,_качели-Доска_для_рисования-Детская_площадка
(declare(salience 40))
(element(param Горки,_качели))
(element(param Доска_для_рисования))=>
(assert(element(param Детская_площадка)))
(assert(appendmessagehalt "Горки,_качели + Доска_для_рисования -> Детская_площадка"))
)

(defrule Светлые_тона_в_итерьере-Наличие_декоративных_растений-Уютный_интерьер
(declare(salience 40))
(element(param Светлые_тона_в_итерьере))
(element(param Наличие_декоративных_растений))=>
(assert(element(param Уютный_интерьер)))
(assert(appendmessagehalt "Светлые_тона_в_итерьере + Наличие_декоративных_растений -> Уютный_интерьер"))
)

(defrule Пригодно_для_детей-Домашняя_атмосфера-Обычное_меню-Детское_кафе
(declare(salience 40))
(element(param Пригодно_для_детей))
(element(param Домашняя_атмосфера))
(element(param Обычное_меню))=>
(assert(element(param Детское_кафе)))
(assert(appendmessagehalt "Пригодно_для_детей + Домашняя_атмосфера + Обычное_меню -> Детское_кафе"))
)

(defrule Пригодно_для_детей-Интернет-Обычное_меню-Детское_кафе
(declare(salience 40))
(element(param Пригодно_для_детей))
(element(param Интернет))
(element(param Обычное_меню))=>
(assert(element(param Детское_кафе)))
(assert(appendmessagehalt "Пригодно_для_детей + Интернет + Обычное_меню -> Детское_кафе"))
)

(defrule Громкая_музыка-Алкоголь-Клубный_интерьер-Клуб
(declare(salience 40))
(element(param Громкая_музыка))
(element(param Алкоголь))
(element(param Клубный_интерьер))=>
(assert(element(param Клуб)))
(assert(appendmessagehalt "Громкая_музыка + Алкоголь + Клубный_интерьер -> Клуб"))
)

(defrule Громкая_музыка-Кальян-Клубный_интерьер-Клуб
(declare(salience 40))
(element(param Громкая_музыка))
(element(param Кальян))
(element(param Клубный_интерьер))=>
(assert(element(param Клуб)))
(assert(appendmessagehalt "Громкая_музыка + Кальян + Клубный_интерьер -> Клуб"))
)

(defrule Громкая_музыка-Интернет-Клубный_интерьер-Клуб
(declare(salience 40))
(element(param Громкая_музыка))
(element(param Интернет))
(element(param Клубный_интерьер))=>
(assert(element(param Клуб)))
(assert(appendmessagehalt "Громкая_музыка + Интернет + Клубный_интерьер -> Клуб"))
)

(defrule Электронная_музыка-Шумная_атмосфера-Громкая_музыка
(declare(salience 40))
(element(param Электронная_музыка))
(element(param Шумная_атмосфера))=>
(assert(element(param Громкая_музыка)))
(assert(appendmessagehalt "Электронная_музыка + Шумная_атмосфера -> Громкая_музыка"))
)

(defrule Музыка-Танцплощадка-Электронная_музыка
(declare(salience 40))
(element(param Музыка))
(element(param Танцплощадка))=>
(assert(element(param Электронная_музыка)))
(assert(appendmessagehalt "Музыка + Танцплощадка -> Электронная_музыка"))
)

(defrule Наличие_медиатехники-Транслирует_звук-Музыка
(declare(salience 40))
(element(param Наличие_медиатехники))
(element(param Транслирует_звук))=>
(assert(element(param Музыка)))
(assert(appendmessagehalt "Наличие_медиатехники + Транслирует_звук -> Музыка"))
)

(defrule Темные_тона_в_итерьере-Разноцветное_искусственное_освещение-Клубный_интерьер
(declare(salience 40))
(element(param Темные_тона_в_итерьере))
(element(param Разноцветное_искусственное_освещение))=>
(assert(element(param Клубный_интерьер)))
(assert(appendmessagehalt "Темные_тона_в_итерьере + Разноцветное_искусственное_освещение -> Клубный_интерьер"))
)

(defrule Шумный_отдых-Трансляция_матчей-Пиво-Спортбар
(declare(salience 40))
(element(param Шумный_отдых))
(element(param Трансляция_матчей))
(element(param Пиво))=>
(assert(element(param Спортбар)))
(assert(appendmessagehalt "Шумный_отдых + Трансляция_матчей + Пиво -> Спортбар"))
)

(defrule Шумный_отдых-Трансляция_матчей-Закуски-Спортбар
(declare(salience 40))
(element(param Шумный_отдых))
(element(param Трансляция_матчей))
(element(param Закуски))=>
(assert(element(param Спортбар)))
(assert(appendmessagehalt "Шумный_отдых + Трансляция_матчей + Закуски -> Спортбар"))
)

(defrule Спортивная_тематика-Алкоголь-Пиво
(declare(salience 40))
(element(param Спортивная_тематика))
(element(param Алкоголь))=>
(assert(element(param Пиво)))
(assert(appendmessagehalt "Спортивная_тематика + Алкоголь -> Пиво"))
)

(defrule Шумная_атмосфера-Любите_кричать-Шумный_отдых
(declare(salience 40))
(element(param Шумная_атмосфера))
(element(param Любите_кричать))=>
(assert(element(param Шумный_отдых)))
(assert(appendmessagehalt "Шумная_атмосфера + Любите_кричать -> Шумный_отдых"))
)

(defrule Наличие_телевизора-Спортивная_тематика-Трансляция_матчей
(declare(salience 40))
(element(param Наличие_телевизора))
(element(param Спортивная_тематика))=>
(assert(element(param Трансляция_матчей)))
(assert(appendmessagehalt "Наличие_телевизора + Спортивная_тематика -> Трансляция_матчей"))
)

(defrule Наличие_медиатехники-Транслирует_звук-Транслирует_видео-Наличие_телевизора
(declare(salience 40))
(element(param Наличие_медиатехники))
(element(param Транслирует_звук))
(element(param Транслирует_видео))=>
(assert(element(param Наличие_телевизора)))
(assert(appendmessagehalt "Наличие_медиатехники + Транслирует_звук + Транслирует_видео -> Наличие_телевизора"))
)

(defrule Обычные_официанты-Уютный_интерьер-Особое_меню-Кафе
(declare(salience 40))
(element(param Обычные_официанты))
(element(param Уютный_интерьер))
(element(param Особое_меню))=>
(assert(element(param Кафе)))
(assert(appendmessagehalt "Обычные_официанты + Уютный_интерьер + Особое_меню -> Кафе"))
)

(defrule Обычные_официанты-Уютный_интерьер-Обычное_меню-Кафе
(declare(salience 40))
(element(param Обычные_официанты))
(element(param Уютный_интерьер))
(element(param Обычное_меню))=>
(assert(element(param Кафе)))
(assert(appendmessagehalt "Обычные_официанты + Уютный_интерьер + Обычное_меню -> Кафе"))
)

(defrule Средняя_ценовая_категория-Наличие_официантов-Обычные_официанты
(declare(salience 40))
(element(param Средняя_ценовая_категория))
(element(param Наличие_официантов))=>
(assert(element(param Обычные_официанты)))
(assert(appendmessagehalt "Средняя_ценовая_категория + Наличие_официантов -> Обычные_официанты"))
)

(defrule Светлые_тона_в_итерьере-Удобные_диваны_/кресла_/стулья-Уютный_интерьер
(declare(salience 40))
(element(param Светлые_тона_в_итерьере))
(element(param Удобные_диваны_/кресла_/стулья))=>
(assert(element(param Уютный_интерьер)))
(assert(appendmessagehalt "Светлые_тона_в_итерьере + Удобные_диваны_/кресла_/стулья -> Уютный_интерьер"))
)

(defrule Веганское_меню-Особое_меню
(declare(salience 40))
(element(param Веганское_меню))=>
(assert(element(param Особое_меню)))
(assert(appendmessagehalt "Веганское_меню -> Особое_меню"))
)

(defrule Вегетарианское_меню-Особое_меню
(declare(salience 40))
(element(param Вегетарианское_меню))=>
(assert(element(param Особое_меню)))
(assert(appendmessagehalt "Вегетарианское_меню -> Особое_меню"))
)

(defrule Не_ем_мясо_и_птицу-Вегетарианское_меню
(declare(salience 40))
(element(param Не_ем_мясо_и_птицу))=>
(assert(element(param Вегетарианское_меню)))
(assert(appendmessagehalt "Не_ем_мясо_и_птицу -> Вегетарианское_меню"))
)

(defrule Не_ем_продукты_животного_происхождения-Не_ем_мясо_и_птицу-Веганское_меню
(declare(salience 40))
(element(param Не_ем_продукты_животного_происхождения))
(element(param Не_ем_мясо_и_птицу))=>
(assert(element(param Веганское_меню)))
(assert(appendmessagehalt "Не_ем_продукты_животного_происхождения + Не_ем_мясо_и_птицу -> Веганское_меню"))
)

(defrule Музыка-Приглашенные_певцы-Живая_музыка
(declare(salience 40))
(element(param Музыка))
(element(param Приглашенные_певцы))=>
(assert(element(param Живая_музыка)))
(assert(appendmessagehalt "Музыка + Приглашенные_певцы -> Живая_музыка"))
)

(defrule Оркестр-Живая_музыка
(declare(salience 40))
(element(param Оркестр))=>
(assert(element(param Живая_музыка)))
(assert(appendmessagehalt "Оркестр -> Живая_музыка"))
)

(defrule Профессиональные_повара-Качественные_натуральные_продукты-Блюда_от_шеф-повара
(declare(salience 40))
(element(param Профессиональные_повара))
(element(param Качественные_натуральные_продукты))=>
(assert(element(param Блюда_от_шеф-повара)))
(assert(appendmessagehalt "Профессиональные_повара + Качественные_натуральные_продукты -> Блюда_от_шеф-повара"))
)

(defrule Высокая_ценовая_категория-Наличие_официантов-Официанты_высокого_уровня
(declare(salience 40))
(element(param Высокая_ценовая_категория))
(element(param Наличие_официантов))=>
(assert(element(param Официанты_высокого_уровня)))
(assert(appendmessagehalt "Высокая_ценовая_категория + Наличие_официантов -> Официанты_высокого_уровня"))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Живая_музыка-Обычное_меню-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара))
(element(param Официанты_высокого_уровня))
(element(param Живая_музыка))
(element(param Обычное_меню))=>
(assert(element(param Ресторан)))
(assert(appendmessagehalt "Блюда_от_шеф-повара + Официанты_высокого_уровня + Живая_музыка + Обычное_меню -> Ресторан"))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Интернет-Обычное_меню-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара))
(element(param Официанты_высокого_уровня))
(element(param Интернет))
(element(param Обычное_меню))=>
(assert(element(param Ресторан)))
(assert(appendmessagehalt "Блюда_от_шеф-повара + Официанты_высокого_уровня + Интернет + Обычное_меню -> Ресторан"))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Живая_музыка-Особое_меню-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара))
(element(param Официанты_высокого_уровня))
(element(param Живая_музыка))
(element(param Особое_меню))=>
(assert(element(param Ресторан)))
(assert(appendmessagehalt "Блюда_от_шеф-повара + Официанты_высокого_уровня + Живая_музыка + Особое_меню -> Ресторан"))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Интернет-Особое_меню-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара))
(element(param Официанты_высокого_уровня))
(element(param Интернет))
(element(param Особое_меню))=>
(assert(element(param Ресторан)))
(assert(appendmessagehalt "Блюда_от_шеф-повара + Официанты_высокого_уровня + Интернет + Особое_меню -> Ресторан"))
)

(defrule Обычное_меню-Самообслуживание-Простенький_интерьер-Столовая
(declare(salience 40))
(element(param Обычное_меню))
(element(param Самообслуживание))
(element(param Простенький_интерьер))=>
(assert(element(param Столовая)))
(assert(appendmessagehalt "Обычное_меню + Самообслуживание + Простенький_интерьер -> Столовая"))
)

(defrule Нет_официантов-Самообслуживание
(declare(salience 40))
(element(param Нет_официантов))=>
(assert(element(param Самообслуживание)))
(assert(appendmessagehalt "Нет_официантов -> Самообслуживание"))
)

(defrule Низкая_ценовая_категория-Только_столы_и_стулья
(declare(salience 40))
(element(param Низкая_ценовая_категория))=>
(assert(element(param Только_столы_и_стулья)))
(assert(appendmessagehalt "Низкая_ценовая_категория -> Только_столы_и_стулья"))
)

(defrule Только_столы_и_стулья-Простенький_интерьер
(declare(salience 40))
(element(param Только_столы_и_стулья))=>
(assert(element(param Простенький_интерьер)))
(assert(appendmessagehalt "Только_столы_и_стулья -> Простенький_интерьер"))
)

(defrule Есть_коты-Возможность_поиграть_с_животными
(declare(salience 40))
(element(param Есть_коты))=>
(assert(element(param Возможность_поиграть_с_животными)))
(assert(appendmessagehalt "Есть_коты -> Возможность_поиграть_с_животными"))
)

(defrule Есть_попугайчики-Возможность_поиграть_с_животными
(declare(salience 40))
(element(param Есть_попугайчики))=>
(assert(element(param Возможность_поиграть_с_животными)))
(assert(appendmessagehalt "Есть_попугайчики -> Возможность_поиграть_с_животными"))
)

(defrule Есть_собаки-Возможность_поиграть_с_животными
(declare(salience 40))
(element(param Есть_собаки))=>
(assert(element(param Возможность_поиграть_с_животными)))
(assert(appendmessagehalt "Есть_собаки -> Возможность_поиграть_с_животными"))
)

(defrule WiFi-Интернет
(declare(salience 40))
(element(param WiFi))=>
(assert(element(param Интернет)))
(assert(appendmessagehalt "WiFi -> Интернет"))
)

(defrule Настольные_игры-Интернет-Наличие_развлечений
(declare(salience 40))
(element(param Настольные_игры))
(element(param Интернет))=>
(assert(element(param Наличие_развлечений)))
(assert(appendmessagehalt "Настольные_игры + Интернет -> Наличие_развлечений"))
)

(defrule Наличие_развлечений-Кальян-Кофе,_чай-Антикафе
(declare(salience 40))
(element(param Наличие_развлечений))
(element(param Кальян))
(element(param Кофе,_чай))=>
(assert(element(param Антикафе)))
(assert(appendmessagehalt "Наличие_развлечений + Кальян + Кофе,_чай -> Антикафе"))
)

(defrule Наличие_развлечений-Возможность_поиграть_с_животными-Кофе,_чай-Антикафе
(declare(salience 40))
(element(param Наличие_развлечений))
(element(param Возможность_поиграть_с_животными))
(element(param Кофе,_чай))=>
(assert(element(param Антикафе)))
(assert(appendmessagehalt "Наличие_развлечений + Возможность_поиграть_с_животными + Кофе,_чай -> Антикафе"))
)

(defrule Наличие_развлечений-Кальян-Десерт-Антикафе
(declare(salience 40))
(element(param Наличие_развлечений))
(element(param Кальян))
(element(param Десерт))=>
(assert(element(param Антикафе)))
(assert(appendmessagehalt "Наличие_развлечений + Кальян + Десерт -> Антикафе"))
)

(defrule Наличие_развлечений-Возможность_поиграть_с_животными-Десерт-Антикафе
(declare(salience 40))
(element(param Наличие_развлечений))
(element(param Возможность_поиграть_с_животными))
(element(param Десерт))=>
(assert(element(param Антикафе)))
(assert(appendmessagehalt "Наличие_развлечений + Возможность_поиграть_с_животными + Десерт -> Антикафе"))
)

(defrule Подают_равиоли-Есть_паста
(declare(salience 40))
(element(param Подают_равиоли))=>
(assert(element(param Есть_паста)))
(assert(appendmessagehalt "Подают_равиоли -> Есть_паста"))
)

(defrule Подают_феттуччини-Есть_паста
(declare(salience 40))
(element(param Подают_феттуччини))=>
(assert(element(param Есть_паста)))
(assert(appendmessagehalt "Подают_феттуччини -> Есть_паста"))
)

(defrule Подают_спагетти-Есть_паста
(declare(salience 40))
(element(param Подают_спагетти))=>
(assert(element(param Есть_паста)))
(assert(appendmessagehalt "Подают_спагетти -> Есть_паста"))
)

(defrule Подают_карбонару-Есть_паста
(declare(salience 40))
(element(param Подают_карбонару))=>
(assert(element(param Есть_паста)))
(assert(appendmessagehalt "Подают_карбонару -> Есть_паста"))
)

(defrule Есть_паста-Итальянское_кафе-Итальянское_заведение
(declare(salience 40))
(element(param Есть_паста))
(element(param Итальянское_кафе))=>
(assert(element(param Итальянское_заведение)))
(assert(appendmessagehalt "Есть_паста + Итальянское_кафе -> Итальянское_заведение"))
)

(defrule Итальянское_заведение-Средняя_ценовая_категория-Итальянское_кафе
(declare(salience 40))
(element(param Итальянское_заведение))
(element(param Средняя_ценовая_категория))=>
(assert(element(param Итальянское_кафе)))
(assert(appendmessagehalt "Итальянское_заведение + Средняя_ценовая_категория -> Итальянское_кафе"))
)

(defrule Итальянское_заведение-Высокая_ценовая_категория-Ресторан_итальянской_кухни
(declare(salience 40))
(element(param Итальянское_заведение))
(element(param Высокая_ценовая_категория))=>
(assert(element(param Ресторан_итальянской_кухни)))
(assert(appendmessagehalt "Итальянское_заведение + Высокая_ценовая_категория -> Ресторан_итальянской_кухни"))
)

(defrule Есть_картошка_фри-Вредная_еда
(declare(salience 40))
(element(param Есть_картошка_фри))=>
(assert(element(param Вредная_еда)))
(assert(appendmessagehalt "Есть_картошка_фри -> Вредная_еда"))
)

(defrule Подают_бургеры-Вредная_еда
(declare(salience 40))
(element(param Подают_бургеры))=>
(assert(element(param Вредная_еда)))
(assert(appendmessagehalt "Подают_бургеры -> Вредная_еда"))
)

(defrule Есть_сладкие_газированные_напитки-Вредная_еда
(declare(salience 40))
(element(param Есть_сладкие_газированные_напитки))=>
(assert(element(param Вредная_еда)))
(assert(appendmessagehalt "Есть_сладкие_газированные_напитки -> Вредная_еда"))
)

(defrule Есть_наггетсы-Вредная_еда
(declare(salience 40))
(element(param Есть_наггетсы))=>
(assert(element(param Вредная_еда)))
(assert(appendmessagehalt "Есть_наггетсы -> Вредная_еда"))
)

(defrule Вредная_еда-Средняя_ценовая_категория-Бургерная
(declare(salience 40))
(element(param Вредная_еда))
(element(param Средняя_ценовая_категория))=>
(assert(element(param Бургерная)))
(assert(appendmessagehalt "Вредная_еда + Средняя_ценовая_категория -> Бургерная"))
)

(defrule Вредная_еда-Низкая_ценовая_категория-Ресторан_быстрого_питания
(declare(salience 40))
(element(param Вредная_еда))
(element(param Низкая_ценовая_категория))=>
(assert(element(param Ресторан_быстрого_питания)))
(assert(appendmessagehalt "Вредная_еда + Низкая_ценовая_категория -> Ресторан_быстрого_питания"))
)

(defrule Подают_роллы_Калифорния-Второе_блюдо-Подают_роллы
(declare(salience 40))
(element(param Подают_роллы_Калифорния))
(element(param Второе_блюдо))=>
(assert(element(param Подают_роллы)))
(assert(appendmessagehalt "Подают_роллы_Калифорния + Второе_блюдо -> Подают_роллы"))
)

(defrule Подают_роллы_Филадельфия-Второе_блюдо-Подают_роллы
(declare(salience 40))
(element(param Подают_роллы_Филадельфия))
(element(param Второе_блюдо))=>
(assert(element(param Подают_роллы)))
(assert(appendmessagehalt "Подают_роллы_Филадельфия + Второе_блюдо -> Подают_роллы"))
)

(defrule Подают_роллы_Аляска-Второе_блюдо-Подают_роллы
(declare(salience 40))
(element(param Подают_роллы_Аляска))
(element(param Второе_блюдо))=>
(assert(element(param Подают_роллы)))
(assert(appendmessagehalt "Подают_роллы_Аляска + Второе_блюдо -> Подают_роллы"))
)

(defrule Подают_роллы_Лава-Второе_блюдо-Подают_роллы
(declare(salience 40))
(element(param Подают_роллы_Лава))
(element(param Второе_блюдо))=>
(assert(element(param Подают_роллы)))
(assert(appendmessagehalt "Подают_роллы_Лава + Второе_блюдо -> Подают_роллы"))
)

(defrule Подают_лапшу_Удон-Второе_блюдо-Есть_классическая_японская_кухня
(declare(salience 40))
(element(param Подают_лапшу_Удон))
(element(param Второе_блюдо))=>
(assert(element(param Есть_классическая_японская_кухня)))
(assert(appendmessagehalt "Подают_лапшу_Удон + Второе_блюдо -> Есть_классическая_японская_кухня"))
)

(defrule Подают_суп_Мисо-Первое_блюдо-Есть_классическая_японская_кухня
(declare(salience 40))
(element(param Подают_суп_Мисо))
(element(param Первое_блюдо))=>
(assert(element(param Есть_классическая_японская_кухня)))
(assert(appendmessagehalt "Подают_суп_Мисо + Первое_блюдо -> Есть_классическая_японская_кухня"))
)

(defrule Подают_роллы-Японское_заведение
(declare(salience 40))
(element(param Подают_роллы))=>
(assert(element(param Японское_заведение)))
(assert(appendmessagehalt "Подают_роллы -> Японское_заведение"))
)

(defrule Есть_классическая_японская_кухня-Японское_заведение
(declare(salience 40))
(element(param Есть_классическая_японская_кухня))=>
(assert(element(param Японское_заведение)))
(assert(appendmessagehalt "Есть_классическая_японская_кухня -> Японское_заведение"))
)

(defrule Японское_заведение-Средняя_ценовая_категория-Японское_кафе
(declare(salience 40))
(element(param Японское_заведение))
(element(param Средняя_ценовая_категория))=>
(assert(element(param Японское_кафе)))
(assert(appendmessagehalt "Японское_заведение + Средняя_ценовая_категория -> Японское_кафе"))
)

(defrule Японское_заведение-Высокая_ценовая_категория-Ресторан_японской_кухни
(declare(salience 40))
(element(param Японское_заведение))
(element(param Высокая_ценовая_категория))=>
(assert(element(param Ресторан_японской_кухни)))
(assert(appendmessagehalt "Японское_заведение + Высокая_ценовая_категория -> Ресторан_японской_кухни"))
)

(defrule Первое_блюдо-Домашная_еда-Русское_заведение
(declare(salience 40))
(element(param Первое_блюдо))
(element(param Домашная_еда))=>
(assert(element(param Русское_заведение)))
(assert(appendmessagehalt "Первое_блюдо + Домашная_еда -> Русское_заведение"))
)

(defrule Подают_пироги-Домашная_еда-Русское_заведение
(declare(salience 40))
(element(param Подают_пироги))
(element(param Домашная_еда))=>
(assert(element(param Русское_заведение)))
(assert(appendmessagehalt "Подают_пироги + Домашная_еда -> Русское_заведение"))
)

(defrule Русское_заведение-Средняя_ценовая_категория-Русское_кафе
(declare(salience 40))
(element(param Русское_заведение))
(element(param Средняя_ценовая_категория))=>
(assert(element(param Русское_кафе)))
(assert(appendmessagehalt "Русское_заведение + Средняя_ценовая_категория -> Русское_кафе"))
)

(defrule Русское_заведение-Высокая_ценовая_категория-Ресторан_русской_кухни
(declare(salience 40))
(element(param Русское_заведение))
(element(param Высокая_ценовая_категория))=>
(assert(element(param Ресторан_русской_кухни)))
(assert(appendmessagehalt "Русское_заведение + Высокая_ценовая_категория -> Ресторан_русской_кухни"))
)

(defrule Подают_пироги-Есть_осетинские_национальные_блюда-Подают_осетинские_пироги
(declare(salience 40))
(element(param Подают_пироги))
(element(param Есть_осетинские_национальные_блюда))=>
(assert(element(param Подают_осетинские_пироги)))
(assert(appendmessagehalt "Подают_пироги + Есть_осетинские_национальные_блюда -> Подают_осетинские_пироги"))
)

(defrule Подают_осетинские_пироги-Кавказское_заведение
(declare(salience 40))
(element(param Подают_осетинские_пироги))=>
(assert(element(param Кавказское_заведение)))
(assert(appendmessagehalt "Подают_осетинские_пироги -> Кавказское_заведение"))
)

(defrule Кавказское_заведение-Средняя_ценовая_категория-Кавказское_кафе
(declare(salience 40))
(element(param Кавказское_заведение))
(element(param Средняя_ценовая_категория))=>
(assert(element(param Кавказское_кафе)))
(assert(appendmessagehalt "Кавказское_заведение + Средняя_ценовая_категория -> Кавказское_кафе"))
)

(defrule Кавказское_заведение-Высокая_ценовая_категория-Ресторан_кавказской_кухни
(declare(salience 40))
(element(param Кавказское_заведение))
(element(param Высокая_ценовая_категория))=>
(assert(element(param Ресторан_кавказской_кухни)))
(assert(appendmessagehalt "Кавказское_заведение + Высокая_ценовая_категория -> Ресторан_кавказской_кухни"))
)

(defrule Подают_Маргарита-Подают_пиццу
(declare(salience 40))
(element(param Подают_Маргарита))=>
(assert(element(param Подают_пиццу)))
(assert(appendmessagehalt "Подают_Маргарита -> Подают_пиццу"))
)

(defrule Подают_4_сыра-Подают_пиццу
(declare(salience 40))
(element(param Подают_4_сыра))=>
(assert(element(param Подают_пиццу)))
(assert(appendmessagehalt "Подают_4_сыра -> Подают_пиццу"))
)

(defrule Подают_Сицилия-Подают_пиццу
(declare(salience 40))
(element(param Подают_Сицилия))=>
(assert(element(param Подают_пиццу)))
(assert(appendmessagehalt "Подают_Сицилия -> Подают_пиццу"))
)

(defrule Подают_Кальцоне-Подают_пиццу
(declare(salience 40))
(element(param Подают_Кальцоне))=>
(assert(element(param Подают_пиццу)))
(assert(appendmessagehalt "Подают_Кальцоне -> Подают_пиццу"))
)

(defrule Подают_пиццу-Средняя_ценовая_категория-Пиццерия
(declare(salience 40))
(element(param Подают_пиццу))
(element(param Средняя_ценовая_категория))=>
(assert(element(param Пиццерия)))
(assert(appendmessagehalt "Подают_пиццу + Средняя_ценовая_категория -> Пиццерия"))
)

(defrule Подают_пиццу-Высокая_ценовая_категория-Ресторан_итальянской_кухни
(declare(salience 40))
(element(param Подают_пиццу))
(element(param Высокая_ценовая_категория))=>
(assert(element(param Ресторан_итальянской_кухни)))
(assert(appendmessagehalt "Подают_пиццу + Высокая_ценовая_категория -> Ресторан_итальянской_кухни"))
)

(defrule Алкоголь-Музыка-Питейное_заведение
(declare(salience 40))
(element(param Алкоголь))
(element(param Музыка))=>
(assert(element(param Питейное_заведение)))
(assert(appendmessagehalt "Алкоголь + Музыка -> Питейное_заведение"))
)

(defrule Питейное_заведение-Высокая_ценовая_категория-Паб_с_крафтовым_пивом
(declare(salience 40))
(element(param Питейное_заведение))
(element(param Высокая_ценовая_категория))=>
(assert(element(param Паб_с_крафтовым_пивом)))
(assert(appendmessagehalt "Питейное_заведение + Высокая_ценовая_категория -> Паб_с_крафтовым_пивом"))
)

(defrule Питейное_заведение-Средняя_ценовая_категория-Бар
(declare(salience 40))
(element(param Питейное_заведение))
(element(param Средняя_ценовая_категория))=>
(assert(element(param Бар)))
(assert(appendmessagehalt "Питейное_заведение + Средняя_ценовая_категория -> Бар"))
)

(defrule Удобные_диваны_/кресла_/стулья-Питейное_заведение-Сидрерия
(declare(salience 40))
(element(param Удобные_диваны_/кресла_/стулья))
(element(param Питейное_заведение))=>
(assert(element(param Сидрерия)))
(assert(appendmessagehalt "Удобные_диваны_/кресла_/стулья + Питейное_заведение -> Сидрерия"))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Живая_музыка-Обычное_меню-Пригодно_для_детей-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара))
(element(param Официанты_высокого_уровня))
(element(param Живая_музыка))
(element(param Обычное_меню))
(element(param Пригодно_для_детей))=>
(assert(element(param Ресторан)))
(assert(appendmessagehalt "Блюда_от_шеф-повара + Официанты_высокого_уровня + Живая_музыка + Обычное_меню + Пригодно_для_детей -> Ресторан"))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Живая_музыка-Обычное_меню-Природа-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара))
(element(param Официанты_высокого_уровня))
(element(param Живая_музыка))
(element(param Обычное_меню))
(element(param Природа))=>
(assert(element(param Ресторан)))
(assert(appendmessagehalt "Блюда_от_шеф-повара + Официанты_высокого_уровня + Живая_музыка + Обычное_меню + Природа -> Ресторан"))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Интернет-Обычное_меню-Пригодно_для_детей-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара))
(element(param Официанты_высокого_уровня))
(element(param Интернет))
(element(param Обычное_меню))
(element(param Пригодно_для_детей))=>
(assert(element(param Ресторан)))
(assert(appendmessagehalt "Блюда_от_шеф-повара + Официанты_высокого_уровня + Интернет + Обычное_меню + Пригодно_для_детей -> Ресторан"))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Интернет-Обычное_меню-Природа-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара))
(element(param Официанты_высокого_уровня))
(element(param Интернет))
(element(param Обычное_меню))
(element(param Природа))=>
(assert(element(param Ресторан)))
(assert(appendmessagehalt "Блюда_от_шеф-повара + Официанты_высокого_уровня + Интернет + Обычное_меню + Природа -> Ресторан"))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Живая_музыка-Особое_меню-Пригодно_для_детей-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара))
(element(param Официанты_высокого_уровня))
(element(param Живая_музыка))
(element(param Особое_меню))
(element(param Пригодно_для_детей))=>
(assert(element(param Ресторан)))
(assert(appendmessagehalt "Блюда_от_шеф-повара + Официанты_высокого_уровня + Живая_музыка + Особое_меню + Пригодно_для_детей -> Ресторан"))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Живая_музыка-Особое_меню-Природа-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара))
(element(param Официанты_высокого_уровня))
(element(param Живая_музыка))
(element(param Особое_меню))
(element(param Природа))=>
(assert(element(param Ресторан)))
(assert(appendmessagehalt "Блюда_от_шеф-повара + Официанты_высокого_уровня + Живая_музыка + Особое_меню + Природа -> Ресторан"))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Интернет-Особое_меню-Пригодно_для_детей-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара))
(element(param Официанты_высокого_уровня))
(element(param Интернет))
(element(param Особое_меню))
(element(param Пригодно_для_детей))=>
(assert(element(param Ресторан)))
(assert(appendmessagehalt "Блюда_от_шеф-повара + Официанты_высокого_уровня + Интернет + Особое_меню + Пригодно_для_детей -> Ресторан"))
)

(defrule Блюда_от_шеф-повара-Официанты_высокого_уровня-Интернет-Особое_меню-Природа-Ресторан
(declare(salience 40))
(element(param Блюда_от_шеф-повара))
(element(param Официанты_высокого_уровня))
(element(param Интернет))
(element(param Особое_меню))
(element(param Природа))=>
(assert(element(param Ресторан)))
(assert(appendmessagehalt "Блюда_от_шеф-повара + Официанты_высокого_уровня + Интернет + Особое_меню + Природа -> Ресторан"))
)


