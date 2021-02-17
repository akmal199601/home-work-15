using System;
using System.Collections.Generic;
using System.Threading;

namespace hw
{
    class Program
    {
        public static List<Client> clients = new List<Client>();
        public static List<Client> balances = new List<Client>();
        static void Main(string[] args)
        {
            balances.AddRange(clients);
            Client client = new Client();
            string choice = "1";
            while (choice != "5")
            {
                Console.Clear();
                TimerCallback kull = new TimerCallback(Clients1);
                Timer x = new Timer(kull, clients, 0, 1000);
                Console.WriteLine("1.Добавление клиента\n2.Изменение баланса клиента\n3.Удаление клиента\n4.Показать баланс клиента");
                choice = Console.ReadLine();
                int id = 0;
                switch (choice)
                {
                    case "1":
                        Console.Write("Id: ");
                        id = int.Parse(Console.ReadLine());
                        Console.Write("Баланс : ");
                        decimal balance = decimal.Parse(Console.ReadLine());
                        Thread novainsert = new Thread(new ThreadStart(() => { client.Insert(id, balance); }));
                        novainsert.Start();
                        break;
                    case "2":
                        Console.WriteLine("Id: ");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Новый Баланс: ");
                        decimal balances = decimal.Parse(Console.ReadLine());
                        Thread novauptade = new Thread(new ThreadStart(() => { client.Update(id, balances); }));
                        novauptade.Start();
                        break;
                    case "3":
                        Console.Write("Id: ");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Клиент удалил");
                        Thread novadelete = new Thread(new ThreadStart(() => { client.Delete(id); }));
                        novadelete.Start();
                        break;
                    case "4":
                        Console.WriteLine("Id: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        Thread novaselect = new Thread(new ThreadStart(() => { client.Select(id); }));
                        novaselect.Start();
                        break;
                }
                Console.ReadKey();
            }

        }
        static void Clients1(object obj)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Balance != balances[i].Balance)
                {
                    Console.ForegroundColor = (balances[i].Balance <= clients[i].Balance) ? ConsoleColor.Green : ConsoleColor.Red;
                    string Difference = (balances[i].Balance <= clients[i].Balance) ? $"+{clients[i].Balance - balances[i].Balance}" : $"{clients[i].Balance - balances[i].Balance}";
                    Console.WriteLine($"Id client: {clients[i].Id} -- Старый баланс: {balances[i].Balance} -- Изменен баланс: {clients[i].Balance} -- Difference: " + Difference);
                    balances[i].Balance = clients[i].Balance;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
        }
    }

    class Client
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }


        public void Insert(int id, decimal balance)
        {
            this.Id = id;
            this.Balance = balance;

            Program.clients.Add(new Client() { Id = id, Balance = balance });
            Program.balances.Add(new Client() { Id = id, Balance = balance });
            Console.WriteLine("Клиент добавлен" + id);
            return;
        }

        public void Update(int id, decimal balance)
        {
            for (int i = 0; i < Program.clients.Count; i++)
            {
                Program.clients[i].Balance = balance;
                return;
            }
        }

        public void Select(int id)
        {
            for (int i = 0; i < Program.clients.Count; i++)
            {
                if (id == Program.clients[i].Id)
                {
                    Console.WriteLine("ID: " + Program.clients[i].Id);
                    Console.WriteLine("Balance: " + Program.clients[i].Balance);
                    return;
                }
            }
        }

        public void Delete(int id)
        {
            for (int i = 0; i < Program.clients.Count; i++)
            {
                if (id == Program.clients[i].Id)
                {
                    Program.clients.RemoveAt(i);
                    return;
                }
            }
        }

    }
}