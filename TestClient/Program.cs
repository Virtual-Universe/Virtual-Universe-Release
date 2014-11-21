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

using System;
using System.Collections.Generic;
using System.IO;
using CommandLine.Utility;

namespace OpenMetaverse.TestClient
{
    public class CommandLineArgumentsException : Exception
    {
    }

    public class Program
    {
        public static string LoginURI;

        private static void Usage()
        {
            Console.WriteLine("Usage: " + Environment.NewLine +
                    "TestClient.exe [--first firstname --last lastname --pass password] [--file userlist.txt] [--loginuri=\"uri\"] [--startpos \"sim/x/y/z\"] [--master \"master name\"] [--masterkey \"master uuid\"] [--gettextures] [--scriptfile \"filename\"]");
        }

        static void Main(string[] args)
        {
            Arguments arguments = new Arguments(args);

            List<LoginDetails> accounts = new List<LoginDetails>();
            LoginDetails account;
            bool groupCommands = false;
            string masterName = String.Empty;
            UUID masterKey = UUID.Zero;
            string file = String.Empty;
            bool getTextures = false;
            bool noGUI = false; // true if to not prompt for input
            string scriptFile = String.Empty;

            if (arguments["groupcommands"] != null)
                groupCommands = true;

            if (arguments["masterkey"] != null)
                masterKey = UUID.Parse(arguments["masterkey"]);

            if (arguments["master"] != null)
                masterName = arguments["master"];

            if (arguments["loginuri"] != null)
                LoginURI = arguments["loginuri"];
            if (String.IsNullOrEmpty(LoginURI))
                LoginURI = Settings.AGNI_LOGIN_SERVER;
            Logger.Log("Using login URI " + LoginURI, Helpers.LogLevel.Info);

            if (arguments["gettextures"] != null)
                getTextures = true;

            if (arguments["nogui"] != null)
                noGUI = true;

            if (arguments["scriptfile"] != null)
            {
                scriptFile = arguments["scriptfile"];
                if (!File.Exists(scriptFile))
                {
                    Logger.Log(String.Format("File {0} Does not exist", scriptFile), Helpers.LogLevel.Error);
                    return;
                }
            }

            if (arguments["file"] != null)
            {
                file = arguments["file"];

                if (!File.Exists(file))
                {
                    Logger.Log(String.Format("File {0} Does not exist", file), Helpers.LogLevel.Error);
                    return;
                }

                // Loading names from a file
                try
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        string line;
                        int lineNumber = 0;

                        while ((line = reader.ReadLine()) != null)
                        {
                            lineNumber++;
                            string[] tokens = line.Trim().Split(new char[] { ' ', ',' });

                            if (tokens.Length >= 3)
                            {
                                account = new LoginDetails();
                                account.FirstName = tokens[0];
                                account.LastName = tokens[1];
                                account.Password = tokens[2];

                                if (tokens.Length >= 4) // Optional starting position
                                {
                                    char sep = '/';
                                    string[] startbits = tokens[3].Split(sep);
                                    account.StartLocation = NetworkManager.StartLocation(startbits[0], Int32.Parse(startbits[1]),
                                        Int32.Parse(startbits[2]), Int32.Parse(startbits[3]));
                                }

                                accounts.Add(account);
                            }
                            else
                            {
                                Logger.Log("Invalid data on line " + lineNumber +
                                    ", must be in the format of: FirstName LastName Password [Sim/StartX/StartY/StartZ]",
                                    Helpers.LogLevel.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("Error reading from " + args[1], Helpers.LogLevel.Error, ex);
                    return;
                }
            }
            else if (arguments["first"] != null && arguments["last"] != null && arguments["pass"] != null)
            {
                // Taking a single login off the command-line
                account = new LoginDetails();
                account.FirstName = arguments["first"];
                account.LastName = arguments["last"];
                account.Password = arguments["pass"];

                accounts.Add(account);
            }
            else if (arguments["help"] != null)
            {
                Usage();
                return;
            }

            foreach (LoginDetails a in accounts)
            {
                a.GroupCommands = groupCommands;
                a.MasterName = masterName;
                a.MasterKey = masterKey;
                a.URI = LoginURI;

                if (arguments["startpos"] != null)
                {
                    char sep = '/';
                    string[] startbits = arguments["startpos"].Split(sep);
                    a.StartLocation = NetworkManager.StartLocation(startbits[0], Int32.Parse(startbits[1]),
                            Int32.Parse(startbits[2]), Int32.Parse(startbits[3]));
                }
            }

            // Login the accounts and run the input loop
            ClientManager.Instance.Start(accounts, getTextures);

            if (!String.IsNullOrEmpty(scriptFile))
                ClientManager.Instance.DoCommandAll("script " + scriptFile, UUID.Zero);

            // Then Run the ClientManager normally
            ClientManager.Instance.Run(noGUI);
        }
    }
}
