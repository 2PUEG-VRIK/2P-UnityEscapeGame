using Firebase;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class SingleGameMNG : MonoBehaviour
{
    private static SingleGameMNG _instance;
    public string playername;
    private float sum;
    
    bool isFinish;
    bool IsPause;
    Text pause;

    private int myrank = -1;

    // 시간
    private float timeCount = 0.0f;
    Text state;
    Text mode;

    // 결과창 시간
    Text time;
    private readonly string DBurl = "https://realtime-unity-default-rtdb.firebaseio.com/";
    private string now_scene = "Start Scene";
    private string activeScene;


    // 1~5등 저장.
    private Dictionary<string, float> top5;
 
    // 인스턴스에 접근하기 위한 프로퍼티
    public static SingleGameMNG Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(SingleGameMNG)) as SingleGameMNG;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);

        isFinish = false;
        IsPause = false;
 
        top5 = new Dictionary<string, float>();
    }

    private void Update()
    {
        activeScene = SceneManager.GetActiveScene().name;
        if (activeScene == "Start Scene" || now_scene == "Finish Scene")
        {
            //Debug.Log("Now is Start Scene");
        }
        else if (activeScene == now_scene)
        {
            //Debug.Log("This is now_scene");
            state = GameObject.Find("State").GetComponent<Text>();
            Timer(); // 타이머 가동은 계속 해야지
        }
        else
        {
            Debug.Log("Scene Change!!");
            now_scene = activeScene;

            if (activeScene == "Finish Scene")
            {
                Debug.Log("Now is Finish Scene");
                //GetRanking();
            }
            else
            {
                isFinish = false;
                timeCount = 0; // 시간 0으로 돌리고

                mode = GameObject.Find("Mode").GetComponent<Text>();
                mode.text = "협동모드\nStage" + activeScene.Substring(activeScene.Length - 1, 1);

                state = GameObject.Find("State").GetComponent<Text>();

                Timer();
            }
        }
    }
 

    public void save_time(bool isFinish)
    {
        Debug.Log("Stage Clear ! " + playername);
        DatabaseReference reference;

        reference = FirebaseDatabase.DefaultInstance.GetReference("Times").Child(playername);
        float pre_cnt = 0.0f;

        reference.GetValueAsync().ContinueWith(task =>
        {
            Debug.Log(" ContinueWith");

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                IDictionary UserInfos = (IDictionary)snapshot.Value;
                pre_cnt = float.Parse(UserInfos["sum"].ToString());
                Debug.Log("pre_cnt : " + pre_cnt.ToString());
                sum = (float)Math.Truncate((pre_cnt + timeCount) * 100) / 100;
                reference.Child("sum").SetValueAsync(sum);

                if (isFinish)
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference("Ranking");

                    // 마지막 씬 오면 내 값을 Ranking DB 0번째에 저장함.
                    reference.Child("0").Child("id").SetValueAsync(playername);
                    reference.Child("0").Child("timestamp").SetValueAsync(sum);

                    GetRanking();
                }
            }
        });
    }


    public void GetRanking()
    {
        Debug.Log("GetRanking");

        // Ranking의 모든 값 가져와서 ArrayList에 저장.
        var gameResult = new Dictionary<string, float>();

        IDictionary temp;

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Ranking");
        reference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                 DataSnapshot snapshot = task.Result;

                // Ranking 안에 있는 모든 값을 Dic에 저장.
                foreach (DataSnapshot data in snapshot.Children)
                {
                    temp = (IDictionary)data.Value;
                    gameResult.Add(temp["id"].ToString(), float.Parse(temp["timestamp"].ToString()));
                    Debug.Log("GetRanking--"+ temp["id"].ToString() + " : " + temp["timestamp"].ToString());
                }

                Debug.Log("---------------A----------------");

                // 가져온 값 오름 차순 정렬.
                var items = from pair in gameResult
                            orderby pair.Value ascending
                            select pair;
                Debug.Log("---------------B----------------");


                //Ranking을 아예 초기화
                //reference.SetValueAsync(null);
                int i = 1;

                // 이 로그가 아예 안나오는 경우가 생김.
                Debug.Log("---------------C----------------");

                foreach (KeyValuePair<string, float> pair in items)
                {
 
                    // 1~5등만 따로 배열에 저장 해두자.
                    if (i<6)
                    {
                        Debug.Log(i + "등 : " + "ID : " + pair.Key + " Record : " + pair.Value);
                        top5.Add(pair.Key, pair.Value);
                    }

                    // 만약 이번에 저장하려는게 현재 사용자의 sum과 같다면. 그게 바로 등수겠지.
                    if (pair.Value == sum)
                    {
                        myrank = i;
                        Debug.Log(myrank + " 등입니다.");
                    }

                    //reference.Child("hello");
                    reference.Child(i.ToString()).Child("id").SetValueAsync(pair.Key);
                    reference.Child(i.ToString()).Child("timestamp").SetValueAsync(pair.Value);
                    i++;
                }
                Debug.Log("---------------D----------------");

            }
        });
    }

    public void Game_Clear()
    {
        // 결과 창에 시간 띄우는 코드
        time = GameObject.Find("Time").GetComponent<Text>();
        //time.text = string.Format("{0:0.00}", timeCount);
        time.text = string.Format("{0:0.00}", sum);
    }

    public string getSum()
    {
        return string.Format("{0:0.00}", sum);
    }
    public int getRank()
    {
        return myrank;
    }

    public Dictionary<string,float> getTop5()
    {
        return top5;
    }
     

    public void Next()
    {
        string scene = GameObject.Find("Exit").GetComponent<Exit>().scene;
        StartCoroutine(LoadSceneCorutine(scene));
    }

    IEnumerator LoadSceneCorutine(string scene)
    {
        yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
    }

    IEnumerator LoadSceneCorutineByIndex(int index)
    {
        yield return SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
    }

    public void Retry()
    {
        StartCoroutine(LoadSceneCorutineByIndex(SceneManager.GetActiveScene().buildIndex));
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit() // 어플리케이션 종료
#endif
    }

    public void Pause()
    {
        Debug.Log("Pause");
        /*일시정지 활성화*/
        if (!IsPause)
        {
            Time.timeScale = 0;
            IsPause = true;
        } else
        {
            Time.timeScale = 1;
            IsPause = false;
        }
    }

    public void Timer()
    {
        if (isFinish)
        {
            return;
        }
        else
        {
            timeCount += Time.deltaTime;
            state.text = "Time : " + string.Format("{0:0.00}", timeCount);
        }
    }

    public void Timer_Stop()
    {
        isFinish = true;
    }
 
}
 