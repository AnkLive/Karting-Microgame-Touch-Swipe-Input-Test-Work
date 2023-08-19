using UnityEngine;

namespace KartGame.KartSystems {
    
    public class SwipeInput : MonoBehaviour
    {
        public delegate void OnSwipeInput(Vector2 direction);
        public static event OnSwipeInput SwipeEvent;

        public delegate void OnMessageOutput(bool isSwipe, float directionSwipe);
        public static event OnMessageOutput MessageOutputEvent;

        private Vector2 _tapPosition;
        private Vector2 _swipeDelta;

        [SerializeField] private float swipeEndZone;

        private bool _isSwiping;

        void FixedUpdate()
        {
            if(Input.touchCount > 0)
            {
                if(Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _isSwiping = true;
                    _tapPosition = Input.GetTouch(0).position;
                }
                else if(Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                   MoveSwipe();
                }
                else if(Input.GetTouch(0).phase == TouchPhase.Canceled ||
                Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    ResetSwipe();
                }
            } 
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
                        _swipeDelta = Input.GetTouch(0).position - _tapPosition;
                }
                if(_swipeDelta.normalized.x < swipeEndZone)
                {
                    if(SwipeEvent != null)
                    {
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

                if(_swipeDelta.y > swipeEndZone)
                {
                    if(SwipeEvent != null)
                    {
                        if(Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                            SwipeEvent(new Vector2(touch.deltaPosition.normalized.x, 0));       
                        else    
                            SwipeEvent(new Vector2(0, touch.deltaPosition.normalized.y));
                    }
                }
                
                if (touch.deltaPosition.y >= 10f || touch.deltaPosition.y <= -10f)
                        MessageOutputEvent?.Invoke(true, touch.deltaPosition.normalized.y);
            }
        }

        private void ResetSwipe()
        {
            _isSwiping = false;

            _tapPosition = Vector2.zero;
            _swipeDelta = Vector2.zero;

            MessageOutputEvent?.Invoke(false, 0);  
        }
    }
}
