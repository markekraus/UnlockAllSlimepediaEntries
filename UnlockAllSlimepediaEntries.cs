using System.Collections.Generic;
using Il2Cpp;
using Il2CppMonomiPark.SlimeRancher.Pedia;
using MelonLoader;
using UnityEngine;

namespace UnlockAllSlimepediaEntries
{
    internal class Entry : MelonMod
    {
        private static HashSet<string> processedSaves = new ();
        private readonly List<string> ignoredPediaEntries = new() {"CoralCove", "GreyLabyrinth", "Locked", "LockedSlime"};
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (!sceneName.Contains("zone") || sceneName == "zoneCore")
                return;
            
            var savedGame = Utility.Get<GameContext>("GameContext").AutoSaveDirector.SavedGame.GameState.GameName;
            if (!processedSaves.Add(savedGame))
                return;
            
            var pediaDirector = Utility.Get<PediaDirector>("SceneContext");
            foreach (var item in Resources.FindObjectsOfTypeAll<PediaEntry>())
            {
                if (ignoredPediaEntries.Contains(item.name) || pediaDirector.IsUnlocked(item))
                    continue;
                try
                {
                    pediaDirector.Unlock(item);
                }
                catch (System.Exception e)
                {
                    LoggerInstance.Msg($"Unable to unlock {item.name} in {sceneName}. {e}");
                }
            }
        }
    }
}
