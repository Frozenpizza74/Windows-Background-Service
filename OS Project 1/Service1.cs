using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Timers;

namespace MyNewService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // start the service
            timer = new Timer();
            timer.Interval = 43200000; //12 Hours=43200000 Milliseconds
            timer.Elapsed += new ElapsedEventHandler(workload);
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
            timer.Dispose();
        }
        private void workload(object sender, ElapsedEventArgs e)
        {
            // get the information from PerfomanceCounter class
            var CpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            var MemoryCounter = new PerformanceCounter("Memory", "Available MBytes");
            var DiskCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
            var NetworkCounter = new PerformanceCounter("Network Interface", "Bytes Total/sec");

            var CpuUsage = CpuCounter.NextValue();
            var MemoryUsage = MemoryCounter.NextValue();
            var DiskUsage = DiskCounter.NextValue();
            var NetworkUsage = NetworkCounter.NextValue();

            // extract the file
            using (var writer = File.AppendText("workload.txt"))
            {
                writer.WriteLine($"CPU Usage: {CpuUsage}%");
                writer.WriteLine($"Memory Usage: {MemoryUsage} MB");
                writer.WriteLine($"Disk Usage: {DiskUsage}%");
                writer.WriteLine($"Network Usage: {NetworkUsage} bytes/sec");
                writer.WriteLine("----------------------------------------");
                writer.Flush();
                writer.Close();
            }

            //send the mail
            using (var client = new SmtpClient())
            {
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                var sender_mail = "hnood882004@ejust.edu.eg";
                var sender_password = "15908236Hn";
                var reciver_mail = "Moustafa.rezq@ejust.ed.eg";
                client.Credentials = new NetworkCredential(sender_mail, sender_password);

                var message = new MailMessage(sender_mail, reciver_mail)
                {
                    Subject = "Operating System Workload",
                    Body = "In this file you will find your Operating System Workload",
                    Attachments =
                   {
                       new Attachment("workload.txt")
                   }
                };
                client.Send(message);
            }
        }
    }
}

