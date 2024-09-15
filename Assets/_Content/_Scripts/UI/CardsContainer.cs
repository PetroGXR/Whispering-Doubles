using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PetroGXR.WhisperingDoubles.UI
{
    public class CardsContainer : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField][Range(0.5f, 5f)] float showCardsDuration = 1f;
        [SerializeField][Range(128f, 512f)] float minCardSize = 256f;

        [Header("UI")]
        [SerializeField] GridLayoutGroup layoutGroup;
        [SerializeField] GameObject prefabCard;

        public int MaxCards
        {
            private set;
            get;
        }

        private List<Card> cards = new List<Card>();

        private void Start()
        {
            RectTransform rectCards = layoutGroup.transform as RectTransform;
            int rows = Mathf.CeilToInt(rectCards.rect.height / (minCardSize + layoutGroup.spacing.y));
            int columns = Mathf.CeilToInt(rectCards.rect.width / (minCardSize + layoutGroup.spacing.x));
            MaxCards = rows * columns;
            
            if(MaxCards % 2 != 0 )
            {
                MaxCards--;
            }
        }

        public void Setup(string back, List<string> faces)
        {
            RectTransform rectCards = layoutGroup.transform as RectTransform;

            float ratio = rectCards.rect.height / rectCards.rect.width;
            int columns = 1;
            int rows = Mathf.CeilToInt(columns * ratio);

            while (rows * columns < faces.Count)
            {
                columns++;
                rows = Mathf.CeilToInt(columns * ratio);
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
