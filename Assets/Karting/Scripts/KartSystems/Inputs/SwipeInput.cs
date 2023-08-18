using UnityEngine;

namespace KartGame.KartSystems {
    
    public class SwipeInput : MonoBehaviour
    {
        public delegate void OnSwipeInput(Vector2 direction);
        public static event OnSwipeInput SwipeEvent;

        private Vector2 _tapPosition;
        private Vector2 _swipeDelta;

        private bool _isSwiping;

        public DisplayMessage throwBackDisplayMessage;
        public DisplayMessage throwForwardDisplayMessage;


        private void Start() 
        {
            throwBackDisplayMessage.gameObject.SetActive(false);
            throwForwardDisplayMessage.gameObject.SetActive(false);
        }

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
                    SwipeEvent(new Vector2(touch.deltaPosition.normalized.x, 0));       
                else    
                    SwipeEvent(new Vector2(0, touch.deltaPosition.normalized.y));
            }
        }

        private void MoveSwipe()
        {
            Touch touch = Input.GetTouch(0);

            if(touch.deltaPosition.normalized.y > 0)
            {
                throwBackDisplayMessage.gameObject.SetActive(true);
            }
            else if(touch.deltaPosition.normalized.y < 0)
            {
                throwForwardDisplayMessage.gameObject.SetActive(true);
            }
            if(SwipeEvent != null)
            {
                if(Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                    SwipeEvent(new Vector2(touch.deltaPosition.normalized.x, 0));       
                else    
                    SwipeEvent(new Vector2(0, touch.deltaPosition.normalized.y));
            }
        }

        private void ResetSwipe()
        {
            _isSwiping = false;

            _tapPosition = Vector2.zero;
            _swipeDelta = Vector2.zero;
            throwBackDisplayMessage.gameObject.SetActive(false);     
            throwForwardDisplayMessage.gameObject.SetActive(false);    
        }
    }
}
