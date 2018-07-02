using BrightIdeasSoftware;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using YtManagement.Model;
using YtManagement.Monitor.Extension;

namespace YtManagement.Monitor
{
    public partial class Form1 : Form
    {
        private readonly TextOverlay _loadingOverlay = new TextOverlay { Text = "Loading...", Transparency = 210, BackColor = Color.LightGray, CornerRounding = 3, BorderColor = Color.Black, BorderWidth = 1.5f, TextColor = Color.Black };


        //private BackgroundWorker worker;

        public Form1()
        {

            InitializeComponent();
            this.toolStripComboBoxApi.Items.Add("http://localhost:50002/api/");
            this.toolStripComboBoxApi.Items.Add("http://diskstation.lampertnet:50002/api/");
            this.toolStripComboBoxApi.SelectedIndex = 0;
            this.comboBoxSearchPosition.DataSource = Enum.GetValues(typeof(SearchPosition)).Cast<SearchPosition>().Select(o => new { Name = o.ToString(), Value = (int)o }).ToList();
            this.comboBoxSearchPosition.DisplayMember = "Name";
            this.comboBoxSearchPosition.ValueMember = "Value";
            //worker = new BackgroundWorker();
            //worker.DoWork += Worker_DoWork;
            //worker.RunWorkerAsync();
        }

        //private void Worker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    //while (true)
        //    //{
        //    //    var resultText = "waiting...";
        //    //    using (var httpClient = new HttpClient())
        //    //    {
        //    //        var response = httpClient.GetAsync(ApiUri + "values").Result;
        //    //        resultText = response.Content.ReadAsStringAsync().Result;
        //    //        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        //    //        {
        //    //            resultText = "Error";
        //    //        }
        //    //    }
        //    //    //this.textBoxResult.BeginInvoke(new Action(() => this.textBoxResult.Text = resultText));
        //    //    Thread.Sleep(100);
        //    //}
        //}

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadRules();
        }

        private void ReloadRules()
        {
            this.fastObjectListViewRules.OverlayText = this._loadingOverlay;
            this.fastObjectListViewRules.EmptyListMsg = null;
            this.fastObjectListViewRules.ClearObjects();
            var rulesResult = YtManagementClient.GetRules();
            if (rulesResult.Status != ActionStatus.Success)
            {
                this.fastObjectListViewRules.EmptyListMsg = rulesResult.Message;
                this.fastObjectListViewRules.OverlayText = null;
                return;
            }
            this.fastObjectListViewRules.EmptyListMsg = "No rules available";
            this.fastObjectListViewRules.OverlayText = null;
            this.fastObjectListViewRules.SetObjects(rulesResult.Data);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var rule = new ManagementRule
            {
                Regex = this.checkBoxRegex.Checked,
                RuleString = this.textBoxRule.Text,
                Target = this.textBoxTarget.Text,
                IgnoreVideo = this.checkBoxIgnore.Checked,
                Priority = (int)this.numericUpDownPriority.Value,
                SearchPosition = (SearchPosition)this.comboBoxSearchPosition.SelectedValue
            };
            var result = YtManagementClient.AddRule(rule);
            if(result.Status != ActionStatus.Success)
            {
                this.ShowTooltip(result.Message, ToolTipIcon.Error);
                return;
            }
            this.ShowTooltip("Success", ToolTipIcon.Info);
            ReloadRules();
        }

        

        private void toolStripComboBoxApi_SelectedIndexChanged(object sender, EventArgs e)
        {
            YtManagementClient.SetApiUri((string)this.toolStripComboBoxApi.SelectedItem);
        }

        private void fastObjectListViewRules_SelectionChanged(object sender, EventArgs e)
        {
            this.textBoxTarget.Clear();
            this.textBoxRule.Clear();
            this.checkBoxRegex.Checked = false;
            this.checkBoxIgnore.Checked = false;
            this.numericUpDownPriority.Value = 0;

            if (!(this.fastObjectListViewRules.SelectedObject is ManagementRule item))
            {
                return;
            }
            this.textBoxRule.Text = item.RuleString;
            this.textBoxTarget.Text = item.Target;
            this.checkBoxRegex.Checked = item.Regex;
            this.checkBoxIgnore.Checked = item.IgnoreVideo;
            this.numericUpDownPriority.Value = item.Priority;
            this.comboBoxSearchPosition.SelectedValue = (int)item.SearchPosition;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (!(this.fastObjectListViewRules.SelectedObject is ManagementRule item))
            {
                return;
            }
            item.RuleString =  this.textBoxRule.Text;
            item.Target = this.textBoxTarget.Text;
            item.Regex = this.checkBoxRegex.Checked;
            item.IgnoreVideo = this.checkBoxIgnore.Checked;
            item.Priority = (int)this.numericUpDownPriority.Value;
            item.SearchPosition = (SearchPosition)this.comboBoxSearchPosition.SelectedValue;
            var result = YtManagementClient.UpdateRule(item);
            if (result.Status != ActionStatus.Success)
            {
                this.ShowTooltip(result.Message, ToolTipIcon.Error);
                return;
            }
            this.ShowTooltip("Success", ToolTipIcon.Info);
            ReloadRules();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(this.fastObjectListViewRules.SelectedObject is ManagementRule item))
            {
                return;
            }
            var result = YtManagementClient.DeleteRule(item.Id);
            if (result.Status != ActionStatus.Success)
            {
                this.ShowTooltip(result.Message, ToolTipIcon.Error);
                return;
            }
            this.ShowTooltip("Success", ToolTipIcon.Info);
            ReloadRules();
        }
    }
}