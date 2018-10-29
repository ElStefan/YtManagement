using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Drawing;
using YtManagement.Common.Model;

namespace YtManagement.Monitor.Extension
{
    public static class ObjectListViewExtension
    {
        private static readonly TextOverlay LoadingOverlay = new TextOverlay
        {
            Text = "Loading...",
            Transparency = 210,
            BackColor = Color.LightGray,
            CornerRounding = 3,
            BorderColor = Color.Black,
            BorderWidth = 1.5f,
            TextColor = Color.Black
        };

        public static void LoadFrom<T>(this ObjectListView olv, Func<ActionResult<IEnumerable<T>>> dataReceiver)
        {
            LoadFrom(olv, dataReceiver, null);
        }

        public static void LoadFrom<T>(this ObjectListView olv, Func<ActionResult<IEnumerable<T>>> dataReceiver, Func<IEnumerable<T>, IEnumerable<T>> manipulateFunc)
        {
            olv.OverlayText = LoadingOverlay;
            olv.EmptyListMsg = null;
            olv.ClearObjects();
            var result = dataReceiver.Invoke();
            if (result.Status != ActionStatus.Success)
            {
                olv.EmptyListMsg = result.Message;
                olv.OverlayText = null;
                return;
            }
            olv.EmptyListMsg = "List is empty";
            olv.OverlayText = null;
            var data = result.Data;
            if (manipulateFunc != null)
            {
                data = manipulateFunc(data);
            }
            olv.SetObjects(data);
        }
    }
}
