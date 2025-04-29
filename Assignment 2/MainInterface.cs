using Assignment_2;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse
{
    class MainInterface
    {
        static void Main(string[] args)
        {
            Client_Manager clientinfo = new Client_Manager();
            clientinfo.MakeFileExist("auction_house.txt");
            UI userinterface = new UI();
            Info_Read file_reader = new Info_Read();
            Info_Save file_saver = new Info_Save();
            List<string> names = new List<string>();
            List<string> emails = new List<string>();
            List<string> passwords = new List<string>();
            List<string> addresses = new List<string>();
            List<int> units = new List<int>();
            List<int> street_numbers = new List<int>();
            List<string> street_names = new List<string>();
            List<string> street_suffixs = new List<string>();
            List<string> citys = new List<string>();
            List<string> states = new List<string>();
            List<int> postcodes = new List<int>();
            List<string> reg_names = new List<string>();
            List<string> reg_emails = new List<string>();
            List<string> reg_passwords = new List<string>();
            List<List<List<string>>> alluserproducts = new List<List<List<string>>>();
            List<List<List<string>>> allpurchasedproducts = new List<List<List<string>>>();
            List<List<List<string>>> solditems = new List<List<List<string>>>();
            List<List<string>> removeditems = new List<List<string>>();
            List<List<List<string>>> dummy = new List<List<List<string>>>();
            List<List<string>> fake = new List<List<string>>();
            List<List<string>> soldfake2 = new List<List<string>>();
            List<List<List<string>>> purchased = new List<List<List<string>>>();
            List<string> fake2 = new List<string>() { "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy" };
            List<string> soldfake = new List<string>() { "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy" };
            List<List<List<string>>> calledpurchased = new List<List<List<string>>>();
            fake.Add(fake2);
            soldfake2.Add(soldfake);
            int option;
            string valid_email = "";
            Console.WriteLine("+------------------------------+ \n| Welcome to the Auction House |\n+------------------------------+");
            file_reader.CallingProducts(alluserproducts);
            file_reader.CallingUserCredentials(names, emails, passwords);
            file_reader.CallingAddresses(units, street_numbers, street_names, street_suffixs, citys, states, postcodes);
            file_reader.CallingPurchased(calledpurchased);



            for (int i = 0; i < 100; i++)
            {
                solditems.Add(fake);
                allpurchasedproducts.Add(fake);
                dummy.Add(fake);
                purchased.Add(soldfake2);
            }

            while (true)
            {
                userinterface.MainMenu(out option);
                if (option == 1)
                {
                    userinterface.Register(out string name, out string email, out string password);

                    reg_names.Add(name);
                    reg_emails.Add(email);
                    reg_passwords.Add(password);
                    if (!names.Contains(name))
                    {
                        names.Add(name);
                        emails.Add(email);
                        passwords.Add(password);
                    }
                    valid_email = email;

                    List<List<string>> dummy3 = new List<List<string>>();
                    List<string> dummy2 = new List<string>() { "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy" };
                    dummy3.Add(dummy2);

                    solditems.Add(dummy3);
                    allpurchasedproducts.Add(dummy3);
                    dummy.Add(dummy3);
                }
                else if (option == 2)
                {
                    string client_name = "";
                    bool containsaddress = true;

                    userinterface.GetSignIn(out string email, out string password);
                    if (emails.IndexOf(email) != -1)
                    {
                        client_name = names[emails.IndexOf(email)];
                    }


                    List<List<string>> dummy4 = new List<List<string>>();
                    List<string> dummy2 = new List<string>() { "0", "item", "description", "cost", "biding", "bider", "bid name" };
                    dummy4.Add(dummy2);

                    if (alluserproducts.Count != emails.Count || alluserproducts.Count == 0)
                    {
                        alluserproducts.Add(dummy4);
                    }

                    file_reader.UserHasAddress(client_name, out bool hasaddress);

                    if (hasaddress)
                    {
                        addresses.Add(email);
                    }

                    if (!addresses.Contains(email))
                    {
                        containsaddress = false;
                    }
                 
                    userinterface.Signin(email, password, reg_names, reg_emails, reg_passwords, names, emails, passwords, containsaddress, out bool advance, out string email2, out int unit, out int street_number, out string street_name, out string street_suffix, out string city, out string state, out int postcode);
                    if (!containsaddress)
                    {
                        units.Add(unit);
                        street_numbers.Add(street_number);
                        street_names.Add(street_name);
                        street_suffixs.Add(street_suffix);
                        citys.Add(city);
                        states.Add(state);
                        postcodes.Add(postcode);
                        addresses.Add(email2);
                    }

                    if (advance == true)
                    {
                        int buyer_email_index = 0;
                        bool leave = false;
                        while (!leave)
                        {
                            string name = "";
                            int name_pos2 = emails.IndexOf(email2);
                            if(name_pos2 == -1)
                            {
                                int name_pos_reg = reg_emails.IndexOf(email);
                                name = reg_names[name_pos_reg];
                            }
                            else
                            {
                                name = names[name_pos2];
                            }
                            userinterface.ClientMenu(out int option2);

                            if (option2 == 1)
                            {
                                int name_index = names.IndexOf(name);
                                clientinfo.AddListing(name, email, out List<string> addeditem);
                                if (alluserproducts[name_index] == dummy4)
                                {
                                    alluserproducts[name_index][0] = addeditem;
                                }
                                else
                                {
                                    alluserproducts[name_index].Add(addeditem);
                                }
                            }

                            int name_index2 = names.IndexOf(name);
                            if (alluserproducts[name_index2][0][1] == "item")
                            {
                               alluserproducts[name_index2].RemoveAt(0);
                            }
                            if (option2 == 2)
                            {
                                int name_index = names.IndexOf(name);
                                if (alluserproducts[name_index][0][1] == "item")
                                {
                                    alluserproducts[name_index].RemoveAt(0);
                                }
                                if (name_index > alluserproducts.Count)
                                {
                                    Console.WriteLine("You have no advertised products at the moment."); 
                                }
                                else
                                {
                                    clientinfo.Ordered(alluserproducts[name_index], out List<List<string>> sortedlist);
                                    clientinfo.ShowListing(name, email, sortedlist);
                                }
                            }
                            if (option2 == 3)
                            {
                                int name_index = names.IndexOf(name);
                                userinterface.SearchProducts(name, email, alluserproducts, out List<List<string>> searchproducts, name_index, out List<List<string>> sortedlisting);

                                if (searchproducts.Count > 0)
                                {
                                    Console.WriteLine("\nWould you like to place a bid on any of these items (yes or no)?");
                                    string input = Console.ReadLine();
                                    string upper = input.ToUpper();
                                    while (true)
                                    {
                                        if (upper == "YES" || upper == "NO")
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Please enter either yes or no");
                                            input = Console.ReadLine();
                                            upper = input.ToUpper();
                                        }
                                    }
                                    if (upper == "YES")
                                    {
                                        List<string> itembought = new List<string> { "dummy","dummy","dummy","dummy","dummy","dummy","dummy"};
                                        clientinfo.Bidding(name, email, sortedlisting, out List<string> ItemBought, out string bid);

                                        

                                        itembought[0] = "0";
                                        string supplieremail = "";


                                        for(int i = 0; i < alluserproducts.Count; i++)
                                        {
                                            for(int j = 0;j < alluserproducts[i].Count; j++)
                                            {
                                                if (alluserproducts[i][j][4] != "-")
                                                {
                                                    if (alluserproducts[i][j][1] == ItemBought[1] && alluserproducts[i][j][2] == ItemBought[2])
                                                    {
                                                        int index_of_listing = alluserproducts.IndexOf(alluserproducts[i]);
                                                        supplieremail = emails[index_of_listing];
                                                    }
                                                }
                                            }
                                        }

                                        ItemBought[6] = bid;
                                        bool skip = false;

                                        for(int i = 0; i < alluserproducts.Count; i++)
                                        {
                                            for(int j = 0; j < alluserproducts[i].Count; j++)
                                            {
                                                if (alluserproducts[i][j][1] == ItemBought[1] && alluserproducts[i][j][2] == ItemBought[2])
                                                {
                                                    alluserproducts[i][j] = ItemBought;
                                                    skip = true;
                                                    break;
                                                }
                                                if (skip)
                                                {
                                                    break;
                                                }
                                            }
                                        }

                                        itembought[1] = supplieremail;
                                        itembought[2] = ItemBought[1];
                                        itembought[3] = ItemBought[2];
                                        itembought[4] = ItemBought[3];
                                        itembought[5] = bid;



                                        string Prompt = "Delivery Option";
                                        clientinfo.DashPrompts(Prompt);
                                        Console.WriteLine("(1) Click and collect\n(2) Home Delivery");
                                        Console.WriteLine("Please select an option between 1 and 2\n");
                                        string deliveryoption = Console.ReadLine();
                                        if (deliveryoption == "1")
                                        {
                                            userinterface.GetTimePeriod(out DateTime valid_start, out DateTime valid_end);
                                            string start_time = valid_start.ToString();
                                            string end_time = valid_end.ToString();
                                            Click_and_Collect clickandcollect = new Click_and_Collect {starttime = start_time, endtime = end_time};

                                            Console.WriteLine(clickandcollect.ShowPrompt());
                                            itembought[6] = $"Collect between {start_time} and {end_time}";
                                        }
                                        else if (deliveryoption == "2")
                                        {
                                            userinterface.GetUnit(out int unitnum);
                                            userinterface.GetStreetNum(out int streetnum);
                                            userinterface.GetStreetName(out string street);
                                            userinterface.GetStreetSuffix(out string streetsuffix);
                                            userinterface.GetCity(out string cityname);
                                            userinterface.GetState(out string statename);
                                            userinterface.GetPostcode(out int postcodenum);

                                            Home_Delivery deliveryaddress = new Home_Delivery { Unit = unitnum, StreetNum = streetnum, Street = street, StreetSuffix = streetsuffix, City = cityname, State = statename, Postcode = postcodenum };
                                            Console.WriteLine(deliveryaddress.ShowPrompt());
                                            itembought[6] = $"Deliver to {unitnum}/{streetnum} {street} {streetsuffix}, {cityname} {statename} {postcodenum}";
                                        }

                                        if (solditems[name_index][0][0] == "dummy")
                                        {
                                            solditems[name_index].Insert(0, itembought);
                                            solditems[name_index].RemoveAt(1);

                                        }
                                        else
                                        {
                                            for (int i = 0; i < solditems[name_index].Count; i++)
                                            {
                                                if (solditems[name_index][i][3] == itembought[3])
                                                {
                                                    solditems[name_index].RemoveAt(i);
                                                    solditems[name_index].Add(itembought);
                                                    break;
                                                }
                                                else
                                                {
                                                    solditems[name_index].Add(itembought);
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            if(option2 == 4)
                            {
                                int name_index = names.IndexOf(name);
                                string Prompt = $"\nList Product Bids for {name}({email})";
                                Console.WriteLine(Prompt);
                                clientinfo.DashPrompts(Prompt);
                                Console.WriteLine();
                                clientinfo.DisplayBidding(name_index, alluserproducts, out List<List<string>> sortedbids, false) ;
                                if (sortedbids.Count > 0)
                                {
                                    userinterface.AskToSell(sortedbids, out List<string> soldlisting);
                                    removeditems.Add(soldlisting);
                                    if (soldlisting != null)
                                    {

                                        alluserproducts[name_index].Remove(soldlisting);
                                    }
                                }
                            }
                            if(option2 == 5)
                            {
                                string Prompt = $"\nPurchased Items for {name}({email})";
                                Console.WriteLine(Prompt);
                                clientinfo.DashPrompts(Prompt);
                                Console.WriteLine();
                                if(calledpurchased.Count == emails.Count)
                                {
                                    if (calledpurchased[name_index2][0][0] != "dummy")
                                    {
                                        clientinfo.DisplayPurchased(calledpurchased[name_index2]);
                                    }
                                    else
                                    {
                                        Console.WriteLine("You have no purchased products at the moment");
                                    }
                                }
                                else
                                {
                                    clientinfo.DetectIfSold(email, removeditems, solditems[name_index2], purchased[name_index2], out List<List<string>> userpurchased);
                                    if (userpurchased[0][1] != "dummy")
                                    {
                                        clientinfo.NoDupe(userpurchased, out List<List<string>> nodupes);

                                        List<List<string>> nodupesclone = new List<List<string>>(nodupes);

                                        clientinfo.DisplayPurchased(nodupesclone);


                                        List<List<string>> purchasedclone = new List<List<string>>(purchased[name_index2]);
                                        for (int i = 0; i < userpurchased.Count; i++)
                                        {
                                            List<string> userpurchaseclone = new List<string>(userpurchased[i]);
                                            purchasedclone.Add(userpurchaseclone);
                                        }
                                        purchased[name_index2] = purchasedclone;

                                    }
                                    else
                                    {
                                        Console.WriteLine("You have no purchased products at the moment");
                                    }
                                }
                            }
                            if (option2 == 6)
                            {
                                leave = true;
                            }
                        }
                    }
                }
                else if (option == 3)
                {
                    File.Delete("auction_house.txt");
                    clientinfo.MakeFileExist("auction_house.txt");
                    file_saver.SaveClientCredentials(reg_names, reg_emails, reg_passwords, units, street_numbers, street_names, street_suffixs, citys, states, postcodes);
                    if (alluserproducts[0].Count != 0)
                    {
                        if (alluserproducts[0][0][1] != "item")
                        {
                            file_saver.SaveProducts(alluserproducts, emails);
                        }
                    }
                    List<List<List<string>>> savingpurchased = new List<List<List<string>>>();
                    for(int i = 0; i<purchased.Count; i++)
                    {
                        clientinfo.NoDupe(purchased[i], out List<List<string>> nodupes);
                        List<List<string>> nodupesclone = new List<List<string>>(nodupes);
                        savingpurchased.Add(nodupesclone);
                    }
                    file_saver.SavePurchasedItems(savingpurchased, emails);
                    userinterface.Exit();
                    break;
                }
            }
        }
    }
}
