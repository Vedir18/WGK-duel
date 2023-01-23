using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneLife : MonoBehaviour
{
    [SerializeField] private float lifespan;
    [SerializeField] private float minSize;
    [SerializeField] private float age = 0;
    [SerializeField] private GameObject self;
    private Transform tr;
    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;
        float ratio = age / lifespan;
        float size = (1-ratio) * 1 + ratio * minSize;
        tr.localScale = new Vector3(size, size, size);

        if (age >= lifespan) Destroy(self);
    }
}
