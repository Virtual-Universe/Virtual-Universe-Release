/***************************************************************************
 *	                VIRTUAL REALITY PUBLIC SOURCE LICENSE
 * 
 * Date				: Sun January 1, 2006
 * Copyright		: (c) 2006-2014 by Virtual Reality Development Team. 
 *                    All Rights Reserved.
 * Website			: http://www.syndarveruleiki.is
 *
 * Product Name		: Virtual Reality
 * License Text     : packages/docs/VRLICENSE.txt
 * 
 * Planetary Info   : Information about the Planetary code
 * 
 * Copyright        : (c) 2014-2024 by Second Galaxy Development Team
 *                    All Rights Reserved.
 * 
 * Website          : http://www.secondgalaxy.com
 * 
 * Product Name     : Virtual Reality
 * License Text     : packages/docs/SGLICENSE.txt
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the WhiteCore-Sim Project nor the
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
***************************************************************************/

namespace Aurora.Modules.Web.Translators
{
    public class EnglishTranslation : ITranslator
    {
        public string LanguageName
        {
            get { return "en"; }
        }

        public string GetTranslatedString(string key)
        {
            switch (key)
            {
                case "GridStatus":
                    return "Virtual Reality Status";
                case "Online":
                    return "Online";
                case "Offline":
                    return "Offline";
                case "TotalUserCount":
                    return "Total Citizens";
                case "TotalRegionCount":
                    return "Total Region Count";
                case "UniqueVisitors":
                    return "Unique Citizens last 30 days";
                case "OnlineNow":
                    return "Citizens Online Now";
                case "HyperGrid":
                    return "HyperGrid (HG)";
                case "Voice":
                    return "Vivox Voice";
                case "Currency":
                    return "V$";
                case "Disabled":
                    return "Disabled";
                case "Enabled":
                    return "Enabled";
                case "News":
                    return "Virtual Times";
                case "Region":
                    return "Virtual Worlds";
                case "Login":
                    return "Login";
                case "UserName":
                case "UserNameText":
                    return "User Name";
                case "Password":
                case "PasswordText":
                    return "Password";
                case "PasswordConfirmation":
                    return "Password Confirmation";
                case "ForgotPassword":
                    return "Forgot Password?";
                case "Submit":
                    return "Submit";
                case "TypeUserNameToConfirm":
                    return "Please type the citizens of this account to confirm you want to delete this account";

                case "SpecialWindowTitleText":
                    return "Special Info Window Title";
                case "SpecialWindowTextText":
                    return "Special Info Window Text";
                case "SpecialWindowColorText":
                    return "Special Info Window Color";
                case "SpecialWindowStatusText":
                    return "Special Info Window Status";
                case "WelcomeScreenManagerFor":
                    return "Welcome Screen Manager For";
                case "ChangesSavedSuccessfully":
                    return "Changes Saved Successfully";


                case "AvatarNameText":
                    return "Citizen Name";
                case "AvatarScopeText":
                    return "Citizen Scope ID";
                case "FirstNameText":
                    return "Your First Name";
                case "LastNameText":
                    return "Your Last Name";
                case "UserAddressText":
                    return "Your Address";
                case "UserZipText":
                    return "Your Zip Code";
                case "UserCityText":
                    return "Your City";
                case "UserCountryText":
                    return "Your Country";
                case "UserDOBText":
                    return "Your Date Of Birth (Month Day Year)";
                case "UserEmailText":
                    return "Your Email";
                case "RegistrationText":
                    return "Citizen Registration";
                case "RegistrationsDisabled":
                    return "Registrations are currently disabled, please try again soon.";
                case "TermsOfServiceText":
                    return "Virtual Reality Laws";
                case "TermsOfServiceAccept":
                    return "Do you accept the Virtual Reality Laws as detailed above?";
                case "Accept":
                    return "Accept";
                case "AvatarNameError":
                    return "You did not enter an avatar name!";
                case "AvatarPasswordError":
                    return "Password is empty or not matching!";
                case "AvatarEmailError":
                return "An email address is required for password recovery! ('none' if unknown)";

               // Virtual Times
                case "OpenNewsManager":
                    return "Open the Virtual Times manager";
                case "NewsManager":
                    return "Virtual Times Manager";
                case "EditNewsItem":
                    return "Edit Virtual Times item";
                case "AddNewsItem":
                    return "Add Virtual Times news item";
                case "DeleteNewsItem":
                    return "Delete Vrtual Times item";
                case "NewsDateText":
                    return "Virtual Times Date";
                case "NewsTitleText":
                    return "Virtual Times Title";
                case "NewsItemTitle":
                    return "Virtual Times Item Title";
                case "NewsItemText":
                    return "Virtual Times Item Text";
                case "AddNewsText":
                    return "Add Virtual Times";
                case "DeleteNewsText":
                    return "Delete Virtual Times";
                case "EditNewsText":
                    return "Edit Virtual Times";
                case "UserProfileFor":
                    return "Citizen Profile For";
                case "UsersGroupsText":
                    return "Groups Joined";
                case "UsersPicksText":
                    return "Picks for";
                case "ResidentSince":
                    return "Citizen Since";
                case "AccountType":
                    return "Account Type";
                case "PartnersName":
                    return "Partner's Name";
                case "AboutMe":
                    return "About Me";
                case "IsOnlineText":
                    return "User Status";
                case "OnlineLocationText":
                    return "Citizen Location";

                case "RegionInformationText":
                    return "Region Information";
                case "OwnerNameText":
                    return "Owner Name";
                case "RegionLocationText":
                    return "Region Location";
                case "RegionSizeText":
                    return "Region Size";
                case "RegionNameText":
                    return "Region Name";
                case "RegionTypeText":
                    return "Region Type";
            case "RegionTerrainText":
                return "Virtual World Terrain";
                case "ParcelsInRegionText":
                    return "Parcels In Virtual World";
                case "ParcelNameText":
                    return "Parcel Name";
                case "ParcelOwnerText":
                    return "Parcel Owner's Name";

                    // Virtual World Page
                case "RegionInfoText":
                    return "Virtual World Info";
                case "RegionListText":
                    return "Virtual WOrld List";
                case "RegionLocXText":
                    return "Virtual World X";
                case "RegionLocYText":
                    return "Virtual World Y";
                case "SortByLocX":
                    return "Sort By Virtual World X";
                case "SortByLocY":
                    return "Sort By Virtual World Y";
                case "SortByName":
                    return "Sort By Virtual World Name";
                case "RegionMoreInfo":
                    return "More Information";
                case "RegionMoreInfoTooltips":
                    return "More info about";
                case "FirstText":
                    return "First";
                case "BackText":
                    return "Back";
                case "NextText":
                    return "Next";
                case "LastText":
                    return "Last";
                case "CurrentPageText":
                    return "Current Page";
                case "MoreInfoText":
                    return "More Info";
                case "OnlineUsersText":
                    return "Online Users";
                case "RegionOnlineText":
                    return "Virtual World Status";
                case "NumberOfUsersInRegionText":
                    return "Number of Citizens in Virtual World";

                    // Menu Buttons
                case "MenuHome":
                    return "Home";
                case "MenuLogin":
                    return "Login";
                case "MenuLogout":
                    return "Logout";
                case "MenuRegister":
                    return "Register";
                case "MenuForgotPass":
                    return "Forgot Password";
                case "MenuNews":
                    return "Virtual Times";
                case "MenuWorld":
                    return "World";
                case "MenuWorldMap":
                    return "World Map";
                case "MenuRegion":
                    return "Virtual World List";
                case "MenuUser":
                    return "Citizen";
                case "MenuOnlineUsers":
                    return "Citizens Online";
                case "MenuUserSearch":
                    return "Citizen Search";
                case "MenuRegionSearch":
                    return "Virtual World Search";
                case "MenuChat":
                    return "Chat";
                case "MenuHelp":
                    return "Help";
                case "MenuViewerHelp":
                    return "Viewer Help";
                case "MenuChangeUserInformation":
                    return "Change Citizen Information";
                case "MenuWelcomeScreenManager":
                    return "Welcome Screen Manager";
                case "MenuNewsManager":
                    return "Virtual Times Manager";
                case "MenuUserManager":
                    return "Citizen Manager";
                case "MenuFactoryReset":
                    return "Factory Reset";
                case "ResetMenuInfoText":
                    return "Resets the menu items back to the most updated defaults";
                case "ResetSettingsInfoText":
                    return "Resets the Web Interface settings back to the most updated defaults";
                case "MenuPageManager":
                    return "Page Manager";
                case "MenuSettingsManager":
                    return "Settings Manager";
                case "MenuManager":
                    return "Admin";

                    // Tooltips Menu Buttons
                case "TooltipsMenuHome":
                    return "Home";
                case "TooltipsMenuLogin":
                    return "Login";
                case "TooltipsMenuLogout":
                    return "Logout";
                case "TooltipsMenuRegister":
                    return "Register";
                case "TooltipsMenuForgotPass":
                    return "Forgot Password";
                case "TooltipsMenuNews":
                    return "News";
                case "TooltipsMenuWorld":
                    return "World";
                case "TooltipsMenuWorldMap":
                    return "World Map";
                case "TooltipsMenuRegion":
                    return "Region List";
                case "TooltipsMenuUser":
                    return "User";
                case "TooltipsMenuOnlineUsers":
                    return "Citizens Online";
                case "TooltipsMenuUserSearch":
                    return "Citizen Search";
                case "TooltipsMenuRegionSearch":
                    return "Region Search";
                case "TooltipsMenuChat":
                    return "Chat";
                case "TooltipsMenuViewerHelp":
                    return "Viewer Help";
                case "TooltipsMenuHelp":
                    return "Help";
                case "TooltipsMenuChangeUserInformation":
                    return "Change Citizen Information";
                case "TooltipsMenuWelcomeScreenManager":
                    return "Welcome Screen Manager";
                case "TooltipsMenuNewsManager":
                    return "Virtual Times Manager";
                case "TooltipsMenuUserManager":
                    return "User Manager";
                case "TooltipsMenuFactoryReset":
                    return "Factory Reset";
                case "TooltipsMenuPageManager":
                    return "Page Manager";
                case "TooltipsMenuSettingsManager":
                    return "Settings Manager";
                case "TooltipsMenuManager":
                    return "Admin Management";

                    // Menu Virtual World
                case "MenuRegionTitle":
                    return "Virtual World";
                case "MenuParcelTitle":
                    return "Parcel";
                case "MenuOwnerTitle":
                    return "Owner";

                    // Menu Profile
                case "MenuProfileTitle":
                    return "Profile";
                case "MenuGroupTitle":
                    return "Group";
                case "MenuPicksTitle":
                    return "Picks";

                    // Urls
                case "WelcomeScreen":
                    return "Welcome Screen";

                    // Tooltips Urls
                case "TooltipsWelcomeScreen":
                    return "Welcome Screen";
                case "TooltipsWorldMap":
                    return "World Map";

                    // Style Switcher
                case "styles1":
                    return "Default Minimalist";
                case "styles2":
                    return "Light Degarde";
                case "styles3":
                    return "Blue Night";
                case "styles4":
                    return "Dark Degrade";
                case "styles5":
                    return "Luminus";

                case "StyleSwitcherStylesText":
                    return "Styles";
                case "StyleSwitcherLanguagesText":
                    return "Languages";
                case "StyleSwitcherChoiceText":
                    return "Choice";

                    // Language Switcher Tooltips
            case "en": 
                return "English";
            case "fr": 
                return "French";
            case "de":
                return "German";
            case "it": 
                return "Italian";
            case "es": 
                return "Spanish";
            case "nl":
                return "Dutch";

                    // Index Page
                case "HomeText":
                    return "Home";
                case "HomeTextWelcome":
                    return "This is our New Virtual World! Join us now, and make a difference!";
                case "HomeTextTips":
                    return "New presentations";
                case "WelcomeToText":
                    return "Welcome to";

                    // World Map Page
                case "WorldMap":
                    return "World Map";
                case "WorldMapText":
                    return "Full Screen";

                    // Chat Page
                case "ChatText":
                    return "Chat Support";

                    // Help Page
                case "HelpText":
                    return "Help";
                case "HelpViewersConfigText":
                    return "Viewer Settings";
                case "AngstromViewer":
                    return "Angstrom Viewer";
                case "AstraViewer":
                    return "Astra Viewer (No longer supported)";
                case "FirestormViewer":
                    return "Firestorm Viewer";
                case "GargantaViewer":
                    return "Garganta Viewer";
                case "KokuaViewer":
                    return "Kokua Viewer";
                case "RabbitViewer":
                   return "Rabbit Viewer";
                case "ImprudenceViewer":
                    return "Imprudence Viewer (No longer supported)";
                case "PhoenixViewer":
                    return "Phoenix Viewer (No longer supported)";
                case "SenkaimonViewer":
                    return "Senkaimon Viewer";
                case "SingularityViewer":
                    return "Singularity Viewer";
                case "VoodooViewer":
                    return "Voodoo Viewer";
                case "ZenViewer":
                    return "Zen Viewer (No longer supported)";

                    //Logout page
                case "Logout":
                    return "Logout";
                case "LoggedOutSuccessfullyText":
                    return "You have been logged out successfully.";

                    //Change user information page
                case "ChangeUserInformationText":
                    return "Change User Information";
                case "ChangePasswordText":
                    return "Change Password";
                case "NewPasswordText":
                    return "New Password";
                case "NewPasswordConfirmationText":
                    return "New Password (Confirmation)";
                case "ChangeEmailText":
                    return "Change Email Address";
                case "NewEmailText":
                    return "New Email Address";
                case "DeleteUserText":
                    return "Delete My Account";
                case "DeleteText":
                    return "Delete";
                case "DeleteUserInfoText":
                    return
                        "This will remove all information about you in the grid and remove your access to this service. If you wish to continue, enter your name and password and click Delete.";
                case "EditText":
                    return "Edit";
                case "EditUserAccountText":
                    return "Edit Citizen Account";

                    //Maintenance page
                case "WebsiteDownInfoText":
                    return "Website is currently down, please try again soon.";
                case "WebsiteDownText":
                    return "Website offline";

                    //404 page
                case "Error404Text":
                    return "Error code";
                case "Error404InfoText":
                    return "404 Page Not Found";
                case "HomePage404Text":
                    return "home page";

                    //505 page
                case "Error505Text":
                    return "Error code";
                case "Error505InfoText":
                    return "505 Internal Server Error";
                case "HomePage505Text":
                    return "home page";

                    //Citizen Search page
                case "Search":
                    return "Search";
                case "SearchText":
                    return "Search";
                case "SearchForUserText":
                    return "Search For A Citizen";
                case "UserSearchText":
                    return "Citizen Search";
                case "SearchResultForUserText":
                    return "Citizen Search Result";

                    //region_search page
                case "SearchForRegionText":
                    return "Search For A Region";
                case "RegionSearchText":
                    return "Region Search";
                case "SearchResultForRegionText":
                    return "Search Result For Region";

                    //Edit Citizen page
                case "AdminDeleteUserText":
                    return "Delete Citizen";
                case "AdminDeleteUserInfoText":
                    return "This deletes the account and destroys all information associated with it.";
                case "BanText":
                    return "Ban";
                case "UnbanText":
                    return "Unban";
                case "AdminTempBanUserText":
                    return "Temp Ban Citizen";
                case "AdminTempBanUserInfoText":
                    return "This blocks the citizen from logging in for the set amount of time.";
                case "AdminBanUserText":
                    return "Ban Citizen";
                case "AdminBanUserInfoText":
                    return "This blocks the citizen from logging in until the user is unbanned.";
                case "AdminUnbanUserText":
                    return "Unban Citizen";
                case "AdminUnbanUserInfoText":
                    return "Removes temporary and permanent bans on the citizen.";
                case "AdminLoginInAsUserText":
                    return "Login as Citizen";
                case "AdminLoginInAsUserInfoText":
                    return
                        "You will be logged out of your Central 46 account, and logged in as this citizen, and will see everything as they see it.";
                case "TimeUntilUnbannedText":
                    return "Time until user is unbanned";
                case "DaysText":
                    return "Days";
                case "HoursText":
                    return "Hours";
                case "MinutesText":
                    return "Minutes";
                case "EdittingText":
                    return "Editing";
                case "BannedUntilText":
                    return "Citizen Banned until:";
                case "KickAUserText":
                    return "Kick Citizen";
                case "KickAUserInfoText":
                    return "Kicks a citizen from Virtual Rreality (logs them out within 30 seconds)";
                case "KickMessageText":
                    return "Message To Citizen";
                case "KickUserText":
                    return "Kick Citizen";
                case "MessageAUserText":
                    return "Send Citizen A Message";
                case "MessageAUserInfoText":
                    return "Sends a citizen a blue-box message (will arrive within 30 seconds)";
                case "MessageUserText":
                    return "Message Citizen";

                    //Factory Reset
                case "FactoryReset":
                    return "Factory Reset";
                case "ResetMenuText":
                    return "Reset Menu To Factory Defaults";
                case "ResetSettingsText":
                    return "Reset Web Settings (Settings Manager page) To Factory Defaults";
                case "Reset":
                    return "Reset";
                case "Settings":
                    return "Settings";
                case "Pages":
                    return "Pages";
                case "DefaultsUpdated":
                    return
                        "defaults updated, go to Factory Reset to update or Settings Manager to disable this warning.";

                    //Page Manager
                case "PageManager":
                    return "Page Manager";
                case "SaveMenuItemChanges":
                    return "Save Menu Item";
                case "SelectItem":
                    return "Select Item";
                case "DeleteItem":
                    return "Delete Item";
                case "AddItem":
                    return "Add Item";
                case "PageLocationText":
                    return "Page Location";
                case "PageIDText":
                    return "Page ID";
                case "PagePositionText":
                    return "Page Position";
                case "PageTooltipText":
                    return "Page Tooltip";
                case "PageTitleText":
                    return "Page Title";
                case "No":
                    return "No";
                case "Yes":
                    return "Yes";
                case "RequiresLoginText":
                    return "Requires Login To View";
                case "RequiresLogoutText":
                    return "Requires Logout To View";
                case "RequiresAdminText":
                    return "Requires Central 46 To View";
                case "RequiresAdminLevelText":
                    return "Level Central 46 Required To View";

                    //Settings Manager
                case "Save":
                    return "Save";
                case "WebRegistrationText":
                    return "Web registrations allowed";
                case "GridCenterXText":
                    return "Virtual Reality Center Location X";
                case "GridCenterYText":
                    return "Virtual Reality Center Location Y";
                case "SettingsManager":
                    return "Settings Manager";
                case "IgnorePagesUpdatesText":
                    return "Ignore pages update warning until next update";
                case "IgnoreSettingsUpdatesText":
                    return "Ignore settings update warning until next update";

                    //Dates
                case "Sun":
                    return "Sun";
                case "Mon":
                    return "Mon";
                case "Tue":
                    return "Tue";
                case "Wed":
                    return "Wed";
                case "Thu":
                    return "Thu";
                case "Fri":
                    return "Fri";
                case "Sat":
                    return "Sat";
                case "Sunday":
                    return "Sunday";
                case "Monday":
                    return "Monday";
                case "Tuesday":
                    return "Tuesday";
                case "Wednesday":
                    return "Wednesday";
                case "Thursday":
                    return "Thursday";
                case "Friday":
                    return "Friday";
                case "Saturday":
                    return "Saturday";

                case "Jan_Short":
                    return "Jan";
                case "Feb_Short":
                    return "Feb";
                case "Mar_Short":
                    return "Mar";
                case "Apr_Short":
                    return "Apr";
                case "May_Short":
                    return "May";
                case "Jun_Short":
                    return "Jun";
                case "Jul_Short":
                    return "Jul";
                case "Aug_Short":
                    return "Aug";
                case "Sep_Short":
                    return "Sep";
                case "Oct_Short":
                    return "Oct";
                case "Nov_Short":
                    return "Nov";
                case "Dec_Short":
                    return "Dec";

                case "January":
                    return "January";
                case "February":
                    return "February";
                case "March":
                    return "March";
                case "April":
                    return "April";
                case "May":
                    return "May";
                case "June":
                    return "June";
                case "July":
                    return "July";
                case "August":
                    return "August";
                case "September":
                    return "September";
                case "October":
                    return "October";
                case "November":
                    return "November";
                case "December":
                    return "December";

            // Citizen Types
            case "UserTypeText":
                return "Citizen Type";
            case "AdminUserTypeInfoText":
                return "The type of citizen (Currently used for periodical stipend payments).";
            case "Guest":
                return "Guest";
            case  "Resident":
                return "Resident";
            case "Member":
                return "Citizen";
            case "Contractor":
                return "Contractor";
            case "Charter_Member":
                return "Charter Citizen";


                    // ColorBox
                case "ColorBoxImageText":
                    return "Image";
                case "ColorBoxOfText":
                    return "of";
                case "ColorBoxPreviousText":
                    return "Previous";
                case "ColorBoxNextText":
                    return "Next";
                case "ColorBoxCloseText":
                    return "Close";
                case "ColorBoxStartSlideshowText":
                    return "Start Slide Show";
                case "ColorBoxStopSlideshowText":
                    return "Stop Slide Show";


                // Maintenance
                case "NoAccountFound":
                    return "No account found";
                case "DisplayInMenu":
                    return "Display In Menu";
                case "ParentText":
                    return "Menu Parent";
                case "CannotSetParentToChild":
                    return "Cannot set menu item as a child to itself.";
                case "TopLevel":
                    return "Top Level";
                case "HideLanguageBarText":
                    return "Hide Language Selection Bar";
                case "HideStyleBarText":
                    return "Hide Style Selection Bar";
            }
            return "UNKNOWN CHARACTER";
        }
    }
}