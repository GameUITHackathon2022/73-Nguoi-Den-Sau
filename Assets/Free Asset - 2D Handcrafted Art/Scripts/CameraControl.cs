using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public Joystick moveAnalog;
    public Button jumpButton;
    public Material grass;
    public List<Light2D> lights;
    public Transform[] checkPointPos;
    public GameObject checkPoint;
    public List<PlayerController> players;
    public static CameraControl instance;
    public bool isSwappable;
    protected Transform trans;
  
    public PlayerController currentPlayer;
    public float SpeedMove = 3;
    public Transform limitMin;
    public Transform limitMax;
    public Vector3 Offset;

    //[HideInInspector] public BaseController controller;

    protected float cameraWidth;
    protected float cameraHeight;
    public float margin_X;
    public float margin_Y;

    //protected Joystick moveAnalog;

    //protected Joystick shootAnalog;
    public float lookAhead;
    private void Awake()
    {
        instance = this;
        trans = transform;
    }
    private void Start()
    {
        grass.SetFloat("_Fade", 0);
    }
    public IEnumerator ResetIsSwappable(float duration)
    {
        isSwappable = false;
        yield return new WaitForSeconds(duration);
        isSwappable = true;
    }
    public void ChangeCheckPointPos(int transIndex)
    {
        checkPoint.transform.position = checkPointPos[transIndex].position;
    }
    public void CameraZoomOut(float duration, Vector3 pos, int index = 0, int size = 15)
    {
        currentPlayer = null;
        this.transform.DOMove(pos, duration/4);
        
        Camera.main.DOOrthoSize(size, duration).OnComplete(()=> { SwitchTarget(index); Camera.main.DOOrthoSize(6, 1); });
        
        
    }
    public void CameraZoomOut(float duration, Vector3 pos, PlayerController player, int size = 15)
    {
        currentPlayer = null;
        this.transform.DOMove(pos, duration / 4);

        Camera.main.DOOrthoSize(size, duration).OnComplete(() => { SwitchTarget(player); Camera.main.DOOrthoSize(6, 1); });


    }
    public void LightAction()
    {
        lights[0].transform.parent.transform.DOMove(new Vector3(48, 20, 0), 5f).SetEase(Ease.InQuad);
        lights[0].color = new Color(1,0.3f,0.8f);
        lights[1].color = new Color(1, 1, 1);
    }
    public void SwitchTarget(int playerIndex)
    {
        currentPlayer = players[playerIndex];
    }
    public void SwitchTarget(PlayerController player)
    {
        currentPlayer = player;
    }
    protected bool IsMarginY()
    {
        if (trans.position.y + margin_Y < currentPlayer.transform.position.y || trans.position.y - margin_Y > currentPlayer.transform.position.y)
            return false;
        else
            return true;
    }
    protected bool IsMarginX()
    {
        if (trans.position.x + margin_X < currentPlayer.transform.position.x || trans.position.x - margin_X > currentPlayer.transform.position.x)
            return false;
        else
            return true;
    }
    public void CameraResize(float size)
    {
        cameraHeight = size / Camera.main.aspect;
        cameraWidth = size;

        float a = Camera.main.orthographicSize;

        //DOVirtual.Float(a, cameraHeight, 1f, v => Camera.main.orthographicSize = v);

        //Camera.main.orthographicSize = size / Camera.main.aspect;
    }

    public void MaxCameraFit()
    {
        Camera.main.transform.position = new Vector3(0, 0, -1);
        float CameraWidth = (limitMax.position.x - limitMin.position.x) / 2f;
        float CameraHeight = (limitMax.position.y - limitMin.position.y) / 2f;
        if (CameraHeight * Camera.main.aspect > CameraWidth) //Width > width
        {
            CameraResize(CameraWidth);
        }
        else
        {
            CameraWidth = CameraHeight * Camera.main.aspect; // width = height / width / height
            CameraResize(CameraWidth);
        }
    }
    protected IEnumerator ZeroLookForward(float time)
    {
        var a = lookAhead;
        lookAhead = 0;
        yield return new WaitForSeconds(time);
        lookAhead = a;
    }
    private void FixedUpdate()
    {
        if (currentPlayer == null)
            return;
        Vector3 posMove = trans.position;
        Vector3 pos = currentPlayer.transform.position + Offset;
        pos.z = trans.position.z;
        if (!IsMarginX() || !IsMarginY())
        {
            posMove = Vector3.Lerp(trans.position, pos, Time.fixedDeltaTime * SpeedMove);
        }

        if (currentPlayer.transform.localScale.x > 0)
        {
            posMove.x -= lookAhead;
        }
        else
        {
            posMove.x += lookAhead;
        }
        posMove.x = Mathf.Clamp(posMove.x, limitMin.position.x + cameraWidth, limitMax.position.x - cameraWidth);
        posMove.y = Mathf.Clamp(posMove.y, limitMin.position.y + cameraHeight, limitMax.position.y - cameraHeight);
        trans.position = posMove;
        
        //if (SoccerGameManager.instance.Gaming)
        //{
        //    if (targetTransform != null)
        //    {
        //        if (oldTargetTransform != targetTransform)
        //        {
        //            //StartCoroutine(ZeroLookForward(1f));
        //            oldTargetTransform = targetTransform;
        //        }
               
        //    }
        //    else
        //    {
        //        targetTransform = SoccerGameManager.instance.playerSoccerController.transform;
        //    }
        //}
        //else
        //    MaxCameraFit();
    }
}
