using System;
using UnityEngine;

namespace KartGame.KartSystems {

    public class TouchInput : BaseInput
    {   
        public string TurnInputName = "Horizontal";
        public string AccelerateButtonName = "Accelerate";
        public string BrakeButtonName = "Brake";

        public override InputData GenerateInput() {
            return new InputData
            {
                Accelerate = Convert.ToBoolean(Input.touchCount),
                Brake = Input.GetButton(BrakeButtonName),
                TurnInput = Input.GetAxis("Horizontal")
            };
        }
    }
}
