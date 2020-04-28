using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMelee : MonoBehaviour
{
    public GameObject meleeAnimation, bossRB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.name);
            meleeAnimation.SetActive(true);
            StartCoroutine("Melee");
        }
    }

    IEnumerator Melee()
    {
        yield return new WaitForSeconds(1.49f);
        meleeAnimation.SetActive(false);
    }
}
