using UnityEngine;

public class RotateDirectionArrow : MonoBehaviour
{
    [SerializeField] private Transform kartTransform;
    private Transform gameObjectTransform;

    private void Awake() {
        gameObjectTransform = GetComponent<Transform>();
    }
    private void Update() 
    {
        gameObjectTransform.localRotation = 
            Quaternion.Euler(gameObjectTransform.localRotation.x, kartTransform.localRotation.y, gameObjectTransform.localRotation.z);;
    }
} 
