﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*  Valour - A free and secure chat client
 *  Copyright (C) 2021 Vooper Media LLC
 *  This program is subject to the GNU Affero General Public license
 *  A copy of the license should be included - if not, see <http://www.gnu.org/licenses/>
 */

namespace Valour.Shared.Users
{
    /// <summary>
    /// This is the base User object, which contains everything needed for public use
    /// </summary>
    public class User
    {
        /// <summary>
        /// The Id of the user
        /// </summary>
        [Key]
        public ulong Id { get; set; }

        /// <summary>
        /// The main display name for the user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The url for the user's profile picture
        /// </summary>
        public string Pfp_Url { get; set; }

        /// <summary>
        /// The Date and Time that the user joined Valour
        /// </summary>
        public DateTime Join_DateTime { get; set; }

        /// <summary>
        /// True if the user is a bot
        /// </summary>
        public bool Bot { get; set; }

        /// <summary>
        /// True if the account has been disabled
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// True if this user is a member of the Valour official staff team. Falsely modifying this 
        /// through a client modification to present non-official staff as staff is a breach of our
        /// license. Don't do that.
        /// </summary>
        public bool Valour_Staff { get; set; }

        /// <summary>
        /// The integer representation of the current user state
        /// </summary>
        public int UserState_Value { get; set; }

        /// <summary>
        /// The last time this user was flagged as active (successful auth)
        /// </summary>
        public DateTime Last_Active { get; set; }

        [NotMapped]
        public UserState UserState
        {
            get
            {
                // Automatically determine
                if (UserState_Value == 0)
                {
                    double minPassed = DateTime.UtcNow.Subtract(Last_Active).TotalMinutes;

                    if (minPassed < 3)
                    {
                        return UserState.Online;
                    }
                    else if (minPassed < 6)
                    {
                        return UserState.Away;
                    }
                    else
                    {
                        return UserState.Offline;
                    }
                }

                // User selected
                return UserState.States[UserState_Value];
            }
            set
            {
                UserState_Value = value.Value;
            }
        }
    }

}
