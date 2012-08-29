using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IBS.Utilities.ASMWSTester
{
    public partial class FindForm : Form
    {
        public Button BtnOk
        {
            get { return btnOk; }
            set { btnOk = value; }
        }

        public string FindValue
        {
            get { return findValue; }
            set { findValue = value; }
        }

        private string findValue;

        public FindForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            findValue = txtValue.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}