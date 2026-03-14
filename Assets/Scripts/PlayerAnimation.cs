using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private Animator animator;

    private Vector2 animationVector;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        HandleAnimationVector();
    }
    private void HandleAnimationVector()
    {
        animationVector = InputManager.Instance.GetInputVectorNormalized();
        animator.SetFloat(HORIZONTAL, animationVector.x);
        animator.SetFloat(VERTICAL, animationVector.y);

    }
}
