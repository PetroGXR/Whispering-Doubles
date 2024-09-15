using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PetroGXR.WhisperingDoubles.UI
{
    public class CardsContainer : MonoBehaviour
    {
        [SerializeField] GridLayoutGroup layoutGroup;
        [SerializeField] GameObject prefabCard;
        [SerializeField] [Range(0.5f, 5f)] float showCardsDuration = 1f;

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

            StartCoroutine(ShowingCards());
        }

        IEnumerator ShowingCards()
        {
            WaitForSeconds wait = new WaitForSeconds(Mathf.Min(0.1f, showCardsDuration / cards.Count));
            foreach(Card card in cards)
            {
                card.Show();
                yield return wait;
            }
        }
    }
}
