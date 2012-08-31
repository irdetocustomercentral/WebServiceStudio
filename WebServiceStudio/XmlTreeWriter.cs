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
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace IBS.Utilities.ASMWSTester
{
    public class XmlTreeWriter : XmlWriter
    {
        public XmlTreeWriter()
        {
            Init();
        }

        private void Ascend()
        {
            Update();
            current = current.Parent;
        }

        public override void Close()
        {
        }

        private void Descend()
        {
            Update();
            TreeNode node1 = new XmlTreeNode("", linePositions[reader.LineNumber - 1]);
            node1.Tag = current.Tag;
            current.Nodes.Add(node1);
            current = node1;
        }

        public void FillTree(string xml, TreeNode root)
        {
            current = root;
            reader = new XmlTextReader(new StringReader(xml));
            initPositions(xml);
            WriteNode(reader, true);
        }

        public override void Flush()
        {
        }

        private int GetPosition(int lineNum, int linePos)
        {
            return ((linePositions[lineNum - 1] + linePos) - 1);
        }

        private void Init()
        {
            current = null;
            state = WriteState.Start;
            attrNames = new StringCollection();
            attrValues = new StringCollection();
        }

        private void initPositions(string text)
        {
            ArrayList list1 = new ArrayList();
            char ch1 = ' ';
            int num1 = 0;
            list1.Add(0);
            for (int num2 = 0; num2 < text.Length; num2++)
            {
                char ch2 = text[num2];
                switch (ch1)
                {
                    case '\n':
                    case '\r':
                        list1.Add(num2 - num1);
                        break;
                }
                if (((ch2 == '\r') && (ch1 == '\n')) || ((ch2 == '\n') && (ch1 == '\r')))
                {
                    ch1 = ' ';
                    num1++;
                }
                else
                {
                    ch1 = ch2;
                }
            }
            list1.Add(text.Length);
            linePositions = list1.ToArray(typeof (int)) as int[];
        }

        public override string LookupPrefix(string ns)
        {
            throw new NotImplementedException();
        }

        private void Update()
        {
            XmlTreeNode node1 = current as XmlTreeNode;
            if (node1 != null)
            {
                node1.EndPosition = linePositions[reader.LineNumber];
            }
            if (name != null)
            {
                StringBuilder builder1 = new StringBuilder();
                for (int num1 = 0; num1 < attrNames.Count; num1++)
                {
                    builder1.Append(" " + attrNames[num1] + " = " + attrValues[num1]);
                }
                current.Text = name + builder1.ToString();
                attrNames.Clear();
                attrValues.Clear();
                name = null;
            }
        }

        public override void WriteBase64(byte[] buffer, int index, int count)
        {
            throw new NotImplementedException();
        }

        public override void WriteBinHex(byte[] buffer, int index, int count)
        {
            throw new NotImplementedException();
        }

        public override void WriteCData(string text)
        {
        }

        public override void WriteCharEntity(char ch)
        {
        }

        public override void WriteChars(char[] buffer, int index, int count)
        {
            WriteRaw(new string(buffer, index, count));
        }

        public override void WriteComment(string text)
        {
        }

        public override void WriteDocType(string name, string pubid, string sysid, string subset)
        {
        }

        public override void WriteEndAttribute()
        {
            state = WriteState.Element;
        }

        public override void WriteEndDocument()
        {
        }

        public override void WriteEndElement()
        {
            Ascend();
            state = WriteState.Element;
        }

        public override void WriteEntityRef(string name)
        {
        }

        public override void WriteFullEndElement()
        {
            Ascend();
            state = WriteState.Element;
        }

        public override void WriteName(string name)
        {
            throw new NotImplementedException();
        }

        public override void WriteNmToken(string name)
        {
            throw new NotImplementedException();
        }

        public override void WriteProcessingInstruction(string name, string text)
        {
        }

        public override void WriteQualifiedName(string localName, string ns)
        {
            WriteRaw(localName);
        }

        public override void WriteRaw(string data)
        {
            if (state == WriteState.Attribute)
            {
                attrValues.Add(data);
            }
            else
            {
                Descend();
                current.Text = current.Text + data;
            }
        }

        public override void WriteRaw(char[] buffer, int index, int count)
        {
            WriteRaw(new string(buffer, index, count));
        }

        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            attrNames.Add(localName);
            state = WriteState.Attribute;
        }

        public override void WriteStartDocument()
        {
        }

        public override void WriteStartDocument(bool standalone)
        {
        }

        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            Descend();
            name = ((prefix != null) && (prefix.Length > 0)) ? (prefix + ":" + localName) : localName;
            state = WriteState.Element;
        }

        public override void WriteString(string text)
        {
            WriteRaw(text);
        }

        public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        {
            throw new NotImplementedException();
        }

        public override void WriteWhitespace(string ws)
        {
        }


        public override WriteState WriteState
        {
            get { return state; }
        }

        public override string XmlLang
        {
            get { throw new NotImplementedException(); }
        }

        public override XmlSpace XmlSpace
        {
            get { throw new NotImplementedException(); }
        }


        private StringCollection attrNames;
        private StringCollection attrValues;
        private TreeNode current;
        private int[] linePositions;
        private string name;
        private XmlTextReader reader;
        private WriteState state;
    }
}
