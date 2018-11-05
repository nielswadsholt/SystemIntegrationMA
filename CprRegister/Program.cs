using Data.Generators;
using Data.Models;
using Data.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;

namespace CprRegister
{
    public class Program
    {
        private static CDM personCDM;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("================================ CPR Register ================================");
                Console.WriteLine("Press");
                Console.WriteLine("\tS to send CPR message to the EUCCID register");
                Console.WriteLine("\tR to receive messages from the EUCCID register");
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

            var address1 = string.Format($"{address.Street} {address.HouseNo}");

            if (!" ".Contains(address.ApartmentNo))
            {
                address1 = string.Format($"{address1}, {address.ApartmentNo}");
            }

            var personCPR = new CPR()
            {
                CprNo = CprGenerator.Generate(gender),
                FirstName = NameGenerator.GenerateFirstName(gender),
                Surname = NameGenerator.GenerateSurname(),
                Address1 = address1,
                Address2 = address.Address2,
                PostalCode = address.PostalCode,
                City = address.City,
                MaritalStatus = "Ugift",
                Spouse = "",
                Children = new List<string>(),
                Mother = EuccidGenerator.Generate(new DateTime(1987, 11, 23)),
                Father = EuccidGenerator.Generate(new DateTime(1984, 3, 18)),
                DoctorCVR = "39227491",
                EuccidNo = EuccidGenerator.Generate(),
                Gender = gender,
            };

            Console.WriteLine("---------- CPR input: ----------");
            personCPR.PrintAll();

            // translate into CDM format
            var personCDM = CprTranslator.CprToCdm(personCPR);

            Console.WriteLine("\n---------- Translated into CDM: ----------");
            personCDM.PrintAll();

            // build message
            var message = new Message()
            {
                Body = personCDM,
                Label = personCDM.ToString()
            };

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

            // send
            cprToEuccidChannel.Send(message);

            Console.WriteLine("\n---------- Message sent. ----------\n\n");
        }

        private static void ReceiveMessages()
        {
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

            // receive and process message
            euccidToCprChannel.Formatter = new XmlMessageFormatter(new Type[] { typeof(CDM) });
            euccidToCprChannel.ReceiveCompleted += new ReceiveCompletedEventHandler(HandleReceivedMessage);
            euccidToCprChannel.BeginReceive();
        }

        private static void HandleReceivedMessage(object sender, ReceiveCompletedEventArgs e)
        {
            MessageQueue messageQueue = (MessageQueue)sender;

            // end async receive and save message
            var message = messageQueue.EndReceive(e.AsyncResult);

            Console.WriteLine("\n---------- CDM message received: ----------");
            personCDM = (CDM)message.Body;
            personCDM.PrintAll();
            
            // translate into CPR format
            var personCPR = CprTranslator.CdmToCpr(personCDM);

            Console.WriteLine("\n---------- Translated into CPR: ----------");
            personCPR.PrintAll();

            Console.WriteLine("\n---------- Serialized as XML: ----------");
            Console.WriteLine(personCPR.ToXml());
            Console.WriteLine("\n");

            // resume async receive
            messageQueue.BeginReceive();
        }
    }
}
