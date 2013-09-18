/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System;
using VirtualUniverse.Framework.ClientInterfaces;
using Nini.Config;
using VirtualUniverse.Framework;

namespace VirtualUniverse.ClientStack
{
    /// <summary>
    ///     Holds drip rates and maximum burst rates for throttling with hierarchical
    ///     token buckets. The maximum burst rates set here are hard limits and can
    ///     not be overridden by client requests
    /// </summary>
    public sealed class ThrottleRates
    {
        /// <summary>
        ///     Drip rate for asset packets
        /// </summary>
        public int Asset;

        /// <summary>
        ///     Maximum burst rate for asset packets
        /// </summary>
        public int AssetLimit;

        /// <summary>
        ///     Drip rate for AvatarInfo packets
        /// </summary>
        public int AvatarInfo;

        /// <summary>
        ///     Burst rate for the parent token bucket
        /// </summary>
        public int AvatarInfoLimit;

        /// <summary>
        ///     Drip rate for cloud packets
        /// </summary>
        public int Cloud;

        /// <summary>
        ///     Maximum burst rate for cloud packets
        /// </summary>
        public int CloudLimit;

        /// <summary>
        ///     Drip rate for terrain packets
        /// </summary>
        public int Land;

        /// <summary>
        ///     Maximum burst rate for land packets
        /// </summary>
        public int LandLimit;

        /// <summary>
        ///     Drip rate for resent packets
        /// </summary>
        public int Resend;

        /// <summary>
        ///     Maximum burst rate for resent packets
        /// </summary>
        public int ResendLimit;

        /// <summary>
        ///     Drip rate for state packets
        /// </summary>
        public int State;

        /// <summary>
        ///     Maximum burst rate for state packets
        /// </summary>
        public int StateLimit;

        /// <summary>
        ///     Drip rate for task packets
        /// </summary>
        public int Task;

        /// <summary>
        ///     Maximum burst rate for task (state and transaction) packets
        /// </summary>
        public int TaskLimit;

        /// <summary>
        ///     Drip rate for texture packets
        /// </summary>
        public int Texture;

        /// <summary>
        ///     Maximum burst rate for texture packets
        /// </summary>
        public int TextureLimit;

        /// <summary>
        ///     Drip rate for the parent token bucket
        /// </summary>
        public int Total;

        /// <summary>
        ///     Burst rate for the parent token bucket
        /// </summary>
        public int TotalLimit;

        /// <summary>
        ///     Drip rate for wind packets
        /// </summary>
        public int Wind;

        /// <summary>
        ///     Maximum burst rate for wind packets
        /// </summary>
        public int WindLimit;

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="config">Config source to load defaults from</param>
        public ThrottleRates(IConfigSource config)
        {
            try
            {
                IConfig throttleConfig = config.Configs["ClientStack.LindenUDP"];

                Resend = throttleConfig.GetInt("resend_default", 12500);
                Land = throttleConfig.GetInt("land_default", 1000);
                Wind = throttleConfig.GetInt("wind_default", 1000);
                Cloud = throttleConfig.GetInt("cloud_default", 1000);
                Task = throttleConfig.GetInt("task_default", 1000);
                Texture = throttleConfig.GetInt("texture_default", 1000);
                Asset = throttleConfig.GetInt("asset_default", 1000);
                State = throttleConfig.GetInt("state_default", 1000);
                AvatarInfo = throttleConfig.GetInt("avatar_info_default", 1000);

                ResendLimit = throttleConfig.GetInt("resend_limit", 18750);
                LandLimit = throttleConfig.GetInt("land_limit", 29750);
                WindLimit = throttleConfig.GetInt("wind_limit", 18750);
                CloudLimit = throttleConfig.GetInt("cloud_limit", 18750);
                TaskLimit = throttleConfig.GetInt("task_limit", 18750);
                TextureLimit = throttleConfig.GetInt("texture_limit", 55750);
                AssetLimit = throttleConfig.GetInt("asset_limit", 27500);
                StateLimit = throttleConfig.GetInt("state_limit", 37000);
                AvatarInfoLimit = throttleConfig.GetInt("avatar_info_limit", 37000);

                Total = throttleConfig.GetInt("client_throttle_max_bps", 0);
                TotalLimit = Total;
            }
            catch (Exception)
            {
            }
        }

        public int GetRate(ThrottleOutPacketType type)
        {
            switch (type)
            {
                case ThrottleOutPacketType.Resend:
                    return Resend;
                case ThrottleOutPacketType.Land:
                    return Land;
                case ThrottleOutPacketType.Wind:
                    return Wind;
                case ThrottleOutPacketType.Cloud:
                    return Cloud;
                case ThrottleOutPacketType.Task:
                    return Task;
                case ThrottleOutPacketType.Texture:
                    return Texture;
                case ThrottleOutPacketType.Asset:
                    return Asset;
                case ThrottleOutPacketType.State:
                    return State;
                case ThrottleOutPacketType.AvatarInfo:
                    return AvatarInfo;
                //                case ThrottleOutPacketType.Unknown:
                default:
                    return 0;
            }
        }

        public int GetLimit(ThrottleOutPacketType type)
        {
            switch (type)
            {
                case ThrottleOutPacketType.Resend:
                    return ResendLimit;
                case ThrottleOutPacketType.Land:
                    return LandLimit;
                case ThrottleOutPacketType.Wind:
                    return WindLimit;
                case ThrottleOutPacketType.Cloud:
                    return CloudLimit;
                case ThrottleOutPacketType.Task:
                    return TaskLimit;
                case ThrottleOutPacketType.Texture:
                    return TextureLimit;
                case ThrottleOutPacketType.Asset:
                    return AssetLimit;
                case ThrottleOutPacketType.State:
                    return StateLimit;
                case ThrottleOutPacketType.AvatarInfo:
                    return AvatarInfoLimit;
                //                case ThrottleOutPacketType.Unknown:
                default:
                    return 0;
            }
        }
    }
}