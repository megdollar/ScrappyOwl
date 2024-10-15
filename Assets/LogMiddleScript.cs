using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMiddleScript : MonoBehaviour
{
    public ScrappyOwlController logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<ScrappyOwlController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            logic.IncreaseScore();
        }
        
    }
}
