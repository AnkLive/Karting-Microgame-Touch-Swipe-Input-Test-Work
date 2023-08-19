using UnityEngine;

namespace KartGame.KartSystems {
    
    public class SwipeInput : MonoBehaviour
    {
        //создаем событие свайпа
        public delegate void OnSwipeInput(Vector2 direction);
        public static event OnSwipeInput SwipeEvent;

        //создаем событие сообщения о рывках
        public delegate void OnMessageOutput(bool isSwipe, float directionSwipe);
        public static event OnMessageOutput MessageOutputEvent;

        //записываем начальную позицию тача
        private Vector2 _tapPosition;
        //записываем направление свайпа
        private Vector2 _swipeDelta;

        //описываем ограниечение по длинне свайпа
        [SerializeField] private float swipeEndZone;

        //храним данные о том, происходит ли свайп
        private bool _isSwiping;

        void FixedUpdate()
        {
            //проверяем количество нажатий на экран
            if(Input.touchCount > 0)
            {
                //проверяем фазу тача
                if(Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    //указываем что происходит свайп
                    _isSwiping = true;
                    //записываем позицию начала свайпа
                    _tapPosition = Input.GetTouch(0).position;
                }
                else if(Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    //вычисляем движение свайпа
                    MoveSwipe();
                }
                else if(Input.GetTouch(0).phase == TouchPhase.Canceled ||
                Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    //сбрасываем данные
                    ResetSwipe();
                }

            } 
            //проверяем, происходит ли свайп
            CheckSwipe();
        }

        private void CheckSwipe()
        {
            _swipeDelta = Vector2.zero;

            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if(_isSwiping)
                {
                    if(Input.touchCount > 0)
                        //устанавливаем значение направления свайпа
                        _swipeDelta = Input.GetTouch(0).position - _tapPosition;
                }
                if(_swipeDelta.normalized.x < swipeEndZone)
                {
                    if(SwipeEvent != null)
                    {
                        //создаем событие о свайпе и передаем данные в зависимости от направления свайпа
                        if(Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                            SwipeEvent(new Vector2(touch.deltaPosition.normalized.x, 0));       
                        else    
                            SwipeEvent(new Vector2(0, touch.deltaPosition.normalized.y));
                    }
                }
            }
        }

        private void MoveSwipe()
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                //проверяем не выходит ли свайп за ограничения
                if(_swipeDelta.y > swipeEndZone)
                {
                    if(SwipeEvent != null)
                    {
                        //создаем событие о свайпе и передаем данные в зависимости от направления свайпа
                        if(Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                            SwipeEvent(new Vector2(touch.deltaPosition.normalized.x, 0));       
                        else    
                            SwipeEvent(new Vector2(0, touch.deltaPosition.normalized.y));
                    }
                }
                
                //создаем событие о быстром свайпе и передаем данные о том что свайп все еще происходит и направлении свайпа
                if (touch.deltaPosition.y >= 10f || touch.deltaPosition.y <= -10f)
                        MessageOutputEvent?.Invoke(true, touch.deltaPosition.normalized.y);
            }
        }

        //сбрасываем данные
        private void ResetSwipe()
        {
            _isSwiping = false;

            _tapPosition = Vector2.zero;
            _swipeDelta = Vector2.zero;

            //создаем событие о том что свайп не происходит
            MessageOutputEvent?.Invoke(false, 0);  
        }
    }
}
