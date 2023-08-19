using System.Collections;
using UnityEngine;

namespace KartGame.KartSystems 
{
    public class SwipeMessage : MonoBehaviour
    {
        //получаем данные об экранах сообщений
        [SerializeField] private DisplayMessage throwBackDisplayMessage;
        [SerializeField] private DisplayMessage throwForwardDisplayMessage;

        //устанавливаем ограничение по времени для появления сообщения
        [SerializeField, Range(0, 2)] private float timeForAQuickSwipe;

        //устанавливаем проверку на возможность вывода сообщения
        private bool nextMessage = true;

        //подписываемся на событие вывода сообщения
        private void Awake() => SwipeInput.MessageOutputEvent += ShowMessage;

        //скрываем панели сообщений
        private void Start() => HideDisplayMessage();

        //показываем сообщения
        private void ShowMessage(bool isSwipe, float directionSwipe)
        {
            if(nextMessage)
            {
                //в зависимости от направления показываем сообщение о броске
                if(directionSwipe > 0)
                    throwForwardDisplayMessage.gameObject.SetActive(true);
                else if(directionSwipe < 0)
                    throwBackDisplayMessage.gameObject.SetActive(true);

                nextMessage = false;
            }

            //запускаем таймер, по истечению времени которого панели сообщений будут скрыты
            StartCoroutine(HideDisplayMessageAfterTime(timeForAQuickSwipe));
        }

        //скрываем панели сообщений
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
