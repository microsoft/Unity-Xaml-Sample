// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandlers : MonoBehaviour {

    public delegate void OnEvent(object arg);

    public OnEvent onEvent = null;

    public readonly static Queue<Action> ExecuteOnMainThread = new Queue<Action>();

    private Text feedbackText;
    private ScrollRect scrollRect;
    private List<string> feedbackMsgs;

    // Use this for initialization
    void Start () {
        feedbackMsgs = new List<string>();

        feedbackText = GameObject.Find("FeedbackText").GetComponent<Text>();
        scrollRect = GameObject.Find("Panel-ScrollRect").GetComponent<ScrollRect>();

        feedbackText.text = "Start of run.\n";
    }

    // Update is called once per frame
    void Update () {
        while (feedbackMsgs.Count > 0)
        {
            _ShowFeedback(feedbackMsgs[0]);
            feedbackMsgs.RemoveAt(0);
        }

    }

    public void OnSendDataToXamlClicked()
    {
        ShowFeedback("OnSendDataToXamlClicked");

        if (onEvent != null)
            onEvent(this);

#if UNITY_WSA_10_0 && !UNITY_EDITOR 
        //if (ble == null)
        //    ble = BluetoothLEHelper.Instance;
        //ble.StartEnumeration();
#endif
    }

    void _ShowFeedback(string msg)
    {
        System.Diagnostics.Debug.WriteLine(msg);
        feedbackText.text += msg + "\n";

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }


    public void ShowFeedback(string msg)
    {
        feedbackMsgs.Add(msg);
    }

}
