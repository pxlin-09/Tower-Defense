using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public float timeBetween = 1f;
    public int waves;
    private float countdown = 2f;
    private int waveNum = 0;
    public Transform SpawnPoint;
    public int money;
    // Start is called before the first frame update
    System.Random random = new System.Random(); 
    public TextMeshProUGUI txt;
    void Start()
    {
        UpdateWavesText();
    }

    // Update is called once per frame
    void Update()
    {
        if (User.instance.GameOver())
        {
            return;
        }
        
        if (countdown <= 0f)
        {
            if (waveNum < waves) StartCoroutine(SpawnWave());
            countdown = timeBetween;
        }
        countdown -= Time.deltaTime; 
    }

    IEnumerator SpawnWave ()
    {
        waveNum++;
        UpdateWavesText();
        for (int i = 0; i < waveNum * 5; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.2f);
        }
        

    }

    void SpawnEnemy()
    {
        Transform ene = (Transform) Instantiate(enemyPrefab, SpawnPoint.position, SpawnPoint.rotation);
        Enemy e = ene.GetComponent<Enemy>();    
        int level = random.Next(1,3);
        e.SetLevel(level);
    }

    void UpdateWavesText()
    {
        txt.text = string.Format("Waves Remaining: {0}", waves - waveNum);
    }
}
