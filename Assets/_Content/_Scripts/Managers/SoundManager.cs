using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PetroGXR.WhisperingDoubles.Managers.UserDataManager;

namespace PetroGXR.WhisperingDoubles.Managers
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] AudioSource audioSourceSFX;
        [SerializeField] AudioClip clipFlip;
        [SerializeField] AudioClip clipCorrect;
        [SerializeField] AudioClip clipWrong;
        [SerializeField] AudioClip clipWinner;
        [SerializeField] AudioClip clipClick;

        public static SoundManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        public void PlayFlip()
        {
            audioSourceSFX.PlayOneShot(clipFlip);
        }

        public void PlayCorrect()
        {
            audioSourceSFX.PlayOneShot(clipCorrect);
        }


        public void PlayWrong()
        {
            audioSourceSFX.PlayOneShot(clipWrong);
        }

        public void PlayWinner()
        {
            audioSourceSFX.PlayOneShot(clipWinner);
        }

        public void PlayClick()
        {
            audioSourceSFX.PlayOneShot(clipClick);
        }
    }
}