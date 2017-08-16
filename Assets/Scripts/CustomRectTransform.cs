using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomRectTransform : ScrollRect {

    [Range(0, 1)]
    public float speed;
    public float scaleFactor;
    public bool hasBulletPoints;
    public BulletPointsHolder bulletPointsHolder;

    public float threshHold;
    public float bigScale;
    public float smallScale;

    List<Vector3> stoppingPositions;
    float adapterWidth;
    bool isDragging;
    float offset;
    float adapterNormalizedSize;

    Vector2 endingAnchorPoint;
    int previousIndex;
    
    List<Transform> gfxTransformList = new List<Transform>();

    protected override void Awake() {
        base.Awake();
		if (!Application.isPlaying)
			return;
        adapterWidth = content.GetChild(0).GetComponent<RectTransform>().rect.width;
        offset = 1f / (2f * (content.childCount - 1));
        adapterNormalizedSize = 1f / (content.childCount - 1);
        PopulateThumbnailList();
    }

    protected override void LateUpdate() { 
        base.LateUpdate();
		if (!Application.isPlaying)
			return;
        updateGfxScale();
        if (isDragging)
            return;
        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, endingAnchorPoint, speed);   
    }
        
    public override void OnBeginDrag(PointerEventData eventData){
        base.OnBeginDrag(eventData);
        isDragging = true;
    }

    public override void OnDrag(PointerEventData eventData){
        base.OnDrag(eventData);
        if (!hasBulletPoints)
            return;
        setAnchorPoints();
    }

    void setAnchorPoints (){

        float correctedHorizontalPosition = horizontalNormalizedPosition + offset;
        int index = -1;
        if (correctedHorizontalPosition > 1)
            index = content.childCount - 1;
        else if (correctedHorizontalPosition < 0)
            index = 0;
        else
            index = (int)(correctedHorizontalPosition / adapterNormalizedSize);

        endingAnchorPoint = new Vector2(-index * adapterWidth, content.anchoredPosition.y);
        if (hasBulletPoints)
            bulletPointsHolder.setSelected(index);
    }

    void PopulateThumbnailList() { 
        foreach (Transform t in content.GetComponentInChildren<Transform>()){
            gfxTransformList.Add(t.GetChild(0));
        }
    }

    void updateGfxScale(){
        foreach(Transform t in gfxTransformList){
            float diff = t.position.x - transform.position.x + 15 ;
            diff = Mathf.Abs(diff);

            if (diff > threshHold){
                t.localScale = Vector3.one * smallScale;
            }
            else{
                t.localScale = Vector3.one * ExponentialyInterpolate (diff);
            }
        }
    }
    
    float LinearInterpolation (float x){
        float a = (smallScale - bigScale) / threshHold;

        return a * x + bigScale;
    }

    float ExponentialyInterpolate (float x){
        float thSqr = threshHold * threshHold;
        float a = (smallScale - bigScale) / thSqr;
        float b = smallScale - thSqr * a - bigScale;
        float c = bigScale;
        return a * x * x + b * x + c;
    }

    public override void OnEndDrag (PointerEventData eventData){
        base.OnEndDrag (eventData);
        setAnchorPoints();
        isDragging = false;
    }
}
