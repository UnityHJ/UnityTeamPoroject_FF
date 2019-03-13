using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EyeCast : MonoBehaviour
{
    [Header("item Tag Name")]
    private string chickTag = "Chicken";    //치킨태그
    private string cokeTag = "Coke";        //콜라태그

    [Header("RayCast")]
    public float rayDist = 100.0f;  //레이작동거리
    private Transform tr;   //레이쏘는 지점
    private Ray ray;        //레이
    private RaycastHit hit; //레이 힛
    private GameObject currHit;
    private GameObject prevHit = null;

    public float passTime = 2f;
    private float startTime = 0;

    private bool isGazing = false;


    [Header("Bool")]
    //public bool emptyChick = true;      //치킨 플레이어 손 안 유무
    //public bool emptyCoke = true;       //코크 플레이어 손 안 유무

    public static EyeCast Instance;

    public delegate void EyeHandler();  //이벤트 핸들할 델리
    public static event EyeHandler OnClick; //이벤트 델리 함수변수

    private GameManager _gm = GameManager.Instance;



    void Start()
    {
        if (EyeCast.Instance == null) Instance = this;
        else if (EyeCast.Instance != null) Destroy(gameObject);
        DontDestroyOnLoad(this);

        tr = GetComponent<Transform>();

        Debug.DrawRay(ray.origin, ray.direction * rayDist, Color.green);

        _gm = GameManager.Instance;
        //_gm.state = GameManager.ChickState.NORMAL;


    }

    void Update()
    {
        Debug.DrawRay(ray.origin, ray.direction * rayDist, Color.green);
        ray = new Ray(tr.position, tr.forward * rayDist);



        if (Physics.Raycast(ray, out hit, rayDist, 1 << LayerMask.NameToLayer("ITEM"))) //레이 ITEM 감지
        {
            if (_gm.chikState == ChickState.HELD || _gm.cokeState == CokeState.HELD)
            {
                isGazing = false;
                return;
            }
            else
            {
                isGazing = true;
                GazeAction(isGazing);
                if (hit.collider.tag == chickTag)   //레이힛이 치킨태그 콜라이더라면?
                {
                    Debug.Log("[GazeAction] 치킨이 감지됐다!!");
                }
                else if (hit.collider.tag == cokeTag)
                {
                    Debug.Log("[GazeAction] 콜라가 감지됐다!!");
                    //this.hit.collider.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
                    //CokeCtrl.Instance.MoveCoke();
                }
            }
        }
        //isGazing = true;
        //    GazeAction();               

        //else isGazing = false;        

        void GazeAction(bool isGaze)
        {
            startTime += Time.deltaTime;            //2초 시간이 채워진 변수
            currHit = this.hit.collider.gameObject; //현재 레이로 감지된 아이템

            if (currHit != prevHit) //현재
            {
                startTime = 0;
                prevHit = currHit;
                return;
            }

            else
            {
                if (startTime >= passTime)  //2초가 넘는다면,
                {
                    this.hit.collider.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);

                    if (this.hit.collider.tag == chickTag)
                    {
                        _gm.chikState = ChickState.MOVING; 
                    }
                    else if (this.hit.collider.tag == cokeTag)
                    {
                        _gm.cokeState = CokeState.MOVING; 
                    }
                }
            }
            GazeBar();                              //시간 서클도는 매서드
        }

        void GazeBar()
        {

        }
    }
}
