using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingShape : Shape
{
    public float maxSize;//start size
    public Vector2 positionMin;//minimum position
    public Vector2 positionMax;//maximum position
    //public Range shrinkSpeedRange;//Min and max for growSpeed field
    //public Range rotationSpeedRange;//Min and max for rotationSpeed field

    float shrinkSpeed;//scale multiplicator for 1s
    float rotationSpeed;//add this rotation each sec
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(Random.Range(positionMin.x, positionMax.x), Random.Range(positionMin.y, positionMax.y), 0);
        gameObject.transform.localScale = maxSize * Vector2.one;

        shrinkSpeed = sizeSpeedRange.PickRandom();
        rotationSpeed = rotationSpeedRange.PickRandom();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
		if (!isReverting)
		{
			gameObject.transform.localScale = (maxSize - ((Time.time - startTime) * shrinkSpeed)) * Vector3.one;
			gameObject.transform.rotation = Quaternion.Euler(0, 0, (Time.time - startTime) * rotationSpeed);

            if (gameObject.transform.localScale.x <= 0)
            {
                GameManager.instance.shapes.Remove(this);
                Destroy(gameObject);
            }
		}
    }

    public override void Revert()
    {
        isReverting = true;

        LeanTween.scale(gameObject, Vector3.one * MAX_SIZE, REVERT_TIME).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.rotateZ(gameObject, rotationSpeed * (Time.time - startTime), REVERT_TIME).setEase(LeanTweenType.easeInOutQuad);
        Destroy(gameObject, REVERT_TIME);
    }
}
