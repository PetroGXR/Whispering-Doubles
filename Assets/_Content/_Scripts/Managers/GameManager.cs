using PetroGXR.WhisperingDoubles.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PetroGXR.WhisperingDoubles.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] CardsContainer cardsContainer;

        [Header("Addressables Asset Paths")]
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
