using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPointsHolder : MonoBehaviour {
	
    public GameObject bulletPointPrefab;
    public CustomRectTransform customRectTransform;
    public int spacing;

    RectTransform rectTransform;

    List<Image> bulletList;

    int previousSelected;

    void Awake () {
        rectTransform = GetComponent<RectTransform>();
        bulletList = new List<Image>();
    }

    void Start ()
    {
        float bulletSize = bulletPointPrefab.GetComponent<RectTransform>().rect.width;
        float skinCount = customRectTransform.transform.GetChild(0).childCount;
        rectTransform.sizeDelta = new Vector2( skinCount * bulletSize + (skinCount-1)*spacing ,rectTransform.sizeDelta.y);

        for (int i = 0; i < skinCount; i++)
        {
            var instance = Instantiate(bulletPointPrefab, transform);
            instance.transform.localScale = Vector3.one;
            bulletList.Add(instance.GetComponent<Image>());
        }
        setSelected(0);
    }
    
    public void setSelected (int index)
    {
        if (bulletList.Count > 0)
            bulletList[previousSelected].color = Color.white;
        bulletList[index].color = Color.grey;
        previousSelected = index;
    }


}
