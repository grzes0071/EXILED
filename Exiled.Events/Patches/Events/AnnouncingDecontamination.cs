﻿// -----------------------------------------------------------------------
// <copyright file="AnnouncingDecontamination.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.Patches.Events
{
    #pragma warning disable SA1313
    using Exiled.Events.Handlers;
    using Exiled.Events.Handlers.EventArgs;
    using HarmonyLib;
    using LightContainmentZoneDecontamination;

    /// <summary>
    /// Patches <see cref="DecontaminationController.UpdateSpeaker"/>.
    /// Adds the <see cref="Map.AnnouncingDecontamination"/> event.
    /// </summary>
    [HarmonyPatch(typeof(DecontaminationController), nameof(DecontaminationController.UpdateSpeaker))]
    public class AnnouncingDecontamination
    {
        /// <summary>
        /// Prefix of <see cref="DecontaminationController.UpdateSpeaker"/>.
        /// </summary>
        /// <param name="__instance">The <see cref="DecontaminationController"/> instance.</param>
        /// <param name="hard"><inheritdoc cref="AnnouncingDecontaminationEventArgs.IsGlobal"/></param>
        /// <returns>Returns a value indicating whether the original method has to be executed or not.</returns>
        public static bool Prefix(DecontaminationController __instance, bool hard)
        {
            var ev = new AnnouncingDecontaminationEventArgs(__instance.nextPhase, hard);

            Map.OnAnnouncingDecontamination(ev);

            __instance.nextPhase = ev.Id;

            return ev.IsAllowed;
        }
    }
}
