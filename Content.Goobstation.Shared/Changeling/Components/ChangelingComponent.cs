// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Tag;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Changeling.Components;

/// <summary>
/// Marks an entity as a changeling, and holds generic changeling data.
/// For the component holding more complex changeling data, see ChangelingIdentityComponent.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState] // Omu, add AutoGenerateComponentState
public sealed partial class ChangelingComponent : Component
{
    /// <summary>
    /// The starting components to be assigned to a changeling.
    /// </summary>
    [DataField]
    public ProtoId<ChangelingStartingEvolutionPrototype> EvolutionsProto = "DefaultChangeling";

    /// <summary>
    /// Have the components been assigned?
    /// </summary>
    [DataField]
    public bool EvolutionsAssigned;

    [DataField]
    public string MindswapText = "changeling"; // only used for mindswap attempts

    // Omu start
    [DataField, AutoNetworkedField]
    public List<string> Messages = ["Our body rejects the use of this weapon!"];

    public TimeSpan LastPopup;

    [DataField, AutoNetworkedField]
    public List<ProtoId<TagPrototype>> BypassTags = [];
    // Omu end
}
