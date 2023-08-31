Перейти в https://github.com/Zombach/HW4SW/tree/master/HW4SW/Source
Если есть свой COM порт то можно использовать его, если нету.
То необходимо установить free-virtual-serial-port-tools,
либо любую альтернативу для создания виртуальных COM портов.
Включить локальный COM_X <=> COM_Y. 
<img width="1431" alt="image" src="https://github.com/Zombach/HW4SW/assets/52016832/95682295-046e-448f-a89d-7963408e0445">

Запустить nats-server

для спама на порт можно использовать asdlemul указав один из (COM_X, COM_Y) портов из предыдущего шага.
<img width="364" alt="image" src="https://github.com/Zombach/HW4SW/assets/52016832/2edd65f0-9d13-4aa7-86e9-4478df6d2f17">

далее зайти в appsettings и при необходимости отредактировать конфигурацию
в поле ComPortName указать второй порт из пары.

Далее запустить HW4SW без параметров. Это для чтения из COM порта и отправки на nats
Ещё раз запустить HW4SW с параметром nats. он будет вычитывать данные с nats сервера.
<img width="431" alt="image" src="https://github.com/Zombach/HW4SW/assets/52016832/4ab2c3d1-d47b-4fde-b598-c6dd2c67ddb8">

Данные будут собираться пока не встретится символ '\r' далее дынные будут отправлены. Сбор начнется сначала
<img width="1172" alt="image" src="https://github.com/Zombach/HW4SW/assets/52016832/c27a0b61-096b-43ed-95c5-c08b271ea6b5">
