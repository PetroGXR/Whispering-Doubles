using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PetroGXR.WhisperingDoubles.UI
{
    [RequireComponent(typeof(Animator))]
    public class MenuManager : MonoBehaviour
    {
        [Header("Settings")]

        [Header("UI")]
        [SerializeField] TextMeshProUGUI textTotalScore;
        [SerializeField] TextMeshProUGUI textLevelsPlayed;

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

        private void UpdateUI()
        {
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