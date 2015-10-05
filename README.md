# uCvarc
  Запуск юнити как сервера для игр.
  ----------------------
Скачиваем build юнити отсюда https://www.dropbox.com/s/yujx283lcs9132n/BUILD.rar?dl=0 (exe файл + библиотеки).
Запускаем exe. Можно поиграть Tutorial (соотв. кнопка в меню), а можно ждать входящих соединений.
Юнити все время (даже во время игры и тренировки) прослушивает порты 14000 для одиночной игры, и 14001 для турнамент режима.

 Одиночный режим
 -----------------------
В одиночном режиме достаточно подключиться одному игроку на порт 14000, после чего начнется игра, будут получены конфигурации и начнется выполнение команд, посылаемых клиентом. Второй игрок будет стоять на месте, или выполнять функцию бота.
Если во время игры произойдет подключение -- игра будет прервана и начнется следующая.

 Турнамент
 -----------------------
В турнамент режиме требуется два игрока (первый будет играть за левого, второй за правого робота). После подключения обоих начнется игра. Этот режим игры не может быть прерван другми подключениями, они будут класться в очередь.
Однако режим турнамента все еще может быть прерван кнопкой "back to main menu" непосредственно из юнити.


  Работа над проектом.
  ----------------------
Для того, чтоб начать работать вам необходимо в первую очередь сделать Fork в гитхабе.
После этого вы делаете Clone уже своего форкнутого проекта и делаете N коммитов туда (как сбилдить см. "Первый запуск").

Когда вы решили, что сделали определенную задачу и хотите влиться в основной репозиторий вам нужно закоммитить все ваши изменения.
Затем сделать пуш, а после этого, уже в интерфейсе github сделать pull request в главный репозиторий. (гуглите "pull request github")

===============================================

  Первый запуск.
  ------------------
Для запуска CVARC.Basic:
 1. Запустить CVARC.V2.sln. Вылезет уведомление о неподгрузившихся проектах. Это нормально.
 2. Правой кнопкой на Solution в SolutionExplorer -> Enable NuGet Package Restore*.
 3. На неподгрузившиеся проекты правой кнопкой -> Load Project.
 4. Build.
  * Для юнити 2015 может надобиться совершить вместо второго пункта вот это:
  * Скачать c nuget.org все три файлика nuget.exe/nuget.config/nuget.targets и положить в папку %SolutionDir%\.nuget
 

Для запуска Cvarc.Unity:
 1. Запустить CVARC.Unity.sln
 2. Правой кнопкой на Solution -> Build Solution. Закрываем Visual Studio
 3. Открываем Unity, подгружаем Юнити-проект (для юнити 5 нужно выбрать "open", и выбрать папку "uCvarc/Unity")
 4. Кнопка плей для запуска.
 5. Для создания Солюшна с юнити кодом необходимо из юнити даблкликнуть на любой файл-скрипт. Он создаст uCvarc/Unity/Unity.sln

=================================

Этот репозиторий -- объедененная и почищенная версия двух старых. Все комменты и удаленные файлы вы можете найти по ссылкам в ранних ранних коммитах:

Юнити: https://github.com/fokychuk/cvarc-unity

Кварк: https://github.com/air-labs/cvarc

==================================

