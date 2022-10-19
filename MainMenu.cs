using UnityEngine;

public class MainMenu : Menu
{
    [SerializeField] private TextControl highscore;

    private void Start()
    {
        highscore.Text = "HIGHSCORE: " + PlayerPrefs.GetInt("Highscore", 0).ToString();
    }
}
