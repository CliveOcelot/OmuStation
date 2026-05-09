using Content.Shared.Item;
using Content.Shared.Popups;
using Content.Shared._Shitmed.Antags.Abductor;
using Content.Shared.PDA;
using Content.Shared.Access.Components;
using Content.Shared.Body.Organ;
using Content.Shared.Body.Part;
using Content.Shared._Omu.Abductor;

namespace Content.Shared._Omu.Abductor;

public abstract class SharedOmuAbductorSystem : EntitySystem
{
    [Dependency] private readonly SharedPopupSystem _popup = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<AbductorComponent, PickupAttemptEvent>(OnPickup);
    }

    private void OnPickup(EntityUid uid, AbductorComponent component, PickupAttemptEvent args)
    {
        if (HasComp<AbductorItemComponent>(args.Item)
            || HasComp<PdaComponent>(args.Item)
            || HasComp<IdCardComponent>(args.Item)
            || HasComp<OrganComponent>(args.Item)
            || HasComp<BodyPartComponent>(args.Item))
            return;

        _popup.PopupClient(Loc.GetString("abductor-pickup-item-fail"), args.Item, uid);
        args.Cancel();
    }
}
