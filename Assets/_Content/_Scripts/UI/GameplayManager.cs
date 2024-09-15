using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PetroGXR.WhisperingDoubles.UI
{
    [RequireComponent(typeof(Animator))]
    public class GameplayManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField][Range(5, 50)] int scorePerMatch = 5;

        [Header("UI")]
        [SerializeField] TextMeshProUGUI textMatches;
        [SerializeField] TextMeshProUGUI textTurns;
        [SerializeField] TextMeshProUGUI textScore;

        Queue<Card> flippedCards = new Queue<Card>();
        Card firstFlippedCard, secondFlippedCard;
        bool validFlip;
        int turns;
        int matches;
        int score;

        Animator animator;
        Animator Animator
        {
            get
            {
                if (animator == null)
                {
                    animator = GetComponent<Animator>();
                }

                return animator;
            }
        }

        private void OnEnable()
        {
            Card.OnCardFlip += Card_OnCardFlip;
        }

        private void OnDisable()
        {
            Card.OnCardFlip -= Card_OnCardFlip;
        }

        private void Update()
        {
            if (flippedCards.Count >= 2)
            {
                firstFlippedCard = flippedCards.Dequeue();
                secondFlippedCard = flippedCards.Dequeue();
                validFlip = firstFlippedCard.Id == secondFlippedCard.Id;
                firstFlippedCard.Shake(validFlip);
                secondFlippedCard.Shake(validFlip);

                turns++;

                if (validFlip)
                {
                    matches++;
                    score += scorePerMatch;
                }

                UpdateUI();
            }
        }

        private void Card_OnCardFlip(Card card)
        {
            flippedCards.Enqueue(card);
        }

        private void UpdateUI()
        {
            textMatches.text = matches.ToString("N0");
            textTurns.text = turns.ToString("N0");
            textScore.text = score.ToString("N0");
        }

        public void StartGameplay()
        {
            turns = 0;
            matches = 0;
            score = 0;

            UpdateUI();

            Animator.SetTrigger("ShowGameplay");
        }
    }
}