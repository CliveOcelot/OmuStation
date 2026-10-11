// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Omu.MindShield;
using Content.Shared.Robotics;
using Robust.Client.UserInterface;

namespace Content.Client._Omu.MindShield.UI;

public sealed class MindShieldBombConsoleBoundUserInterface : BoundUserInterface
{
    [ViewVariables]
    public MindShieldBombConsoleWindow _window = default!;

    public MindShieldBombConsoleBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();

        _window = this.CreateWindow<MindShieldBombConsoleWindow>();
        _window.SetEntity(Owner);

        _window.OnDisablePressed += address =>
        {
            SendMessage(new MindShieldBombConsoleDisableMessage(address));
        };
        _window.OnDestroyPressed += address =>
        {
            SendMessage(new MindShieldBombConsoleDestroyMessage(address));
        };
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);

        if (state is not MindShieldBombConsoleState cast)
            return;

        _window.UpdateState(cast);
    }
}
