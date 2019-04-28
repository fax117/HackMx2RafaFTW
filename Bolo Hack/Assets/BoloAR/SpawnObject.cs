using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    // Start is called before the first frame update
    private float boloX;
    private float boloY;
    private float boloZ;
    private Vector3 boloPosition;
    public float boloXmin;
    public float boloXmax;
    public float boloYmin;
    public float boloYmax;
    public float boloZmin;
    public float boloZmax;
    public float boloNumber;

    public GameObject bolos;
    void Start() {
        for (int i = 0; i < boloNumber; i++) {
            Invoke("SpawnBolo", 2);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBolo() {
        GameObject boloClone;
        
        boloX = Random.Range(boloXmin, boloXmax);
        boloY = Random.Range(boloYmin, boloYmax);
        boloZ = Random.Range(boloZmin, boloZmax);

        boloClone = Instantiate(bolos, new Vector3(boloX, boloY, boloZ), transform.rotation * Quaternion.Euler (-90f, 0f, 0f));
    }

    
}
