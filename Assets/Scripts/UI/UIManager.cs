using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Initializable
{

    [SerializeField] private GameObject part_menu;
    [SerializeField] private GameObject part_game;
    [SerializeField] private GameObject part_end;

    [SerializeField] private Canvas canvas;
    [SerializeField] private ScrollSnap scrollSnap;

    [SerializeField] private Text part_menu_text_coins;
    [SerializeField] private Text part_menu_text_best_score;

    [SerializeField] private Text part_game_text_ammo;
    [SerializeField] private Text part_game_text_score;
    [SerializeField] private Text part_game_text_coins;

    [SerializeField] private Text part_end_text_best_score;
    [SerializeField] private Text part_end_text_coins;
    [SerializeField] private Text part_end_text_score;

    [SerializeField] private bool menuEnabled = false;
    [SerializeField] private bool gameEnabled = false;
    [SerializeField] private bool endEnabled = false;

    public override void OnInit()
    {
        canvas.worldCamera = Bootstrap.Camera;
        canvas.planeDistance = 6;

        HideAll();
      
        part_menu.SetActive(true);

        menuEnabled = true;

        part_menu_text_coins.text = "Coins: " + Bootstrap.Coins;
        part_menu_text_best_score.text = "BEST SCORE: " + Bootstrap.BestScore;
    }

    private void HideAll()
    {
        part_menu.SetActive(false);
        part_game.SetActive(false);
        part_end.SetActive(false);
        menuEnabled = false;
        gameEnabled = false;
        endEnabled = false;
    }

    private void Update()
    {
        if(gameEnabled)
        {
            part_game_text_ammo.text = Bootstrap.Player.GetInfoAboutAmmo();
            part_game_text_coins.text = "Coins: " + Bootstrap.Coins.ToString();
            part_game_text_score.text = "Score: " + Bootstrap.Score.ToString();
        }
    }

    public void Shoot()
    {
        Bootstrap.Player.Shoot();
    }

    public void ToMenu()
    {
        HideAll();
        part_menu.SetActive(true);
    }

    public void ShowEnd()
    {
        HideAll();
        part_end.SetActive(true);

        part_end_text_best_score.text = "Best score: " + Bootstrap.BestScore;
        part_end_text_score.text = "Score: " + Bootstrap.Score;
        part_end_text_coins.text = "Coins: " + Bootstrap.Coins;
    }

    public void Play()
    {
        HideAll();
        part_game.SetActive(true);
       
        switch (scrollSnap.closestElementIndex)
        {
            case 0:
                {
                    Bootstrap.SpawnPlayer(Resources.Load<GameObject>("Prefabs/Guns/PM"));
                }break;
            case 1:
                {
                    Bootstrap.SpawnPlayer(Resources.Load<GameObject>("Prefabs/Guns/AK"));
                }
                break;
            case 2:
                {
                    Bootstrap.SpawnPlayer(Resources.Load<GameObject>("Prefabs/Guns/SVD"));
                }
                break;
        }

        gameEnabled = true;
    }
}
