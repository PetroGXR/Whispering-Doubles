using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PetroGXR.WhisperingDoubles.UI
{
    public class CardsContainer : MonoBehaviour
    {
        [SerializeField] GridLayoutGroup layoutGroup;
        [SerializeField] GameObject prefabCard;

        private List<Card> cards = new List<Card>();

        public void Setup(string back, List<string> faces)
        {
            RectTransform rectCards = layoutGroup.transform as RectTransform;

            float ratio = rectCards.rect.height / rectCards.rect.width;
            int columns = 1;
            int rows = Mathf.FloorToInt(columns * ratio);

            while (rows * columns < faces.Count)
            {
                columns++;
                rows = Mathf.FloorToInt(columns * ratio);
                
            }

            float minSize = Mathf.Min((rectCards.rect.width - columns * layoutGroup.spacing.x) / columns, (rectCards.rect.height - rows * layoutGroup.spacing.y) / rows);
            layoutGroup.cellSize = Vector2.one * minSize;

            while (faces.Count > 0)
            {
                int next = Random.Range(0, faces.Count);

                Card card = Instantiate(prefabCard, layoutGroup.transform).GetComponent<Card>();
                card.Setup(faces[next], back);
                cards.Add(card);

                faces.RemoveAt(next);
            }
        }
    }
}
