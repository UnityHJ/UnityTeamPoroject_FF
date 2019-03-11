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
<<<<<<< HEAD
    private GameObject currHit;
    private GameObject prevHit;

    public float passTime = 2f;
    private float startTime = 0;

    private bool isGazing = false;
=======
>>>>>>> 8455f2ef3fd18386132250a27e916532554f977e


    [Header("Bool trigger")]
    public bool emptyChick = true;      //치킨 플레이어 손 안 유무
    public bool emptyCoke = true;       //코크 플레이어 손 안 유무

    public static EyeCast Instance;

    public delegate void EyeHandler();  //이벤트 핸들할 델리
    public static event EyeHandler OnClick; //이벤트 델리 함수변수

<<<<<<< HEAD
    private GameManager _gm = GameManager.Instance;


=======
>>>>>>> 8455f2ef3fd18386132250a27e916532554f977e

    void Start()
    {
        if (EyeCast.Instance == null) Instance = this;
        else if (EyeCast.Instance != null) Destroy(gameObject);
        DontDestroyOnLoad(this);

        tr = GetComponent<Transform>();

        Debug.DrawRay(ray.origin, ray.direction * rayDist, Color.green);
<<<<<<< HEAD

        _gm = GameManager.Instance;
        //_gm.state = GameManager.ChickState.NORMAL;


=======
>>>>>>> 8455f2ef3fd18386132250a27e916532554f977e
    }

    void Update()
    {
        Debug.DrawRay(ray.origin, ray.direction * rayDist, Color.green);

        ray = new Ray(tr.position, tr.forward * rayDist);

        if (Physics.Raycast(ray, out hit, rayDist, 1 << LayerMask.NameToLayer("ITEM"))) //레이 ITEM 감지
        {

<<<<<<< HEAD

            if (hit.collider.tag == chickTag)   //레이힛이 치킨태그 콜라이더라면?
            {
                Debug.Log("치킨이 감지됐다!!");
                isGazing = true;
                GazeAction();
=======
            if (hit.collider.tag == chickTag)   //레이힛이 치킨태그 콜라이더라면?
            {
                Debug.Log("치킨이 감지됐다!!");     

                this.hit.collider.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);   //OnClic 이벤트발동 메세지 발송

                if (emptyChick) //치킨 플레이어 손 안 없다면?
                {
                    ChickCtrl.Instance.isMoving = true;
                    emptyChick = false;
                }
                else
                {
                   
                }           
>>>>>>> 8455f2ef3fd18386132250a27e916532554f977e
            }
            else if (hit.collider.tag == cokeTag)
            {
                Debug.Log("콜라가 감지됐다!!");
<<<<<<< HEAD

                //this.hit.collider.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
=======
             
                this.hit.collider.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
>>>>>>> 8455f2ef3fd18386132250a27e916532554f977e

                //CokeCtrl.Instance.MoveCoke();

                var _rb = GameObject.FindGameObjectWithTag(cokeTag).GetComponent<Rigidbody>();
                _rb.AddForce(Vector3.up * 30f);
<<<<<<< HEAD
            }
        }
        else isGazing = false;
    }

    void GazeAction()
    {
        if (isGazing)
        {
            startTime += Time.deltaTime;
            currHit = this.hit.collider.gameObject;

            if (currHit != prevHit)
            {
                startTime = 0;
                prevHit = currHit;
                return;
            }

            if (startTime >= passTime)
            {
                this.hit.collider.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
                _gm.state = GameManager.ChickState.MOVING;
            }

            GazeBar();
        }
        else
        {
            startTime = 0;
        }
    }

    void GazeBar()
    {

=======
            }        
        }          
>>>>>>> 8455f2ef3fd18386132250a27e916532554f977e
    }
}
