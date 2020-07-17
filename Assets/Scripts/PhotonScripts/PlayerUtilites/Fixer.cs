using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fixer : MonoBehaviour
{

    public GameObject Environment;
    public GameObject multiplayerEnemyPrefab;

    void Start()
    {
        Environment = this.gameObject;
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "MultiplayerEndless")
        {

            foreach (EnemyController singleplayerEnemy in Environment.GetComponentsInChildren<EnemyController>())
            {
                if (singleplayerEnemy.gameObject.tag == "Enemy")
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        MasterManager.NetworkInstantiate(multiplayerEnemyPrefab, singleplayerEnemy.gameObject.GetComponent<Transform>().position,
                       singleplayerEnemy.gameObject.GetComponent<Transform>().rotation);
                    }
                    Destroy(singleplayerEnemy.gameObject);
                }



            }

        }


    }
   

}
