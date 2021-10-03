using System;
using UnityEngine;
using System.Threading.Tasks;

//create customized animations
public static class GuiAnimUtils
{
    public static void FadeIn(GameObject gameObject, float time = 1, float delay = 0)
    {
        // LeanTween.alphaCanvas(gameObject, 0, 0);
        LeanTween.alpha(gameObject, 255, time)
            .setDelay(delay);
    }

    public static void FadeOut(GameObject gameObject, float time = 1, float delay = 0)
    {
        LeanTween.alpha(gameObject, 0, time)
            .setDelay(delay);
    }

    public static void MoveX(GameObject gameObject, float distanceX, float time = 1, float delay = 0)
    {
        float oldX = gameObject.transform.localPosition.x;
        gameObject.transform.Translate(Vector3.left * -distanceX);
        LeanTween.moveLocalX(gameObject, oldX, time)
            .setEase(LeanTweenType.easeInOutBack)
            .setDelay(delay);
    }

    public static void MoveY(GameObject gameObject, float distanceY, float time = 1, float delay = 0)
    {
        float oldY = gameObject.transform.localPosition.y;
        gameObject.transform.Translate(Vector3.up * -distanceY);
        LeanTween.moveLocalY(gameObject, oldY, time)
            .setEase(LeanTweenType.easeInOutBack)
            .setDelay(delay);
    }
}