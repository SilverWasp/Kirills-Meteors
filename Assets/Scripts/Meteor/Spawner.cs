using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;
    [SerializeField] private float spawnDistance;
    [SerializeField] private GameObject meteor;
    [SerializeField] private List<Sprite> spriteList;

    public GameObject Meteor { get { return meteor; } set { meteor = value; } }
    private void Spawn()
    {
        Vector2 spawnPosition = Random.insideUnitCircle.normalized * spawnDistance;
        Quaternion spawnRotation = Quaternion.AngleAxis(Random.Range(-30, 30), new Vector3(0, 0, 1));
        GameObject newMeteor = Instantiate(meteor, spawnPosition,spawnRotation);
        newMeteor.GetComponent<Meteor>().Kick(spawnRotation * -spawnPosition);
        SpriteRenderer sr = newMeteor.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            sr = newMeteor.AddComponent<SpriteRenderer>();
        }
        int index = Random.Range(0, spriteList.Count);
        sr.sprite = spriteList[index];

    }

    private void Start()
    {
        InvokeRepeating("Spawn", 0, spawnRate);
    }
}
