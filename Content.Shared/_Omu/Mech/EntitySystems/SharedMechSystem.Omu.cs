using Content.Shared.Mech.Components;
using Content.Shared.Mech.Equipment.Components;
using Robust.Shared.Configuration;
using Content.Shared._Omu.CCVars;
using Content.Shared.Weapons.Melee.Events;

namespace Content.Shared._Omu.Mech.EntitySystems;

/// <summary>
/// Handles some Omu specific mech functionality.
/// </summary>
public abstract partial class OmuSharedMechSystem : EntitySystem
{
    private bool _canUseMeleeMechWeaponsInHand;

    [Dependency] private readonly IConfigurationManager _config = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<MechEquipmentComponent, AttemptMeleeEvent>(OnAttemptMelee);

        Subs.CVar(_config, OmuCCVars.MeleeMechWeaponsInHand, value => _canUseMeleeMechWeaponsInHand = value, true);

    }
    private void OnAttemptMelee(Entity<MechEquipmentComponent> ent, ref AttemptMeleeEvent args)
    {
        if (ent.Comp.EquipmentOwner == null)
        {
            if (!_canUseMeleeMechWeaponsInHand)
                args.Cancelled = true;
        }
    }
}
