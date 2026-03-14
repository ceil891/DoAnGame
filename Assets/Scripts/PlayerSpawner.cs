using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform spawnPoint;

    void Start()
    {
        int index = PlayerPrefs.GetInt("SelectedCharacter", 0);

        GameObject player = Instantiate(
            playerPrefabs[index],
            spawnPoint.position,
            Quaternion.identity
        );

        // ⭐ GÁN START POINT CHO RESPAWN (DÒNG QUAN TRỌNG)
        PlayerRespawn respawn = player.GetComponent<PlayerRespawn>();
        if (respawn != null)
        {
            respawn.startPoint = spawnPoint;
        }
        else
        {
            Debug.LogError("PlayerRespawn script không tồn tại!");
        }

        // 🎥 Camera follow player
        CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
        if (cam != null)
        {
            cam.target = player.transform;
        }
    }
}
