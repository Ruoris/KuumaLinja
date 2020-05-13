using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistBuff : MonoBehaviour
{
  private GameObject player;
  private GameObject scientistPanel;

    void Start()
    {
        scientistPanel = GameObject.Find("Misc stuff/Canvas/ScientistPanel");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
      

    }
    public void AmmoBuff()
    {
        GameObject boostPanel = GameObject.FindWithTag("BuffPanels");
        if (GameStatus.status.ammobuffsCollected < 3)
        {
        GameStatus.status.ammobuffsCollected++;
        GameStatus.status.AmmobuffSetter();
        }
        
      
        player.GetComponent<Weapons>().AmmoBuff();
        boostPanel.SetActive(false);
        scientistPanel.SetActive(false);
        Time.timeScale = 1;
      
    }
    public void SpeedBuff()
    {
        GameObject boostPanel = GameObject.FindWithTag("BuffPanels");
        GameStatus.status.movementSpeedsCollected++;
        GameStatus.status.MovementSpeedSetter();
        player.GetComponent<Weapons>().MovementSpeedBuff();
        boostPanel.SetActive(false);
        scientistPanel.SetActive(false);
        Time.timeScale =1;
    }
    public void VisionBuff()
    {
        GameObject boostPanel = GameObject.FindWithTag("BuffPanels");
        GameStatus.status.radiusIncreasesCollected++;
        GameStatus.status.RadiusIncreaseSetter();
        player.GetComponent<Weapons>().RadiusIncrease();
        boostPanel.SetActive(false);
        scientistPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void No()
    {
        scientistPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
