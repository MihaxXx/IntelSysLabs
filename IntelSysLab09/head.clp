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
	(slot confidence)
)

(defrule greeting
	(declare (salience 100))
	=>
	(assert (element (param Максим_Валентинович) (confidence 100)))
	(assert (element (param Clips) (confidence 100)))
	(assert (appendmessagehalt "Добро пожаловать в очередной не deprecated кошмар разработчика!"))
)

(defrule НеСпроситьаПоинтересоваться
	(declare (salience 20))
	(element (param Максим_Валентинович) (confidence ?с0))
	=>
	(assert (appendmessagehalt "[S]"))
)

(defrule ПолучиласьБеда
	(declare (salience 80))
	(element (param Максим_Валентинович) (confidence ?с0))
	(element (param Clips))
	(element (param Удачный_выбор))
	=>
	(assert (element (param Победа) (confidence 100) ))
	(assert (appendmessagehalt "Максим_Валентинович, Clips, Удачный_выбор -> Победа"))
)
(defrule ПолучиласьБеда
	(declare (salience 80))
	(element (param Максим_Валентинович) (confidence ?с0))
	(element (param Clips) (confidence ?с1))
	(element (param Неудачный_выбор) (confidence ?с2))
	=>
	(assert (element (param Беда) (confidence 100)))
	(assert (appendmessagehalt "Максим_Валентинович, Clips, Неудачный_выбор -> Беда"))
)

(defrule Беда_не_приходит_одна
	(declare (salience 80))
	(element (param Беда) (confidence ?с0))
	=>
	(assert (appendmessagehalt "[4:20] Guess I'll die("))
)
;==================================================================================