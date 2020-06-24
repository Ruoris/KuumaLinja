using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyControllerChanger : MonoBehaviour
{
    EnemyController enemyController;
    EnemyControllerMultiplayer controllerMultiplayer;

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        enemyController = GetComponent<EnemyController>();
        controllerMultiplayer = GetComponent<EnemyControllerMultiplayer>();
        if (scene.name == "MultiplayerEndless")
        {
           
            enemyController.enabled = false;
            
            controllerMultiplayer.enabled = true;

        }
        if (scene.name != "MultiplayerEndless")
        {
            enemyController.enabled = true;

            controllerMultiplayer.enabled = false;

        }
    }

   
}
