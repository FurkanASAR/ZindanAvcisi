using UnityEngine;
using UnityEngine.UIElements;

public class AttackPointController : MonoBehaviour
{
    //[SerializeField] Player player;
    [SerializeField] float attackPointDistanceMultiplier;

    private Vector3 lastAttackPosition;

    Vector3 inputVector;
    private void Update()
    {
        SetAttackPointLocalPosition();
    }
    public void SetAttackPointLocalPosition()
    {        
        inputVector = InputManager.Instance.GetInputVectorNormalized();
        if (inputVector != Vector3.zero)
        {
            lastAttackPosition = inputVector;
        }
        transform.localPosition = lastAttackPosition * attackPointDistanceMultiplier;
    }
}
