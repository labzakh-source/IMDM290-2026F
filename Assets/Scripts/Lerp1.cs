// UMD IMDM290 
// Instructor: Myungin Lee
    // [a <-----------> b]
    // Lerp : Linearly interpolates between two points. 
    // https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Vector3.Lerp.html

using UnityEngine;

public class Lerp1 : MonoBehaviour
{
    GameObject[] spheres;
    static int numSphere = 200; 
    float time = 0f;
    Vector3[] startPosition, endPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Assign proper types and sizes to the variables.
        spheres = new GameObject[numSphere];
        startPosition = new Vector3[numSphere]; 
        endPosition = new Vector3[numSphere]; 
        
        // Define target positions. Start = random, End = heart 
        for (int i =0; i < numSphere; i++){
            // Random start positions
            float r = 10f;
            startPosition[i] = new Vector3(
                r * Random.Range(-1f, 1f), 
                r * Random.Range(-1f, 1f), 
                r * Random.Range(-1f, 1f));        
        
            // Circular end position
            float baseRadius = 2.5f;
            float starPoints = 5f;

            float angle = i * 2 * Mathf.PI / numSphere;
            float radius = baseRadius * (1f + 0.5f * Mathf.Sin(starPoints * angle));

            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);
            endPosition[i] = new Vector3(x, y, 10f);
        }
        // Let there be spheres..
        for (int i =0; i < numSphere; i++){
            // Draw primitive elements:
            // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/GameObject.CreatePrimitive.html
            spheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere); 

            // Position
            spheres[i].transform.position = startPosition[i];

            // Color. Get the renderer of the spheres and assign colors.
            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            // HSV color space: https://en.wikipedia.org/wiki/HSL_and_HSV
            sphereRenderer.material.color = new Color(.1f, 0.6f, .3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Measure Time 
        time += Time.deltaTime; // Time.deltaTime = The interval in seconds from the last frame to the current one
        // what to update over time?
        for (int i =0; i < numSphere; i++){
            // Lerp : Linearly interpolates between two points.
            // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Vector3.Lerp.html
            // Vector3.Lerp(startPosition, endPosition, lerpFraction)
            
            // lerpFraction variable defines the point between startPosition and endPosition (0~1)
            // let it oscillate over time using sin function
            float lerpFraction = Mathf.Sin(time) * 0.5f + 0.5f;
            // Lerp logic. Update position
            spheres[i].transform.position = Vector3.Lerp(startPosition[i], endPosition[i], lerpFraction);
            // For now, start positions and end positions are fixed. But what if you change it over time?
            // startPosition[i]; endPosition[i];
            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            // Color Update over time
            float g = 0.7f + 0.4f * Mathf.Sin(time + i * 0.05f);
            sphereRenderer.material.color = new Color(g, 0f, 0.1f);
            
        }
    }
}
