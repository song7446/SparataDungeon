using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterAction : MonoBehaviour
{
    Camera camera;
    [SerializeField]LayerMask layerMask;
    [SerializeField] GameObject promtGO;
    [SerializeField]TextMeshProUGUI nameText;
    [SerializeField]TextMeshProUGUI descriptionText;

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
        FindInterAction();
    }

    void FindInterAction()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1f, layerMask))
        {
            promtGO.SetActive(true);
            UpdatePrompt(hit.collider.GetComponent<MapObject>());
        }
    }

    void UpdatePrompt(MapObject mapObject)
    {
        nameText.text = mapObject.GetObjectName();
        descriptionText.text = mapObject.GetObjectDescription();
    }
}
