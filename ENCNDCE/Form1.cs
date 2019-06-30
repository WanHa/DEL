using DEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENCNDCE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Log log = new Log();
            //升成路径
            string DebugBase = Environment.CurrentDirectory;
            string GenBase = DebugBase.Replace(@"bin\Debug", @"Info\");
            string LogBase = DebugBase.Replace(@"bin\Debug", @"Log\");
            try
            {
                string usr = this.textBox1.Text.Trim();
                string pwd = this.textBox2.Text.Trim();

                if (String.IsNullOrEmpty(usr))
                {
                    MessageBox.Show("请输入用户名");
                    return;
                }

                if (String.IsNullOrEmpty(pwd))
                {
                    MessageBox.Show("请输入密码");
                    return;
                }
                
                FileNPath fnp = new FileNPath();
                fnp.CreatePath(GenBase);
                fnp.CreatePath(LogBase);

                //升成用户信息xml文件
                string UserFilePath = GenBase + usr + ".xml";
                bool userExisted = fnp.FileExisted(UserFilePath);
                XML xml = new XML();
                DecryptNEncrypt dne = new DecryptNEncrypt();
                if (userExisted)
                {
                    xml.UpdateXml("PWD", dne.Encryption(pwd), UserFilePath);
                    //MessageBox.Show(dne.Decrypt(dne.Encryption(pwd)));
                    //记录log
                    log.WriteLog(LogBase, "用户" + usr + "更新密码");
                }
                else
                {
                    List<string> Auserinfo = new List<string>();
                    UserInfo ui = new UserInfo();
                    ui.USR = dne.Encryption(usr);
                    ui.PWD = dne.Encryption(pwd);
                    List<UserInfo> lui = new List<UserInfo>();
                    lui.Add(ui);
                    xml.ObjListToXml<UserInfo>(lui, UserFilePath);
                    log.WriteLog(LogBase, "创建新用户:" + usr);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(LogBase, "发生异常:"+ ex.ToString());
                //throw;
            }

            

        }
    }
}
