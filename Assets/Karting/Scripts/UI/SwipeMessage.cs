using System.Collections;
using UnityEngine;

namespace KartGame.KartSystems 
{
    public class SwipeMessage : MonoBehaviour
    {
        [SerializeField] private DisplayMessage throwBackDisplayMessage;
        [SerializeField] private DisplayMessage throwForwardDisplayMessage;

        [SerializeField, Range(0, 2)] private float timeForAQuickSwipe;

        private bool nextMessage = true;

        private void Awake() => SwipeInput.MessageOutputEvent += ShowMessage;


        private void Start() 
        {
            throwBackDisplayMessage.gameObject.SetActive(false);
            throwForwardDisplayMessage.gameObject.SetActive(false);
        }

        private void ShowMessage(bool isSwipe, float directionSwipe)
        {
            if(nextMessage)
            {
                if(directionSwipe > 0)
                    throwForwardDisplayMessage.gameObject.SetActive(true);
                else if(directionSwipe < 0)
                    throwBackDisplayMessage.gameObject.SetActive(true);

                nextMessage = false;
            }
            StartCoroutine(HideDisplayMessageAfterTime(timeForAQuickSwipe));
        }

        private void HideDisplayMessage()
        {
            throwBackDisplayMessage.gameObject.SetActive(false);     
            throwForwardDisplayMessage.gameObject.SetActive(false);  
        }

        private IEnumerator HideDisplayMessageAfterTime(float timeForAQuickSwipe)
        {
            float timer = 0f;
            while (timer < timeForAQuickSwipe) 
            {
                yield return null;
                timer += Time.unscaledDeltaTime;
            }
            HideDisplayMessage();
            nextMessage = true;
        }
    }
}
