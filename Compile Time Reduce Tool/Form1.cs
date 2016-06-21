using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        List<string> cppList = new List<string>();
        
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
                if(0 != cppList.Count)
                {
                    BuildCppFile(ofd.FileName);
                    /*ofd.file*/
                }
                else
                {
                    ResultTextBox.Text = "파일 읽기에 실패했습니다. 다시 시도하세요";
                }
            }
        }

        private bool LoadXML(string fileName)
        {
            ResultTextBox.Text += "----- 파일을 읽습니다. ----- \n";
            xmlData = new XmlTextReader(fileName);

            int index = 1;
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
                                ResultTextBox.Text += index++ +". " + data + "\n";
                                cppList.Add(data);
 
                            }
                            break;
                    }
                }
            }
            return true;
        }

        private void BuildCppFile(string fileName)
        {
            ProcessStartInfo cmd = new ProcessStartInfo();
            Process process = new Process();
            cmd.FileName = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe";
            cmd.WindowStyle = ProcessWindowStyle.Hidden;             // cmd창이 숨겨지도록 하기
            cmd.CreateNoWindow = false;                               // cmd창을 띄우지 안도록 하기

            cmd.UseShellExecute = false;
            cmd.RedirectStandardOutput = true;           // cmd창에서 데이터를 가져오기
            cmd.RedirectStandardInput = true;            // cmd창으로 데이터 보내기
            cmd.RedirectStandardError = true;            // cmd창에서 오류 내용 가져오기

            process.EnableRaisingEvents = false;
            process.StartInfo = cmd;
            process.Start();
            process.StandardInput.Write(@"msbuild /?" + Environment.NewLine); // 명령어를 보낼때는 꼭 마무리를 해줘야 한다. 그래서 마지막에 NewLine가 필요하다
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();
            StringBuilder sb = new StringBuilder();
            sb.Append("[Result Info]" + DateTime.Now + "\r\n");
            sb.Append(result);
            sb.Append("\r\n");

            ResultTextBox.Text = sb.ToString();
            process.WaitForExit();
            process.Close();
        }
    }
}
