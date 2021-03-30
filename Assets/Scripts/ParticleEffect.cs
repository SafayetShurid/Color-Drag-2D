using PE2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    public float speedOffset = .01f;
    public float lengthMultiplier = 40f;
    public int numToSpawn = 200;
    public WrapAroundType wrapAround;

    public static ParticleEffect instance;

    public void Awake()
    {
        instance = this;
    }

    public void SpawnExplosion(Vector2 position,ColorType colorType)
    {
        float hue1 = Random.Range(0, 6);
        float hue2 = (hue1 + Random.Range(0, 2)) % 6f;
        Color colour1 = StaticExtensions.Color.FromHSV(hue1, 0.5f, 1);
        Color colour2 = StaticExtensions.Color.FromHSV(hue2, 0.5f, 1);

        for (int i = 0; i < numToSpawn; i++)
        {
            float speed = (18f * (1f - 1 / Random.Range(1f, 10f))) * speedOffset;

            var state = new ParticleBuilder()
            {
                velocity = StaticExtensions.Random.RandomVector2(speed, speed),
                wrapAroundType = wrapAround,
                lengthMultiplier = lengthMultiplier,
                velocityDampModifier = 0.94f,
                removeWhenAlphaReachesThreshold = true
            };

            var colour = Color.Lerp(ColorPicker.instance.GetColorRGBValue(colorType), ColorPicker.instance.GetColorRGBValue(colorType), Random.Range(0, 1));

            float duration = 320f;
            var initialScale = new Vector2(2f, 1f);


            ParticleFactory.instance.CreateParticle(position, colour, duration, initialScale, state);
        }
    }
}
