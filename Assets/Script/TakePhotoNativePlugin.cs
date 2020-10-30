using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class TakePhotoNativePlugin : MonoBehaviour
{
    // Comment : เป็น Plugin สำหรับ Save รูปภาพ //

    public GameObject secondPanel;
    public Text textDebug;
    public Text textDebugGet;

    public void ButtonCaptureScreenShot()
    {
        StartCoroutine("TakeScreenShot");
    }

    IEnumerator TakeScreenShot()
    {
        string albumName = "YakGameImage";
        string timeStamp = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");

        secondPanel.SetActive(false);

        yield return new WaitForEndOfFrame();
        Texture2D tex = new Texture2D(Screen.width, Screen.height);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        yield return tex;

        secondPanel.SetActive(true);

        // Save Image to Gallery //

        NativeGallery.SaveImageToGallery(tex, albumName, timeStamp + ".png", (success, path) => textDebug.text = "Media save result: " + success + " " + path);

        // To avoid memory leaks //

        Destroy(tex);
    }

    public void ButtonGetImage()
    {
        // ลอง Get แบบอันเดียวก่อน //

        string filePath;

        NativeGallery.GetImageFromGallery((path) =>
        {
            filePath = path;

            textDebugGet.text = "ImagePath" + path;
        }, "Select a PNG Image", "image/png");

        //NativeGallery.LoadImageAtPath((filePath));
    }
}
