using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [SerializeField] private TMP_Text coinText;

    [SerializeField] private PlayerController playerController;

    private int coinCount = 0;
    private int gemCount = 0;
    public bool isGameOver = false; 
    private Vector3 playerPosition;

    //Level Complete

    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] TMP_Text leveCompletePanelTitle;
    [SerializeField] TMP_Text levelCompleteCoins;




   
    private int totalCoins = 0;
  



    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
{
    UpdateGUI();
    UIManager.instance.fadeFromBlack = true;
    
    playerController = FindObjectOfType<PlayerController>();
    if (playerController != null)
    {
        playerPosition = playerController.transform.position;
    }
    else
    {
        Debug.LogError("PlayerController not found in the scene!");
    }

    FindTotalPickups();
}

    public void IncrementCoinCount()
    {
        coinCount++;
        UpdateGUI();
    }
    public void IncrementGemCount()
    {
        gemCount++;
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        coinText.text = coinCount.ToString();
  
    }

public void Death()
{
    if (!isGameOver)
    {
        isGameOver = true; // Сразу помечаем игру как завершённую
        UIManager.instance.DisableMobileControls();
        UIManager.instance.fadeToBlack = true;
        
        if (playerController != null)
            playerController.gameObject.SetActive(false);

        StartCoroutine(DeathCoroutine());
    }
}
 
    public void FindTotalPickups()
    {

        pickup[] pickups = GameObject.FindObjectsOfType<pickup>();

        foreach (pickup pickupObject in pickups)
        {
            if (pickupObject.pt == pickup.pickupType.coin)
            {
                totalCoins += 1;
            }
           
        }


      
    }
    public void LevelComplete()
    {
       


        levelCompletePanel.SetActive(true);
        leveCompletePanelTitle.text = "LEVEL COMPLETE";



        levelCompleteCoins.text = "COINS COLLECTED: "+ coinCount.ToString() +" / " + totalCoins.ToString();
 
    }
   
public IEnumerator DeathCoroutine()
{
    yield return new WaitForSeconds(1f);
    
    // Проверяем, не уничтожен ли playerController
    if (playerController != null)
        playerController.transform.position = playerPosition;
    
    yield return new WaitForSeconds(1f);
    
    // Проверяем, не перезагружается ли уже сцена
    if (isGameOver)
    {
        StopAllCoroutines(); // Останавливаем все корутины
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}



}

