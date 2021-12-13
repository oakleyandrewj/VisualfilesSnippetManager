using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace VBLSnippetManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadSnippetsFromXmlFile();
        }

        private void btnCopyClipboard_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(txtEditor.Text);
        }

        private void LoadSnippetsFromXmlFile()
        {
            XmlDocument xml = new XmlDocument();
            var contents = "";
            using (StreamReader streamReader = new StreamReader("C:\\Users\\Oakley\\Source\\repos\\VBLSnippetManager\\VBLSnippetManager\\snippets.xml", Encoding.UTF8))
            {
                contents = streamReader.ReadToEnd();
            }
            xml.LoadXml(contents);

            XmlNodeList xnList = xml.SelectNodes("/snippets/snippet");
            foreach (XmlNode xn in xnList)
            {
                string title = xn["title"].InnerText;
                //string lastName = xn["description"].InnerText;
                //txtEditor.Text += firstName + lastName;
                listBoxSnippets.Items.Add(title);
            }

        }

        private void listBoxSnippets_SelectedIndexChanged(object sender, EventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show(listBoxSnippets.SelectedItem.ToString());
            string title = "";
            string description = "";
            string content = "";

            XmlDocument xml = new XmlDocument();
            var contents = "";
            using (StreamReader streamReader = new StreamReader("C:\\Users\\Oakley\\Source\\repos\\VBLSnippetManager\\VBLSnippetManager\\snippets.xml", Encoding.UTF8))
            {
                contents = streamReader.ReadToEnd();
            }
            xml.LoadXml(contents);

            XmlNodeList xnList = xml.SelectNodes("/snippets/snippet");
            foreach (XmlNode xn in xnList)
            {
                if (listBoxSnippets.SelectedItem.ToString() == xn["title"].InnerText)
                {
                    title = xn["title"].InnerText;
                    description = xn["description"].InnerText;
                    content = xn["content"].InnerText;

                }
                txtName.Text = title;
                txtDescription.Text = description;
                txtEditor.Text = content.Replace("*","&");
                
            }
        }
    }
}
