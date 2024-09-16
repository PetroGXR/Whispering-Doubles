using PetroGXR.WhisperingDoubles.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PetroGXR.WhisperingDoubles.UI
{
    [RequireComponent(typeof(Animator))]
    public class MenuManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] CardsContainer cardsContainer;

        [Header("Settings")]

        [Header("UI")]
        [SerializeField] TextMeshProUGUI textTotalScore;
        [SerializeField] TextMeshProUGUI textLevelsPlayed;
        [SerializeField] TextMeshProUGUI textCards;
        [SerializeField] Button buttonPlus;
        [SerializeField] Button buttonMinus;
        [SerializeField] Button buttonPlay;
        [SerializeField] Image imagePlusDisabled;
        [SerializeField] Image imageMinusDisabled;

        int levelCards = 4;

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

        private void Start()
        {
            buttonPlay.onClick.AddListener(() =>
            {
                Hide();
                GameManager.Instance.StartGame(levelCards);
            });

            buttonPlus.onClick.AddListener(() =>
            {
                levelCards += 2;
                UserDataManager.Instance.SetLevelCards(levelCards);
                UpdateUI();
            });

            buttonMinus.onClick.AddListener(() =>
            {
                levelCards -= 2;
                UserDataManager.Instance.SetLevelCards(levelCards);
                UpdateUI();
            });

            levelCards = UserDataManager.Instance.LevelCards;
            UpdateUI();
        }

        private void UpdateUI()
        {
            buttonPlus.interactable = levelCards + 2 <= cardsContainer.MaxCards;
            buttonMinus.interactable = levelCards - 2 >= 4;

            imagePlusDisabled.enabled = !buttonPlus.interactable;
            imageMinusDisabled.enabled = !buttonMinus.interactable;

            textTotalScore.text = UserDataManager.Instance.Score.ToString("N0");
            textLevelsPlayed.text = UserDataManager.Instance.PlayedLevels.ToString("N0");
            textCards.text = UserDataManager.Instance.LevelCards.ToString();
        }

        public void Show()
        {
            UpdateUI();

            Animator.SetTrigger("Show");
        }

        public void Hide()
        {
            Animator.SetTrigger("Hide");
        }
    }
}