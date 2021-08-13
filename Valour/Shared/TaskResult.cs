﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*  Valour - A free and secure chat client
 *  Copyright (C) 2021 Vooper Media LLC
 *  This program is subject to the GNU Affero General Public license
 *  A copy of the license should be included - if not, see <http://www.gnu.org/licenses/>
 */

namespace Valour.Shared
{
    public class TaskResult : TaskResult<string>
    {
        public TaskResult(bool success, string response) : base(success, response, null)
        {

        }
    }

    public class TaskResult<T>
    {
        [JsonProperty]
        [System.Text.Json.Serialization.JsonInclude]
        public string Message { get; set; }

        [JsonProperty]
        [System.Text.Json.Serialization.JsonInclude]
        public bool Success { get; set; }

        [JsonProperty]
        [System.Text.Json.Serialization.JsonInclude]
        public T Data { get; set; }

        public TaskResult(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public override string ToString()
        {
            if (Success)
            {
                return $"[SUCC] {Message}";
            }

            return $"[FAIL] {Message}";
        }
    }
}
