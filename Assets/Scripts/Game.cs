using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class Game : MonoBehaviour
{

    private int carregarFlechaInicial;

    public float waveDelay = 3f;

    [SerializeField]
    private int carrregarFlechaMax;

    private int carregarPedraInicial;

    [SerializeField]
    private int carrregarPedraaMax;

    List<GameObject> FlechasList = new List<GameObject>();
    List<GameObject> PedraList = new List<GameObject>();

    [SerializeField]
    Player player_ref;

    public GameObject municaoFlechasPrefab;
    public GameObject municaoPedraPrefab;

    [SerializeField]
    public Queue<Inimigo> inimigos = new Queue<Inimigo>();

    [SerializeField]
    Transform[] spawns = new Transform[3];

    public GameObject inimigo;

    [SerializeField]
    public int maxQueue;

    int maxSpawnPoints = 3;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] spawnsGO = GameObject.FindGameObjectsWithTag("spawnPoint");

        for (int i = 0; i < spawnsGO.Length; i++)
        {
            if (i >= spawns.Length)
            {
                break;
            }

            spawns[i] = spawnsGO[i].transform;

            Vector3 pos = spawns[i].position;

            spawns[i].position = pos;
        }

        if (Time.time >= carregarFlechaInicial + carrregarFlechaMax)
        {
            SpawnarFlecha(Random.Range(4,2));
            
        }
        if (Time.time >= carregarPedraInicial + carrregarPedraaMax)
        {
            SpawnarPedra(Random.Range(5, 3));
        }

        StartCoroutine("SpawnWaves");
    }    
    
    IEnumerator SpawnWaves()
    {
        while(player_ref != null)//Enquanto o player está vivo
        {
            spawnInimigos();

            yield return new WaitForSeconds(waveDelay);
        }
    }

    public void SpawnarFlecha(int disFlePlay)
    {
            Vector3 position = player_ref.transform.position;
            position.x += disFlePlay;
            position.y += disFlePlay;
            position.z = -1;

        if (FlechasList.Any())
        {
            int index = FlechasList.FindLastIndex(x=>x.GetType()==typeof(Flecha));
            Flecha F = (Flecha)FlechasList[index];
            FlechasList.RemoveAt(index);
            F.transform.position = position;
            F.gameObject.SetActive(true);
        }
        else
        {
            GameObject ru = Instantiate(municaoFlechasPrefab, position, Quaternion.identity);

            FlechasList.Add(ru);
        }
    }

    public void SpawnarPedra(int disPedPlay)
    {
        Vector3 position = player_ref.transform.position;
        position.x += disPedPlay;
        position.y += disPedPlay;
        position.z = -1;

        if (PedraList.Any())
        {
            int index = PedraList.FindLastIndex(x => x.GetType() == typeof(Pedra));
            Pedra P = (Pedra)PedraList[index];
            PedraList.RemoveAt(index);
            P.transform.position = position;
            P.gameObject.SetActive(true);
        }
        else
        {
            GameObject fe = Instantiate(municaoPedraPrefab, position, Quaternion.identity);

            FlechasList.Add(fe);
        }
    }

    public void spawnInimigos()
    {
        if (inimigos.Count > 0)
        {
            respawn();
        }
        else
        {
            int spawnLocation = Random.Range(0, maxSpawnPoints);

            if (!spawns[spawnLocation])
            {
                return;
            }

            Vector3 position = spawns[spawnLocation].position;

            Inimigo i = Instantiate(inimigo, position, Quaternion.identity).GetComponent<Inimigo>();

            i.spawnPoint = spawnLocation;
        }
    }

    public void respawn()
    {
        int spawnLocation = Random.Range(0, maxSpawnPoints);

        Inimigo i = inimigos.Dequeue();

        while (spawnLocation == i.spawnPoint)
        {
            spawnLocation = Random.Range(0, maxSpawnPoints);
        }

        i.spawnPoint = spawnLocation;

        i.transform.position = spawns[i.spawnPoint].position;

        i.gameObject.SetActive(true);
    }

    public void addToPool(Inimigo i)
    {
        if (inimigos.Count < maxQueue)
        {
            i.gameObject.SetActive(false);

            inimigos.Enqueue(i);
        }
        else
        {
            Destroy(i.gameObject);
        }
    }
}
