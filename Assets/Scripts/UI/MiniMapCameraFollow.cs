using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{
    public Transform player;
    public float height = 20f;

    void LateUpdate()
    {
        if (player == null)
            return;

        transform.position = new Vector3(
            player.position.x,
            player.position.y + height,
            player.position.z
        );

        // Always look straight down
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
