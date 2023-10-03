using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private static Bootstrap bootstrap;
    private SaveManager saveManager;

    [SerializeField] private bool deleteData;

    [SerializeField] private GameObject _camera_object;
    [SerializeField] private GameObject _ui_object;
    [SerializeField] private GameObject _item_generator_object;

    [SerializeField] private new CameraFollow camera;
    [SerializeField] private UIManager ui;
    [SerializeField] private GunController player;
    [SerializeField] private ItemGenerator itemGenerator;

    [SerializeField] private int coins;
    [SerializeField] private int score;
    [SerializeField] private int bestscore;

    private void Awake()
    {
        bootstrap = this;
        saveManager = new SaveManager();

        if (deleteData) saveManager.DeleteAll();

        coins = saveManager.GetDataInt("Coins");
        bestscore = saveManager.GetDataInt("Best score");
        score = 0;

        camera = Instantiate(_camera_object).GetComponent<CameraFollow>();
        ui = Instantiate(_ui_object).GetComponent<UIManager>();

        ui.OnInit();
    }

    public static void SpawnPlayer(GameObject gameObject)
    {
        bootstrap.player = Instantiate(gameObject).GetComponent<GunController>();
        bootstrap.player.OnInit();

        bootstrap.itemGenerator = Instantiate(bootstrap._item_generator_object).GetComponent<ItemGenerator>();

        CameraFollow follow = Camera.GetComponent<CameraFollow>();
        follow.follow = true;
        follow.target = Player.transform;

        bootstrap.itemGenerator.OnInit();

        bootstrap.score = 0;
    }

    public static void Save()
    {
        SaveManager.SaveDataInt("Coins", Coins);
        SaveManager.SaveDataInt("Best score", BestScore);
    }

    public static void EndGame()
    {
        bootstrap.camera.follow = false;

        Destroy(bootstrap.itemGenerator.gameObject);
        Destroy(bootstrap.player.gameObject);

        bootstrap.bestscore = bootstrap.bestscore > bootstrap.score ? bootstrap.bestscore : bootstrap.score;

        bootstrap.ui.ShowEnd();
        Save();
    }

    public static void EarnCoin()
    {
        bootstrap.coins++;
    }

    public static Camera Camera { get { return bootstrap.camera.GetComponent<Camera>(); } }

    public static GunController Player {  get { return bootstrap.player; } }

    public static SaveManager SaveManager { get { return bootstrap.saveManager; } }

    public static int Coins { get { return bootstrap.coins; } }
    public static int Score { get { return bootstrap.score; } set { bootstrap.score = value; } }
    public static int BestScore { get { return bootstrap.bestscore; } }
}