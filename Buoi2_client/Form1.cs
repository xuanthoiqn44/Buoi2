using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;


namespace Buoi2_client
{
    public partial class FormClient : Form
    {
        byte[] data = new byte[1024];
        string input, stringData;
        IPEndPoint ipep;
        Socket server;
        int recv;
        string Data;
        string mess = "Client:";
        int flag = 0;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public FormClient()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void buttonexit_Click(object sender, EventArgs e)
        {
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            this.Close();
        }

        private void buttonsend_Click(object sender, EventArgs e)
        {
            input = textBox1.Text;
            data = new byte[1024];
            data = Encoding.ASCII.GetBytes(mess + input);
            listBoxclient.Items.Add(mess + input);
            server.Send(data);
            data = new byte[1024];
            server.Receive(data);
            textBox1.Text = "";
            listBoxclient.Items.Add(Encoding.ASCII.GetString(data));
            
        }

        private void buttonketnoi_Click(object sender, EventArgs e)
        {
            ipep = new IPEndPoint(IPAddress.Parse(textBox2.Text), 9999);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                listBoxclient.Items.Add("ket noi thanh cong!");
            }
            catch (SocketException ex)
            {
                listBoxclient.Items.Add("Unable to connect to servers.");
                listBoxclient.Items.Add(ex.ToString());
                return;
            }
        }
    }
}
