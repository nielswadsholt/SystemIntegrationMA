using Data.Generators;
using Data.Models;
using Data.Translators;
using System;
using System.Linq;
using System.Messaging;

namespace EuccidRegister
{
    public class Program
    {
        private static CDM personCDM;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("============================== EU-CCID Register ==============================");
                Console.WriteLine("Press");
                Console.WriteLine("\tS to send EUCCID message to the CPR register");
                Console.WriteLine("\tR to receive messages from the CPR register");
                Console.WriteLine("\tQ to exit the program\n");

                char c = ' ';
                while (!"srq".Contains(c))
                {
                    Console.Write("\b \b");
                    c = char.ToLower(Console.ReadKey().KeyChar);
                }

                if (c == 's')
                {
                    Console.Write("\b \b");
                    SendMessage();
                }
                else if (c == 'r')
                {
                    Console.Write("\b \b");
                    ReceiveMessages();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

        private static void SendMessage()
        {
            var rnd = new Random();
            var gender = new string[] { "F", "M" }[rnd.Next(0, 2)];
            var address = AddressGenerator.Generate();

            var personEUCCID = new EUCCID()
            {
                ChristianName = NameGenerator.GenerateFirstName(gender),
                FamilyName = NameGenerator.GenerateSurname(),
                EuccidNo = EuccidGenerator.Generate(new DateTime(1987, 4, 23)),
                Gender = gender,
                StreetAndHouseNo = string.Format($"{address.Street} {address.HouseNo}"),
                ApartmentNo = address.ApartmentNo,
                PostalCode = address.PostalCode,
                City = address.City,
                CountryOfResidence = "Denmark"
            };

            Console.WriteLine("---------- EUCCID input: ----------");
            personEUCCID.PrintAll();

            // translate into CDM format
            var personCDM = EuccidTranslator.EuccidToCdm(personEUCCID);

            Console.WriteLine("\n---------- Translated into CDM: ----------");
            personCDM.PrintAll();

            // build message
            var message = new Message()
            {
                Body = personCDM,
                Label = personCDM.ToString()
            };

            // build channel
            var path = @".\Private$\EUCCID_to_CPR";

            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path);
            }

            var euccidToCprChannel = new MessageQueue(path)
            {
                Label = "EU-CCID to CPR"
            };

            // send
            euccidToCprChannel.Send(message);

            Console.WriteLine("\n---------- Message sent. ----------\n\n");
        }

        private static void ReceiveMessages()
        {
            // build channel
            var path = @".\Private$\CPR_to_EUCCID";

            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path);
            }

            var cprToEuccidChannel = new MessageQueue(path)
            {
                Label = "CPR to EU-CCID"
            };

            // receive and process message
            cprToEuccidChannel.Formatter = new XmlMessageFormatter(new Type[] { typeof(CDM) });
            cprToEuccidChannel.ReceiveCompleted += new ReceiveCompletedEventHandler(HandleReceivedMessage);
            cprToEuccidChannel.BeginReceive();
        }

        private static void HandleReceivedMessage(object sender, ReceiveCompletedEventArgs e)
        {
            MessageQueue messageQueue = (MessageQueue)sender;

            // end async receive and save message
            var message = messageQueue.EndReceive(e.AsyncResult);

            Console.WriteLine("\n---------- CDM message received: ----------");
            personCDM = (CDM)message.Body;
            personCDM.PrintAll();

            // translate into EU-CCID format
            var personEUCCID = EuccidTranslator.CdmToEuccid(personCDM);

            Console.WriteLine("\n---------- Translated into EUCCID: ----------");
            personEUCCID.PrintAll();

            Console.WriteLine("\n---------- Serialized as XML: ----------");
            Console.WriteLine(personEUCCID.ToXml());
            Console.WriteLine("\n");

            // resume async receive
            messageQueue.BeginReceive();
        }
    }
}
