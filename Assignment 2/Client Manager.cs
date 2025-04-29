using AuctionHouse;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using System.Text.Json;

namespace Assignment_2
{
    //this class is to read and store clients data, saving data should be called before termination
    // when program runs, should delete all data from textfile and re store everything it has on the memory of the coding app
    class Client_Manager
    {

        public Client_Manager()
        {

        }

        public void DashPrompts(string Prompt)
        {
            List<string> dash = new List<string>();
            for (int i = 0; i < Prompt.Length; i++)
            {
                dash.Add("-");
            }
            string dash_string = String.Join("", dash);
            Console.WriteLine(dash_string);
        }
        public void CheckInDataBase(string email, out int value)
        {
            value = 0;
            using (StreamReader reader = new StreamReader("auction_house.txt"))
            {
                string contents = reader.ReadToEnd();
                if (contents.Contains(email))
                {
                    Console.WriteLine("The supplied address is already in use");
                    reader.Close();
                }
                else
                {
                    value = 1;
                    reader.Close();
                }
            }
        }
        public void MakeFileExist(string filename)
        {

            if (!File.Exists(filename))
            {
                FileStream stream;
                stream = File.Create(filename);
                stream.Close();

            }

        }
        public void AddListing(string name, string email, out List<string> addeditem)
        {
            addeditem = new List<string>(7);
            UI askproducts = new UI();
            askproducts.Advertisement(name, email, out string product_name, out string product_description, out string product_cost);
            addeditem.Add("0");
            //make into a list with item num,product name, product desc, product cost, bider name , bidder email, bidder amt

            addeditem.Add(product_name);
            addeditem.Add(product_description);
            addeditem.Add(product_cost);

            //use anoth method with if statements to see if there is any bid on product
            addeditem.Add("-");
            addeditem.Add("-");
            addeditem.Add("-");

            Console.WriteLine($"\nSuccessfully added product {product_name}, {product_description}, {product_cost}.");
        }
        public void Ordered(List<List<string>> list, out List<List<string>> sortedlist)
        {
            List<string> sorted = new List<string>();
            sortedlist = new List<List<string>>();
            for (int i = 0; i < list.Count; i++)
            {
                sorted.Add(list[i][1]);
            }
            sorted.Sort();
            for (int i = 0; i < sorted.Count; i++)
            {
                for (int j = 0; j < sorted.Count; j++)
                {
                    if (sorted[i].Contains(list[j][1]))
                    {
                        sortedlist.Add(list[j]);
                        sortedlist[i][0] = (i + 1).ToString();
                    }
                }
            }

        }
        public void ShowListing(string name, string email, List<List<string>> ProductList)
        {
            string Prompt = $"\nProduct List for {name}({email})";
            Console.WriteLine(Prompt);
            List<string> dash = new List<string>();
            for (int i = 0; i < Prompt.Length; i++)
            {
                dash.Add("-");
            }
            string dash_string = String.Join("", dash);
            Console.WriteLine(dash_string);
            Console.WriteLine();
            List<string> heading = new List<string>(7) { "Item #", "Product name", "Description", "List price", "Bidder name", "Bidder email", "Bid amt" };
            if (ProductList.Count == 0)
            {
                Console.WriteLine("You have no advertised products at the moment.");
            }
            else
            {
                if (ProductList[ProductList.Count - 1][0] == "1")
                {
                    Console.WriteLine("You have no advertised products at the moment.");
                }
                else
                {
                    foreach (string title in heading)
                    {
                        Console.Write(title + "     ");
                    }
                    Console.WriteLine();

                    for (int i = 0; i < ProductList.Count; i++)
                    {
                        List<string> row = new List<string>();
                        row = ProductList[i];
                        foreach (string title in row)
                        {
                            Console.Write($"{title}" + "     ");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
        public void Bidding(string name, string email, List<List<string>> sortedproducts, out List<string> ItemBought, out string bid)
        {
            Data_Validator bidding = new Data_Validator();
            int results = 0;
            string Bidder = "";
            bid = "";
            Console.WriteLine($"\nPlease enter a non-negative integer between 1 and {sortedproducts.Count}");
            string input = Console.ReadLine();
            results = int.Parse(input);
            List<string> product = new List<string>();

            if (sortedproducts[results - 1][6] == "-")
            {
                Bidder = "current highest bidder $0.00";
            }
            else
            {
                Bidder = $"current highest bidder {sortedproducts[results - 1][6]}";
            }
            Console.WriteLine($"Bidding for {sortedproducts[results - 1][1]} (regular price {sortedproducts[results - 1][3]}), {Bidder}");

            while (true)
            {
                Console.WriteLine("\nHow much do you bid?");
                string input2 = Console.ReadLine();
                bidding.CheckCurrency(input2, out bool valid);
                if (valid)
                {
                    if (sortedproducts[results - 1][6] == "-")
                    {
                        sortedproducts[results - 1][6] = input2;
                        sortedproducts[results - 1][4] = name;
                        sortedproducts[results - 1][5] = email;
                        Console.WriteLine($"\nYour bid of {input2} for {sortedproducts[results - 1][1]} is placed\n");
                        for (int i = 0; i < 6; i++)
                        {
                            product.Add(sortedproducts[results - 1][i]);
                        }
                        product.Add("-");
                        ItemBought = product;
                        bid = input2;
                        break;
                    }
                    else
                    {
                        string previous_bid = sortedproducts[results - 1][6];
                        string substring = previous_bid.Substring(1);
                        double prior_bid = double.Parse(substring);
                        string substring2 = input2.Substring(1);
                        double latest_bid = double.Parse(substring2);
                        if (latest_bid > prior_bid)
                        {
                            sortedproducts[results - 1][6] = input2;
                            sortedproducts[results - 1][4] = name;
                            sortedproducts[results - 1][5] = email;
                            Console.WriteLine($"\nYour bid of {input2} for {sortedproducts[results - 1][1]} is placed\n");
                            for (int i = 0; i < 6; i++)
                            {
                                product.Add(sortedproducts[results - 1][i]);
                            }
                            product.Add("-");
                            ItemBought = product;
                            bid = input2;
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Bid must be greater than {sortedproducts[results - 1][6]}");
                        }
                    }
                }
            }

        }
        public void DisplayBidding(int name_index, List<List<List<string>>> alluserproducts, out List<List<string>> sortedbiddings, bool selling)
        {
            List<List<string>> bids = new List<List<string>>();
            for (int i = 0; i < alluserproducts[name_index].Count; i++)
            {
                if (alluserproducts[name_index][i][6] != "-")
                {
                    bids.Add(alluserproducts[name_index][i]);
                }
            }
            List<string> heading = new List<string>();
            sortedbiddings = bids;
            if (!selling)
            {
                List<string> displayrow = new List<string> { "Item #", "Product name", "Description", "List price", "Bidder name", "Bidder email", "Bid amt" };
                heading = displayrow;
            }
            else
            {
                List<string> displayrow = new List<string> { "Item #", "Seller email", "Product name", "Description", "List Price", "Sold price", "Delivery option" };
                heading = displayrow;
            }

            if (bids.Count > 0)
            {
                foreach (string title in heading)
                {
                    Console.Write(title + "     ");
                }
                Console.WriteLine();

                //bids should be sorted
                Ordered(bids, out List<List<string>> sortedbids);
                sortedbiddings = sortedbids;
                if (sortedbids[0][1] != "dummy")
                {
                    for (int i = 0; i < sortedbids.Count; i++)
                    {
                        List<string> row = new List<string>();
                        row = sortedbids[i];
                        foreach (string item in row)
                        {
                            Console.Write($"{item}" + "     ");
                        }
                        Console.WriteLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("No bids were found.");
            }

        }

        public void DetectIfSold(string email, List<List<string>> removedlist, List<List<string>> bidsplaced, List<List<string>> userremoved, out List<List<string>> userremovedout)
        {
            List<List<string>> userremovedclone = new List<List<string>>(userremoved);
            if (removedlist[removedlist.Count-1] == null)
            {
                removedlist.RemoveAt(removedlist.Count-1);
            }
            if (bidsplaced.Count != 0)
            {
                for (int i = 0; i < bidsplaced.Count; i++)
                {
                    for (int j = 0; j < removedlist.Count; j++)
                    {
                        string check = removedlist[j][1];
                        if (bidsplaced[i][2] == removedlist[j][1] && bidsplaced[i][3] == removedlist[j][2])
                        {
                            if (bidsplaced[i][1] != email)
                            {
                                userremovedclone.Add(bidsplaced[i]);
                                if (userremovedclone[0][1] == "dummy")
                                {
                                    userremovedclone.RemoveAt(0);
                                }
                                removedlist.RemoveAt(0);
                            }
                        }
                    }
                }
            }
            userremovedout = new List<List<string>>(userremovedclone);
        }

        public void DisplayPurchased(List<List<string>> solditems)
        {
            List<string> displayrow = new List<string> { "Item #", "Seller email", "Product name", "Description", "List Price", "Sold price", "Delivery option" };

            if(solditems.Count > 0)
            {
                if (solditems[0][0] != "dummy")
                {
                    foreach (string title in displayrow)
                    {
                        Console.Write(title + "     ");
                    }
                    Console.WriteLine();


                    OrderedPurchase(solditems, out List<List<string>> sortedbids);
                    for (int i = 0; i < sortedbids.Count; i++)
                    {
                        List<string> row = new List<string>();
                        row = sortedbids[i];
                        foreach (string item in row)
                        {
                            Console.Write($"{item}" + "     ");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("You have no purchased products at the moment");
                }
            }

        }
        public void NoDupe(List<List<string>> list, out List<List<string>> nodupes)
        {
            List<string> sorted = new List<string>();
            List<List<string>> temp = new List<List<string>>();
            List<string> noDupes = new List<string>();
            int position = 0;
            string productname = "";
            string previous = "";
           
            for (int i = 0; i < list.Count; i++)
            {
                sorted.Add(list[i][2]);
            }
            if (sorted.Contains("dummy"))
            {
                int finder = sorted.IndexOf("dummy");
                sorted.RemoveAt(finder);
            }
            sorted.Sort();

            for(int i = 0; i < sorted.Count; i++)
            {
                productname = sorted[i];
                if(productname != previous)
                {
                    noDupes.Add(productname);
                }
                previous = sorted[i];
            }


            for (int i = 0; i<noDupes.Count; i++)
            {
                for(int j = 0; j < list.Count;j++)
                {
                    if (list[j][2] == noDupes[i])
                    {
                        position = j;
                        break;
                    }
                }
                temp.Add(list[position]);
            }
            nodupes = new List<List<string>>(temp);
        }
        public void OrderedPurchase(List<List<string>> list, out List<List<string>> sortedlist)
        {
            List<string> sorted = new List<string>();
            sortedlist = new List<List<string>>();
            for (int i = 0; i < list.Count; i++)
            {
                sorted.Add(list[i][2]);
            }
            sorted.Sort();
            for (int i = 0; i < sorted.Count; i++)
            {
                for (int j = 0; j < sorted.Count; j++)
                {
                    if (sorted[i].Contains(list[j][2]))
                    {
                        sortedlist.Add(list[j]);
                        sortedlist[i][0] = (i + 1).ToString();
                    }
                }
            }

        }
    }
}
