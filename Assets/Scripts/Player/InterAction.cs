using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InterAction : MonoBehaviour
{
    // 카메라 
    Camera camera;
    // 상호작용물체용 레이어 마스크 
    [SerializeField] LayerMask layerMask;
    // 설명창 오브젝트
    [SerializeField] GameObject promtGO;
    // 상호작용할 물체 이름 
    [SerializeField] TextMeshProUGUI nameText;
    // 상호작용할 물체 설명
    [SerializeField] TextMeshProUGUI descriptionText;
    // 최근 본 오브젝트
    GameObject curLookObject;

    private void Awake()
    {
        camera = Camera.main;
        promtGO.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 상호작용 할 물체 찾기 함수 
        FindInterAction();
    }

    // 상호작용 할 물체 찾기 함수 
    void FindInterAction()
    {
        // 화면의 중앙에서 레이저
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        // 레이어마스크로 설정한 물체가 레이저에 맞았을 때(길, 건물, 기타 오브젝트로 설정)
        if (Physics.Raycast(ray, out hit, 1f, layerMask))
        {
            // 설명창 활성화 
            promtGO.SetActive(true);
            // 최근 본 오브젝트 설정 
            curLookObject = hit.transform.gameObject;
            // 설명창 최근 본 오브젝트로 업데이트
            UpdatePrompt(hit.collider.GetComponent<InteractableObject>());
        }
        // 레이어마스크로 설정한 물체 중 레이저에 맞은게 없다면 
        else
        {
            // 설명창 초기화
            ClearPrompt();
            // 최근 본 오브젝트 비우기
            curLookObject = null;
            // 설명창 비활성화
            promtGO.SetActive(false);
        }
    }

    // 설명창 업데이트 함수 
    void UpdatePrompt(InteractableObject mapObject)
    {
        // 맞은 물체의 이름과 설명 가져오기 
        nameText.text = mapObject.GetObjectName();
        descriptionText.text = mapObject.GetObjectDescription();
    }

    // 설명창 초기화 함수 
    void ClearPrompt()
    {
        nameText.text = "";
        descriptionText.text = "";
    }

    // 상호작용 키보드 E로 설정
    public void OnInterAction(InputAction.CallbackContext context)
    {
        // E를 눌렀을 때 
        if (context.phase == InputActionPhase.Started)
        {
            // 최근 본 오브젝트가 있고 그 오브젝트에 Items 스크립트를 가지고 있다면 - 길이나 건물에는 Items 스크립트가 없기 때문
            if (curLookObject != null && curLookObject.TryGetComponent<Items>(out Items item))
            {
                // UIInventory에 아이템 넣기 
                UIManager.Instance.UIInventory.PushItem(item);
                // 최근 본 오브젝트 비활성화 - Destory 할려고 했지만 그렇게 하면 PushItem에서 넣은 item도 사라짐 
                curLookObject.SetActive(false);
                // 최근 본 오브젝트 비우기 
                curLookObject = null;
            }
        }
    }
}
