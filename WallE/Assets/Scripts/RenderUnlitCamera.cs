using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderUnlitCamera : MonoBehaviour
{


    public Camera cam;
    public RenderTexture renderTexture;
    public Texture2D texture2D;
    private Texture2D virtualPhoto;


    // Use this for initialization
    void Start()
    {
        renderTexture = cam.targetTexture;
        texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
    }

    public void RenderCamera()
    {
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();



        byte[] bytes;
        bytes = texture2D.EncodeToPNG();

        System.IO.File.WriteAllBytes(OurTempSquareImageLocation(), bytes);
        Debug.Log("SHE WROTE");

    }

    private string OurTempSquareImageLocation()
    {
        string r = Application.persistentDataPath + "/p.png";
        return r;
    }



    public IEnumerator CompareTexture(Text text)
    {
		text.gameObject.SetActive(true);
		int count = 0;
		float loadingCount = 0;
		int purpleCount = 0;
		int blueCount = 0;
        for (int i = 0; i < texture2D.width; i++)
        {
            for (int j = 0; j < texture2D.height; j++)
            {
                Color c = texture2D.GetPixel(i, j);
				// if(c.r > 0.1f && c.b < 0.1f){
				// 	redCount++;
				// }
				if(c.r < 0.2f && c.b > 0.2f){
					blueCount++;
				}
				if(c.r > 0.2f && c.b > 0.2f){
					purpleCount++;
				}

				// Debug.Log(c);
				
				loadingCount++;
				count++;
				if(count >= 100){
					text.text = "Loading Results: " + Mathf.FloorToInt(100 * loadingCount / (renderTexture.width*renderTexture.height)) + "%";
					yield return null;
					count = 0;
				}
            }
        }
		Debug.Log("purpleCount:" + purpleCount);
		// Debug.Log("redCount:" + redCount);
		Debug.Log("blueCount:" + blueCount);

		Debug.Log("Overlap Percentage: " + (100f * (float)purpleCount / (float)(blueCount + purpleCount)) + "%");
		
		text.text = "Overlap Percentage: " + (100f * (float)purpleCount / (float)(blueCount + purpleCount)) + "%";
		
		yield return null;
    }
}
