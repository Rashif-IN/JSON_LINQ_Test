using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace JSON_Test
{
   //num1
        public class Profile
        {
            public string Full_name
            { get; set; }
            public DateTime Birthday
            { get; set; }
            public List <string> Phones
            { get; set; }
        }
        public class Article
        {
            public int Id
            { get; set; }
            public string Title
            { get; set; }
            public DateTime Published_at
            { get; set; }
        }
        public class User
        {
            public int Id
            { get; set; }
            public string Username
            { get; set; }
            public Profile Profile
            { get; set; }
            public List <Article> Articles
            { get; set; }
        }

    //num2
        public class Customer
        {
            public int Id
            { get; set; }
            public string Name
            { get; set; }
        }
        public class Items
        {
            public int Id
            { get; set; }
            public string Name
            { get; set; }
            public int Qty
            { get; set; }
            public int Price
            { get; set; }
        }
        public class Order
        {
            public string Order_id
            { get; set; }
            public DateTime Created_at
            { get; set; }
            public Customer Customer
            { get; set; }
            public List <Items> Items
            { get; set; }
        }

    //num3
        public class Placement
        {
            public int Room_id
            { get; set; }
            public string Name
            { get; set; }
        }
        public class Inventory
        {
            public int Inventory_id
            { get; set; }
            public string Name
            { get; set; }
            public string Type
            { get; set; }
            public List <string> Tags
            {get;set;}
            public int Purchased_at
            { get; set; }
            public Placement Placement
            { get; set; }
        }

    class Program
    {
        static void Main(string[] args)
        {
            //Num1-Your tasks:
            var json1 = File.ReadAllText(@"/Users/user/Projects/JSON_Test/JSON_Test/num1.json");
            var num1 = JsonConvert.DeserializeObject<List<User>>(json1);

            //Find users who doesn't have any phone numbers.
            IEnumerable<string> Phoneless =
                from X in num1
                where X.Profile.Phones.Count == 0
                select X.Profile.Full_name;
            Console.WriteLine("users who doesn't have any phone numbers:");
            Console.Write(String.Join(", ", Phoneless));
            Console.WriteLine(" ");

            ////Find users who have articles. //keluar duplikat
            IEnumerable<string> HaveArticle =
                from X in num1
                from Y in X.Articles
                where (Y.Title).Length != 0
                select X.Profile.Full_name;
            var result = HaveArticle.Distinct();
            Console.WriteLine("users who have articles: ");
            Console.Write(String.Join(", ", result));
            Console.WriteLine(" ");

            ////Find users who have "annis" on their name.
            IEnumerable<string> Annis =
                from X in num1
                where ((X.Profile.Full_name).ToLower()).Contains("annis")
                select X.Profile.Full_name;
            Console.WriteLine("users who have \"annis\" on their name:");
            Console.Write(String.Join(", ", Annis));
            Console.WriteLine(" ");

            ////Find users who have articles on year 2020.
            IEnumerable<string> HaveArticle2020 =
                from X in num1
                from Y in X.Articles
                where (Y.Published_at).Year == 2020
                select X.Profile.Full_name;
            Console.WriteLine("users who have articles on year 2020: ");
            Console.Write(String.Join(", ", HaveArticle2020));
            Console.WriteLine(" ");

            ////Find users who are born on 1986.
            IEnumerable<string> Born86 =
                from X in num1
                where X.Profile.Birthday.Year == 1986
                select X.Profile.Full_name;
            Console.WriteLine("users who are born on 1986:");
            Console.Write(String.Join(", ", Born86));
            Console.WriteLine(" ");

            ////Find articles that contain "tips" on the title.
            IEnumerable<string> TipsArticle =
                from X in num1
                from Y in X.Articles
                where (Y.Title).Contains("Tips")
                select Y.Title;
            Console.WriteLine("articles that contain \"tips\" on the title: ");
            Console.Write(String.Join(", ", TipsArticle));
            Console.WriteLine(" ");

            ////Find articles published before August 2019.
            IEnumerable<string> pubAug =
                from X in num1
                from Y in X.Articles
                where (Y.Published_at).Year <2019 ||( (Y.Published_at).Month < 8 && (Y.Published_at).Year == 2019)
                select Y.Title;

            Console.WriteLine("articles published before August 2019: ");
            Console.Write(String.Join(", ", pubAug));
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");







            ////Num2-You need to do three things:
            var json2 = File.ReadAllText(@"/Users/user/Projects/JSON_Test/JSON_Test/num2.json");
            var num2 = JsonConvert.DeserializeObject<List<Order>>(json2);

            ////Find all purchases made in February.
            //IEnumerable<string> purFeb =
            //  from X in num2
            //  where (X.Created_at).Month == 2
            //  select X.Order_id;

            var purFeb = num2.Where
                (X => X.Created_at.Month == 02)
                .Select(X => X.Order_id);

            Console.WriteLine("all purchases made in February: ");
            Console.Write(String.Join(", ", purFeb));
            Console.WriteLine(" ");

            ////Find all purchases made by Ari, and add grand total by sum all total price of items. The output should only a number.
            //IEnumerable<int> ariSum =
            //  from X in num2
            //  where X.Customer.Name == "Ari"
            //  from Y in X.Items
            //  select Y.Price * Y.Qty;
            //int sum = ariSum.Sum();

            var sum = num2.Where
                (X => X.Customer.Name == "Ari")
                .Sum(X => X.Items.Sum(X => X.Price * X.Qty));

            //Console.WriteLine("ari belanja: ");
            //Console.Write(String.Join(", ", ariSum));
            Console.WriteLine(" ");
            Console.WriteLine($"total ari belanja: {sum}");
            Console.WriteLine(" ");




            ////Find people who have purchases with grand total lower than 300000.The output is an array of people name.Duplicate name is not allowed.
            //List<string> kaumHemat = new List<string>();
            //IEnumerable<string> Bname =
            //  from X in num2
            //  select X.Customer.Name;
            //var Buyer = Bname.Distinct().ToArray();
            //for (int i = 0; i < Buyer.Count(); i++)
            //{
            //    IEnumerable<int> sumEveryone = from X in num2
            //      where X.Customer.Name == Buyer[i]
            //      from Y in X.Items
            //      select Y.Price * Y.Qty;
            //    int summ = sumEveryone.Sum();
            //    if (summ < 300000)
            //    {
            //        kaumHemat.Add(Buyer[i]);
            //        summ = 0;
            //    }
            //    else
            //    {
            //        summ = 0;
            //    }
            //}

            var kaumHemat =
                num2.GroupBy
                (
                    X => X.Customer.Name,
                    X => X.Items.Sum
                        (X => X.Price * X.Qty),
                        (name, total) => new
                            {
                                Key = name, Total = total.Sum()
                            }
                )
                .Where(X => X.Total < 300000)
                .Select(X => X.Key);

            Console.WriteLine("para kaum hemat who have purchases with grand total lower than 300000: ");
            Console.Write(String.Join(", ", kaumHemat));
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");





            ////Num3-Your tasks:
            var json3 = File.ReadAllText(@"/Users/user/Projects/JSON_Test/JSON_Test/num3.json");
            var num3 = JsonConvert.DeserializeObject<List<Inventory>>(json3);

            //Find items in Meeting Room, and save it to items.json.
            IEnumerable<Inventory> MeetRoomItem =
              from X in num3
              where X.Placement.Name == "Meeting Room"
              select X;

            //var MeetRoomItem = num3.Where
            //    (X => X.Placement.Name == "Meeting Room");

            var MeetRoomItemFile = JsonConvert.SerializeObject(MeetRoomItem);
            File.WriteAllText(@"/Users/user/Projects/JSON_LINQ_Test/JSON_Test/items.json", MeetRoomItemFile);

            ////Find all electronic devices, and save it to electronic.json.
            IEnumerable<Inventory> Electronics =
              from X in num3
              where X.Type == "electronic"
              select X;

            //var Electronics = num3.Where
            //    (X => X.Type == "electronic");

            var ElectronicsFile = JsonConvert.SerializeObject(Electronics);
            File.WriteAllText(@"/Users/user/Projects/JSON_LINQ_Test/JSON_Test/electronic.json", ElectronicsFile);

            //Find all furnitures, and save it to furnitures.json.
            IEnumerable<Inventory> Furnitures =
              from X in num3
              where X.Type == "furniture"
              select X;

            //var Furnitures = num3.Where
            //    (X => X.Type == "furniture");

            var FurnitureFile = JsonConvert.SerializeObject(Furnitures);
            File.WriteAllText(@"/Users/user/Projects/JSON_LINQ_Test/JSON_Test/furnitures.json", FurnitureFile);

            //Find all items was purchased at 16 Januari 2020, and save it to purchased - at - 2020 - 01 - 16.json.
            IEnumerable<Inventory> ItemJan =
              from X in num3
              where X.Purchased_at.ToString().Contains("15791")
              select X;

            //var ItemJan = num3.Where
            //    (X => X.Purchased_at.ToString().Contains("15791"));

            var ItemJanFile = JsonConvert.SerializeObject(ItemJan);
            File.WriteAllText(@"/Users/user/Projects/JSON_LINQ_Test/JSON_Test/purchased - at - 2020 - 01 - 16.json", ItemJanFile);


            ////Find all items with brown color, all-browns.json.
            IEnumerable<Inventory> Brown =
              from X in num3
              where X.Tags.Contains("brown")
              select X;

            //var Brown = num3.Where
            //    (X => X.Tags.Contains("brown"));

            var BrownFile = JsonConvert.SerializeObject(Brown);
            File.WriteAllText(@"/Users/user/Projects/JSON_LINQ_Test/JSON_Test/all-browns.json", BrownFile);
         

        }
    }
}
