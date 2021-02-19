using System;
using System.Collections.Generic;
using System.Threading;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {

            {
                TimerCallback timer = new TimerCallback(Client.updateBalance);
                Timer timerC = new Timer(timer, Client.castemer, 0, 1000);//Для таймера 
                while (true)
                {
                    Console.WriteLine(@"Choose Command:
                        1. Insert  ---> 1
                        2. Select  ---> 2
                        3. Update  ---> 3
                        4. Delete  ---> 4");
                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 1:
                            Thread insert = new Thread(new ThreadStart(Client.Insert));
                            insert.Start(); insert.Join();
                            break;
                        case 2:
                            Thread select = new Thread(new ThreadStart(Client.Select));
                            select.Start(); select.Join();
                            break;
                        case 3:
                            Thread update = new Thread(new ThreadStart(Client.UpdateById));
                            update.Start(); update.Join();
                            break;
                        case 4:
                            Thread delete = new Thread(new ThreadStart(Client.DeleteById));
                            delete.Start(); delete.Join();
                            break;
                        default:
                            Console.WriteLine("Incorrect Command");
                            break;
                    }
                }
            }
        }
        class Client // класс КЛИЕНТ 
        {
            public static List<Client> castemer = new List<Client>();
            public static List<Client> Checkcustomer = new List<Client>();

            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public decimal Balance { get; set; }
            public Client(int ID, string firstName, string lastName, decimal balance)
            {
                Id = ID;
                FirstName = firstName;
                LastName = lastName;
                Balance = balance;
            }
            public Client(string firstname, string lastname, decimal balance)
            {
                FirstName = firstname;
                LastName = lastname;
                Balance = balance;
            }
            public static void Select()
            {
                foreach (var item in castemer)
                {
                    Console.WriteLine($"Id: {item.Id}|\t FirstName: {item.FirstName}|\t LastName: {item.LastName}|\t " +
                        $"Balace: {item.Balance}|");
                }
            }
            public static void Insert()
            {
                Console.Clear();
                Console.Write("Enter FirstName = ");
                string FirstName = Console.ReadLine();
                Console.Write("Enter LastName = ");
                string LastName = Console.ReadLine();
                Console.Write("Enter Balance = ");
                var Balance = decimal.Parse(Console.ReadLine());
                Client clients = new Client(FirstName, LastName, Balance);
                castemer.Add(clients);
                Checkcustomer.Add(clients);
            }
            public static void UpdateById()
            {
                Console.Clear();
                Console.Write("Enter Id =  ");
                int Id = int.Parse(Console.ReadLine());
                Console.Write("Enter FirstName = ");
                string FirstName = Console.ReadLine();
                Console.Write("Enter LastName =  ");
                string LastName = Console.ReadLine();
                Console.Write("Enter Balance =  ");
                var balance = decimal.Parse(Console.ReadLine());
                Client clients = new Client(Id, FirstName, LastName, balance);
                foreach (var item in castemer)
                {
                    if (Id == item.Id)
                    {
                        int index = castemer.IndexOf(item);
                        castemer[index] = clients;
                        break;
                    }
                }
            }
            public static void DeleteById()
            {
                Console.Clear();
                Console.Write("Id: ");

                int Id = int.Parse(Console.ReadLine());
                foreach (var item in castemer)
                {
                    if (Id == item.Id)
                    {
                        Checkcustomer.Remove(item);
                        castemer.Remove(item);
                        Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Element deleted from list"); Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                }
            }
            public static void updateBalance(object objects)
            {
                List<Client> list = objects as List<Client>;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Balance > Checkcustomer[i].Balance)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"ID:{list[i].Id}\nBalance before transaction:{Checkcustomer[i].Balance}\nBalance after transaction: {list[i].Balance}\nDifference: +{list[i].Balance - Checkcustomer[i].Balance}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Checkcustomer[i].Balance = list[i].Balance;
                    }
                    else if (list[i].Balance < Checkcustomer[i].Balance)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"ID:{list[i].Id}\nBalance before transaction:{Checkcustomer[i].Balance}\nBalance after transaction: {list[i].Balance}\nDifference: {list[i].Balance - Checkcustomer[i].Balance}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Checkcustomer[i].Balance = list[i].Balance;
                    }
                }
            }
        }
    }
}
