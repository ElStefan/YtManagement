﻿namespace YtManagement.Monitor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fastObjectListViewRules = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumnId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnRegex = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnTarget = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnRule = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStripRules = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.checkBoxRegex = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRule = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTarget = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxApi = new System.Windows.Forms.ToolStripComboBox();
            this.olvColumnIgnore = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnLastMatch = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnPriority = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.checkBoxIgnore = new System.Windows.Forms.CheckBox();
            this.numericUpDownPriority = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.olvColumnSearchPos = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.comboBoxSearchPosition = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListViewRules)).BeginInit();
            this.contextMenuStripRules.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1134, 511);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1126, 485);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Rules";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fastObjectListViewRules);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.comboBoxSearchPosition);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.numericUpDownPriority);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxIgnore);
            this.splitContainer1.Panel2.Controls.Add(this.buttonUpdate);
            this.splitContainer1.Panel2.Controls.Add(this.buttonAdd);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxRegex);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxRule);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxTarget);
            this.splitContainer1.Size = new System.Drawing.Size(1120, 479);
            this.splitContainer1.SplitterDistance = 835;
            this.splitContainer1.TabIndex = 2;
            // 
            // fastObjectListViewRules
            // 
            this.fastObjectListViewRules.AllColumns.Add(this.olvColumnId);
            this.fastObjectListViewRules.AllColumns.Add(this.olvColumnRegex);
            this.fastObjectListViewRules.AllColumns.Add(this.olvColumnIgnore);
            this.fastObjectListViewRules.AllColumns.Add(this.olvColumnRule);
            this.fastObjectListViewRules.AllColumns.Add(this.olvColumnSearchPos);
            this.fastObjectListViewRules.AllColumns.Add(this.olvColumnTarget);
            this.fastObjectListViewRules.AllColumns.Add(this.olvColumnLastMatch);
            this.fastObjectListViewRules.AllColumns.Add(this.olvColumnPriority);
            this.fastObjectListViewRules.CellEditUseWholeCell = false;
            this.fastObjectListViewRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnId,
            this.olvColumnRegex,
            this.olvColumnIgnore,
            this.olvColumnRule,
            this.olvColumnSearchPos,
            this.olvColumnTarget,
            this.olvColumnLastMatch,
            this.olvColumnPriority});
            this.fastObjectListViewRules.ContextMenuStrip = this.contextMenuStripRules;
            this.fastObjectListViewRules.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastObjectListViewRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastObjectListViewRules.FullRowSelect = true;
            this.fastObjectListViewRules.GridLines = true;
            this.fastObjectListViewRules.HideSelection = false;
            this.fastObjectListViewRules.Location = new System.Drawing.Point(0, 0);
            this.fastObjectListViewRules.Name = "fastObjectListViewRules";
            this.fastObjectListViewRules.RowHeight = 20;
            this.fastObjectListViewRules.ShowGroups = false;
            this.fastObjectListViewRules.Size = new System.Drawing.Size(835, 479);
            this.fastObjectListViewRules.TabIndex = 0;
            this.fastObjectListViewRules.UseCompatibleStateImageBehavior = false;
            this.fastObjectListViewRules.UseFilterIndicator = true;
            this.fastObjectListViewRules.UseFiltering = true;
            this.fastObjectListViewRules.View = System.Windows.Forms.View.Details;
            this.fastObjectListViewRules.VirtualMode = true;
            this.fastObjectListViewRules.SelectionChanged += new System.EventHandler(this.fastObjectListViewRules_SelectionChanged);
            // 
            // olvColumnId
            // 
            this.olvColumnId.AspectName = "Id";
            this.olvColumnId.Text = "Id";
            // 
            // olvColumnRegex
            // 
            this.olvColumnRegex.AspectName = "Regex";
            this.olvColumnRegex.CheckBoxes = true;
            this.olvColumnRegex.Text = "Regex";
            this.olvColumnRegex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvColumnTarget
            // 
            this.olvColumnTarget.AspectName = "Target";
            this.olvColumnTarget.Text = "Target";
            this.olvColumnTarget.Width = 100;
            // 
            // olvColumnRule
            // 
            this.olvColumnRule.AspectName = "RuleString";
            this.olvColumnRule.Text = "Rule";
            this.olvColumnRule.Width = 300;
            // 
            // contextMenuStripRules
            // 
            this.contextMenuStripRules.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStripRules.Name = "contextMenuStripRules";
            this.contextMenuStripRules.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(163, 206);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate.TabIndex = 6;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(163, 177);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 5;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // checkBoxRegex
            // 
            this.checkBoxRegex.AutoSize = true;
            this.checkBoxRegex.Location = new System.Drawing.Point(82, 26);
            this.checkBoxRegex.Name = "checkBoxRegex";
            this.checkBoxRegex.Size = new System.Drawing.Size(57, 17);
            this.checkBoxRegex.TabIndex = 4;
            this.checkBoxRegex.Text = "Regex";
            this.checkBoxRegex.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Rule:";
            // 
            // textBoxRule
            // 
            this.textBoxRule.Location = new System.Drawing.Point(82, 72);
            this.textBoxRule.Name = "textBoxRule";
            this.textBoxRule.Size = new System.Drawing.Size(156, 20);
            this.textBoxRule.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Target:";
            // 
            // textBoxTarget
            // 
            this.textBoxTarget.Location = new System.Drawing.Point(82, 125);
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.Size = new System.Drawing.Size(156, 20);
            this.textBoxTarget.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.toolStripComboBoxApi});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1134, 27);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(58, 23);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // toolStripComboBoxApi
            // 
            this.toolStripComboBoxApi.Items.AddRange(new object[] {
            "http://localhost:50002/api",
            "http://diskstation.lampertnet:50002/api"});
            this.toolStripComboBoxApi.Name = "toolStripComboBoxApi";
            this.toolStripComboBoxApi.Size = new System.Drawing.Size(350, 23);
            this.toolStripComboBoxApi.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxApi_SelectedIndexChanged);
            // 
            // olvColumnIgnore
            // 
            this.olvColumnIgnore.AspectName = "IgnoreVideo";
            this.olvColumnIgnore.CheckBoxes = true;
            this.olvColumnIgnore.Text = "Ignore";
            this.olvColumnIgnore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvColumnLastMatch
            // 
            this.olvColumnLastMatch.AspectName = "LastMatch";
            this.olvColumnLastMatch.AspectToStringFormat = "{0:dd.MM.yyyy HH:mm:ss}";
            this.olvColumnLastMatch.Text = "LastMatch";
            this.olvColumnLastMatch.Width = 110;
            // 
            // olvColumnPriority
            // 
            this.olvColumnPriority.AspectName = "Priority";
            this.olvColumnPriority.Text = "Priority";
            // 
            // checkBoxIgnore
            // 
            this.checkBoxIgnore.AutoSize = true;
            this.checkBoxIgnore.Location = new System.Drawing.Point(82, 49);
            this.checkBoxIgnore.Name = "checkBoxIgnore";
            this.checkBoxIgnore.Size = new System.Drawing.Size(85, 17);
            this.checkBoxIgnore.TabIndex = 7;
            this.checkBoxIgnore.Text = "Ignore video";
            this.checkBoxIgnore.UseVisualStyleBackColor = true;
            // 
            // numericUpDownPriority
            // 
            this.numericUpDownPriority.Location = new System.Drawing.Point(82, 151);
            this.numericUpDownPriority.Name = "numericUpDownPriority";
            this.numericUpDownPriority.Size = new System.Drawing.Size(156, 20);
            this.numericUpDownPriority.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Priority:";
            // 
            // olvColumnSearchPos
            // 
            this.olvColumnSearchPos.AspectName = "SearchPosition";
            this.olvColumnSearchPos.Text = "Search in";
            // 
            // comboBoxSearchPosition
            // 
            this.comboBoxSearchPosition.FormattingEnabled = true;
            this.comboBoxSearchPosition.Location = new System.Drawing.Point(82, 98);
            this.comboBoxSearchPosition.Name = "comboBoxSearchPosition";
            this.comboBoxSearchPosition.Size = new System.Drawing.Size(156, 21);
            this.comboBoxSearchPosition.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Search in:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 538);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListViewRules)).EndInit();
            this.contextMenuStripRules.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripRules;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.CheckBox checkBoxRegex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTarget;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxApi;
        private BrightIdeasSoftware.FastObjectListView fastObjectListViewRules;
        private BrightIdeasSoftware.OLVColumn olvColumnId;
        private BrightIdeasSoftware.OLVColumn olvColumnRegex;
        private BrightIdeasSoftware.OLVColumn olvColumnTarget;
        private BrightIdeasSoftware.OLVColumn olvColumnRule;
        private BrightIdeasSoftware.OLVColumn olvColumnIgnore;
        private BrightIdeasSoftware.OLVColumn olvColumnLastMatch;
        private BrightIdeasSoftware.OLVColumn olvColumnPriority;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownPriority;
        private System.Windows.Forms.CheckBox checkBoxIgnore;
        private BrightIdeasSoftware.OLVColumn olvColumnSearchPos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxSearchPosition;
    }
}

