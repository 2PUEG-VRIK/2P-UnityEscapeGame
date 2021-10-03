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
    public float sum;

    bool isFinish;
    bool IsPause;
    Text pause;

    private int myrank = 123;

    // 시간
    private float timeCount = 0.0f;
    Text state;
    Text mode;

    // 결과창 시간
    Text time;
    private readonly string DBurl = "https://escape-game-3382c-default-rtdb.firebaseio.com/";
    private string now_scene = "Start Scene";
    private string activeScene;


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
                pause = GameObject.Find("Pause").GetComponentInChildren<Text>();

                Timer();
            }
        }
    }

    public void GetRanking()
    {
        //Debug.Log("Now is Finish Scene");
        Debug.Log("GetRanking");

        // Ranking의 모든 값 가져와서 ArrayList에 저장.
        var gameResult = new Dictionary<string, float>();

        IDictionary temp;

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Ranking").Child("mode3");
        reference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("IsCompleted");
                DataSnapshot snapshot = task.Result;

                foreach (DataSnapshot data in snapshot.Children)
                {
                    temp = (IDictionary)data.Value;
                    gameResult.Add(temp["id"].ToString(), float.Parse(temp["timestamp"].ToString()));
                    //Debug.Log("data : " + temp["id"] + " time : " + temp["timestamp"]);
                }

                // Order by values.
                var items = from pair in gameResult
                            orderby pair.Value ascending
                            select pair;

                //Ranking>mode3을 아예 초기화 하고 정렬값으로 바뀌버림.
                reference.SetValueAsync("");
                int i = 1;
                myrank = 333;

                foreach (KeyValuePair<string, float> pair in items)
                {
                    Debug.Log("-------------------------------");
                    Debug.Log("pair.Value : " + pair.Value + " sum : " + sum);

                    // 만약 이번에 저장하려는게 
                    if (pair.Value == sum)
                    {
                        myrank = i;
                        Debug.Log(myrank + " 등입니다.");
                    }

                    reference.Child(i.ToString()).Child("id").SetValueAsync(pair.Key);
                    reference.Child(i.ToString()).Child("timestamp").SetValueAsync(pair.Value);
                    i++;
                    Debug.Log("data : " + pair.Key + " time : " + pair.Value);
                    Debug.Log("-------------------------------");
                }
            }
        });
    }


    public void save_time(bool isFinish)
    {
        Debug.Log("Game Clear ! " + playername);
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
                sum = (float)Math.Truncate((pre_cnt + timeCount) * 10000) / 10000;
                reference.Child("sum").SetValueAsync(sum);

                if (isFinish)
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference("Ranking").Child("mode3");

                    // 마지막 씬 오면 내 값을 Ranking DB 0번째에 저장함.
                    reference.Child("0").Child("id").SetValueAsync(playername);
                    reference.Child("0").Child("timestamp").SetValueAsync(sum);

                    GetRanking();
                }
            }
        });

    }

    public void Game_Clear()
    {
        time = GameObject.Find("Time").GetComponent<Text>();
        time.text = string.Format("{0:0.00}", timeCount);
    }

    public string getSum()
    {
        return string.Format("{0:0.00}", sum);
    }
    public string getRank()
    {
        return myrank.ToString();
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

    public void Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
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
        /*일시정지 활성화*/
        if (!IsPause)
        {
            Time.timeScale = 0;
            IsPause = true;
            pause.text = "Resume";
        }
        else
        {
            Time.timeScale = 1;
            IsPause = false;
            pause.text = "Pause";
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
 