using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMNG : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Mode1_Start()
    {
        SceneManager.LoadScene("md1_1");
    }

    public void Mode2_Start()
    {
        SceneManager.LoadScene("md2_1");
    }

    public void Mode3_Start()
    {
        SceneManager.LoadScene("md3_1");
    }
}
