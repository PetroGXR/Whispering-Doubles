using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace PetroGXR.WhisperingDoubles.UI
{
    [RequireComponent(typeof(Animator))]
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        public static UnityAction<Card> OnCardFlip;

        [SerializeField] Image imageFace;
        [SerializeField] Image imageBack;

        AsyncOperationHandle<Sprite> handleFace;
        AsyncOperationHandle<Sprite> handleBack;

        bool facedDown;

        Animator animator;
        Animator Animator
        {
            get
            {
                if(animator == null)
                {
                    animator = GetComponent<Animator>();
                }

                return animator;
            }
        }

        public string Id
        {
            private set;
            get;
        }

        public void Setup(string face, string back)
        {
            Id = face.Replace('/', '-').Replace("_", "").Replace(".", "-");
            Addressables.LoadAssetAsync<Sprite>(face).Completed += OnLoadFaceDone;
            Addressables.LoadAssetAsync<Sprite>(back).Completed += OnLoadBackDone;
        }

        private void OnLoadFaceDone(AsyncOperationHandle<Sprite> handle)
        {
            if(handle.Status == AsyncOperationStatus.Succeeded)
            {
                imageFace.sprite = handle.Result;
            }
        }

        private void OnLoadBackDone(AsyncOperationHandle<Sprite> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                imageBack.sprite = handle.Result;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!facedDown) return;

            facedDown = false;
            Animator.SetTrigger("FaceUp");
        }

        public void Show()
        {
            Animator.SetTrigger("Show");
        }

        public void Shake(bool valid)
        {
            Animator.SetTrigger(valid ? "Valid" : "Invalid");
        }

        public void FacedUp()
        {
            OnCardFlip?.Invoke(this);
        }

        public void FacedDown()
        {
            facedDown = true;
        }

        public void Destroy()
        {
            if (handleFace.IsValid())
            {
                Addressables.Release(handleFace);
            }

            if (handleBack.IsValid())
            {
                Addressables.Release(handleBack);
            }

            Destroy(gameObject);
        }
    }
}
