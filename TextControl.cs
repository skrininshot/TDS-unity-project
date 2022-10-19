using UnityEngine;
using UnityEngine.UI;

public class TextControl : MonoBehaviour
{
    public string Text
    {
        get
        {
            return text;
        }
        set
        {
            transform.GetChild(0).GetComponent<Text>().text = value;
            transform.GetChild(1).GetComponent<Text>().text = value;
        }
    }
    private string text;

    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<Text>().text;
    }

    public void Clear()
    {
        text = "";
        transform.GetChild(0).GetComponent<Text>().text = "";
        transform.GetChild(1).GetComponent<Text>().text = "";
    }
}