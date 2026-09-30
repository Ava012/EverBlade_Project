using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player;
    public float height = 20f;

    void LateUpdate()
    {
        transform.position = new Vector3(
            player.position.x,
            player.position.y + height,
            player.position.z
        );
    }
}
