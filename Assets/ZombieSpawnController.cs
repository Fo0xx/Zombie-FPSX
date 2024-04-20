using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZombieSpawnController : MonoBehaviour
{
    public int initialZombiePerWave = 5;
    public int currentZombiePerWave;

    public float spawnDelay = 0.5f;

    public int currentWave = 0;
    public float waveCooldown = 10;

    public bool inCooldown = false;
    public float cooldownCounter = 0;

    public List<Enemy> currentZombiesAlive;

    public GameObject zombiePrefab;

    public TextMeshProUGUI titleWaveOver;
    public TextMeshProUGUI cooldownCounterUI;
    public TextMeshProUGUI currentWaveUI;

    private void Start()
    {
        currentZombiePerWave = initialZombiePerWave;
        GlobalReferences.Instance.waveNumber = 0;
        StartNextWave();
    }

    private void Update()
    {
        List<Enemy> deadZombies = new List<Enemy>();
        foreach (Enemy zombie in currentZombiesAlive)
        {
            if (zombie.isDead)
            {
                deadZombies.Add(zombie);
            }
        }

        foreach (Enemy zombie in deadZombies)
        {
            currentZombiesAlive.Remove(zombie);
        }

        deadZombies.Clear();

        if (currentZombiesAlive.Count == 0 && !inCooldown)
        {
            StartCoroutine(WaveCooldown());
        }

        if (inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
        } else
        {
            // Reset the cooldown counter
            cooldownCounter = waveCooldown;
        }

        cooldownCounterUI.text = cooldownCounter.ToString("F0");
    }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;
        titleWaveOver.gameObject.SetActive(true);

        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;
        titleWaveOver.gameObject.SetActive(false);

        currentZombiePerWave += 3;

        StartNextWave();
    }

    private void StartNextWave()
    {
        currentZombiesAlive.Clear();

        currentWave++;
        GlobalReferences.Instance.waveNumber = currentWave;

        currentWaveUI.text = "Wave " + currentWave.ToString();

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentZombiePerWave; i++)
        {
            Vector3 spawnOffset = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            Vector3 spawnPosition = transform.position + spawnOffset;

            // Spawn zombie
            var zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);

            // Get the enemy script
            Enemy enemyScript = zombie.GetComponent<Enemy>();

            // Track the zombie
            currentZombiesAlive.Add(enemyScript);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
