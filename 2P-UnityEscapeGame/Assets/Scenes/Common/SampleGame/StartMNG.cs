using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMNG : MonoBehaviour
{
    public InputField playerNM;
    public GameObject notDestroy;
 
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
        notDestroy.GetComponent<SingleGameMNG>().playername = playerNM.text;
        createFolder(playerNM.text);

        //SceneManager.LoadScene("md3_1");
        SceneManager.LoadScene("sample1");
    }


    private void createFolder(string playername)
    {
        // 사용자 이름으로 폴더 생성하고 sum 0.0으로 초기화
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference.Child("Times").Child(playername).Child("sum").SetValueAsync(0);
    }
}
