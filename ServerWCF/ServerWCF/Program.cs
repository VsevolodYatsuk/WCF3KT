using System;
using System.ServiceModel;

namespace ServerWCF
{
    [ServiceContract]
    public interface IMessageService
    {
        [OperationContract]
        string GetHelloWorld();
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MessageService : IMessageService
    {
        public string GetHelloWorld()
        {
            return "Hello, World!";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string address = "net.tcp://localhost:6565/MessageService";
            Uri[] baseAddresses = { new Uri(address) };

            MessageService service = new MessageService();
            ServiceHost host = new ServiceHost(service, baseAddresses);

            var binding = new NetTcpBinding(SecurityMode.None);

            host.AddServiceEndpoint(typeof(IMessageService), binding, "");

            host.Opened += (sender, e) => Console.WriteLine("Service is running...");
            host.Open();

            Console.WriteLine("Press Enter to stop the service...");
            Console.ReadLine();

            host.Close();
        }
    }
}
