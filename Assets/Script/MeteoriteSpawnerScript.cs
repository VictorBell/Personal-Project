using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MeteoriteSpawnerScript : MonoBehaviour
{
    [SerializeField] private float spawnSpeed = 1.0f;
    [SerializeField] private float xSpawnRangeFrom;
    [SerializeField] private float xSpawnRangeTo;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] GameObject gameOverHood;
    private int score;
    private int bestScore = 0;
    AudioSource audioSource;
    [SerializeField] AudioClip kudah;
    [SerializeField] GameObject startScreen;
    public static MeteoriteSpawnerScript SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("bestScore");
        if (bestScore == 0)
        {
            PlayerPrefs.SetInt(("bestScore"), bestScore);
        }
        else
        {
            bestScoreText.text = ("Best Score : " + bestScore);
        }
        Time.timeScale = 0;
        // Loop through list of pooled objects,deactivating them and adding them to the list 
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform); // set as children of Spawn Manager
        }
    }
    public void StartGame()
    {
        startScreen.SetActive(false);
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
        score = 0;
        ShowScore();
        Invoke("SpawnMeteorites", spawnSpeed);
    }
    private void SpawnMeteorites()
    {
        GameObject pooledProjectile = GetPooledObject();
        if (pooledProjectile != null)
        {
            pooledProjectile.SetActive(true); // activate it
            pooledProjectile.transform.position = SetPos(14); // position it
        }
        spawnSpeed *= 0.98f;
        Invoke("SpawnMeteorites", spawnSpeed);
        score++;
        ShowScore();

    }

     // Set random position
     public Vector3 SetPos(int yPos)
    {
        return new Vector3(Random.Range(xSpawnRangeFrom, xSpawnRangeTo), yPos, -1);
  
    }
    public void GameOver()
    {
        audioSource.PlayOneShot(kudah);
        Time.timeScale = 0;
        if (score > bestScore)
        {
            bestScore = score;
            bestScoreText.text = ("Best Score : " + bestScore);
            PlayerPrefs.SetInt(("bestScore"), bestScore);
        }
        gameOverHood.SetActive(true);
    }
    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    void ShowScore()
    {
        scoreText.text = ("Score : " + score);
    }
    public GameObject GetPooledObject()
    {
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // otherwise, return null   
        return null;
    }
}

