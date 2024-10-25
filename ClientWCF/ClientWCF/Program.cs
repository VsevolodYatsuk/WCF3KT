using System;
using System.ServiceModel;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace ClientWCF
{
    [ServiceContract]
    public interface IMessageService
    {
        [OperationContract]
        string GetHelloWorld();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var clientSection = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;
                if (clientSection != null)
                {
                    foreach (ChannelEndpointElement endpoint in clientSection.Endpoints)
                    {
                        Console.WriteLine($"Available Endpoint: {endpoint.Name} with Contract: {endpoint.Contract}");
                    }
                }

                ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("NetTcpEndpoint");
                IMessageService client = factory.CreateChannel();

                string response = client.GetHelloWorld();
                Console.WriteLine("Response from service: " + response);

                factory.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
