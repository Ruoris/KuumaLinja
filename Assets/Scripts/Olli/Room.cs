using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Room", menuName = "Rooms/Room")]
public class Room : ScriptableObject
{
    public new string roomType;

    public GameObject wall;
    public GameObject halfWall;
    public GameObject wallCorner;
    public GameObject door;
    public GameObject floor;

    public GameObject[] furniture;

    public int roomX;
    public int roomY;
}
