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
    // Start is called before the first frame update
    void Start()
    {
        if (Time.time >= carregarFlechaInicial + carrregarFlechaMax)
        {
            SpawnarFlecha(Random.Range(4,2));
            
        }
        if (Time.time >= carregarPedraInicial + carrregarPedraaMax)
        {
            SpawnarPedra(Random.Range(5, 3));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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


}
