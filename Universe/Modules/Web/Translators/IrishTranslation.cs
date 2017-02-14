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

using System.Collections.Generic;

namespace Universe.Modules.Web.Translators
{
	public class IrishTranslation : ITranslator
	{
		public string LanguageName {
			get { return "ga"; }
		}

		public string GetTranslatedString (string key)
		{
			if (dictionary.ContainsKey (key))
				return dictionary [key];
			return ":" + key + ":";
		}

		readonly Dictionary<string, string> dictionary = new Dictionary<string, string> {
			// Generic
			{ "No", "Níl" },
			{ "Yes", "Is ea" },
			{ "Submit", "Cuir" },
			{ "Accept", "Glac" },
			{ "Save", "Sábháil" },
			{ "Cancel", "Cealaigh" },
			{ "FirstText", "An chéad" },
			{ "BackText", "Ar ais" },
			{ "NextText", "Ar Aghaidh" },
			{ "LastText", "Last" },
			{ "CurrentPageText", "Leathanach Reatha" },
			{ "MoreInfoText", "Tuilleadh eolais" },
			{ "NoDetailsText", "Aon sonraí aimsíodh..." },
			{ "MoreInfo", "Tuilleadh eolais" },
			{ "Name", "Ainm" },
			{ "ObjectNameText", "réad" },
			{ "LocationText", "suíomh" },
			{ "UUIDText", "UUID" },
			{ "DetailsText", "Tuairisc" },
			{ "NotesText", "Nóta" },
			{ "SaveUpdates", "Sábháil nuashonruithe" },
			{ "ActiveText", "Gníomhach" },
			{ "CheckedText", "seiceáilte" },
			{ "CategoryText", "Catagóir" },
			{ "SummaryText", "Achoimre" },
			{ "MaturityText", "aibíocht" },
			{ "DateText", "dáta" },
			{ "TimeText", "Am" },
			{ "MinuteText", "nóiméad" },
			{ "MinutesText", "nóiméad" },
			{ "HourText", "uair an chloig" },
			{ "HoursText", "uaireanta" },
			{ "EditText", "Edit" },
			{ "EdittingText", "eagarthóireacht" },

			// Status information
			{ "GridStatus", "Stádas greille" },
			{ "Online", "Líne" },
			{ "Offline", "líne" },
			{ "TotalUserCount", "Úsáideoirí Iomlán" },
			{ "TotalRegionCount", "Iomlán Líon Réigiún" },
			{ "UniqueVisitors", "caite Cuairteoirí uathúla 30 lá" },
			{ "OnlineNow", "Líne Anois" },
			{ "InterWorld", "Idir an Domhain (IWC)" },
			{ "HyperGrid", "HyperGrid (HG)" },
			{ "Voice", "guth" },
			{ "Currency", "airgeadra" },
			{ "Disabled", "Faoi mhíchumas" },
			{ "Enabled", "cumasaithe" },
			{ "News", "Nuacht" },
			{ "Region", "Réigiún" },

			// User login
			{ "Login", "Logáil isteach" },
			{ "UserName", "Ainm Úsáideora" },
			{ "UserNameText", "Ainm Úsáideora" },
			{ "Password", "Focal Faire" },
			{ "PasswordText", "Focal Faire" },
			{ "PasswordConfirmation", "Pasfhocal Deimhniú" },
			{ "ForgotPassword", "Dearmad ar pasfhocal?" }, 
			{ "TypeUserNameToConfirm", "Iontráil an ainm úsáideora ar an gcuntas a dhearbhú gur mian leat an cuntas seo a scriosadh"},

            // Special windows
			{ "SpecialWindowTitleText", "Speisialta Teideal Info Fuinneog"},
			{ "SpecialWindowTextText", "Info Fuinneog Speisialta Téacs"},
			{ "SpecialWindowColorText", "Speisialta Infowindow Dath"},
			{ "SpecialWindowStatusText", "Speisialta Stádas Info Fuinneog"},
			{ "WelcomeScreenManagerFor", "Bainisteoir Scáileán Fáilte Do"},
			{ "ChangesSavedSuccessfully", "Athruithe Sábháilte éirigh"},

            // User registration
			{ "AvatarNameText", "Avatar Ainm"},
			{ "AvatarScopeText", "ID Scóip Avatar"},
			{ "FirstNameText", "Do Chéad Ainm"},
			{ "LastNameText", "D'Sloinne"},
			{ "UserAddressText", "do Seoladh"},
			{ "UserZipText", "Do Cód Zip"},
			{ "UserCityText", "do Chathair"},
			{ "UserCountryText", "Do thír"},
			{ "UserDOBText", "Do Dáta Breithe (Mí Lá Caille)"},
			{ "UserEmailText", "Do Ríomhphost"},
			{ "UserHomeRegionText", "réigiún Baile"},
			{ "RegistrationText", "clárú Avatar"},
			{ "RegistrationsDisabled", "Clárúcháin feidhm faoi láthair, le do thoil bain triail eile as go luath."},
			{ "TermsOfServiceText", "Tearmaí Seirbhís"},
			{ "TermsOfServiceAccept", "An bhfuil tú ag glacadh leis na Téarmaí Seirbhíse mar atá sonraithe thuas?"},
			{ "AvatarNameError", "Níor iontráil tú ainm avatar!"},
			{ "AvatarPasswordError", "Focal faire folamh nó nach mheaitseáil!"},
			{ "AvatarEmailError", "Tá seoladh ríomhphoist ag teastáil do phasfhocal a ghnóthú! ('None' más anaithnid"},
			{ "AvatarNameSpacingError", "Ba chóir D'ainm avatar a \"Ainm Sloinne\"!"},

            // News
			{ "OpenNewsManager", "Oscail an bainisteoir nuachta"},
			{ "NewsManager", "Bainisteoir Nuacht"},
			{ "EditNewsItem", "mhír nuachta Edit"},
			{ "AddNewsItem", "Cuir mhír nuachta nua"},
			{ "DeleteNewsItem", "Scrios mír nuachta"},
			{ "NewsDateText", "Nuacht Dáta"},
			{ "NewsTitleText", "News Teideal"},
			{ "NewsItemTitle", "Teideal News Item"},
			{ "NewsItemText", "News Item Téacs"},
			{ "AddNewsText", "Cuir Nuacht"},
			{ "DeleteNewsText", "Scrios Nuacht"},
			{ "EditNewsText", "Cuir Nuacht"},

            // User Profile
			{ "UserProfileFor", "Próifíl Úsáideora Do"},
			{ "UsersGroupsText", "grúpaí Joined"},
			{ "GroupNameText", "Grúpa"},
			{ "UsersPicksText", "picks do"},
			{ "ResidentSince", "Cónaitheach Ós"},
			{ "AccountType", "Cineál Cuntas"},
			{ "PartnersName", "Ainm Pháirtí"},
			{ "AboutMe", "Mar gheall orm"},
			{ "IsOnlineText", "Stádas Úsáideora"},
			{ "OnlineLocationText", "Úsáideoir Suíomh"},
			{ "Partner", "Comhpháirtí"},
			{ "Friends", "Cairde"},
			{ "Nothing", "Ar bith"},
			{ "ChangePass", "Athraigh do phasfhocal"},
			{ "NoChangePass", "Ní féidir a athrú ar an focal faire, le do thoil bain triail eile as amach anseo"},

            // Region Information
			{ "RegionInformationText", "Eolas Réigiún"},
			{ "OwnerNameText", "úinéir Ainm"},
			{ "RegionLocationText", "Réigiún Suíomh"},
			{ "RegionSizeText", "Réigiún Méid"},
			{ "RegionNameText", "Réigiún Ainm"},
			{ "RegionTypeText", "Réigiún Cineál"},
			{ "RegionPresetTypeText", "Réigiún Réamhshocraithe Cineál"},
			{ "RegionDelayStartupText", "Moill scripteanna tosú"},
			{ "RegionPresetText", "Réigiún Réamhshocraithe"},
			{ "RegionTerrainText", "Réigiún tír-raon"},
			{ "ParcelsInRegionText", "Parcels I Réigiún"},
			{ "ParcelNameText", "Beartán Ainm"},
			{ "ParcelOwnerText", "Ainm Beartán úinéara"},

            // Region List
			{ "RegionInfoText", "Réigiún Info"},
			{ "RegionListText", "Liosta réigiún"},
			{ "RegionLocXText", "Réigiún X"},
			{ "RegionLocYText", "Réigiún Y"},
			{ "SortByLocX", "Sórtáil De Réigiún X"},
			{ "SortByLocY", "Sórtáil De Réigiún Y"},
			{ "SortByName", "Sort By Name Réigiún"},
			{ "RegionMoreInfo", "Tuilleadh eolais"},
			{ "RegionMoreInfoTooltips", "Tuilleadh eolais faoi"},
			{ "OnlineUsersText", "Úsáideoirí Ar Líne"},
			{ "OnlineFriendsText", "Cairde Líne"},
			{ "RegionOnlineText", "Stádas Réigiún"},
			{ "RegionMaturityText", "rochtain Rátáil"},
			{ "NumberOfUsersInRegionText", "Líon na n Úsáideoirí sa réigiún"},

            // Region manager
			{ "AddRegionText", "Cuir Réigiún"},
			{ "Mainland", "Mórthír"},
			{ "Estate", "eastát"},
			{ "FullRegion", "Réigiún iomlána"},
            { "Homestead", "Homestead"},
            { "Openspace", "Openspace"},
            { "Flatland", "flatland"},
			{ "Grassland", "Féarach"},
			{ "Island", "oileán"},
			{ "Aquatic", "uisceach"},
			{ "Custom", "Saincheaptha"},
			{ "RegionPortText", "Réigiún Port"},
			{ "RegionVisibilityText", "Infheicthe do na comharsana"},
			{ "RegionInfiniteText", "Réigiún infinite"},
			{ "RegionCapacityText", "Cumas réad Réigiún"},
			{ "NormalText", "gnáth"},
			{ "DelayedText", "Moillithe"},

            // Estate management
			{"AddEstateText", "Cuir Eastát"},
			{"EstateText", "eastát"},
			{"EstatesText", "Eastáit"},
			{"PricePerMeterText", "Praghas in aghaidh an mhéadair chearnaigh"},
			{"PublicAccessText", "rochtain phoiblí"},
			{"AllowVoiceText", "Ceadaigh guth"},
			{"TaxFreeText", "Saor ó cháin"},
			{"AllowDirectTeleportText", "Ceadaigh teleporting díreach"},

            // Menus 
			{ "MenuHome", "Baile"},
			{ "MenuLogin", "Logáil isteach"},
			{ "MenuLogout", "Logáil Amach"},
			{ "MenuRegister", "clár"},
			{ "MenuForgotPass", "Dearmad ar pasfhocal"},
			{ "MenuNews", "Nuacht"},
			{ "MenuWorld", "Domhanda"},
			{ "MenuWorldMap", "Léarscáil an Domhain"},
			{ "MenuRegion", "Liosta réigiún"},
			{ "MenuUser", "Úsáideoir"},
			{ "MenuOnlineUsers", "Úsáideoirí Ar Líne"},
			{ "MenuUserSearch", "Úsáideoir Search"},
			{ "MenuRegionSearch", "Réigiún Search"},
			{ "MenuChat", "Comhrá"},
			{ "MenuHelp", "Cabhrú"},
			{ "MenuViewerHelp", "amharcán Cabhair"},
			{ "MenuChangeUserInformation", "Faisnéis Athraigh Úsáideoir"},
			{ "MenuWelcomeScreenManager", "Bainisteoir Scáileán Fáilte"},
			{ "MenuNewsManager", "Bainisteoir Nuacht"},
			{ "MenuUserManager", "Bainisteoir Úsáideora"},
			{ "MenuFactoryReset", "Monarcha Athshocraigh"},
			{ "ResetMenuInfoText", "Resets na míreanna roghchlár ar ais go dtí na mainneachtainí mó suas chun dáta"},
			{ "ResetSettingsInfoText", "Resets an suímh Chomhéadain Gréasáin ar ais go dtí na mainneachtainí mó suas chun dáta"},
			{ "MenuPageManager", "Bainisteoir leathanach"},
			{ "MenuSettingsManager", "Socruithe WebUI"},
			{ "MenuManager", "Bainistíocht"},
			{ "MenuSettings", "Socruithe"},
			{ "MenuRegionManager", "Bainisteoir Réigiún"},
			{ "MenuEstateManager", "Bainisteoir eastát"},
            { "MenuManagerSimConsole", "Simulator console"},
			{ "MenuPurchases", "Ceannacháin Úsáideora"},
			{ "MenuMyPurchases", "Mo Ceannacháin"},
			{ "MenuTransactions", "Idirbhearta Úsáideoir"},
			{ "MenuMyTransactions", "Mo Idirbhearta"},
            { "MenuClassifieds", "Classifieds"},
            { "MenuMyClassifieds", "Mo Classifieds"},
			{ "MenuEvents", "Imeachtaí"},
			{ "MenuMyEvents", "Mo Imeachtaí"},
			{ "MenuStatistics", "Staitisticí Viewer"},
			{ "MenuGridSettings", "Socruithe greille"},

            // Menu Tooltips
			{ "TooltipsMenuHome", "Baile"},
			{ "TooltipsMenuLogin", "Logáil isteach"},
			{ "TooltipsMenuLogout", "Logáil Amach"},
			{ "TooltipsMenuRegister", "clár"},
			{ "TooltipsMenuForgotPass", "Dearmad ar pasfhocal"},
			{ "TooltipsMenuNews", "Nuacht"},
			{ "TooltipsMenuWorld", "Domhanda"},
			{ "TooltipsMenuWorldMap", "Léarscáil an Domhain"},
			{ "TooltipsMenuUser", "Úsáideoir"},
			{ "TooltipsMenuOnlineUsers", "Úsáideoirí Ar Líne"},
			{ "TooltipsMenuUserSearch", "Úsáideoir Search"},
			{ "TooltipsMenuRegionSearch", "Réigiún Search"},
			{ "TooltipsMenuChat", "Comhrá"},
			{ "TooltipsMenuViewerHelp", "amharcán Cabhair"},
			{ "TooltipsMenuHelp", "Cabhrú"},
			{ "TooltipsMenuChangeUserInformation", "Faisnéis Athraigh Úsáideoir"},
			{ "TooltipsMenuWelcomeScreenManager", "Bainisteoir Scáileán Fáilte"},
			{ "TooltipsMenuNewsManager", "Bainisteoir Nuacht"},
			{ "TooltipsMenuUserManager", "Bainisteoir Úsáideora"},
			{ "TooltipsMenuFactoryReset", "Monarcha Athshocraigh"},
			{ "TooltipsMenuPageManager", "Bainisteoir leathanach"},
			{ "TooltipsMenuSettingsManager", "Bainisteoir Socruithe"},
			{ "TooltipsMenuManager", "Bainistíocht Riarachán"},
			{ "TooltipsMenuSettings", "Socruithe WebUI"},
			{ "TooltipsMenuRegionManager", "Réigiún chruthú / in eagar"},
			{ "TooltipsMenuEstateManager", "bainistíocht eastáit"},
			{ "TooltipsMenuManagerSimConsole", "Líne Insamhlóir console"},
			{ "TooltipsMenuPurchases", "eolas Ceannach"},
			{ "TooltipsMenuTransactions", "eolas Idirbheart"},
			{ "TooltipsMenuClassifieds", "Classifieds eolais"},
			{ "TooltipsMenuEvents", "eolas Imeacht"},
			{ "TooltipsMenuStatistics", "Staitisticí Viewer"},
			{ "TooltipsMenuGridSettings", "socruithe greille"},

            // Menu Region box
			{ "MenuRegionTitle", "Réigiún"},
            { "MenuParcelTitle", "Parcels"},
			{ "MenuOwnerTitle", "úinéir"},
			{ "TooltipsMenuRegion", "Réigiún Mionsonraí"},
			{ "TooltipsMenuParcel", "Beartáin Réigiún"},
			{ "TooltipsMenuOwner", "eastát Úinéir"},

            // Menu Profile Box
			{ "MenuProfileTitle", "Próifíl"},
			{ "MenuGroupTitle", "grúpaí"},
            { "MenuPicksTitle", "Picks"},
			{ "MenuRegionsTitle", "réigiúin"},
			{ "TooltipsMenuProfile", "Próifíl Úsáideora"},
			{ "TooltipsMenuGroups", "Grúpaí Úsáideoirí"},
            { "TooltipsMenuPicks", "User Selections"},
			{ "TooltipsMenuRegions", "Réigiúin Úsáideora"},
			{ "UserGroupNameText", "grúpa Úsáideora"},
			{ "PickNameText", "Pioc ainm"},
			{ "PickRegionText", "suíomh"},

            // Urls
			{ "WelcomeScreen", "Fáilte Scáileán"},

            // Tooltips Urls
			{ "TooltipsWelcomeScreen", "Fáilte Scáileán"},
			{ "TooltipsWorldMap", "Léarscáil an Domhain"},

            // Index
			{ "HomeText", "Baile"},
			{ "HomeTextWelcome", "Is é seo ár Virtual Domhanda Nua! Bí linn anois, agus difríocht!"},
			{ "HomeTextTips", "cur i láthair nua"},
			{ "WelcomeToText", "Fáilte go"},

            // World Map
			{ "WorldMap", "Léarscáil an Domhain"},
			{ "WorldMapText", "Scáileán iomlán"},

            // Chat
			{ "ChatText", "Tacaíocht comhrá"},

            // Help
			{ "HelpText", "Cabhrú"},
			{ "HelpViewersConfigText", "amharcán Cumraíocht"},

            // Logout
			{ "Logout", "Logáil Amach"},
			{ "LoggedOutSuccessfullyText", "Bhí tú ag logáilte amach go rathúil."},

            // Change user information page
			{ "ChangeUserInformationText", "Faisnéis Athraigh Úsáideoir"},
			{ "ChangePasswordText", "Athraigh do phasfhocal"},
			{ "NewPasswordText", "Focal Faire Nua"},
			{ "NewPasswordConfirmationText", "Pasfhocal Nua (Daingniú)"},
			{ "ChangeEmailText", "Athraigh Seoladh Ríomhphoist"},
			{ "NewEmailText", "New Seoladh Ríomhphoist"},
			{ "DeleteUserText", "Scrios Mo Chuntas"},
			{ "DeleteText", "Scrios"},
            { "DeleteUserInfoText",
				"Bainfidh sé seo gach eolas fút sa ghreille agus bain do rochtain ar an tseirbhís. Más mian leat dul ar aghaidh, cuir isteach d'ainm agus do phasfhocal agus cliceáil Scrios."},
			{ "EditUserAccountText", "Cuntas Cuir Úsáideora"},

            // Maintenance
			{ "WebsiteDownInfoText", "Suíomh Gréasáin síos faoi láthair, le do thoil bain triail eile as go luath"},
			{ "WebsiteDownText", "láithreán offline"},

            // Http 404
			{ "Error404Text", "cód earráid"},
			{ "Error404InfoText", "404 Page Gan Aimsiú"},
			{ "HomePage404Text", "leathanach Baile"},

            // Http 505
			{ "Error505Text", "cód earráid"},
			{ "Error505InfoText", "505 Earráid Freastalaí Inmheánach"},
			{ "HomePage505Text", "leathanach Baile"},

            // User search
            { "Search", "Search"},
            { "SearchText", "Search"},
			{ "SearchForUserText", "Cuardaigh Chun A Úsáideoir"},
			{ "UserSearchText", "Úsáideoir Search"},
			{ "SearchResultForUserText", "Toradh Cuardaigh Do Úsáideoir"},

            // Region search
			{ "SearchForRegionText", "Cuardaigh Chun A Réigiún"},
			{ "RegionSearchText", "Réigiún Search"},
			{ "SearchResultForRegionText", "Toradh Cuardaigh Do Réigiún"},

            // Edit user
			{ "AdminDeleteUserText", "Scrios Úsáideoir"},
			{ "AdminDeleteUserInfoText", "Scrios sé seo sa chuntas agus milleann fhaisnéis go léir a bhaineann leis."},
            { "BanText", "Ban"},
            { "UnbanText", "Unban"},
			{ "AdminTempBanUserText", "Temp Ban Úsáideora"},
			{ "AdminTempBanUserInfoText", "Seo bloic an t-úsáideoir ó logáil ar an méid atá leagtha ama."},
			{ "AdminBanUserText", "Ban Úsáideoir"},
			{ "AdminBanUserInfoText", "bloic sé seo an t-úsáideoir ó logáil isteach go dtí go bhfuil an t-úsáideoir unbanned."},
			{ "AdminUnbanUserText", "Unban Úsáideoir"},
			{ "AdminUnbanUserInfoText", "Bain toirmeasc sealadach agus buan ar an úsáideoir."},
			{ "AdminLoginInAsUserText", "Logáil isteach mar úsáideora"},
            { "AdminLoginInAsUserInfoText",
				"Beidh tú a bheith logáilte amach as do chuntas admin, agus logáilte isteach mar an t-úsáideoir, agus beidh gach rud a fheiceáil mar a fheiceann siad é."},
			{ "TimeUntilUnbannedText", "Am go dtí go bhfuil úsáideoir unbanned"},
			{ "BannedUntilText", "Úsáideoir cosc dtí:"},
			{ "KickAUserText", "Ciceáil Úsáideora"},
			{ "KickAUserInfoText", "Thosaíonn úsáideoir as an eangach (logs iad amach taobh istigh de 30 soicind)"},
			{ "KickMessageText", "Teachtaireacht Go Úsáideora"},
			{ "KickUserText", "Ciceáil Úsáideora"},
			{ "MessageAUserText", "Seol Úsáideora Teachtaireacht"},
			{ "MessageAUserInfoText", "Seolann úsáideoir teachtaireacht gorm-bhosca (a thagann laistigh de 30 soicind)"},
			{ "MessageUserText", "Teachtaireacht Úsáideora"},

            // Transactions
			{ "TransactionsText", "Idirbhearta"},
			{ "DateInfoText", "Roghnaigh réimse dáta"},
			{ "DateStartText", "Ag tosú Dáta"},
			{ "DateEndText", "cur deireadh Dáta"},
			{ "30daysPastText", "Roimhe Seo 30 lá"},
			{ "TransactionToAgentText", "Go Úsáideora"},
			{ "TransactionFromAgentText", "ó Úsáideora"},
			{ "TransactionDateText", "dáta"},
			{ "TransactionDetailText", "Tuairisc"},
			{ "TransactionAmountText", "méid"},
			{ "TransactionBalanceText", "Iarmhéid"},
			{ "NoTransactionsText", "Aon idirbheart aimsíodh..."},
			{ "PurchasesText", "ceannacháin"},
			{ "LoggedIPText", "Logged seoladh IP"},
			{ "NoPurchasesText", "Gan ceannacháin aimsíodh..."},
			{ "PurchaseCostText", "costas"},
            
            // Classifieds
			{ "ClassifiedsText", "Rangaithe"},
			{ "ClassifiedText", "Rangaithe"},
			{ "ListedByText", "liostaithe ag"},
            { "CreationDateText", "Added"},
			{ "ExpirationDateText", "Dhul in éag"},
			{ "DescriptionText", "Tuairisc"},
			{ "PriceOfListingText", "Praghas"},

            // Classified categories
			{ "CatAll", "Gach"},
            { "CatSelect", ""},
			{ "CatShopping", "Siopadóireacht"},
			{ "CatLandRental", "Cíos talún"},
			{ "CatPropertyRental", "Cíos maoine"},
			{ "CatSpecialAttraction", "Attraction speisialta"},
			{ "CatNewProducts", "Táirgí Nua"},
			{ "CatEmployment", "Fostaíocht"},
            { "CatWanted", "Wanted"},
			{ "CatService", "seirbhís"},
			{ "CatPersonal", "Pearsanta"},
           
            // Events
			{ "EventsText", "Imeachtaí"},
			{ "EventNameText", "Imeacht"},
			{ "EventLocationText", "Sa chás go"},
			{ "HostedByText","Óstáil ag"},
			{ "EventDateText", "Cathain"},
			{ "EventTimeInfoText", "Ba chóir go mbeadh am na himeachta a bheith am áitiúil"},
			{ "CoverChargeText", "muirear chlúdach"},
			{ "DurationText", "Fad ama"},
			{ "AddEventText", "Cuir imeacht"},

            // Event categories
			{ "CatDiscussion", "plé"},
			{ "CatSports", "Spóirt"},
			{ "CatLiveMusic", "Ceol beo"},
            { "CatCommercial", "Commercial"},
			{ "CatEntertainment", "Nightlife / Siamsaíocht"},
			{ "CatGames", "Cluichí / Comórtais"},
            { "CatPageants", "Pageants"},
			{ "CatEducation", "oideachas"},
			{ "CatArtsCulture", "Na hEalaíona / Cultúr"},
			{ "CatCharitySupport", "Carthanachta Support Group"},
			{ "CatMiscellaneous", "Ilghnéitheach"},

            // Event lookup periods
			{ "Next24Hours", "Ar Aghaidh 24 uair an chloig"},
			{ "Next10Hours", "Ar Aghaidh 10 uair an chloig"},
			{ "Next4Hours", "Ar Aghaidh 4 uair an chloig"},
			{ "Next2Hours", "Ar Aghaidh 2 uair an chloig"},

            // Sim Console
			{ "SimConsoleText", "Sim Ordú Console"},
			{ "SimCommandText", "Ordú"},

            // Statistics
			{ "StatisticsText", "staitisticí amharcán"},
			{ "ViewersText", "úsáid amharcán"},
			{ "GPUText", "cártaí Graphics"},
			{ "PerformanceText", "meáin feidhmíochta"},
			{ "FPSText", "Frámaí / dara"},
			{ "RunTimeText", "am Rith"},
			{ "RegionsVisitedText", "réigiúin ar tugadh cuairt orthu"},
			{ "MemoryUseageText", "úsáid cuimhne"},
			{ "PingTimeText", "am Ping"},
			{ "AgentsInViewText", "Gníomhairí i bhfianaise"},
			{ "ClearStatsText", "sonraí staidrimh Clear"},

            // Abuse reports
			{ "MenuAbuse", "Tuarascálacha Mí-úsáid"},
			{ "TooltipsMenuAbuse", "tuarascálacha ar mhí-úsáid Úsáideora"},
			{ "AbuseReportText", "Tuairisc ar Dhrochúsáid"},
			{ "AbuserNameText", "mí-úsáideoir"},
			{ "AbuseReporterNameText", "Tuairisceoir"},
			{ "AssignedToText", "sannta chuig"},
 
            // Factory_reset
			{ "FactoryReset", "Monarcha Athshocraigh"},
			{ "ResetMenuText", "Athshocraigh Roghchlár Go Réamhshocruithe Monarcha"},
			{ "ResetSettingsText", "Athshocraigh Socruithe Web (Socruithe Bainisteoir leathanach) Chun Réamhshocruithe Monarcha"},
			{ "Reset", "Athshocraigh"},
			{ "Settings", "Socruithe"},
			{ "Pages", "Leathanaigh"},
			{ "UpdateRequired", "thabhairt cothrom le dáta ag teastáil"},
			{ "DefaultsUpdated", "mainneachtainí updated, téigh go dtí Factory Reset Bainisteoir Socruithe cothrom le dáta nó a chur ar ceal an rabhadh."},

            // Page_manager
			{ "PageManager", "Bainisteoir leathanach"},
			{ "SaveMenuItemChanges", "Sábháil Clár Mír"},
			{ "SelectItem", "Roghnaigh Mír"},
			{ "DeleteItem", "Scrios Mír"},
			{ "AddItem", "Cuir Mír"},
			{ "PageLocationText", "leathanach Suíomh"},
			{ "PageIDText", "leathanach ID"},
			{ "PagePositionText", "leathanach Post"},
			{ "PageTooltipText", "leathanach Tooltip"},
			{ "PageTitleText", "Teideal an Leathanaigh"},
			{ "RequiresLoginText", "Éilíonn Logáil a Féach"},
			{ "RequiresLogoutText", "Éilíonn Logout a Féach"},
			{ "RequiresAdminText", "Éilíonn Riarachán a Féach"},
			{ "RequiresAdminLevelText", "Teastáil Level Riarachán a Féach"},

            // Grid settings
			{ "GridSettingsManager", "Greille Bainisteoir Socruithe"},
			{ "GridnameText", "ainm greille"},
			{ "GridnickText", "leasainm greille"},
			{ "WelcomeMessageText", "Logáil teachtaireacht fáilte"},
			{ "GovernorNameText", "córas Gobharnóir"},
			{ "MainlandEstateNameText", "eastáit Mórthír"},
			{ "SystemEstateNameText", "eastáit córas"},
			{ "BankerNameText", "baincéir córas"},
			{ "MarketPlaceOwnerNameText", "úinéir Córas margadh"},

            // Settings manager
			{ "WebRegistrationText", "clárúcháin gréasáin cheadaítear"},
			{ "GridCenterXText", "Greille Center Suíomh X"},
			{ "GridCenterYText", "Greille Center Suíomh Y"},
			{ "SettingsManager", "Bainisteoir Socruithe"},
			{ "IgnorePagesUpdatesText", "Déan neamhaird Leathanaigh rabhaidh nuashonrú dtí nuashonrú seo chugainn"},
			{ "IgnoreSettingsUpdatesText", "Déan neamhaird suímh rabhaidh nuashonrú dtí nuashonrú seo chugainn"},
			{ "HideLanguageBarText", "Folaigh Teanga Bar Roghnú"},
			{ "HideStyleBarText", "Folaigh Stíl Bar Roghnú"},
            { "HideSlideshowBarText", "Hide Slideshow Bar"},
			{ "LocalFrontPageText", "leathanach tosaigh áitiúil"},
			{ "LocalCSSText", "stílbhileog CSS áitiúil"},

            // Dates
            { "Sun", "Sun"},
            { "Mon", "Mon"},
            { "Tue", "Tue"},
            { "Wed", "Wed"},
            { "Thu", "Thu"},
            { "Fri", "Fri"},
            { "Sat", "Sat"},
			{ "Sunday", "Dé Domhnaigh"},
			{ "Monday", "Dé Luain"},
			{ "Tuesday", "Dé Máirt"},
			{ "Wednesday", "Dé Céadaoin"},
			{ "Thursday", "Déardaoin"},
			{ "Friday", "Dé hAoine"},
			{ "Saturday", "Dé Sathairn"},

            { "Jan_Short", "Jan"},
            { "Feb_Short", "Feb"},
            { "Mar_Short", "Mar"},
            { "Apr_Short", "Apr"},
            { "May_Short", "May"},
            { "Jun_Short", "Jun"},
            { "Jul_Short", "Jul"},
            { "Aug_Short", "Aug"},
            { "Sep_Short", "Sep"},
            { "Oct_Short", "Oct"},
            { "Nov_Short", "Nov"},
            { "Dec_Short", "Dec"},

			{ "January", "eanáir"},
			{ "February", "feabhra"},
			{ "March", "Márta"},
			{ "April", "aibreán"},
			{ "May", "Bealtaine"},
			{ "June", "meitheamh"},
			{ "July", "iúil"},
			{ "August", "Lúnasa"},
			{ "September", "Meán Fómhair"},
			{ "October", "Deireadh Fómhair"},
			{ "November", "samhain"},
			{ "December", "nollaig"},

            // User types
			{ "UserTypeText", "cineál Úsáideora"},
			{ "AdminUserTypeInfoText", "An cineál úsáideora (Faoi láthair a úsáidtear le haghaidh íocaíochtaí stipinn tréimhsiúla)."},
			{ "Resident", "Cónaitheach"},
			{ "Member", "ball"},
			{ "Contractor", "Conraitheoir"},
			{ "Mentor", "Mentor"},
            { "Staff", "Foireann"},
            { "Assistant Developer", "Forbróir Cúnta"},
            { "Core Developer", "Forbróir Core"},

            // ColorBox
			{ "ColorBoxImageText", "Íomha"},
			{ "ColorBoxOfText", "na"},
			{ "ColorBoxPreviousText", "roimhe Seo"},
			{ "ColorBoxNextText", "Ar Aghaidh"},
			{ "ColorBoxCloseText", "Dún"},
			{ "ColorBoxStartSlideshowText", "Tosaigh Taispeántas Sleamhnán"},
			{ "ColorBoxStopSlideshowText", "Stop Taispeántas Sleamhnán"},

            // Maintenance
			{ "NoAccountFound", "Níl cuntas aimsíodh"},
			{ "DisplayInMenu", "Taispeáin In Roghchlár"},
			{ "ParentText", "roghchlár Tuismitheoir"},
			{ "CannotSetParentToChild", "Ní féidir a shocrú mír roghchlár mar leanbh chun é féin."},
			{ "TopLevel", "Ardleibhéil"},

            // Style Switcher
            { "styles1", "Default Minimalist"},
            { "styles2", "Light Degarde"},
            { "styles3", "Blue Night"},
            { "styles4", "Dark Degrade"},
            { "styles5", "Luminus"},

			{ "StyleSwitcherStylesText", "stíleanna"},
			{ "StyleSwitcherLanguagesText", "teangacha"},
			{ "StyleSwitcherChoiceText", "rogha"},

            // Language Switcher Tooltips
            { "en", "English" },
            { "fr", "Français" },
            { "de", "Deutsch" },
            { "it", "Italiano" },
            { "es", "Español" },
            { "nl", "Nederlands" },
            { "ru", "Русский" },
			{ "ga", "Irish" }
        };
    }
}