﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NYear.ODA.DevTool
{
    public partial class ToolMain : MdiParentForm
    {
        public ToolMain()
        {
            InitializeComponent();

            mncExcuteSQL.Enabled = false;
            tlbrExecuteSQL.Enabled = false;
            mncDBCopy.Enabled = false;
            tlbrDBCopy.Enabled = false;
            mncORMCodeCreate.Enabled = false;
            mncDBConnectTest.Enabled = false;
            tlbrDBConnectTest.Enabled = false;
            tlbrORMCodeCreate.Enabled = false;
            mncORMCodeSave.Enabled = false;
            tlbrORMCodeSave.Enabled = false;

            mncQueryWm.Enabled = false;
            mncDBCopyWm.Enabled = false;
            mncORMCreateWm.Enabled = false;
            CurrentDatabase.DBConnected += CurrentDatabase_DBConnected;
        }


        private void ShowNewForm_DBCopy(object sender, EventArgs e)
        {
            DBCopy childForm = new DBCopy();
            childForm.MdiParent = this;
            childForm.Text = "数据库复制";
            childForm.Show();
        }
        private void ShowNewForm_ORMCode(object sender, EventArgs e)
        {
            ORMCode childForm = new ORMCode();
            childForm.MdiParent = this;
            childForm.Text = "ORM代码生成";
            childForm.Show();
        }
        private void ShowNewForm_SQLDevlop(object sender, EventArgs e)
        {
            SQLDevlop childForm = new SQLDevlop();
            childForm.MdiParent = this;
            childForm.Text = "SQL语脚本执行";
            childForm.Show();
        }


        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void ToolMain_Shown(object sender, EventArgs e)
        {
            SessionStart start = new SessionStart();
            start.ShowInTaskbar = false;
            start.ShowDialog();
        }

        private void mncNewConnect_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length > 0)
                if (MessageBox.Show("是否关闭当前所有子窗口", "关闭窗口", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    foreach (Form F in this.MdiChildren)
                        F.Close();
            SessionStart start = new SessionStart();
            start.ShowInTaskbar = false;
            start.ShowDialog();
        }

        void CurrentDatabase_DBConnected(object sender, EventArgs e)
        {
            if (CurrentDatabase.DataSource != null)
            {
                mncQueryWm.Enabled = true;
                mncDBCopyWm.Enabled = true;
                mncORMCreateWm.Enabled = true;
                ShowNewForm_SQLDevlop(this, EventArgs.Empty);
                if (this.MdiChildren.Length > 0)
                    this.MdiChildren[0].WindowState = FormWindowState.Maximized;

                this.Text = CurrentDatabase.DataSource.DBAType.ToString();
            }
        }

        #region 事件控制
        EventHandler _ExecuteSQL;
        public override event EventHandler ExecuteSQL
        {
            add
            {
                _ExecuteSQL += value;
                mncExcuteSQL.Click += value;
                tlbrExecuteSQL.Click += value;
                mncExcuteSQL.Enabled = true;
                tlbrExecuteSQL.Enabled = true;
            }
            remove
            {
                _ExecuteSQL -= value;
                mncExcuteSQL.Click -= value;
                tlbrExecuteSQL.Click -= value;
                if (_ExecuteSQL == null)
                {
                    mncExcuteSQL.Enabled = false;
                    tlbrExecuteSQL.Enabled = false;
                }
            }
        }
        EventHandler _DBCopy;
        public override event EventHandler DBCopy
        {
            add
            {
                _DBCopy += value;
                mncDBCopy.Click += value;
                tlbrDBCopy.Click += value;
                mncDBCopy.Enabled = true;
                tlbrDBCopy.Enabled = true;
            }
            remove
            {
                _DBCopy -= value;
                mncDBCopy.Click -= value;
                tlbrDBCopy.Click -= value;
                if (_DBCopy == null)
                {
                    mncDBCopy.Enabled = false;
                    tlbrDBCopy.Enabled = false;
                }
            }
        }
        EventHandler _DBConnectTest;
        public override event EventHandler DBConnectTest
        {
            add
            {
                _DBConnectTest += value;
                mncDBConnectTest.Click += value;
                tlbrDBConnectTest.Click += value;
                mncDBConnectTest.Enabled = true;
                tlbrDBConnectTest.Enabled = true;
            }
            remove
            {
                _DBConnectTest -= value;
                mncDBConnectTest.Click -= value;
                tlbrDBConnectTest.Click -= value;

                if (_DBConnectTest == null)
                {
                    mncDBConnectTest.Enabled = false;
                    tlbrDBConnectTest.Enabled = false;
                }

            }
        }
        EventHandler _ORMCodeCreate;
        public override event EventHandler ORMCodeCreate
        {
            add
            {
                _ORMCodeCreate += value;
                mncORMCodeCreate.Click += value;
                tlbrORMCodeCreate.Click += value;

                mncORMCodeCreate.Enabled = true;
                tlbrORMCodeCreate.Enabled = true;
            }
            remove
            {
                _ORMCodeCreate -= value;
                mncORMCodeCreate.Click -= value;
                tlbrORMCodeCreate.Click -= value;
                if (_ORMCodeCreate == null)
                {
                    mncORMCodeCreate.Enabled = false;
                    tlbrORMCodeCreate.Enabled = false;
                }
            }
        }

        EventHandler _ORMCodeSave;
        public override event EventHandler ORMCodeSave
        {
            add
            {
                _ORMCodeSave += value;
                mncORMCodeSave.Click += value;
                tlbrORMCodeSave.Click += value;
                mncORMCodeSave.Enabled = true;
                tlbrORMCodeSave.Enabled = true;
            }
            remove
            {
                _ORMCodeSave -= value;
                mncORMCodeSave.Click -= value;
                tlbrORMCodeSave.Click -= value;
                if (_ORMCodeSave == null)
                {
                    mncORMCodeSave.Enabled = false;
                    tlbrORMCodeSave.Enabled = false;
                }
            }
        }
        #endregion




    }
}