using UnityEngine;

public class FinalScreen : Menu
{
   public TextControl Highscore;
    public TextControl Score;
    private void Start()
    {
        Highscore.Text = "HIGHSCORE: " + PlayerPrefs.GetInt("Highscore", 0).ToString();
    }
}