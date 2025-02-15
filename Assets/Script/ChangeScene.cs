using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    [SerializeField] private int spawnPointID = 0;
    [SerializeField] private int sceneID = 0;

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.PrepareTransition(spawnPointID, sceneID);
    }
}
