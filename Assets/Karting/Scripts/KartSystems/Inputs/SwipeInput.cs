using UnityEngine;

namespace KartGame.KartSystems {
    
    public class SwipeInput : MonoBehaviour
    {
        public delegate void OnSwipeInput(Vector2 direction);
        public static event OnSwipeInput SwipeEvent;

        private Vector2 _tapPosition;
        private Vector2 _swipeDelta;

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

            Touch touch = Input.GetTouch(0);

            if(_isSwiping)
            {
                if(Input.touchCount > 0)
                    _swipeDelta = Input.GetTouch(0).position - _tapPosition;
            }
            if(SwipeEvent != null)
            {
                if(Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                    SwipeEvent(touch.deltaPosition.normalized);
                else    
                    SwipeEvent(_swipeDelta.y > 0 ? Vector2.up : Vector2.down); 
            }
        }

        private void MoveSwipe()
        {
            Touch touch = Input.GetTouch(0);

            if(SwipeEvent != null)
            {
                if(Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                    SwipeEvent(touch.deltaPosition.normalized);
                    
                else    
                    SwipeEvent(_swipeDelta.y > 0 ? Vector2.up : Vector2.down);

                Debug.Log(Mathf.Abs(touch.deltaPosition.normalized.x));
            }
        }

        private void ResetSwipe()
        {
            _isSwiping = false;

            _tapPosition = Vector2.zero;
            _swipeDelta = Vector2.zero;
        }
    }
}
