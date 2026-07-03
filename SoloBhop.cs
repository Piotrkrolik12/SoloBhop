using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;

namespace SoloBhopPlugin;

public class SoloBhop : BasePlugin
{
    public override string ModuleName => "Solo AutoBhop Bez VIP";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "Wlasny";

    private const ulong MojeSteamID = 76561198759908442; 

    public override void Load(bool hotReload)
    {
        RegisterListener<Listeners.OnTick>(OnTick);
    }

    private void OnTick()
    {
        foreach (var player in Utilities.GetPlayers())
        {
            if (player == null || !player.IsValid || player.IsBot || player.SteamID != MojeSteamID)
                continue;

            var pawn = player.PlayerPawn.Value;
            if (pawn == null || !pawn.IsValid) 
                continue;

            if ((player.Buttons & PlayerButtons.Jump) != 0)
            {
                if ((pawn.Flags & (uint)PlayerFlags.FL_ONGROUND) == 0)
                {
                    player.Buttons &= ~PlayerButtons.Jump;
                }
            }
        }
    }
}
