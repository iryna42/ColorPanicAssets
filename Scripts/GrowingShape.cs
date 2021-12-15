using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingShape : Shape
{
    public Vector2 positionMin;//minimum position
    public Vector2 positionMax;//maximum position
    //public Range growSpeedRange;//Min and max for growSpeed field
    //public Range rotationSpeedRange;//Min and max for rotationSpeed field

    [Tooltip("Minimum distance btw player and shape to spawn")]
    public float minPlayerDistance = 1.5f;

    float growSpeed;//scale multiplicator for 1s
    float rotationSpeed;//add this rotation each sec
    float startTime;
    bool readyToDestrooy;//isIn the readyToDestroy queue

    // Start is called before the first frame update
    void Start()
    {
        Vector2 pos = Vector2.zero;
        Vector2 delta = Vector2.zero;

        while (delta.magnitude < minPlayerDistance)
		{
            pos = new Vector3(Random.Range(positionMin.x, positionMax.x), Random.Range(positionMin.y, positionMax.y), 0);
            delta = (Vector2)GameManager.instance.player.transform.position - pos;
        }

        gameObject.transform.position = pos;
        gameObject.transform.localScale = Vector3.zero;

        growSpeed = sizeSpeedRange.PickRandom();
        rotationSpeed = rotationSpeedRange.PickRandom();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
		if (!isReverting)
		{
			gameObject.transform.localScale = (Time.time - startTime) * growSpeed * Vector3.one;
			gameObject.transform.rotation = Quaternion.Euler(0, 0, (Time.time - startTime) * rotationSpeed);

			if (gameObject.transform.localScale.x > MAX_SIZE && !readyToDestrooy)
			{
				GameManager.instance.readyToDestroy.Enqueue(this);
				readyToDestrooy = true;
			} 
		}
    }

    public override void Revert()
    {
        isReverting = true;

        LeanTween.scale(gameObject, Vector3.zero, REVERT_TIME).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.rotateZ(gameObject, -rotationSpeed * (Time.time - startTime), REVERT_TIME).setEase(LeanTweenType.easeInOutQuad);
        Destroy(gameObject, REVERT_TIME);
    }
}
