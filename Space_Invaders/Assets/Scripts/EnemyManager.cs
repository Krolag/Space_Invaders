using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class EnemyManager : MonoBehaviour
{
    [Header("Statistics file")]
    [SerializeField] private TextAsset stats;

    [Header("Grid variables")]
    [SerializeField] private Vector2 initPosition;
    [SerializeField] private Vector2 offset;
    [SerializeField] private Vector2 boundries;
    [SerializeField] private float speed;
    private int direction;

    /* How often EnemyManager should pick few enemies to send their projectile */
    [Header("Enemies pewpew variables")]
    [SerializeField] private float timeBetweenEnemiesPick;
    [SerializeField] private float enemiesToPick;
    private float cTimeBetweenEnemiesPick;

    [Header("Enemy types")]
    [SerializeField] private GameObject basicEnemy;
    [SerializeField] private Color[] colors;
    [SerializeField] private List<GameObject> enemies;

    private void Start()
    {
        direction = 1;

        /* Open XML statistic document */
        XmlDocument document = new XmlDocument();
        document.LoadXml(stats.text);
        /* Add pattern for enemies stats */
        string XmlPathPattern = "//statistics/enemies/enemy";
        XmlNodeList nodeList = document.SelectNodes(XmlPathPattern);

        foreach (XmlNode node in nodeList)
        {
            /* Get values from document */
            XmlNode id = node.FirstChild;
            XmlNode attackDamage = id.NextSibling;
            XmlNode health = attackDamage.NextSibling;
            XmlNode number = health.NextSibling;
            XmlNode projectileSpeed = number.NextSibling;
            XmlNode timeBetweenProjectiles = projectileSpeed.NextSibling;

            /* Instantiate enemies of given type */
            for (int i = 0; i < int.Parse(number.InnerXml); i++)
            {
                /* Create new enemy */
                var enemyTMP = Instantiate(basicEnemy, Vector3.zero, Quaternion.identity);
                /* Set its initial variables */
                enemyTMP.GetComponent<Enemy>().SetInitVariables(
                    colors[int.Parse(id.InnerXml)],
                    float.Parse(attackDamage.InnerXml),
                    float.Parse(health.InnerXml),
                    float.Parse(projectileSpeed.InnerXml),
                    float.Parse(timeBetweenProjectiles.InnerXml)
                );
                enemyTMP.transform.parent = transform;
                enemyTMP.SetActive(false);
                enemies.Add(enemyTMP);
            }
        }

        /* Shuffle list */
        Shuffle();

        /* Set enemies on grid */
        CreateAndSetOnGrid();
    }

    private void Update()
    {
        if (cTimeBetweenEnemiesPick >= timeBetweenEnemiesPick)
        {
            PickRandomEnemies();
            cTimeBetweenEnemiesPick = 0;
        }

        CheckHowManyEnemiesLeft();

        Move();

        cTimeBetweenEnemiesPick += Time.deltaTime;
    }

    private void Move()
    {
        /* Create two enemy tmp game objects */
        GameObject enemyLeft = enemies[0];
        GameObject enemyRight = enemies[0];
        GameObject enemyBottom = enemies[0];

        /* At first - find the far left & far right & far bottom enemies */
        foreach (GameObject go in enemies)
        {
            /* Far left */
            if (go.transform.position.x < enemyLeft.transform.position.x)
                enemyLeft = go;

            /* Far right */
            if (go.transform.position.x > enemyRight.transform.position.x)
                enemyRight = go;

            /* Far bottom */
            if (go.transform.position.y < enemyBottom.transform.position.y)
                enemyBottom = go;
        }

        if (enemyLeft.transform.position.x <= boundries.x)
        {
            direction = 1;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f, transform.position.z);
        }    
        if (enemyRight.transform.position.x >= boundries.y)
        {
            direction = -1;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f, transform.position.z);
        }
        transform.position += new Vector3(direction * speed, 0, 0) * Time.deltaTime;

        if (enemyBottom.transform.position.y <= -1.7f)
            GameEvents.Instance.ChangeUI(false);
    }

    private void CheckHowManyEnemiesLeft()
    {
        List<GameObject> listTMP = new List<GameObject>();

        /* Check if any enemy is deactivated */
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].activeSelf)
                listTMP.Add(enemies[i]);
        }

        /* Assign new list */
        enemies = listTMP;

        if (enemies.Count <= 0)
        { 
            GameEvents.Instance.ChangeUI(true);
        }
    }

    private void PickRandomEnemies()
    {
        List<int> randomEnemyIndex = new List<int>();
        int size = enemies.Count;

        if (enemiesToPick >= size)
            enemiesToPick = size;

        /* Pick random enemies to send their projectiles */
        for (int i = 0; i < enemiesToPick; i++)
            randomEnemyIndex.Add(Random.Range(i, size));

        /* For each index in randomEnemyIndex list, pick enemy and let it shoot */
        foreach (int i in randomEnemyIndex)
        {
            var enemyTMP = enemies[i].GetComponent<Enemy>();

            if (!enemyTMP.CanShoot)
                enemyTMP.CanShoot = true;
        }
    }

    private void Shuffle()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject tmp = enemies[i];
            int randomIndex = Random.Range(i, enemies.Count);
            enemies[i] = enemies[randomIndex];
            enemies[randomIndex] = tmp;
        }
    }

    private void CreateAndSetOnGrid()
    {
        /* Create grid */
        List<Vector2> grid = new List<Vector2>();
        
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                grid.Add(new Vector2(initPosition.x + offset.x * j, initPosition.y - offset.y * i));
            }
        }

        /* Set enemies on grid */
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.position = new Vector3(grid[i].x, grid[i].y, 0);
            enemies[i].SetActive(true);
        }
    }
}