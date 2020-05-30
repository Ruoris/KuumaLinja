using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoDiaSkip : MonoBehaviour
{
   
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene(); ;

        if (scene.buildIndex == 3)
        {
            if (this.gameObject.tag == "DT1" && GameStatus.status.Level1DT1)
            {
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.tag == "DT2" && GameStatus.status.Level1DT2)
            {
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.tag == "DT3" && GameStatus.status.Level1DT3)
            {
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.tag == "DT4" && GameStatus.status.Level1DT4)
            {
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.tag == "DT5" && GameStatus.status.Level1DT5)
            {
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.tag == "DT6" && GameStatus.status.Level1DT6)
            {
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.tag == "DT7" && GameStatus.status.Level1DT7)
            {
                this.gameObject.SetActive(false);
            }
           
        }
        if (scene.buildIndex == 6)
        {
            if (this.gameObject.tag == "DT1" && GameStatus.status.Level6DT1)
            {
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.tag == "DT2" && GameStatus.status.Level6DT2)
            {
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.tag == "DT3" && GameStatus.status.Level6DT3)
            {
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.tag == "DT4" && GameStatus.status.Level6DT4)
            {
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.tag == "DT5" && GameStatus.status.Level6DT5)
            {
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.tag == "DT6" && GameStatus.status.Level6DT6)
            {
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.tag == "DT7" && GameStatus.status.Level6DT7)
            {
                this.gameObject.SetActive(false);
            }

        }

    }

   
}
