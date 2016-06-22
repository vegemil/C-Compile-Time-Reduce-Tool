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
        string projectFileName;
        string solutionFileName;
        
        public Form1()
        {
            InitializeComponent();
/*            ofd.Filter = "Visual Studio Project|*.vcxproj";*/
            ofd.Title = "Visual Studio Project File Select";
            ofd.Multiselect = true;
            ofd.InitialDirectory = @"C:\";
        }
              

        private void FileBrowseButton_Click(object sender, EventArgs e)
        {
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                string[] files = ofd.FileNames;
                for (int i = 0; i < files.Count(); ++i )
                {
                    if(true == files[i].Contains(".sln"))
                    {
                        solutionFileName = files[i];
                    }
                    else if (true == files[i].Contains(".vcxproj"))
                    {
                        projectFileName = files[i];
                    }
                }

                LoadXML(projectFileName);

                if(0 != cppList.Count)
                {
                    BuildCppFile(solutionFileName);
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
            cmd.FileName = @"cmd";
            cmd.Arguments = @"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\Tools\VsDevCmd.bat";
            cmd.WindowStyle = ProcessWindowStyle.Hidden;             // cmd창이 숨겨지도록 하기
            cmd.CreateNoWindow = false;                               // cmd창을 띄우지 안도록 하기

            cmd.UseShellExecute = false;
            cmd.RedirectStandardOutput = true;           // cmd창에서 데이터를 가져오기
            cmd.RedirectStandardInput = true;            // cmd창으로 데이터 보내기
            cmd.RedirectStandardError = true;            // cmd창에서 오류 내용 가져오기

            process.EnableRaisingEvents = false;
            process.StartInfo = cmd;
            process.Start();
            string str = @"msbuild " + fileName + @" /t:ClCompile /p:SelectedFiles=""" + cppList[0];
//             str = @"msbuild " + fileName + @" /t:ClCompile /p:Configulation=Debug /p:Platform=x64";
            process.StandardInput.WriteLine(@"cd c:/");
            process.StandardInput.Write(str + Environment.NewLine); // 명령어를 보낼때는 꼭 마무리를 해줘야 한다. 그래서 마지막에 NewLine가 필요하다
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
