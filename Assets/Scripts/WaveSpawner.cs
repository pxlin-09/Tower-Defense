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
    // Start is called before the first frame update

    public TextMeshProUGUI txt;
    void Start()
    {
        UpdateWavesText();
    }

    // Update is called once per frame
    void Update()
    {
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
        for (int i = 0; i < waveNum * 10; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.2f);
        }
        

    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, SpawnPoint.position, SpawnPoint.rotation);
    }

    void UpdateWavesText()
    {
        txt.text = string.Format("Waves Remaining: {0}", waves - waveNum);
    }
}
