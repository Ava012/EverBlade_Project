using UnityEngine;

public class MiniMapPlayerMarker : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float height = 2f;

    void LateUpdate()
    {
        if (player == null)
            return;

        // Stay directly above the player
        transform.position = new Vector3(
            player.position.x,
            player.position.y + height,
            player.position.z
        );

        // Match player's Y rotation
        transform.rotation = Quaternion.Euler(
            0f,
            player.eulerAngles.y,
            0f
        );
    }
}
