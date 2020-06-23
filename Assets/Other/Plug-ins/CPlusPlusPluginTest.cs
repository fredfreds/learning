using System.Runtime.InteropServices;
using System;

namespace Plugins
{
    public class CPlusPlusPluginTest : UnityEngine.MonoBehaviour
    {
        const string dll = "Dll2";

        [DllImport(dll)]
        private static extern float Add(float a, float b);

        [DllImport(dll)]
        private static extern IntPtr Hello();

        private void Start()
        {
            UnityEngine.Debug.Log(Add(3, 4));
            UnityEngine.Debug.Log(Marshal.PtrToStringAnsi(Hello()));
        }
    }
}