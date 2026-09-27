// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Shared.Changeling.Components;
using Content.Goobstation.Shared.Overlays;
using Content.Shared.Body.Systems;
using Content.Shared.Eye.Blinding.Components;
using Content.Shared.Popups;
using Content.Shared.Tag;
using Content.Shared.Weapons.Ranged.Events;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Goobstation.Shared.Changeling.Systems;

public abstract class SharedChangelingSystem : EntitySystem
{
    [Dependency] protected readonly SharedBodySystem Body = default!;
    [Dependency] private readonly TagSystem _tagSystem = default!; // Omu
    [Dependency] private readonly IRobustRandom _random = default!; // Omu
    [Dependency] private readonly SharedPopupSystem _popup = default!; // Omu
    [Dependency] private readonly IGameTiming _timing = default!; // Omu

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ChangelingIdentityComponent, SwitchableOverlayToggledEvent>(OnVisionToggle);
        SubscribeLocalEvent<ChangelingComponent, ShotAttemptedEvent>(OnAttemptGunshot); // Omu
    }

    private void OnVisionToggle(Entity<ChangelingIdentityComponent> ent, ref SwitchableOverlayToggledEvent args)
    {
        if (args.User != ent.Owner)
            return;

        if (TryComp(ent, out EyeProtectionComponent? eyeProtection))
            eyeProtection.ProtectionTime = args.Activated ? TimeSpan.Zero : TimeSpan.FromSeconds(10);

        UpdateFlashImmunity(ent, !args.Activated);
    }

    protected virtual void UpdateFlashImmunity(EntityUid uid, bool active) { }

    // Omu start
    private void OnAttemptGunshot(Entity<ChangelingComponent> ent, ref ShotAttemptedEvent args)
    {
        if (!_tagSystem.HasAnyTag(args.Used, ent.Comp.BypassTags))
        {
            var time = _timing.CurTime;

            if (ent.Comp.Messages.Count != 0 && time > ent.Comp.LastPopup + TimeSpan.FromSeconds(1))
            {
                ent.Comp.LastPopup = time;
                _popup.PopupClient(Loc.GetString(_random.Pick(ent.Comp.Messages)), args.User);
            }

            args.Cancel();
        }
    }
    // Omu end
}
