using System;
using UnityEngine;

namespace KartGame.KartSystems 
{
    public class TouchInput : BaseInput
    {   
        private Vector2 SwipeDirection;

        //подписываемся на событие свайпа
        private void Awake() => SwipeInput.SwipeEvent += OnSwipe;

        //получаем направление свайпа
        private void OnSwipe(Vector2 direction) => SwipeDirection = direction;

        public override InputData GenerateInput() 
        {
            return new InputData
            {
                Accelerate = Convert.ToBoolean(Input.touchCount),
                TurnInput = SwipeDirection.x
            };
        }

        //отписываемся от события свайпа
        private void OnDestroy() => SwipeInput.SwipeEvent -= OnSwipe;
    }
}
