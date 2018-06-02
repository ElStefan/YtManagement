﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YtManagement.Monitor.Extension
{
    public static class ControlExtensions
    {
        private static ToolTip toolTip;
        public static void ShowTooltip(this Control control, string message, ToolTipIcon icon)
        {
            toolTip?.Dispose();

            toolTip = new ToolTip();
            toolTip.ToolTipTitle = icon.ToString();
            var pos = control.PointToClient(Cursor.Position);
            toolTip.ToolTipIcon = icon;
            pos.X += 20;
            pos.Y += 10;
            toolTip.Show(message, control, pos, 5000);
        }
    }
}