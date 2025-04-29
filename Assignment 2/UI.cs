using System;
using System.IO;
using System.Collections.Generic;
using Assignment_2;
using System.Reflection;
using System.Formats.Asn1;
using System.Transactions;
using System.Xml.Linq;
using System.Reflection.Metadata.Ecma335;

namespace AuctionHouse
{
    class UI
    {
        public string Name { get; }
        public string Email { get; }


        public UI()
        {

        }

        public void MainMenu(out int response)
        {
            Client_Manager client_Manager = new Client_Manager();
            Console.WriteLine();
            string PROMPT = "Main Menu";
            Console.WriteLine(PROMPT);
            client_Manager.DashPrompts(PROMPT);
            Console.WriteLine("(1) Register\n(2) Sign in\n(3) Exit\n\nPlease select an option between 1 and 3");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int selection))
                {
                    if (selection > 0 && selection < 4)
                    {
                        response = selection;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid number");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }
        }
        private void AskForName(out string valid_name)
        {
            valid_name = "";
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter your name");
                string name = Console.ReadLine();
                Data_Validator username = new Data_Validator();
                username.CheckValidName(name, out bool valid);
                if (valid)
                {
                    valid_name = name;
                    break;

                }

            }
        }
        private void AskForEmail(out string valid_email)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter your email address");
                string email = Console.ReadLine();
                Data_Validator useremail = new Data_Validator();
                useremail.CheckValidEmail(email, out bool valid);
                if (valid)
                {
                    valid_email = email;
                    Client_Manager emailReader = new Client_Manager();
                    emailReader.CheckInDataBase(valid_email, out int value);
                    if (value == 1)
                    {
                        break;
                    }
                }
            }
        }
        private void AskForEmailIgnoreRepeats(out string valid_email)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter your email address");
                string email = Console.ReadLine();
                Data_Validator useremail = new Data_Validator();
                useremail.CheckValidEmail(email, out bool valid);
                if (valid)
                {
                    valid_email = email;
                    break;
                }
            }
        }
        private void AskForPassword(out string valid_password, bool registerorsignin)
        {
            const string passPROMPT = "Please choose a password\n* At least 8 characters\n* No white space characters\n* At least one upper-case letter\n* At least one lower-case letter\n* At least one digit\n* At least one special character";
            const string passPROMPT2 = "Please enter your password";

            while (true)
            {
                Console.WriteLine();
                if (registerorsignin)
                {
                    Console.WriteLine(passPROMPT);
                }
                else
                {
                    Console.WriteLine(passPROMPT2);
                }
                string password = Console.ReadLine();
                Data_Validator userpassword = new Data_Validator();
                userpassword.CheckValidPassword(password, out bool valid);
                if (valid)
                {
                    valid_password = password;
                    break;
                }
            }
        }
        public void Register(out string name, out string email, out string password )
        {
            Client_Manager client_Manager = new Client_Manager();
            Console.WriteLine();
            string PROMPT = "Registration";
            Console.WriteLine(PROMPT);
            client_Manager.DashPrompts(PROMPT);
            AskForName(out string valid_name);
            AskForEmail(out string valid_email);
            AskForPassword(out string valid_password, true);
            Console.WriteLine();
            Console.WriteLine("Client {0}({1}) has successfully registered at the Auction House.", valid_name, valid_email);

            name = valid_name;
            email = valid_email;
            password = valid_password;
        }
        public void GetSignIn(out string email, out string password)
        {
            Client_Manager client_Manager = new Client_Manager();
            Console.WriteLine();
            string PROMPT = "Sign In";
            Console.WriteLine(PROMPT);
            client_Manager.DashPrompts(PROMPT);
            AskForEmailIgnoreRepeats(out string client_email);
            AskForPassword(out string client_password, false);
            email = client_email;
            password = client_password;

        }
        public void Signin(string client_email, string client_password, List<string> reg_names, List<string> reg_emails, List<string> reg_passwords, List<string> names, List<string> emails, List<string> passwords, bool hasaddress, out bool advance, out string email, out int unit2, out int street_number2, out string street_name2, out string street_suffix2, out string city2, out string state2, out int postcode2)
        {
            string name = "";
            advance = false;
            unit2 = 0;
            street_number2 = 0;
            city2 = "";
            state2 = "";
            postcode2 = 0;
            street_suffix2 = "";
            street_name2 = "";
            bool proceed = false;
            bool checktextfile = false;
            for (int i = 0; i < reg_emails.Count; i++)
            {
                if ((reg_emails[i] == client_email) && (reg_passwords[i] == client_password))
                {
                    proceed = true;
                    name = reg_names[i];
                    checktextfile = true;
                }
            }
            if (!checktextfile)
            {
                for (int i = 0; i < emails.Count; i++)
                {
                    if ((emails[i] == client_email) && (passwords[i] == client_password))
                    {
                        proceed = true;
                        name = names[i];
                    }
                }
            }

            if (proceed == true)
            {
                advance = true;
                email = client_email;

                if (!hasaddress)
                {
                    AskForAddress(name, email, out int unit, out int street_number, out string street_name, out string street_suffix, out string city, out string state, out int postcode);
                    unit2 = unit;
                    street_number2 = street_number;
                    street_name2 = street_name;
                    street_suffix2 = street_suffix;
                    city2 = city;
                    postcode2 = postcode;
                    state2 = state;
                }
            }
            else
            {
                Console.WriteLine("Your email and password does not match with out database");
                email = "";
            }


        }
        public void GetUnit(out int int_unit)
        {
            while (true)
            {
                int_unit = 0;
                Data_Validator first_time_sign = new Data_Validator();
                Console.WriteLine("Unit number (0 = none):");
                string unit = Console.ReadLine();
                first_time_sign.CheckUnit(unit, out int valid_unit);
                if (valid_unit != -1)
                {
                    int_unit = valid_unit;
                    break;
                }
            }
        }
        public void GetStreetNum(out int int_street)
        {
            int_street = 0;
            Data_Validator first_time_sign = new Data_Validator();
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Street number:");
                string street = Console.ReadLine();
                first_time_sign.CheckStreet(street, out int valid_street);
                if (valid_street != 0)
                {
                    int_street = valid_street;
                    break;
                }
            }
        }

        public void GetStreetName(out string string_street)
        {
            string_street = "";
            Data_Validator first_time_sign = new Data_Validator();
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Street:");
                string street = Console.ReadLine();
                first_time_sign.StreetName(street, out string valid_street);
                if (valid_street != "")
                {
                    string_street = valid_street;
                    break;
                }
            }
        }
        public void GetStreetSuffix(out string string_suffix)
        {
            Data_Validator first_time_sign = new Data_Validator();
            string_suffix = "";
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Street suffix:");
                string suffix = Console.ReadLine();
                first_time_sign.StreetSuffix(suffix, out string valid_suffix);
                if (valid_suffix != "")
                {
                    string_suffix = valid_suffix;
                    break;
                }
            }
        }

        public void GetCity(out string string_city)
        {
            Data_Validator first_time_sign = new Data_Validator();
            string_city = "";
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("City:");
                string city = Console.ReadLine();
                first_time_sign.CityName(city, out string valid_city);
                if (valid_city != "")
                {
                    string_city = valid_city;
                    break;
                }
            }
        }

        public void GetState(out string string_state)
        {
            Data_Validator first_time_sign = new Data_Validator();
            string_state = "";
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("State (ACT, NSW, NT, QLD, SA, TAS, VIC, WA):");
                string state = Console.ReadLine();
                first_time_sign.State(state, out string valid_state);
                if (valid_state != "")
                {
                    string_state = valid_state;
                    break;
                }
            }
        }

        public void GetPostcode(out int int_postcode)
        {
            Data_Validator first_time_sign = new Data_Validator();
            int_postcode = 0;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Postcode (1000 .. 9999):");
                string postcode = Console.ReadLine();
                first_time_sign.Postcode(postcode, out int valid_postcode);
                if (valid_postcode != 0)
                {
                    int_postcode = valid_postcode;
                    break;
                }
            }
        }
        public void AskForAddress(string name, string email, out int int_unit, out int int_street, out string string_street, out string string_suffix, out string string_city, out string string_state, out int int_postcode)
        {
            Console.WriteLine();
            string Prompt = $"Personal Details for {name}({email})";
            Console.WriteLine(Prompt);
            List<string> dash = new List<string>();

            for (int i = 0; i < Prompt.Length; i++)
            {
                dash.Add("-");
            }
            string dash_string = String.Join("", dash);
            Console.WriteLine(dash_string);

            Console.WriteLine();
            Console.WriteLine("Please provide your home address.\n");

            GetUnit(out int unit);
            int_unit = unit;

            GetStreetNum(out int street_num);
            int_street = street_num;

            GetStreetName(out string street_name);
            string_street = street_name;

            GetStreetSuffix(out string street_suffix);
            string_suffix = street_suffix;

            GetCity(out string city);
            string_city = city;

            GetState(out string state);
            string_state = state;

            GetPostcode(out int postcode);
            int_postcode = postcode;


            if (int_unit > 0)
            {
                Console.WriteLine($"Address has been updated to {int_unit}/{int_street} {string_street} {string_suffix}, {string_city} {string_state} {int_postcode}");
            }
            else
            {
                Console.WriteLine($"Address has been updated to {int_street} {string_street} {string_suffix}, {string_city} {string_state} {int_postcode}");
            }
        }
        public void Exit()
        {
            Console.WriteLine("+---------------------------------------------------+\n| Good bye, thank you for using the Auction House! |\n+---------------------------------------------------+");
        }
        public void ClientMenu(out int option)
        {
            Client_Manager client_Manager = new Client_Manager();
            
            string PROMPT = "\nClient Menu";
            Console.WriteLine(PROMPT);
            client_Manager.DashPrompts(PROMPT);
            Console.WriteLine("(1) Advertise Product");
            Console.WriteLine("(2) View My Product List");
            Console.WriteLine("(3) Search For Advertised Products");
            Console.WriteLine("(4) View Bids On My Products");
            Console.WriteLine("(5) View My Purchased Items");
            Console.WriteLine("(6) Log off");
            Console.WriteLine();
            Console.WriteLine("Please select an option between 1 and 6");
            option = int.Parse(Console.ReadLine());
        }
        public void Advertisement(string name, string email, out string product_name, out string product_description, out string product_cost)
        {
            string Prompt = $"Product Advertisement for {name}({email})";
            Console.WriteLine($"\n{Prompt}");
            List<string> dash = new List<string>();
            for (int i = 0; i < Prompt.Length; i++)
            {
                dash.Add("-");
            }
            string dash_string = String.Join("", dash);
            Console.WriteLine(dash_string);

            Data_Validator products = new Data_Validator();

            product_name = "";
            while (true)
            {
                Console.WriteLine("\nProduct name");
                string product = Console.ReadLine();
                products.CheckProduct(product, out bool valid);
                if (valid)
                {
                    product_name = product;
                    break;
                }
            }
            product_description = "";
            while (true)
            {
                Console.WriteLine("\nProduct description");
                string description = Console.ReadLine();
                products.CheckProductDescription(product_name, description, out bool valid);
                if (valid)
                {
                    product_description = description;
                    break;
                }
            }
            product_cost = "";
            while (true)
            {
                Console.WriteLine("\nProduct price ($d.cc)");
                string cost = Console.ReadLine();
                products.CheckCurrency(cost, out bool valid);
                if (valid)
                {
                    product_cost = cost;
                    break;
                }
            }

        }

        public void SearchProducts(string name, string email, List<List<List<string>>> alluserproducts,  out List<List<string>> searchproducts, int name_index, out List<List<string>> sortedlisting)
        {
            Client_Manager client = new Client_Manager();
            List<List<List<string>>> nouserproductincluded = new List<List<List<string>>>();
            nouserproductincluded.AddRange(alluserproducts);
            nouserproductincluded.RemoveAt(name_index);
            searchproducts = new List<List<string>>();
            string Prompt = $"\nProduct search for {name}({email})";
            Console.WriteLine(Prompt);
            client.DashPrompts(Prompt);
            //should turn into method
            Console.WriteLine("\nPlease supply a search phrase (ALL to see all products)");
            string phrase = Console.ReadLine();

            if (phrase == "ALL")
            {
                KeywordAll(nouserproductincluded, out searchproducts);
            }
            else
            {
                for(int i=0; i< nouserproductincluded.Count; i++)
                {
                    for(int j = 0; j < nouserproductincluded[i].Count; j++)
                    {
                       if (nouserproductincluded[i][j][1].Contains(phrase) || nouserproductincluded[i][j][2].Contains(phrase))
                       {
                          searchproducts.Add(nouserproductincluded[i][j]);

                       }
                    }
                }
            }
            SortListofSearchedItems(searchproducts, out List<List<string>> sortedsearch);
            DisplayListofSearchedItems(sortedsearch);
            sortedlisting = sortedsearch;
        }

        private void KeywordAll(List<List<List<string>>> alluserproducts, out List<List<string>> allproducts)
        {
            allproducts = new List<List<string>>();
            for(int i = 0; i < alluserproducts.Count; i++)
            {
                for(int j = 0; j<alluserproducts[i].Count; j++)
                {
                    allproducts.Add(alluserproducts[i][j]);
                }
            }
        }

        private void SortListofSearchedItems(List<List<string>> searchedproducts, out List<List<string>> sortedsearch)
        {
            List<string> arranged = new List<string>();
            sortedsearch = new List<List<string>>();
            string productname = "";
            for (int i = 0; i< searchedproducts.Count; i++)
            {
                arranged.Add(searchedproducts[i][1]);
            }
            arranged.Sort();
            while(sortedsearch.Count < arranged.Count)
            {
                for(int i = 0; i < arranged.Count; i++)
                {
                    productname = arranged[i]; 
                    for(int j =0; j < searchedproducts.Count; j++)
                    {
                        if (searchedproducts[j][1] == productname)
                        {
                            sortedsearch.Add(searchedproducts[j]);
                            break;
                        }
                    }
                }
            }
            for(int j = 0; j < sortedsearch.Count; j++)
            {
                sortedsearch[j][0] = (j + 1).ToString();
            }
        }

        private void DisplayListofSearchedItems(List<List<string>> sortedsearch)
        {
            Client_Manager client = new Client_Manager();
            string Prompt = "\nSearch results";
            Console.WriteLine(Prompt);

            client.DashPrompts(Prompt);

            
            List<string> heading = new List<string>(7) { "Item #", "Product name", "Description", "List price", "Bidder name", "Bidder email", "Bid amt" };
            if (sortedsearch.Count == 0)
            {
                Console.WriteLine("No products found");
            }
            else
            {
                foreach (string title in heading)
                {
                    Console.Write(title + "     ");
                }
                Console.WriteLine();

            }

            for (int i = 0; i < sortedsearch.Count; i++)
            {
                List<string> row = new List<string>();
                row = sortedsearch[i];
                foreach (string title in row)
                {
                  Console.Write($"{title}" + "     ");
                }
                  Console.WriteLine();
            }
        }

        public void AskToSell(List<List<string>> bidproductlisting, out List<string> soldproduct)
        {
            bool advance = false;
            soldproduct = null;
            while (true)
            {
                Console.WriteLine("\nWould you like to sell something (yes or no)?");
                string input = Console.ReadLine();
                string upper = input.ToUpper();
                if (upper == "YES")
                {
                    advance = true;
                    break;
                }
                else if(upper != "NO")
                {
                    Console.WriteLine("Please provide valid input");
                }
                else
                {
                    break;
                }
            }

            if (advance)
            {
                while (true)
                {
                    Console.WriteLine($"\nPlease enter a non-negative integer between 1 and {bidproductlisting.Count}");
                    string input = Console.ReadLine();
                    bool change = int.TryParse(input, out int value);
                    if (!change || value > bidproductlisting.Count || value <= 0)
                    {
                        Console.WriteLine("Please enter a valid input");
                    }
                    else
                    {
                        Console.WriteLine($"\nYou have sold {bidproductlisting[value - 1][1]} to {bidproductlisting[value - 1][4]} for {bidproductlisting[value - 1][6]}");
                        soldproduct = bidproductlisting[value - 1];
                        break;
                    }
                }

            }

            
        }

        public void GetTimePeriod(out DateTime valid_start, out DateTime valid_end)
        {

            DateTime now = DateTime.Now;
            DateTime hourafternow = now.AddHours(1);
            valid_start = DateTime.Now;
            valid_end = DateTime.Now;

            while (true)
            {
                Console.WriteLine("\nDelivery window start (dd/mm/yy hh:mm)");
                string input = Console.ReadLine();
                if(DateTime.TryParse(input, out DateTime start_time))
                {
                    if (start_time < hourafternow)
                    {
                        Console.WriteLine("        Delivery window start must be at least one hour in the future.");

                    }
                    else
                    {
                        valid_start = start_time;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("        Please enter a valid date and time.");
                }
            }

            DateTime hourafter_valid_start = valid_start.AddHours(1);
            while (true)
            {
                Console.WriteLine("\nDelivery window end (dd/mm/yy hh:mm)");
                string input2 = Console.ReadLine();
                if(DateTime.TryParse(input2, out DateTime end_time))
                {
                    if (end_time < hourafter_valid_start)
                    {
                        Console.WriteLine("         Delivery window end must be at least one hour later than the start");
                    }
                    else
                    {
                        valid_end = end_time;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("        Please enter a valid date and time.");
                }
            }

        }
    }
}