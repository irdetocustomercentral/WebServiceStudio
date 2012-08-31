	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
using System.Windows.Forms;

namespace IBS.Utilities.ASMWSTester
{
    public class XmlTreeNode : TreeNode
    {
        public XmlTreeNode(string text, int startPos) : base(text)
        {
            this.startPos = startPos;
        }


        public int EndPosition
        {
            get { return endPos; }
            set { endPos = value; }
        }

        public int StartPosition
        {
            get { return startPos; }
            set { startPos = value; }
        }


        private int endPos;
        private int startPos;
    }
}
