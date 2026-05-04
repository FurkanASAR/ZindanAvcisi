using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public static FollowPlayer Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

        private void LateUpdate()
        {
            if (Player.Instance != null)
            {
                Vector3 playerPosition = Player.Instance.transform.position;
                transform.position = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);
            }
    }
}
