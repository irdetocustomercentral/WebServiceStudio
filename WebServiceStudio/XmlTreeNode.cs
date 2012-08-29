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