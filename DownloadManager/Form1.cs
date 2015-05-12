using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DownloadManager
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
       


        private void downloadbtn_Click(object sender, EventArgs e)
        {
           
            WebClient myWebClient = new WebClient();

            //Declarations for string objects
            string downloadURL, path;
            //raw URL taken from user
           downloadURL =  this.downloadURL.Text;
            path = this.savePath.Text;
          
           
           Uri tmp = new Uri(downloadURL);
           string EndPathFileName = tmp.Segments.Last();
           path = path + @"\" + EndPathFileName;

           HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://archive.org/download/AbMeriNigahonMein_201408/Ab%20Meri%20Nigahon%20Mein.wmv");

           // Set some reasonable limits on resources used by this request
           request.MaximumAutomaticRedirections = 4;
           request.MaximumResponseHeadersLength = 4;
           // Set credentials to use for this request.
           request.Credentials = CredentialCache.DefaultCredentials;
           HttpWebResponse response = (HttpWebResponse)request.GetResponse();

           Console.WriteLine("Content length is {0}", response.ContentLength);
           Console.WriteLine("Content type is {0}", response.ContentType);

           // Get the stream associated with the response.
           Stream receiveStream = response.GetResponseStream();

           // Pipes the stream to a higher level stream reader with the required encoding format. 
           StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

           Console.WriteLine("Response stream received.");
           //  Console.WriteLine(readStream.ReadToEnd());
           response.Close();
           readStream.Close();
           
           //downloads file using async method
          
           myWebClient.DownloadFileAsync(tmp, path);

           downloadbtn.Text = "Download Started";
           downloadbtn.Enabled = false;

           myWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
           myWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);


     
            
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {


            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());

        }


        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            
            
            MessageBox.Show("Download Completed");

        }

       /* private void btnLoad_Click(object sender, EventArgs e)
        {

        }
        */
        

      

    
     
	}
}
































      


       
   

          