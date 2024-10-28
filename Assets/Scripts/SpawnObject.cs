using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawnObject;

    void Start()
    {
        if (_spawnObject.Length > 0)
        {
            GameObject objectToSpawn = _spawnObject[Random.Range(0, _spawnObject.Length)];
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogWarning("Aucun objet à instancier dans le tableau spawnObject !");
        }
    }
}
