﻿using System.Reflection;
using NitroxClient.Communication.Abstract;
using NitroxModel.Core;
using NitroxModel.Helper;
using UnityEngine;

namespace NitroxPatcher.Patches.Dynamic;

public sealed partial class IngameMenu_QuitGame_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((IngameMenu t) => t.QuitGame(default(bool)));

    public static void Prefix()
    {
        // TODO: Remove this patch after fixing that no MP resources are left on disconnect. So that we can return to main menu.
        IMultiplayerSession multiplayerSession = NitroxServiceLocator.LocateService<IMultiplayerSession>();
        multiplayerSession.Disconnect();
        Application.Quit();
    }
}
