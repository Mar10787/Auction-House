using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    //class for all methods that require reading the file
    class Info_Read
    {
        public Info_Read()
        {

        }
        
        public void UserHasAddress(string name, out bool hasaddress)
        {
            hasaddress = false;
            string unit = "";
            int int_unit = 0;
            string line = "";
            bool advance = false;
            using (StreamReader reader2 = new StreamReader("auction_house.txt"))
            {
                for (int i = 1; i < 10000; i++)
                {
                    line = reader2.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    if (advance)
                    {
                        if (line.Contains("!@#$%"))
                        {
                            hasaddress = true;
                            advance = false;
                        }
                    }
                    if (line.Contains(name))
                    {
                        advance = true;
                    }

                }
            }
        }
        public void CallingUserCredentials(List<string> names, List<string> emails, List<string> passwords)
        {
            string lines = "";
            List<string> credentials = new List<string>();
            using (StreamReader reader = new StreamReader("auction_house.txt"))
            {
                for (int i = 0; i < 1000; i++)
                {
                    lines = reader.ReadLine();
                    if (lines == null)
                    {
                        break;
                    }
                    if (lines.Contains("@") && lines.Contains("$|!"))
                    {
                        if (!lines.Contains("Product for"))
                        {
                            credentials = lines.Split("$|!").ToList();
                            names.Add(credentials[0]);
                            emails.Add(credentials[1]);
                            passwords.Add(credentials[2]);

                        }
                    }
                }
            }
        }

        public void CallingProducts(List<List<List<string>>> alluserproducts)
        {
            string line = "";
            string product = "";
            bool new_user = false;
            List<List<string>> product_list = new List<List<string>>();
            using (StreamReader reading = new StreamReader("auction_house.txt"))
            {
                for (int i = 0; i < 10000; i++)
                {
                    line = reading.ReadLine();
                    if (line != null)
                    {
                        if (line.Contains("Product for"))
                        {
                            List<List<string>> listoflist = new List<List<string>>();
                            if (product_list.Count != 0)
                            {
                                alluserproducts.Add(product_list);
                            }
                            product_list = listoflist;
                            new_user = true;

                        }
                        
                        if (new_user)
                        {
                            List<string> productlist = new List<string>();
                            product = line;
                            productlist = product.Split("#!").ToList<string>();
                            if (productlist.Count != 1)
                            {
                                productlist.RemoveAt(7);
                                product_list.Add(productlist);
                            }
                        }

                        if (line.Contains("Product End"))
                        {
                            alluserproducts.Add(product_list);
                            break;
                        }
                    }
                }
            }
        }
        public void CallingAddresses(List<int> units, List<int> streetnums, List<string> streets, List<string> streetsuffix,List<string> cities, List<string> states, List<int> postcodes )
        {
            using (StreamReader addressfinder = new StreamReader("auction_house.txt"))
            {

                for(int i = 1; i < 1000; i++)
                {

                    string readline = addressfinder.ReadLine();
                    if(readline != null)
                    {
                        if (readline.Contains("!@#$%"))
                        {
                            List<string> addressline = new List<string>();
                            addressline = readline.Split("!@#$%").ToList();
                            units.Add(int.Parse(addressline[0]));
                            streetnums.Add(int.Parse(addressline[1]));
                            streets.Add(addressline[2]);
                            streetsuffix.Add(addressline[3]);
                            cities.Add(addressline[4]);
                            states.Add(addressline[5]);
                            postcodes.Add(int.Parse(addressline[6]));
                        }
                        else if(readline.Contains("Product for"))
                        {
                            break;
                        }
                    }
                    else if(readline==null)
                    {
                        break;
                    }
                    
                }
            }
        }

        public void CallingPurchased(List<List<List<string>>> alluserpurchased)
        {
            string line = "";
            string product = "";
            bool new_user = false;
            List<List<string>> product_list = new List<List<string>>();
            using (StreamReader reading = new StreamReader("auction_house.txt"))
            {
                for (int i = 0; i < 10000; i++)
                {
                    line = reading.ReadLine();
                    if (line != null)
                    {
                        if (line.Contains("Purchased for"))
                        {
                            List<List<string>> listoflist = new List<List<string>>();
                            if (product_list.Count != 0)
                            {
                                alluserpurchased.Add(product_list);
                            }
                            product_list = listoflist;
                            new_user = true;

                        }

                        if (new_user)
                        {
                            List<string> productlist = new List<string>();
                            product = line;
                            productlist = product.Split(")(*&^").ToList<string>();
                            if (productlist.Count != 1)
                            {
                                productlist.RemoveAt(7);
                                product_list.Add(productlist);
                            }
                            if (product == "dummy")
                            {
                                productlist.Add("dummy");
                                product_list.Add(productlist);
                            }
                        }

                        if (line.Contains("Purchases Finished"))
                        {
                            alluserpurchased.Add(product_list);
                            break;
                        }
                    }

                }
            }
        }
    }
}
