
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

using System;
using System.Windows.Forms;
using WebServiceStudio.BatchRun;
using WebServiceStudio.BatchRun.ConfigNode;

namespace WebServiceStudio
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
