using System.Reflection;

namespace ECommons.IPC.Subscribers.Lifestream;

public enum CustomAliasKind
{
    [Obfuscation] Teleport_to_Aetheryte,
    [Obfuscation] Move_to_point,
    [Obfuscation] Navmesh_to_point,
    [Obfuscation] Change_world,
    [Obfuscation] Use_Aethernet,
    [Obfuscation] Circular_movement,
    [Obfuscation] Interact,
    [Obfuscation] Mount_Up,
    [Obfuscation] Select_Yes,
    [Obfuscation] Select_List_Option,
    [Obfuscation] Confirm_Contents_Finder,
    [Obfuscation] Wait_for_Transition,
    [Obfuscation] Return_to_Home_World,
    [Obfuscation] Close_UI,
    [Obfuscation] Go_to_House,
    [Obfuscation] Go_to_Apartment,
}
