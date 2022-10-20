using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public void Open()
    {
        transform.GetChild(0).localEulerAngles = new Vector3(0, 0, 245);
        transform.GetChild(1).localEulerAngles = new Vector3(0, 0, 115);
    }

    public void Close()
    {
        transform.GetChild(0).localEulerAngles = new Vector3(0, 0, 0);
        transform.GetChild(1).localEulerAngles = new Vector3(0, 0, 0);
    }
}
