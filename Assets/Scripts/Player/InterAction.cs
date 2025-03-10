using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InterAction : MonoBehaviour
{
    // ī�޶� 
    Camera camera;
    // ��ȣ�ۿ빰ü�� ���̾� ����ũ 
    [SerializeField] LayerMask layerMask;
    // ����â ������Ʈ
    [SerializeField] GameObject promtGO;
    // ��ȣ�ۿ��� ��ü �̸� 
    [SerializeField] TextMeshProUGUI nameText;
    // ��ȣ�ۿ��� ��ü ����
    [SerializeField] TextMeshProUGUI descriptionText;
    // �ֱ� �� ������Ʈ
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
        // ��ȣ�ۿ� �� ��ü ã�� �Լ� 
        FindInterAction();
    }

    // ��ȣ�ۿ� �� ��ü ã�� �Լ� 
    void FindInterAction()
    {
        // ȭ���� �߾ӿ��� ������
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        // ���̾��ũ�� ������ ��ü�� �������� �¾��� ��(��, �ǹ�, ��Ÿ ������Ʈ�� ����)
        if (Physics.Raycast(ray, out hit, 1f, layerMask))
        {
            // ����â Ȱ��ȭ 
            promtGO.SetActive(true);
            // �ֱ� �� ������Ʈ ���� 
            curLookObject = hit.transform.gameObject;
            // ����â �ֱ� �� ������Ʈ�� ������Ʈ
            UpdatePrompt(hit.collider.GetComponent<InteractableObject>());
        }
        // ���̾��ũ�� ������ ��ü �� �������� ������ ���ٸ� 
        else
        {
            // ����â �ʱ�ȭ
            ClearPrompt();
            // �ֱ� �� ������Ʈ ����
            curLookObject = null;
            // ����â ��Ȱ��ȭ
            promtGO.SetActive(false);
        }
    }

    // ����â ������Ʈ �Լ� 
    void UpdatePrompt(InteractableObject mapObject)
    {
        // ���� ��ü�� �̸��� ���� �������� 
        nameText.text = mapObject.GetObjectName();
        descriptionText.text = mapObject.GetObjectDescription();
    }

    // ����â �ʱ�ȭ �Լ� 
    void ClearPrompt()
    {
        nameText.text = "";
        descriptionText.text = "";
    }

    // ��ȣ�ۿ� Ű���� E�� ����
    public void OnInterAction(InputAction.CallbackContext context)
    {
        // E�� ������ �� 
        if (context.phase == InputActionPhase.Started)
        {
            // �ֱ� �� ������Ʈ�� �ְ� �� ������Ʈ�� Items ��ũ��Ʈ�� ������ �ִٸ� - ���̳� �ǹ����� Items ��ũ��Ʈ�� ���� ����
            if (curLookObject != null && curLookObject.TryGetComponent<Items>(out Items item))
            {
                // UIInventory�� ������ �ֱ� 
                UIManager.Instance.UIInventory.PushItem(item);
                // �ֱ� �� ������Ʈ ��Ȱ��ȭ - Destory �ҷ��� ������ �׷��� �ϸ� PushItem���� ���� item�� ����� 
                curLookObject.SetActive(false);
                // �ֱ� �� ������Ʈ ���� 
                curLookObject = null;
            }
        }
    }
}
