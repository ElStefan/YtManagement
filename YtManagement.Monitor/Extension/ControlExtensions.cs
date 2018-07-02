using System.Windows.Forms;

namespace YtManagement.Monitor.Extension
{
    public static class ControlExtensions
    {
        private static ToolTip toolTip;
        public static void ShowTooltip(this Control control, string message, ToolTipIcon icon)
        {
            toolTip?.Dispose();

            toolTip = new ToolTip
            {
                ToolTipTitle = icon.ToString(),
                ToolTipIcon = icon
            };
            var pos = control.PointToClient(Cursor.Position);
            pos.X += 20;
            pos.Y += 10;
            toolTip.Show(message, control, pos, 5000);
        }
    }
}
