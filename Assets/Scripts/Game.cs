using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class Game : MonoBehaviour
{
    List<GameObject> FlechasList = new List<GameObject>();

    [SerializeField]
    Player player_ref;

    public GameObject municaoFlechasPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
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


}
