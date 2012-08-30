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
	




using System;
using System.IO;
using System.Text;
using System.Xml;

namespace IBS.Utilities.ASMWSTester
{
    internal class MessageTracer
    {
        private static void DumpBytes(Stream stream, int length, StringBuilder sb)
        {
            byte[] buffer1 = new byte[0x400];
            int num1 = 0x10;
            char[] chArray1 = new char[0x30];
            char[] chArray2 = new char[0x10];
            int num2 = 0;
            int num3 = 0;
            do
            {
                num3 = stream.Read(buffer1, 0, min(length - num2, buffer1.Length));
                int num4 = 0;
                while (num4 < num3)
                {
                    if ((num2%num1) == 0)
                    {
                        for (int num5 = 0; num5 < num1; num5++)
                        {
                            char ch1;
                            chArray2[num5] = ch1 = ' ';
                            chArray1[(3*num5) + 2] = ch1 = ch1;
                            chArray1[3*num5] = chArray1[(3*num5) + 1] = ch1;
                        }
                    }
                    byte num6 = buffer1[num4];
                    int num7 = num2%num1;
                    chArray1[3*num7] = HexChar((num6 >> 4) & 15);
                    chArray1[(3*num7) + 1] = HexChar(num6 & 15);
                    chArray2[num7] = char.IsControl((char) num6) ? '.' : ((char) num6);
                    if (((num2%num1) == (num1 - 1)) || (num4 == (num3 - 1)))
                    {
                        sb.Append(
                            string.Format("{0,8}: {1} {2}\n", (num2/0x10)*0x10, new string(chArray1),
                                          new string(chArray2)));
                    }
                    num4++;
                    num2++;
                }
            } while ((num2 < length) && (num3 > 0));
        }

        private static string GetCharset(string contentType)
        {
            int num1 = contentType.IndexOf(';');
            if (num1 >= 0)
            {
                string text1 = contentType.Substring(num1 + 1).TrimStart(null);
                if (string.Compare(text1, 0, "charset", 0, "charset".Length, true) == 0)
                {
                    string text2 = text1.Substring("charset".Length);
                    int num2 = text2.IndexOf('=');
                    if (num2 >= 0)
                    {
                        return text2.Substring(num2 + 1).Trim(new char[] {' ', '\'', '"', '\t'});
                    }
                }
            }
            return string.Empty;
        }

        private static Encoding GetEncoding(string contentType)
        {
            string text1 = GetCharset(contentType);
            Encoding encoding1 = null;
            try
            {
                if (text1.Length > 0)
                {
                    encoding1 = Encoding.GetEncoding(text1);
                }
            }
            catch (Exception)
            {
            }
            return ((encoding1 == null) ? new ASCIIEncoding() : encoding1);
        }

        private static char HexChar(int nibble)
        {
            if (nibble < 10)
            {
                return (char) ((ushort) (nibble + 0x30));
            }
            return (char) ((ushort) (nibble + 0x37));
        }

        private static int min(int a, int b)
        {
            return ((a < b) ? a : b);
        }

        internal static string ReadMessage(Stream from, string contentType)
        {
            return ReadMessage(from, (int) from.Length, contentType);
        }

        internal static string ReadMessage(Stream from, int len, string contentType)
        {
            if ((contentType.StartsWith("text/xml") || contentType.StartsWith("application/soap+xml")) ||
                (contentType == "http://schemas.xmlsoap.org/soap/envelope/"))
            {
                byte[] buffer1 = ReadStream(from, len);
                XmlDocument document1 = new XmlDocument();
                document1.InnerXml = GetEncoding(contentType).GetString(buffer1);
                StringWriter writer1 = new StringWriter();
                XmlTextWriter writer2 = new XmlTextWriter(writer1);
                writer2.Formatting = Formatting.Indented;
                document1.Save(writer2);
                return writer1.ToString();
            }
            if (contentType.StartsWith("text"))
            {
                byte[] buffer2 = ReadStream(from, len);
                from.Read(buffer2, 0, len);
                return GetEncoding(contentType).GetString(buffer2, 0, len);
            }
            StringBuilder builder1 = new StringBuilder();
            DumpBytes(from, len, builder1);
            return builder1.ToString();
        }

        private static byte[] ReadStream(Stream stream, int len)
        {
            if (len >= 0)
            {
                byte[] buffer1 = new byte[len];
                stream.Read(buffer1, 0, len);
                return buffer1;
            }
            Chunk chunk1 = null;
            Chunk chunk2 = null;
            int num1 = 0;
            while (true)
            {
                Chunk chunk3 = new Chunk();
                if (chunk1 == null)
                {
                    chunk1 = chunk3;
                }
                chunk3.Buffer = new byte[0x400];
                chunk3.Size = stream.Read(chunk3.Buffer, 0, chunk3.Buffer.Length);
                chunk3.Next = null;
                if (chunk2 != null)
                {
                    chunk2.Next = chunk3;
                }
                num1 += chunk3.Size;
                if (chunk3.Size < chunk3.Buffer.Length)
                {
                    break;
                }
                chunk2 = chunk3;
            }
            byte[] buffer2 = new byte[num1];
            while (chunk1 != null)
            {
                Array.Copy(chunk1.Buffer, 0, buffer2, 0, chunk1.Size);
                chunk1 = chunk1.Next;
            }
            return buffer2;
        }

        internal static int WriteMessage(Stream stream, string contentType, string str)
        {
            byte[] buffer1 = new UTF8Encoding(true).GetBytes(str);
            stream.Write(buffer1, 0, buffer1.Length);
            return buffer1.Length;
        }


        public const string ApplicationXml = "application/soap+xml";
        public const string SoapEnvUri = "http://schemas.xmlsoap.org/soap/envelope/";
        public const string TextHtml = "text/html";
        public const string TextPlain = "text/plain";
        public const string TextXml = "text/xml";


        private class Chunk
        {
            public byte[] Buffer;
            public Chunk Next;
            public int Size;
        }
    }
}
