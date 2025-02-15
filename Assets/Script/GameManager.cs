using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [Header("Scene")]
    public int oldScenePosition;
    public Vector3 oldPlayerPosition;
    public Quaternion oldPlayerRotation;
    private int nextSpawnPointID;

    [Header("Combat")]
    public Pnj_Data[] nextEnnemis = new Pnj_Data[3];
    public Pnj_Data[] HeroTeam = new Pnj_Data[3];


    private void Awake()
    {
        // Préparation d'un singleton (cf slides)
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PrepareTransition(int spawnPointID, int sceneID)
    {
        // Stocker le spawn point id à atteindre dans la prochaine scene
        nextSpawnPointID = spawnPointID;
        Scene scene = SceneManager.GetActiveScene();
        oldScenePosition = scene.buildIndex;
        SceneManager.LoadScene(sceneID);
    }

    public void ReturnPlayerAtOldScene()
    {
        SceneManager.LoadScene(oldScenePosition);
    }

    public void InitPlayer(PlayerController playerController)
    {
        // si le spawnpoint == -1 => on le remet a son ancienne position
        if(nextSpawnPointID == -1)
        {
            Debug.Log("reviens");
            Debug.Log(oldPlayerPosition);
            playerController.Init(oldPlayerPosition);
            playerController.GetComponent<Rigidbody>().rotation = oldPlayerRotation;
            return;
        }

        // Trouver le spawn point où placer le joueur
        SpawnPointController[] list = FindObjectsOfType<SpawnPointController>();
        foreach (SpawnPointController spawnPointController in list)
        {
            if(spawnPointController.SpawnPointID == nextSpawnPointID)
            {
                playerController.Init(spawnPointController.transform.position);
                break;
            }
            
        }
    }


}