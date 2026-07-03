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
        // Rejestrujemy nasłuchiwanie komend ruchu gracza
        RegisterListener<Listeners.OnUserCmd>(OnUserCmd);
    }

    private void OnUserCmd(CCSPlayerController player, CommandInfo cmd)
    {
        if (player == null || !player.IsValid || player.IsBot || player.SteamID != MojeSteamID)
            return;

        var pawn = player.PlayerPawn.Value;
        if (pawn == null || !pawn.IsValid)
            return;

        // Sprawdzamy czy gracz trzyma przycisk skoku
        if ((cmd.Buttons & PlayerButtons.Jump) != 0)
        {
            // Jeśli nie stoi na ziemi, usuwamy flagę skoku z tej klatki (symulacja puszczenia spacji)
            if ((pawn.Flags & (uint)PlayerFlags.FL_ONGROUND) == 0)
            {
                cmd.Buttons &= ~PlayerButtons.Jump;
            }
        }
    }
}
