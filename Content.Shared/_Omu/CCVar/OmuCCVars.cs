using Robust.Shared.Configuration;

namespace Content.Shared._Omu.CCVars;

/// <summary>
/// Omu specific cvars.
/// </summary>
[CVarDefs]
// ReSharper disable once InconsistentNaming - Shush you
public sealed partial class OmuCCVars
{
    /// <summary>
    /// Whether you can use melee mech weapons in hand.
    /// </summary>
    public static readonly CVarDef<bool> MeleeMechWeaponsInHand =
        CVarDef.Create("mech.melee_mech_weapons_in_hand", false, CVar.SERVER | CVar.REPLICATED);
}
