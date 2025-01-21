using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSecond : MonoBehaviour
{
    private Transform cameraTransform;
    // Start is called before the first frame update
    private void Awake()
    {
        cameraTransform = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, cameraTransform.position.y + 5f);
    }
}
