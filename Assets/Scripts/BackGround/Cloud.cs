using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float endPointX = -49f;
    [SerializeField] private float startPointX = -88f;

    [SerializeField] private float yFrequency = 1f;
    [SerializeField] private float yAmplitude = 1f;

    [SerializeField] private float minScale = .3f;
    [SerializeField] private float maxScale = .5f;

    private float initialY;
    private float time;

    private void Start()
    {
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
        if(transform.position.x >= endPointX)
        {
            transform.position = new Vector3(startPointX,transform.position.y, transform.position.z);
            
            float randomScale = Random.Range(minScale, maxScale);
            transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        }

        time += Time.deltaTime;
        float yOffset = Mathf.Sin(time * yFrequency) * yAmplitude;
        transform.position = new Vector3(transform.position.x, initialY + yOffset, transform.position.z);


    }
}
