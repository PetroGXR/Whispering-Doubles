using PetroGXR.WhisperingDoubles.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PetroGXR.WhisperingDoubles.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] CardsContainer cardsContainer;
        [SerializeField] GameplayManager gameplayManager;

        [Header("Addressables Asset Paths")]
        [SerializeField] List<string> cardFaces = new List<string>();
        [SerializeField] List<string> cardBacks = new List<string>();

        public void StartGame(int cards)
        {
            List<string> faces = new List<string>();
            string face;

            while (faces.Count < cards)
            {
                do
                {
                    face = cardFaces[Random.Range(0, cardFaces.Count)];
                }
                while (faces.Contains(face));

                faces.Add(face);
                faces.Add(face);
            }

            cardsContainer.Setup(cardBacks[Random.Range(0, cardBacks.Count)], faces);
            gameplayManager.StartGameplay();
        }

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

        [ContextMenu("Test Game")]
        private void TestGame()
        {
            StartGame(Random.Range(4, 8));
        }
#endif
    }
}
