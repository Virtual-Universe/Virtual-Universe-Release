/*
 * Copyright (c) Contributors, http://virtual-planets.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 * For an explanation of the license of each contributor and the content it 
 * covers please see the Licenses directory.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the Virtual Universe Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

namespace Universe.Modules.Web.Translators
{
    public class RussianTranslation : ITranslator
    {
        public string LanguageName
        {
            get { return "ru"; }
        }

        public string GetTranslatedString(string key)
        {
            switch (key)
            {
                // Generic
                case "No": return "Нет";
                case "Yes": return "да";
                case "Submit": return "Отправить";
                case "Accept": return "принимать";
                case "Save": return "Сохранить";
                case "FirstText": return "Первый";
                case "BackText": return "назад";
                case "NextText": return "следующий";
                case "LastText": return "Последний";
                case "CurrentPageText": return "Текущая страница";
                case "MoreInfoText": return "Больше информации";
                case "NoDetailsText": return "Никаких подробностей не найдено...";
                case "MoreInfo": return "Больше информации";

                case "ObjectNameText": return "объект";
                case "LocationText": return "Место нахождения";
                case "UUIDText": return "UUID";
                case "DetailsText": return "Описание";
                case "NotesText": return "Заметки";
                case "SaveUpdates": return "Сохранить обновления";
                case "ActiveText": return "активный";
                case "CheckedText": return "Проверено";
                case "CategoryText": return "категория";
                case "SummaryText": return "Резюме";

                // Status information
                case "GridStatus": return "Сетка Статус";
                case "Online": return "В сети";
                case "Offline": return "Не в сети";
                case "TotalUserCount": return "Всего пользователей";
                case "TotalRegionCount": return "Общий граф Регион";
                case "UniqueVisitors": return "Уникальные посетители Последние 30 дней";
                case "OnlineNow": return "Сейчас в сети";
                case "HyperGrid": return "HyperGrid (HG)";
                case "Voice": return "голос";
                case "Currency": return "валюта";
                case "Disabled": return "Отключено";
                case "Enabled": return "Включено";
                case "News": return "Новости";
                case "Region": return "Область";

                // User login
                case "Login": return "Авторизоваться";
                case "UserName": return "имя пользователя";
                case "UserNameText": return "имя пользователя";
                case "Password": return "пароль";
                case "PasswordText": return "пароль";
                case "PasswordConfirmation": return "Подтверждение пароля";
                case "ForgotPassword": return "Забыли пароль?";
                case "TypeUserNameToConfirm": return "Пожалуйста, введите имя пользователя этой учетной записи, чтобы подтвердить, что вы хотите удалить эту учетную запись";

                // Special windows
                case "SpecialWindowTitleText": return "Специальный Заголовок Окно информации";
                case "SpecialWindowTextText": return "Специальное информационное окно Текст";
                case "SpecialWindowColorText": return "Специальный InfoWindow Цвет";
                case "SpecialWindowStatusText": return "Особый статус информационного окна";
                case "WelcomeScreenManagerFor": return "Добро пожаловать менеджер экрана для";
                case "ChangesSavedSuccessfully": return "Изменения сохранены Успешно";

                // User registration
                case "AvatarNameText": return "Аватар Имя";
                case "AvatarScopeText": return "Аватар для Идентификатор области";
                case "FirstNameText": return "Твое имя";
                case "LastNameText": return "Ваша фамилия";
                case "UserAddressText": return "Ваш адресс";
                case "UserZipText": return "Ваш почтовый индекс";
                case "UserCityText": return "Ваш город";
                case "UserCountryText": return "Твоя страна";
                case "UserDOBText": return "Ваша дата рождения (месяц день год)";
                case "UserEmailText": return "Ваш адрес электронной почты";
                case "UserHomeRegionText": return "Главная область";
                case "RegistrationText": return "Аватар для регистрации";
                case "RegistrationsDisabled": return "Регистрации в настоящее время отключены, пожалуйста, попробуйте еще раз в ближайшее время";
                case "TermsOfServiceText": return "Условия использования";
                case "TermsOfServiceAccept": return "Вы принимаете условия предоставления услуг, как описано выше?";
                case "AvatarNameError": return "Вы не ввели имя аватара!";
                case "AvatarPasswordError": return "Пароль пуст или не соответствует!";
                case "AvatarEmailError": return " Адрес электронной почты необходим для восстановления пароля! ('Никто', если неизвестен)";
                case "AvatarNameSpacingError": return "Ваше имя аватара должно быть \"Имя  Фамилия\"!";

                // news
                case "OpenNewsManager": return "Откройте диспетчер новостей";
                case "NewsManager": return "Новости менеджер";
                case "EditNewsItem": return "пункт Редактировать новости";
                case "AddNewsItem": return "Добавить новый пункт новости";
                case "DeleteNewsItem": return "Удалить новость";
                case "NewsDateText": return "Новости Дата";
                case "NewsTitleText": return "Заголовок новости";
                case "NewsItemTitle": return "Название элемента новостей";
                case "NewsItemText": return "Текст элемента Новости";
                case "AddNewsText": return "Добавить Новости";
                case "DeleteNewsText": return "Удалить Новости";
                case "EditNewsText": return "Редактировать Новости";

                // User Profile
                case "UserProfileFor": return "Профиль пользователя Для";
                case "UsersGroupsText": return "Группы Регистрация";
                case "GroupNameText": return "группа";
                case "UsersPicksText": return "Выборы для";
                case "ResidentSince": return "Житель с";
                case "AccountType": return "тип аккаунта";
                case "PartnersName": return "Имя партнера";
                case "AboutMe": return "Обо мне";
                case "IsOnlineText": return "Статус пользователь";
                case "OnlineLocationText": return "Пользователь Расположение";

                // Region Information
                case "RegionInformationText": return "Регион информация";
                case "OwnerNameText": return "Имя владельца";
                case "RegionLocationText": return "Регион Расположение";
                case "RegionSizeText": return "Регион Размер";
                case "RegionNameText": return "Имя Регион";
                case "RegionTypeText": return "Тип Регион";
                case "RegionDelayStartupText": return "Задержка запуска скриптов";
                case "RegionPresetText": return "Регион Предустановленная";
                case "RegionTerrainText": return "Регион местности";
                case "ParcelsInRegionText": return "Посылки в регионе";
                case "ParcelNameText": return "Имя посылок";
                case "ParcelOwnerText": return "Имя Parcel владельца";

                // Region List
                case "RegionInfoText": return "Информация о регионе";
                case "RegionListText": return "Список область";
                case "RegionLocXText": return "Регион X";
                case "RegionLocYText": return "Регион Y";
                case "SortByLocX": return "Сортировать По регионам X";
                case "SortByLocY": return "Сортировать По регионам Y";
                case "SortByName": return "Сортировать по Имя Регион";
                case "RegionMoreInfo": return "Больше информации";
                case "RegionMoreInfoTooltips": return "Более подробную информацию о";
                case "OnlineUsersText": return "Пользователи на сайте";
                case "RegionOnlineText": return "Регион Статус";
                case "RegionMaturityText": return "Рейтинг Доступ";
                case "NumberOfUsersInRegionText": return "Количество пользователей в регионе";

                // Region manager
                case "Mainland": return "материк";
                case "Estate": return "имущество";
                case "FullRegion": return "Полный Регион";
                case "Homestead": return "усадьба";
                case "Openspace": return "Открытое пространство";
                case "Flatland": return "Плоская земля";
                case "Grassland": return "луг";
                case "Island": return "остров";
                case "Aquatic": return "водный";
                case "Custom": return "изготовленный на заказ";
                case "RegionPortText": return "Регион Порт";
                case "RegionVisibilityText": return "Видна соседей";
                case "RegionInfiniteText": return "Бесконечные область";
                case "RegionCapacityText": return "вместимость объекта Регион";

                // Main Menu Buttons
                case "MenuHome": return "Главная";
                case "MenuLogin": return "Авторизоваться";
                case "MenuLogout": return "Выйти";
                case "MenuRegister": return "регистр";
                case "MenuForgotPass": return "Забыли пароль";
                case "MenuNews": return "Новости";
                case "MenuWorld": return "Мир";
                case "MenuWorldMap": return "Карта мира";
                case "MenuRegion": return "Список область";
                case "MenuUser": return "пользователь";
                case "MenuOnlineUsers": return "Пользователи на сайте";
                case "MenuUserSearch": return "Пользователи Поиск";
                case "MenuRegionSearch": return "область поиска";
                case "MenuChat": return "чат";
                case "MenuHelp": return "Помогите";
                case "MenuViewerHelp": return "Телезритель Помощь";
                case "MenuChangeUserInformation": return "Изменение информации пользователя";
                case "MenuWelcomeScreenManager": return "Добро пожаловать менеджер экрана";
                case "MenuNewsManager": return "Новости менеджер";
                case "MenuUserManager": return "абонентская система управления";
                case "MenuFactoryReset": return "Сброс к заводским настройкам";
                case "ResetMenuInfoText": return "Сброс пунктов меню назад к наиболее обновленные значения по умолчанию";
                case "ResetSettingsInfoText": return "Сбрасывает параметры веб-интерфейса к наиболее обновленные значения по умолчанию";
                case "MenuPageManager": return "Менеджер страниц";
                case "MenuSettingsManager": return "WebUI Настройки";
                case "MenuManager": return "управление";
                case "MenuSettings": return "Настройки";
                case "MenuRegionManager": return "Регион-менеджер";
                case "MenuManagerSimConsole": return "Тренажер консоли";
                case "MenuPurchases": return "Закупки пользователей";
                case "MenuMyPurchases": return "Мои покупки";
                case "MenuTransactions": return "Операции пользователя";
                case "MenuMyTransactions": return "Мои сделки";
                case "MenuMyClassifieds": return "Мои объявления";
                case "MenuStatistics": return "Средство просмотра статистики";
                case "MenuGridSettings": return "Настройки сетки";

                // Main Menu Tooltips
                case "TooltipsMenuHome": return "Главная";
                case "TooltipsMenuLogin": return "Авторизоваться";
                case "TooltipsMenuLogout": return "Выйти";
                case "TooltipsMenuRegister": return "регистр";
                case "TooltipsMenuForgotPass": return "Забыли пароль";
                case "TooltipsMenuNews": return "Новости";
                case "TooltipsMenuWorld": return "Мир";
                case "TooltipsMenuWorldMap": return "Карта мира";
                case "TooltipsMenuUser": return "пользователь";
                case "TooltipsMenuOnlineUsers": return "Пользователи на сайте";
                case "TooltipsMenuUserSearch": return "Пользователи Поиск";
                case "TooltipsMenuRegionSearch": return "область поиска";
                case "TooltipsMenuChat": return "чат";
                case "TooltipsMenuViewerHelp": return "Телезритель Помощь";
                case "TooltipsMenuHelp": return "Помогите";
                case "TooltipsMenuChangeUserInformation": return "Изменение информации пользователя";
                case "TooltipsMenuWelcomeScreenManager": return "Добро пожаловать менеджер экрана";
                case "TooltipsMenuNewsManager": return "Новости менеджер";
                case "TooltipsMenuUserManager": return "абонентская система управления";
                case "TooltipsMenuFactoryReset": return "Сброс к заводским настройкам";
                case "TooltipsMenuPageManager": return "Менеджер страниц";
                case "TooltipsMenuSettingsManager": return "Диспетчер настроек";
                case "TooltipsMenuManager": return "Управление администратора";
                case "TooltipsMenuSettings": return "WebUI настройки";
                case "TooltipsMenuRegionManager": return "Регион создавать / редактировать";
                case "TooltipsMenuManagerSimConsole": return "Интернет консоли симулятор";
                case "TooltipsMenuPurchases": return "информация о покупке";
                case "TooltipsMenuTransactions": return "информация об операции";
                case "TooltipsMenuStatistics": return "Средство просмотра статистики";
                case "TooltipsMenuGridSettings": return "Настройки сетки";

                // Menu Region box
                case "MenuRegionTitle": return "Область";
                case "MenuParcelTitle": return "Посылки";
                case "MenuOwnerTitle": return "владелец";
                case "TooltipsMenuRegion": return "Регион Подробнее";
                case "TooltipsMenuParcel": return "Регион Посылки";
                case "TooltipsMenuOwner": return "Владелец недвижимости";

                // Menu Profile Box
                case "MenuProfileTitle": return "Профиль";
                case "MenuGroupTitle": return "групп";
                case "MenuPicksTitle": return "Выборы";
                case "MenuRegionsTitle": return "районы";
                case "TooltipsMenuProfile": return "Профиль пользователя";
                case "TooltipsMenuGroups": return "Группы пользователей";
                case "TooltipsMenuPicks": return "Выбор пользователя";
                case "TooltipsMenuRegions": return "Регионы пользователей";
                case "UserGroupNameText": return "Группа пользователей";
                case "PickNameText": return "Выберите имя";
                case "PickRegionText": return "Место нахождения";

                // Urls
                case "WelcomeScreen": return "экран приветствия";

                // Tooltips Urls
                case "TooltipsWelcomeScreen": return "экран приветствия";
                case "TooltipsWorldMap": return "Карта мира";

                // Style Switcher
                case "styles1": return "Default Minimalist";
                case "styles2": return "Light Degarde";
                case "styles3": return "Blue Night";
                case "styles4": return "Dark Degrade";
                case "styles5": return "Luminus";

                case "StyleSwitcherStylesText": return "Стили";
                case "StyleSwitcherLanguagesText": return "Языки";
                case "StyleSwitcherChoiceText": return "Выбор";

                // Language Switcher Tooltips
                case "en": return "English";
                case "fr": return "Français";
                case "de": return "Deutsch";
                case "it": return "Italiano";
                case "es": return "Español";
                case "nl": return "Nederlands";
                case "ru": return "Русский";

                // Index Page
                case "HomeText": return "Главная";
                case "HomeTextWelcome": return "Это наш новый виртуальный мир! Присоединяйтесь к нам сейчас, и сделать разницу!";
                case "HomeTextTips": return "Новые презентации";
                case "WelcomeToText": return "Добро пожаловать в";

                // World Map Page
                case "WorldMap": return "Карта мира";
                case "WorldMapText": return "Полноэкранный";

                // Chat Page
                case "ChatText": return "Чат поддержки";

                // Help Page
                case "HelpText": return "Помогите";
                case "HelpViewersConfigText": return "Конфигурация просмотра";

                case "AngstromViewer": return "Angstrom Viewer";
                case "AstraViewer": return "Astra Viewer";
                case "FirestormViewer": return "Firestorm Viewer";
                case "KokuaViewer": return "Kokua Viewer";
                case "ImprudenceViewer": return "Imprudence Viewer";
                case "PhoenixViewer": return "Phoenix Viewer";
                case "SingularityViewer": return "Singularity Viewer";
                case "VoodooViewer": return "Voodoo Viewer";
                case "ZenViewer": return "Zen Viewer";

                //Logout page
                case "Logout": return "Выйти";
                case "LoggedOutSuccessfullyText": return "Вы вышли из системы успешно.";

                //Change user information page
                case "ChangeUserInformationText": return "Изменение информации пользователя";
                case "ChangePasswordText": return "Изменить пароль";
                case "NewPasswordText": return "новый пароль";
                case "NewPasswordConfirmationText": return "Новый пароль (подтверждение)";
                case "ChangeEmailText": return "Изменить адрес электронной почты";
                case "NewEmailText": return "Новый адрес электронной почты";
                case "DeleteUserText": return "Удалите мой аккаунт";
                case "DeleteText": return "Удалить";
                case "DeleteUserInfoText":
                    return "Это удалит всю информацию о вас в сетке и удалить доступ к этой услуге. Если вы хотите продолжить, введите имя и пароль и нажмите кнопку Удалить.";
                case "EditText": return "редактировать";
                case "EditUserAccountText": return "Редактирование учетных записей пользователей";

                //Maintenance page
                case "WebsiteDownInfoText": return "Сайт в настоящее время вниз, пожалуйста, попробуйте еще раз в ближайшее время.";
                case "WebsiteDownText": return "Сайт форума";

                //http_404 page
                case "Error404Text": return "Код ошибки";
                case "Error404InfoText": return "404 Страница не найдена";
                case "HomePage404Text": return "домашняя страница";

                //http_505 page
                case "Error505Text": return "Код ошибки";
                case "Error505InfoText": return "505 Внутренняя ошибка сервера";
                case "HomePage505Text": return "домашняя страница";

                //user_search page
                case "Search": return "Поиск";
                case "SearchText": return "Поиск";
                case "SearchForUserText": return "Поиск пользователя";
                case "UserSearchText": return "Пользователи Поиск";
                case "SearchResultForUserText": return "Результат поиска для пользователя";

                //region_search page
                case "SearchForRegionText": return "Поиск для региона";
                case "RegionSearchText": return "область поиска";
                case "SearchResultForRegionText": return "Результат поиска для региона";

                //Edit user page
                case "AdminDeleteUserText": return "Удалить пользователя";
                case "AdminDeleteUserInfoText": return "Это удаляет учетную запись и уничтожает всю информацию, связанную с ним.";
                case "BanText": return "запрет";
                case "UnbanText": return "Разбанить";
                case "AdminTempBanUserText": return "Временный запрет пользователя";
                case "AdminTempBanUserInfoText": return "Это блокирует пользователей от входа в систему в течение заданного промежутка времени.";
                case "AdminBanUserText": return "Пан Пользователь";
                case "AdminBanUserInfoText": return "Это блокирует пользователей от входа в систему, пока пользователь не Unbanned.";
                case "AdminUnbanUserText": return "Разбанить пользователя";
                case "AdminUnbanUserInfoText": return "Удаляет временные и постоянные запреты на пользователя.";
                case "AdminLoginInAsUserText": return "Войти как пользователь";
                case "AdminLoginInAsUserInfoText":
                    return "Вы будете зарегистрированы из вашей учетной записи администратора, и вошли в систему под именем этого пользователя, и будет видеть все, как они видят это";
                case "TimeUntilUnbannedText": return "Время, пока пользователь не Unbanned";
                case "DaysText": return "дней";
                case "HoursText": return "часов";
                case "MinutesText": return "минут";
                case "EdittingText": return "редактирование";
                case "BannedUntilText": return "Пользователь не запрещена до:";
                case "KickAUserText": return "Кик Пользователь";
                case "KickAUserInfoText": return "Стартует пользователь из сетки (регистрирует их в течение 30 секунд)";
                case "KickMessageText": return "Обращение к пользователю";
                case "KickUserText": return "Кик Пользователь";
                case "MessageAUserText": return "Отправить сообщение пользователю";
                case "MessageAUserInfoText": return "Посылает пользователю сообщение синий ящик (будет поступать в течение 30 секунд)";
                case "MessageUserText": return "Сообщение пользователя";

                // Transactions
                case "TransactionsText": return "операции";
                case "DateInfoText": return "Выберите диапазон дат";
                case "DateStartText": return "дата начала";
                case "DateEndText": return "Дата окончания";
                case "30daysPastText": return "Предыдущие 30 дней";
                case "TransactionToAgentText": return "Для пользователя";
                case "TransactionFromAgentText": return "От пользователя";
                case "TransactionDateText": return "Дата";
                case "TransactionDetailText": return "Описание";
                case "TransactionAmountText": return "Количество";
                case "TransactionBalanceText": return "Баланс";
                case "NoTransactionsText": return "Нет сделок не найдено...";
                case "PurchasesText": return "Покупки";
                case "LoggedIPText": return "Зарегистрированный IP-адрес";
                case "NoPurchasesText": return "Не найдено ни одной покупки...";
                case "PurchaseCostText": return "Стоимость";

                // Classifieds
                case "ClassifiedsText": return "Объявления";

                // Sim Console
                case "SimConsoleText": return "Да командной консоли";
                case "SimCommandText": return "команда";

                //factory_reset
                case "FactoryReset": return "Сброс к заводским настройкам";
                case "ResetMenuText": return "Меню Сброс заводских настроек по умолчанию";
                case "ResetSettingsText": return "Сбросить настройки Web (страницы Диспетчер параметров) заводских настроек по умолчанию";
                case "Reset": return "Сброс";
                case "Settings": return "настройки";
                case "Pages": return "страницы";
                case "DefaultsUpdated": return "по умолчанию обновляются, перейдите Factory Reset, чтобы обновить или Settings Manager, чтобы отключить это предупреждение.";

                //page_manager
                case "PageManager": return "Менеджер страниц";
                case "SaveMenuItemChanges": return "Пункт меню Сохранить";
                case "SelectItem": return "Выберите предмет";
                case "DeleteItem": return "Удалить пункт";
                case "AddItem": return "Добавить элемент";
                case "PageLocationText": return "Расположение страницы";
                case "PageIDText": return "Идентификатор страницы";
                case "PagePositionText": return "Страница Позиция";
                case "PageTooltipText": return "Страница подсказке";
                case "PageTitleText": return "Заголовок страницы";
                case "RequiresLoginText": return "Требуется пароль, чтобы просмотреть";
                case "RequiresLogoutText": return "Требуется Выход для просмотра";
                case "RequiresAdminText": return "Требуется Администратор для просмотра";
                case "RequiresAdminLevelText": return "Необходимый уровень администратора для просмотра";

                // grid settings
                case "GridSettingsManager": return "Сетка Settings Manager";
                case "GridnameText": return "название сетки";
                case "GridnickText": return "Сетка ник";
                case "WelcomeMessageText": return "Вход приветственное сообщени";
                case "GovernorNameText": return "система управления";
                case "MainlandEstateNameText": return "Материковый недвижимости";
                case "RealEstateOwnerNameText": return "Владелец системы недвижимости";
                case "SystemEstateNameText": return "система недвижимости";
                case "BankerNameText": return "система банкира";
                case "MarketPlaceOwnerNameText": return "Владелец системы Торговая площадка";

                //settings manager page
                case "WebRegistrationText": return "Веб-регистрация допускается";
                case "GridCenterXText": return "Сетка Центр Расположение X";
                case "GridCenterYText": return "Сетка Центр Расположение Y";
                case "SettingsManager": return "Диспетчер настроек";
                case "IgnorePagesUpdatesText": return "Игнорировать предупреждения стр обновления до следующего обновления";
                case "IgnoreSettingsUpdatesText": return "Игнорировать предупреждения настроек обновления до следующего обновления";
                case "HideLanguageBarText": return "Скрыть панель выбора языка";
                case "HideStyleBarText": return "Скрыть панель выбора стиля";
                case "HideSlideshowBarText": return "Скрыть панель слайд-шоу";
                case "LocalFrontPageText": return "Местные Главная страница";
                case "LocalCSSText": return "Местных стилей CSS";

                // statistics
                case "StatisticsText": return "Средство просмотра статистики";
                case "ViewersText": return "Средство просмотра использования";
                case "GPUText": return "Видеокарты";
                case "PerformanceText": return "средние производительности";
                case "FPSText": return "Кадров / сек";
                case "RunTimeText": return "время работы";
                case "RegionsVisitedText": return "Регионы посетили";
                case "MemoryUseageText": return "использование памяти";
                case "PingTimeText": return "Пинг времени";
                case "AgentsInViewText": return "Агенты в поле зрения";
                case "ClearStatsText": return "Очищать данные статистики";

                // abuse reports
                case "MenuAbuse": return "Злоупотребление Отчеты";
                case "TooltipsMenuAbuse": return "Отчеты о нарушении Пользователь";
                case "AbuseReportText": return "Сообщение о нарушении";
                case "AbuserNameText": return "Недостоверная";
                case "AbuseReporterNameText": return "репортер";
                case "AssignedToText": return "Назначена";

                //Times
                case "Sun":
                    return "солнце";
                case "Mon":
                    return "понедельник";
                case "Tue":
                    return "вторник";
                case "Wed":
                    return "Мы б";
                case "Thu":
                    return "четверг";
                case "Fri":
                    return "пятница";
                case "Sat":
                    return "Сидел";
                case "Sunday":
                    return "Воскресенье";
                case "Monday":
                    return "понедельник";
                case "Tuesday":
                    return "вторник";
                case "Wednesday":
                    return "среда";
                case "Thursday":
                    return "Четверг";
                case "Friday":
                    return "пятница";
                case "Saturday":
                    return "суббота";

                case "Jan_Short":
                    return "январь";
                case "Feb_Short":
                    return "февраль";
                case "Mar_Short":
                    return "март";
                case "Apr_Short":
                    return "апрель";
                case "May_Short":
                    return "май";
                case "Jun_Short":
                    return "июнь";
                case "Jul_Short":
                    return "июль";
                case "Aug_Short":
                    return "август";
                case "Sep_Short":
                    return "сентябрь";
                case "Oct_Short":
                    return "октябрь";
                case "Nov_Short":
                    return "ноябрь";
                case "Dec_Short":
                    return "декабрь";

                case "January":
                    return "январь";
                case "February":
                    return "февраль";
                case "March":
                    return "Март";
                case "April":
                    return "апрель";
                case "May":
                    return "май";
                case "June":
                    return "июнь";
                case "July":
                    return "июль";
                case "August":
                    return "август";
                case "September":
                    return "сентябрь";
                case "October":
                    return "октября";
                case "November":
                    return "ноябрь";
                case "December":
                    return "Декабрь";

                // User types
                case "UserTypeText":
                    return "Тип пользователя";
                case "AdminUserTypeInfoText":
                    return "Тип пользователя (в настоящее время используется для периодических платежей стипендиатов).";
                case "Guest":
                    return "гость";
                case "Resident":
                    return "резидент";
                case "Member":
                    return "член";
                case "Contractor":
                    return "подрядчик";
                case "Charter_Member":
                    return "Устав Участник";

                // ColorBox
                case "ColorBoxImageText":
                    return "Образ";
                case "ColorBoxOfText":
                    return "из";
                case "ColorBoxPreviousText":
                    return "предыдущий";
                case "ColorBoxNextText":
                    return "следующий";
                case "ColorBoxCloseText":
                    return "Закрыть";
                case "ColorBoxStartSlideshowText":
                    return "Начать показ слайдов";
                case "ColorBoxStopSlideshowText":
                    return "Остановка слайд-шоу";

                // Maintenance
                case "NoAccountFound":
                    return "Ни один счет не найдено";
                case "DisplayInMenu":
                    return "Дисплей В меню";
                case "ParentText":
                    return "Меню родитель";
                case "CannotSetParentToChild":
                    return "Невозможно установить пункт меню, как ребенок к себе.";
                case "TopLevel":
                    return "Верхний уровень";
            }

            return "UNKNOWN CHARACTER";
        }
    }
}