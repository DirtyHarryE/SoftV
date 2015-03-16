using UnityEngine;
using System.Collections;

public class CoinSpawnerB2_Final : MonoBehaviour {

    public GameObject coin;
    public float speed = 5.0f;
    public Transform spawnPoint;

    private float maxWidth;
    private int coinsEarned;
    private int totalNumberSpawned = 0;

    private bool dirRight = true;
    private bool gameOver = false;

    private float fireRate = 0.15f;
    private float nextFire;

    void Start()
    {
        //Time.timeScale = 0;

        //nice little piece of code that finds the maximum width of the screen
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0);
        Vector3 targetWidth = Camera.main.ScreenToWorldPoint(upperCorner);
        //which we use to set out max spawn range (so objects don't spawn off screen)
        float coinWidth = coin.renderer.bounds.size.x;
        maxWidth = targetWidth.x - coinWidth;


        Debug.Log("COINS : [" + StateChallenge.Instance.GetCoinsEarned() + "];");
        //get the coin score from the previous screen
        coinsEarned = StateChallenge.Instance.GetCoinsEarned();
        if (coinsEarned == 0) coinsEarned = 14; // default debug value used when testing scene directly, should be removed from final builds


    }

    void MoveDropper()
    {
        //move the dropper back and forth
        if (dirRight) transform.Translate(Vector2.right * speed * Time.deltaTime);
        else transform.Translate(-Vector2.right * speed * Time.deltaTime);
        if (transform.position.x >= maxWidth) dirRight = false;
        if (transform.position.x <= -maxWidth) dirRight = true;
        MoveSpawnPoint(0.18f);
    }

    void MoveSpawnPoint(float amount)
    {
        //hacky code to have the spawnpoint behind the dropper so visually it looks better
        if (dirRight) spawnPoint.localPosition = new Vector3(amount, spawnPoint.localPosition.y ,spawnPoint.localPosition.z);
        else spawnPoint.localPosition = new Vector3(-amount, spawnPoint.localPosition.y, spawnPoint.localPosition.z);
    }

    void Update()
    {
        if (!gameOver)
        {
            CoinFiring();
            GameOverTrigger();
        }
        else
        {
            //Application.LoadLevel("B3");
            Debug.Log("rhg");
            GameController.Instance.ChangeState(GameController.States.ChapterSelect);
        }
    }

    void CoinFiring()
    {
        MoveDropper();

        //when the mouse button is pressed fire a coin, you can also hold to fire
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            SpawnCoin();
        }

    }

    void GameOverTrigger()
    {
        //if all coins have finished falling end the game and show the score
        if (GameObject.FindWithTag("Coin") == null && ReturnCoinsLeft() <= 0)
        {
            gameOver = true;
        }
    }

    void SpawnCoin()
    {
        if (ReturnCoinsLeft() > 0)
        {
            //spawn a coin and keep track of it in our counter
            GameObject coinInstance = Instantiate(coin, spawnPoint.transform.position, Quaternion.identity) as GameObject;
            coinInstance.rigidbody2D.AddForce(-Vector2.up * 250);
            totalNumberSpawned++;
        }
    }

    //Functions to be referenced by the UI

    public int ReturnCoinsLeft()
    {
        return coinsEarned - totalNumberSpawned;
    }

}
