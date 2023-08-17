using System;
using UnityEngine;

namespace KartGame.KartSystems {

    public class TouchInput : BaseInput
    {   
        public string TurnInputName = "Horizontal";
        public string AccelerateButtonName = "Accelerate";
        public string BrakeButtonName = "Brake";

<<<<<<< HEAD
        public Vector2 SwipeDirection;

        private void Awake() => SwipeInput.SwipeEvent += OnSwipe;

        private void OnSwipe(Vector2 direction) => SwipeDirection = direction;

=======
>>>>>>> f7bb07d (init commit)
        public override InputData GenerateInput() {
            return new InputData
            {
                Accelerate = Convert.ToBoolean(Input.touchCount),
                Brake = Input.GetButton(BrakeButtonName),
<<<<<<< HEAD
                TurnInput = SwipeDirection.x
=======
                TurnInput = Input.GetAxis("Horizontal")
>>>>>>> f7bb07d (init commit)
            };
        }
    }
}
