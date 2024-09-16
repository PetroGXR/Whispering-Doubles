using PetroGXR.WhisperingDoubles.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PetroGXR.WhisperingDoubles.UI
{
    [RequireComponent(typeof(Animator))]
    public class GameplayManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField][Range(10, 100)] int scorePerMatch = 10;
        [SerializeField][Range(1, 10)] int lossPerTurn = 5;

        [Header("UI")]
        [SerializeField] TextMeshProUGUI textMatches;
        [SerializeField] TextMeshProUGUI textTurns;
        [SerializeField] TextMeshProUGUI textScore;
        [SerializeField] TextMeshProUGUI textFinishScore;
        [SerializeField] TextMeshProUGUI textFinishBonus;
        [SerializeField] Button buttonMenu;
        [SerializeField] Button buttonReplay;

        Queue<Card> flippedCards = new Queue<Card>();
        Card firstFlippedCard, secondFlippedCard;
        bool validFlip;
        int turns;
        int matches;
        int score;
        int targetMatches;

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

        private void Start()
        {
            buttonMenu.onClick.AddListener(() =>
            {
                GameManager.Instance.ShowMenu();
            });

            buttonReplay.onClick.AddListener(() =>
            {
                Hide();
                GameManager.Instance.StartGame(targetMatches * 2);
            });
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
                    SoundManager.Instance.PlayCorrect();
                }
                else
                {
                    SoundManager.Instance.PlayWrong();
                }

                UpdateUI();

                if(matches == targetMatches) 
                {
                    Invoke(nameof(FinishGame), 1.5f);
                }
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

        private void FinishGame()
        {
            int bonus = Mathf.Max(0, score - (turns - matches) * lossPerTurn);
            textFinishScore.text = score.ToString("N0");
            textFinishBonus.text = bonus.ToString("N0");
            UserDataManager.Instance.AddScore(bonus + score);
            UserDataManager.Instance.CountPlayedLevel();
            Animator.SetTrigger("ShowFinish");
            SoundManager.Instance.PlayWinner();
        }

        public void StartGameplay(int target)
        {
            targetMatches = target;
            turns = 0;
            matches = 0;
            score = 0;

            UpdateUI();

            Animator.SetTrigger("ShowGameplay");
        }

        public void Hide()
        {
            Animator.SetTrigger("HideFinish");
        }
    }
}