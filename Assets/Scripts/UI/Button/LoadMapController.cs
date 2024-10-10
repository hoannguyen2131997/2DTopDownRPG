using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class LoadMapController : MonoBehaviour
{
    [SerializeField] private GameObject UIMapCollection;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private AssetReference mapAssetReference;
    private Button selectedMapBtn;

    private void Awake()
    {
        selectedMapBtn = this.GetComponent<Button>();

    }

    private void Start()
    {
        if(mapManager != null)
        {
            selectedMapBtn.onClick.AddListener(OnButton);
        }
        else
        {
            Debug.Log("Error: MapManager refernce not find!");
        }
    }

    private void OnButton()
    {
        mapManager.LoadObjectAddressable(mapAssetReference); 
        UIMapCollection.SetActive(false);
    }
}
