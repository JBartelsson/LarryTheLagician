using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{

    [SerializeField] Image potionImage;
    [SerializeField] Item itemType;
    public float disabledAlpha = 0.47f;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnInventoryUpdated += Instance_OnInventoryUpdated;
    }

    private void Instance_OnInventoryUpdated(object sender, List<ItemSO> e)
    {
        if (e.Any((x) => x.itemtype == itemType))
        {
            potionImage.color = new Color(potionImage.color.r, potionImage.color.g, potionImage.color.b, 1f);
        } else
        {
            potionImage.color = new Color(potionImage.color.r, potionImage.color.g, potionImage.color.b, disabledAlpha);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
