<<<<<<< HEAD
	//WebServiceStudio Application to test WCF web services.

	//Copyright (C) 2012  Irdeto Customer Care And Billing

	//This program is free software: you can redistribute it and/or modify
	//it under the terms of the GNU General Public License as published by
	//the Free Software Foundation, either version 3 of the License, or
	//(at your option) any later version.

	//This program is distributed in the hope that it will be useful,
	//but WITHOUT ANY WARRANTY; without even the implied warranty of
	//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	//GNU General Public License for more details.

	//You should have received a copy of the GNU General Public License
	//along with this program.  If not, see http://www.gnu.org/licenses/

	//Web service Studio has been modifided to understand more complex input types
	//such as Iredeto's Customer Care input type of Base Query Request. 
	




=======
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
>>>>>>> SandBox
using System;
using System.Windows.Forms;
using IBS.Utilities.ASMWSTester.BatchRun;
using IBS.Utilities.ASMWSTester.BatchRun.ConfigNode;

namespace IBS.Utilities.ASMWSTester
{
    public partial class ValueFieldsForm : Form
    {
        public ValueFieldsForm()
        {
            InitializeComponent();
        }

        private void ValueFieldsForm_Load(object sender, EventArgs e)
        {
            BindList();
        }

        private void BindList()
        {
            listBox1.Items.Clear();

            if (BatchRunCongifFileHelper.testConfigNode != null)
            {
                foreach (VauleFieldConfigNode vauleField in BatchRunCongifFileHelper.testConfigNode.ValueFields)
                {
                    listBox1.Items.Add(vauleField.Name);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            VauleFieldConfigNode vauleFieldConfigNode = new VauleFieldConfigNode();
            vauleFieldConfigNode.Name = textBox1.Text;

            BatchRunCongifFileHelper.testConfigNode.ValueFields.Add(vauleFieldConfigNode);

            BindList();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                selectVauleField = listBox1.SelectedItem.ToString();
                Close();
            }
        }

        public string SelectVauleField
        {
            get { return selectVauleField; }
            set { selectVauleField = value; }
        }

        private string selectVauleField;
    }
}
