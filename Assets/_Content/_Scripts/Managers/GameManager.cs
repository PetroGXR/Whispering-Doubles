using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace PetroGXR.WhisperingDoubles
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] List<string> cardFaces = new List<string>();
        [SerializeField] List<string> cardBacks = new List<string>();

#if UNITY_EDITOR
        [ContextMenu("Load Addressables Asset Paths")]
        private void LoadPaths()
        {
            var faces = UnityEditor.AddressableAssets.AddressableAssetSettingsDefaultObject.Settings.DefaultGroup.entries.Where(entry => entry.labels.Contains("CardFace"));
            cardFaces.Clear();
            foreach (var face in faces)
            {
                cardFaces.Add(face.AssetPath);
            }

            var backs = UnityEditor.AddressableAssets.AddressableAssetSettingsDefaultObject.Settings.DefaultGroup.entries.Where(entry => entry.labels.Contains("CardBack"));
            cardBacks.Clear();
            foreach (var back in backs)
            {
                cardBacks.Add(back.AssetPath);
            }
        }
#endif
    }
}
