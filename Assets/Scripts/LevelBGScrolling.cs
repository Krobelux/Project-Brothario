using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBGScrolling : MonoBehaviour
{

    public float scrollSpeed = 0.01f;
    public float vertScrollSpeed = 0.01f;

    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //rend.material.mainTextureOffset = new Vector2(Time.time * scrollSpeed, 0);
        rend.material.mainTextureOffset = new Vector2(Time.time * scrollSpeed, Time.time * scrollSpeed);
    }
}
