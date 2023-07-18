using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public GameObject[] FishList;

    public GameObject FishPrefebs;

    public int FishCount;

    public Vector3 range = new Vector3(5, 5, 5);

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, range * 2);

        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, 0.2f);
    }
    // Start is called before the first frame update
    void Start()
    {
        FishList = new GameObject[FishCount];
        for (int i = 0; i < FishCount; i++)
        {
            Vector3 FishPos = new Vector3(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y), 0);

            FishList[i] = Instantiate(FishPrefebs, this.transform.position + FishPos, Quaternion.identity) as GameObject;
            FishList[i].GetComponent<FishMove>().FishManager = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
