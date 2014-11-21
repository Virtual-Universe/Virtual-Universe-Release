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

using Nini.Config;
using OpenMetaverse;
using OpenMetaverse.StructuredData;
using System;
using System.Reflection;
using Aurora.Framework.Modules;
using Aurora.Framework.SceneInfo;
using Aurora.Framework.Services;
using Aurora.Framework.Servers.HttpServer;
using Aurora.Framework.Servers.HttpServer.Implementation;
using Aurora.Framework.Servers.HttpServer.Interfaces;
using Aurora.Framework.ConsoleFramework;

namespace Aurora.Modules.MarketPlaceAPI
{
    public enum TransactionType2
    {
        // When buying something from the MarketPlace
        MarketBuy      =   20001,
        // When selling something on the MarketPlace
        MarketSell     =   20002,
        // MarketPlace Fee for placing something on the MarketPlace
        MarketFee      =   20003,
        // MarketPlace Advertising Fee for having an advertisement
        MarketAdFee    =   20004
    }

    public class MarketPlaceAPIModule : IService
    {
		// TODO: This is the explanation what the MarketPlaceAPI will do
		//
		// * Register the user as a MarketPlace user
		//
		// [register] gets input [STRING FullAvatarName, UUID MarketPlaceIdentifier] and outputs [UUID AvatarKey, BOOLEAN Registration]
		//
		// * External MarketPlace can use the following calls
		//
		// [get_user_info] gets input [UUID AvatarKEY] and outputs [Balance of Avatar]
		//
		// [purchase_pre] gets input [UUID AvatarKEY, INTEGER amount] and outputs a true/false when the amount can be subtracted
		//
		// [purchasing] gets input [UUID AvatarKEY, INTEGER amount, INTEGER Transaction] and outputs [BOOLEAN success, INTEGER TransactionType2, UUID transaction]
		//
        
        #region Startup
        
        public string Name
        {
            get { return "MarketPlaceAPIModule"; }
        }        
        
        public void Initialize(IConfigSource config, IRegistryCore registry)
        {
        }

        public void Start(IConfigSource config, IRegistryCore registry)
        {
            IConfig handlerConfig = config.Configs["MarketPlace"];
            if (handlerConfig.GetString("MarketPlaceHandler", "") != Name)
            {
                MainConsole.Instance.Info("[MarketPlaceAPI]: MarketPlaceAPI Handler not set");
                return;
            }
            MainConsole.Instance.Info("[MarketPlaceAPI]: MarketPlaceAPI has been started");
        }

        public void FinishedStartup()
        {
            
        }
        #endregion

        #region Money Regulators
        private void GetBalance(UUID agentID)
        {
            
        }

        public bool Charge(UUID agentID, int amount, string text, TransactionType type)
        {
            return true;
        }

        private bool Transfer(UUID toID, UUID fromID, int amount, string description, TransactionType2 type)
        {
            return true;
        }
        #endregion
    }
}