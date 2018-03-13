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

namespace Server
{
    public partial class FormServer : Form
    {
        IPEndPoint ipep;
        Socket newsock;
        Socket client;
        int recv;
        IPEndPoint clientep;
        byte[] data = new byte[1024];
        string welcome;
        string mess = "Server: ";
        string input;
        int flag = 1;
        string stringData;
        public FormServer()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Buoi2_client.FormClient FORM = new Buoi2_client.FormClient();
            int a = FORM.Flag;
            try
            {
                ipep = new IPEndPoint(IPAddress.Any, 9999);
                newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                newsock.Bind(ipep);
                newsock.Listen(10);
                listBoxserver.Items.Add("Waiting for a client...");
                client = newsock.Accept();
                clientep = (IPEndPoint)client.RemoteEndPoint;
                string kn = "Connected with '" + clientep.Address + "' at port '" + clientep.Port + "'";
                listBoxserver.Items.Add(kn);
                data = new byte[1024];
                client.Receive(data);
                listBoxserver.Items.Add(mess + Encoding.ASCII.GetString(data));
                
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex);
            }
            

        }

        private void buttonsend_Click(object sender, EventArgs e)
        {
            input = textBox1.Text;
            listBoxserver.Items.Add(mess + input);
            textBox1.Text = "";
            data = Encoding.ASCII.GetBytes(mess + input );
            client.Send(data, data.Length, SocketFlags.None);
            data = new byte[1024];
            client.Receive(data);
            listBoxserver.Items.Add(Encoding.ASCII.GetString(data));
        }

        private void buttonexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
