using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    //class for all methods that require saving to the file
    class Info_Save
    {
        public Info_Save()
        {

        }

        public void SaveClientCredentials(List<string> names, List<string> emails, List<string> passwords, List<int> units, List<int> street_numbers, List<string> street_names, List<string> street_suffixs, List<string> citys, List<string> states, List<int> postcodes)
        {
            using (StreamWriter sw = new StreamWriter("auction_house.txt", true))
            {
                for (int i = 0; i < names.Count; i++)
                {
                    sw.WriteLine(names[i] + "$|!" + emails[i] + "$|!" + passwords[i]);
                    if (units.Count == names.Count)
                    {
                        sw.Write(units[i] + "!@#$%" + street_numbers[i] + "!@#$%" + street_names[i]+"!@#$%"+street_suffixs[i]+"!@#$%"+ citys[i]+"!@#$%"+ states[i]+"!@#$%"+ postcodes[i]);
                        sw.WriteLine();
                    }
                    
                }

            }
        }

        public void SaveProducts(List<List<List<string>>> AllProducts, List<string> emails)
        {
            using (StreamWriter products = new StreamWriter("auction_house.txt", true))
            {
                for (int i = 0; i < emails.Count; i++)
                {
                    products.WriteLine("Product for " + emails[i]);
                    for (int j = 0; j < AllProducts[i].Count; j++)
                    {
                        foreach (string s in AllProducts[i][j])
                        {
                            products.Write(s + "#!");
                        }
                        products.WriteLine();
                    }
                }
                products.Write("Product End");
                products.WriteLine();
            }
        }
        public void SavePurchasedItems(List<List<List<string>>> PurchasedItems, List<string> emails)
        {
            using (StreamWriter products = new StreamWriter("auction_house.txt", true))
            {
                for (int i = 0; i < emails.Count; i++)
                {
                    products.WriteLine("Purchased for " + emails[i]);
                    if (PurchasedItems[i].Count == 0)
                    {
                        products.Write("dummy");
                        products.WriteLine();
                    }
                    for (int j = 0; j < PurchasedItems[i].Count; j++)
                    {
                        foreach (string s in PurchasedItems[i][j])
                        {
                            products.Write(s + ")(*&^");
                        }
                        products.WriteLine();
                    }
                }
                products.Write("Purchases Finished");
            }
        }
    }
}
