using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamielControllerScript : MonoBehaviour
{
    const int SPEED = 10;
    [SerializeField]
    private GameObject InaPrefab;

    [SerializeField]
    private GameObject BackgroundMusic;

    private Rigidbody2D rb2D;

    private BackgroundMusicController bgMusicController;

    float timer = 0;

    float duration = 8;

    int totalIna = 3;

    bool isBgMusicChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        bgMusicController = BackgroundMusic.GetComponent<BackgroundMusicController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (totalIna > 0)
        {
            timer += Time.deltaTime;
            if (timer > duration)
            {
                timer = 0;

                Vector3 vec3 = transform.position;

                Instantiate(InaPrefab, new Vector3(vec3.x, vec3.y * 0.8f, 0), Quaternion.identity);
                totalIna--;
            }
        } else
        {
            if (!isBgMusicChanged && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                bgMusicController.ChangeToBossTheme(true);
                isBgMusicChanged = true;
            }
        }


        Moving();
    }

    private void FixedUpdate()
    {
        
    }

    private void Moving()
    {
        var v3 = transform.position;

        if (v3.x <-8)
        {
            // print("Move RIGHT");
            rb2D.velocity = new Vector2(0, 0);
            Vector2 vec2r = new Vector2(SPEED, 0);
            rb2D.AddForce(vec2r, ForceMode2D.Impulse);
            return;
        }

        if (v3.x > 8)
        {
            // print("Move LEFT");
            rb2D.velocity = new Vector2(0, 0);
            Vector2 vec2r = new Vector2(-SPEED, 0);
            rb2D.AddForce(vec2r, ForceMode2D.Impulse);
            return;
        }

    }
}
