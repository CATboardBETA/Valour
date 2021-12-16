﻿using System.Text.Json.Serialization;
using Valour.Api.Client;
using Valour.Shared.Items;
using Valour.Shared.Items.Planets;

namespace Valour.Api.Items.Planets;

/*  Valour - A free and secure chat client
*  Copyright (C) 2021 Vooper Media LLC
*  This program is subject to the GNU Affero General Public license
*  A copy of the license should be included - if not, see <http://www.gnu.org/licenses/>
*/

public class Invite : Item<Invite>, ISharedInvite
{
    /// <summary>
    /// the invite code
    /// </summary>
    [JsonPropertyName("Code")]
    public string Code { get; set; }

    /// <summary>
    /// The planet the invite is for
    /// </summary>
    [JsonPropertyName("Planet_Id")]
    public ulong Planet_Id { get; set; }

    /// <summary>
    /// The user that created the invite
    /// </summary>
    [JsonPropertyName("Issuer_Id")]
    public ulong Issuer_Id { get; set; }

    /// <summary>
    /// The time the invite was created
    /// </summary>
    [JsonPropertyName("Time")]
    public DateTime Time { get; set; }

    /// <summary>
    /// The length of the invite before its invaild
    /// </summary>
    [JsonPropertyName("Hours")]
    public int? Hours { get; set; }

    [JsonInclude]
    [JsonPropertyName("ItemType")]
    public override ItemType ItemType => ItemType.Invite;

    /// <summary>
    /// Returns the invite for the given invite code
    /// </summary>
    public static async Task<Invite> FindAsync(string code, bool force_refresh = false)
    {
        if (!force_refresh)
        {
            var cached = ValourCache.Get<Invite>(code);
            if (cached is not null)
                return cached; 
        }

        var invResult = await ValourClient.GetJsonAsync<Invite>($"api/invite/{code}");

        if (invResult is not null)
            await ValourCache.Put(code, invResult);

        return invResult;
    }

    /// <summary>
    /// Returns the name of the invite's planet
    /// </summary>
    public async Task<string> GetPlanetNameAsync() =>
        await ValourClient.GetJsonAsync<string>($"api/invite/{Code}/planet/name") ?? "Name not found";
    
    /// <summary>
    /// Returns the icon of the invite's planet
    /// </summary>
    public async Task<string> GetPlanetIconUrl() =>
        await ValourClient.GetJsonAsync<string>($"api/invite/{Code}/planet/icon_url") ?? "";
}
