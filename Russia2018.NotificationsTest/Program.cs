
namespace Russia2018.NotificationsTest
{
    using Microsoft.Azure.NotificationHubs;
    using System;

    public class Program
    {
        private static NotificationHubClient hub;

        public static void Main(string[] args)
        {
            hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://russia2018.servicebus.windows.net/;" + 
                                                                         "SharedAccessKeyName=DefaultFullSharedAccessSignature;" +
                                                                         "SharedAccessKey=z/zPg9aiY3IMLfXpfKT86sNPIDSOdBHBK+UHT2rudZI=", "Russia2018");

            do
            {
                Console.WriteLine("Type a new message:");
                var message = Console.ReadLine();
                SendNotificationAsync(message);
                Console.WriteLine("The message was sent...");
            } while (true);
        }

        private static async void SendNotificationAsync(string message)
        {
           await hub.SendFcmNativeNotificationAsync("{ \"data\" : {\"Message\":\"" + message + "\"}}");
        }

    }

}
