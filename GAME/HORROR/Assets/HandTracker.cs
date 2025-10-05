using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class HandTracker
{
    private const string DLL_NAME = "handtracker";

    [StructLayout(LayoutKind.Sequential)]
    public struct CAPI_ControlOutput
    {
        [MarshalAs(UnmanagedType.I1)] // wymusza bool jako 1 bajt
        public bool isAttack;

        //[MarshalAs(UnmanagedType.LPStr)]
        //public string movement;\
        public byte movement;
    }

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool HT_OnInit(out IntPtr tracker, string modelPath);

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern void HT_OnImageRaw(IntPtr tracker, IntPtr data, int width, int height, int stride);

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern void HT_GetOutputImageRaw(IntPtr tracker, IntPtr data, int width, int height, int stride);

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern CAPI_ControlOutput HT_GetControl(IntPtr tracker);

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern void HT_Destroy(IntPtr tracker);
}
