using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Parallax : MonoBehaviour
{
    private float length, startPos;
    [SerializeField] private GameObject mCamera;
    [SerializeField] private float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        BackGroundEffect();
    }

    private void BackGroundEffect()
    {
        //Parallax effect
        float distance = (mCamera.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        //Repeats background
        float temp = (mCamera.transform.position.x * (1 - parallaxEffect));
        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
