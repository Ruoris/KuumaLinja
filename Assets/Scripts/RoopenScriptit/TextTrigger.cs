using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public Textcreater text;
    public void TriggerText()
    {
        FindObjectOfType<Textmanager>().StartText(text);
    }
}
