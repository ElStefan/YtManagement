using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            olv.SetObjects(result.Data);
        }
    }
}
