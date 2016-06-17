using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace Compile_Time_Reduce_Tool
{
    public partial class Form1 : Form
    {
        OpenFileDialog ofd = new OpenFileDialog();
       XmlTextReader xmlData = null;
        
        public Form1()
        {
            InitializeComponent();
            ofd.Filter = "Visual Studio Project|*.vcxproj";
            ofd.Title = "Visual Studio Project File Select";
            ofd.InitialDirectory = @"C:\";
        }
              

        private void FileBrowseButton_Click(object sender, EventArgs e)
        {
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                FilePathTextBox.Text = ofd.FileName;
                LoadXML(ofd.FileName);
                
            }
        }

        private bool LoadXML(string fileName)
        {
            xmlData = new XmlTextReader(fileName);

            while(xmlData.Read())
            {
                if(xmlData.NodeType == XmlNodeType.Element)
                {
                    switch(xmlData.Name)
                    {
                        case "ClCompile":
                        case "ClInclude":
                            string data = xmlData.GetAttribute("Include");
                            if(data != null)
                            {
                                ResultTextBox.Text += data + "\n";
                            }
                            break;
                    }
                }
            }
            return false;
        }


    }
}
