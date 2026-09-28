using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

namespace ECommons.IPC.Subscribers.Lifestream;

    public class CustomAliasCommand
    {
        [Obfuscation] public CustomAliasKind Kind;
        [Obfuscation] public Vector3 Point;
        [Obfuscation] public List<Vector3> ExtraPoints;
        [Obfuscation] public uint Aetheryte;
        [Obfuscation] public int World;
        [Obfuscation] public Vector2 CenterPoint;
        [Obfuscation] public Vector3 CircularExitPoint;
        [Obfuscation] public (float Min, float Max)? Clamp;
        [Obfuscation] public float Precision;
        [Obfuscation] public int Tolerance;
        [Obfuscation] public bool WalkToExit;
        [Obfuscation] public float SkipTeleport;
        [Obfuscation] public uint DataID;
        [Obfuscation] public bool UseTA;
        [Obfuscation] public List<string> SelectOption;
        [Obfuscation] public bool StopOnScreenFade;
        [Obfuscation] public bool NoDisableYesAlready;
        [Obfuscation] public bool UseFlight;
        [Obfuscation] public float Scatter;
        [Obfuscation] public bool MountUpConditional;
        [Obfuscation] public bool RequireTerritoryChange;
        [Obfuscation] public uint Territory;
        [Obfuscation] public float? InteractDistance;
        [Obfuscation] public int Timeout;
        [Obfuscation] public bool RequireUiOpen;
        [Obfuscation] public bool ExcelOnlyFirst;
    }
