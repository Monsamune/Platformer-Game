using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour{
    public GameObject platformPrefab;

    void Start(){
        for (int i = 0; i < 100; i++){
            float xPosition = Random.Range(-7f, 7f);
            float yPosition = i * -20f;
            Vector3 position = new Vector3(xPosition, yPosition, 0);
            Instantiate(platformPrefab, position, Quaternion.identity);
        }

    }
}
