using System;
using System.Collections.Generic;
using System.Reflection;

namespace ECommons.IPC.Subscribers.Lifestream;

    public class CustomAlias
    {
        [Obfuscation] public string ExportedName;
        [Obfuscation] public Guid GUID;
        [Obfuscation] public string Alias;
        [Obfuscation] public bool Enabled;
        [Obfuscation] public List<CustomAliasCommand> Commands;
    }