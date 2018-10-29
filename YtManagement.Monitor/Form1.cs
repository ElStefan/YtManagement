using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using YtManagement.Common;
using YtManagement.Common.Model;
using YtManagement.Monitor.Extension;

namespace YtManagement.Monitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            this.toolStripComboBoxApi.Items.Add("http://diskstation.lampertnet:50002/api/");
            this.toolStripComboBoxApi.Items.Add("http://localhost:50002/api/");
            this.toolStripComboBoxApi.SelectedIndex = 0;
            this.comboBoxSearchPosition.DataSource = Enum.GetValues(typeof(SearchPosition)).Cast<SearchPosition>().Select(o => new { Name = o.ToString(), Value = (int)o }).ToList();
            this.comboBoxSearchPosition.DisplayMember = "Name";
            this.comboBoxSearchPosition.ValueMember = "Value";
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSelectedTab();
        }

        private void LoadSelectedTab()
        {
            switch (this.tabControlMain.SelectedTab.Name)
            {
                case nameof(this.tabPageRules):

                    ReloadRules();
                    break;
                case nameof(this.tabPageProcessedVideos):
                    ReloadProcessedVideos();
                    break;
            }
        }

        private void ReloadRules()
        {
            this.fastObjectListViewRules.LoadFrom(YtManagementClient.GetRules);
        }

        private void ReloadProcessedVideos()
        {
            this.fastObjectListViewProcessedVideos.LoadFrom(YtManagementClient.GetProcessedVideos, o => o.OrderByDescending(p => p.PublishedAt));
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

        private void createRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(this.fastObjectListViewProcessedVideos.SelectedObject is YtVideo item))
            {
                return;
            }
            this.tabControlMain.SelectedTab = this.tabPageRules;

            this.textBoxRule.Text = item.Title;
            this.textBoxTarget.Text = "unknown";
            this.checkBoxRegex.Checked = false;
            this.checkBoxIgnore.Checked = false;
            this.numericUpDownPriority.Value = 4;
            this.comboBoxSearchPosition.SelectedValue = SearchPosition.VideoTitle;
        }
    }
}