using UnityEngine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class HandTrackerController : MonoBehaviour
{
    public string modelPath = "C:/Users/spiry/Desktop/hackYea/HCKYEAH/models_onnx_converted/model_hand_tracker.onnx";
    public RawImage display; // przypisz w Inspectorze UI RawImage
    //public int cameraIndex = 1; // która kamera u¿ywana


    private IntPtr tracker;
    private WebCamTexture cam;
    private Texture2D tex;
    private Texture2D outputTex; // do wyœwietlania wyniku
    private byte[] rawArray;
    private byte[] outArray;
    private byte counter = 0;

    public bool is_attack = false;
    public byte movement = 0;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log($"Camera {i}: {devices[i].name}");
        }
        int cameraIndex = 1; // np. pierwsza kamera
        cam = new WebCamTexture(WebCamTexture.devices[cameraIndex].name,320,240,30);
        cam.Play();

        StartCoroutine(InitTexture());
    }

    System.Collections.IEnumerator InitTexture()
    {
        while (cam.width <= 16) yield return null;

        tex = new Texture2D(cam.width, cam.height, TextureFormat.RGB24, false);
        rawArray = new byte[cam.width * cam.height * 3]; // bufor do RGB

        if (!HandTracker.HT_OnInit(out tracker, modelPath))
            Debug.LogError("Failed to initialize HandTracker!");
        display.texture = outputTex;
    }

    void Update()
    {
        if (cam.didUpdateThisFrame && tex != null && tracker != IntPtr.Zero)
        {
            var colors = cam.GetPixels32();

            // szybka konwersja
            for (int i = 0; i < colors.Length; i++)
            {
                byte mono = colors[i].r;
                rawArray[i * 3 + 0] = mono;
                rawArray[i * 3 + 1] = mono;
                rawArray[i * 3 + 2] = mono;
            }

            int width = tex.width;
            int height = tex.height;
            int stride = width * 3;

            // Wyœlij do C++
            var handle = GCHandle.Alloc(rawArray, GCHandleType.Pinned);
            try
            {
                HandTracker.HT_OnImageRaw(tracker, handle.AddrOfPinnedObject(), width, height, stride);
            }
            finally { handle.Free(); }

            // Odbierz wynik z C++
            //handle = GCHandle.Alloc(outArray, GCHandleType.Pinned);
            //try
            //{
            //    HandTracker.HT_GetOutputImageRaw(tracker, handle.AddrOfPinnedObject(), width, height, stride);
            //}
            //finally { handle.Free(); }

            //// Wyœwietl
            //outputTex.LoadRawTextureData(outArray);
            //outputTex.Apply(false);

            if (outputTex == null)
            {
                outputTex = new Texture2D(cam.width, cam.height, TextureFormat.RGB24, false);
                outArray = new byte[cam.width * cam.height * 3];
                display.texture = outputTex;
            }

            if (tracker == IntPtr.Zero) return;

            var outHandle = GCHandle.Alloc(outArray, GCHandleType.Pinned);
            try
            {
                IntPtr outPtr = outHandle.AddrOfPinnedObject();
                HandTracker.HT_GetOutputImageRaw(tracker, outPtr, cam.width, cam.height, cam.width * 3);
            }
            catch (Exception e)
            {
                Debug.LogError($"HT_GetOutputImageRaw crashed: {e.Message}");
            }
            finally
            {
                outHandle.Free();
            }

            outputTex.LoadRawTextureData(outArray);
            outputTex.Apply(false);

            // Sterowanie
            var ctrl = HandTracker.HT_GetControl(tracker);
            is_attack = ctrl.isAttack;
            movement = ctrl.movement;
            Debug.Log($"Attack={ctrl.isAttack}, Movement={ctrl.movement}");
        }
    }


    void OnDestroy()
    {
        if (tracker != IntPtr.Zero)
            HandTracker.HT_Destroy(tracker);
    }
}
