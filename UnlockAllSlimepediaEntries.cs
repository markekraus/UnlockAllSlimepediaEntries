using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace UnlockAllSlimepediaEntries
{
    internal class Entry : MelonMod
    {
        private static bool processed = false;
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {

            if (processed || !sceneName.Contains("zone") || sceneName == "zoneCore")
                return;
            var pediaDirector = Utility.Get<PediaDirector>("SceneContext");
            foreach (var item in Resources.FindObjectsOfTypeAll<PediaEntry>())
            {
                if (item.name == "Locked")
                    continue;
                pediaDirector.Unlock(item);
            }
            processed = true;
        }
    }
}
