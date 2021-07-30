using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterfliesController : MonoBehaviour
{
    [SerializeField] private Transform _character;
    private Vector3 _distance;
    public List<GameObject> butterflyList = new List<GameObject>();

    void Awake()
    {
        _distance = transform.position - _character.position;
        foreach (Transform child in transform.GetChild(0))
            butterflyList.Add(child.gameObject);

    }

    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, _distance.y + _character.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, newPosition, 0.6f);
    }

    public void SpawnButterfly()
    {
        for (int i = 0; i < 5; i++)
        {
            int rnd = Random.Range(0, 5);
            butterflyList.Add(Instantiate(butterflyList[rnd], transform.GetChild(0)));
        }      
    }

    public void DestroyButterfly()
    {
        if(butterflyList.Count >= 5)
            for (int i = 0; i < 5; i++)
                Destroy(butterflyList[i]);
        butterflyList.RemoveRange(0, 5);
        print(butterflyList.Count);

    }
}
