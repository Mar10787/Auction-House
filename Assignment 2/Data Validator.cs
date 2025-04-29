using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse
{
    class Data_Validator
    {
        public Data_Validator()
        {
        }
        public void CheckValidName(string name, out bool valid)
        {
            valid = false;
            if (name == "")
            {
                Console.WriteLine("       Your supplied value is not a valid name");
            }
            else
            {
                valid = true;
            }
        }

        public void CheckValidEmail(string email, out bool valid)
        {
            valid = false;
            int count_ats = 0;
            int num_of_ats = 0;
            int certain_char = 0;
            int end_prefix = 0;
            int period_detection = 0;
            int start_end_period = 0;

            char[] charEmail = email.ToCharArray();
            foreach (char ch in charEmail)
            {
                string string_char = ch.ToString();
                if (string_char == "@")
                {
                    num_of_ats++;
                }
            }
            if (email.IndexOf("@") != 0 || email.IndexOf("@") != email.Length - 1)
            {
                count_ats++;
            }

            if (num_of_ats != 1)
            {
                Console.WriteLine("        Your supplied value is not a valid email address");
            }
            else if (count_ats == 0)
            {
                Console.WriteLine("        Your supplied value is not a valid email address");
            }
            else
            {
                int at_position = email.IndexOf("@");
                string prefix = email.Substring(0, at_position);
                int LetandDig = 0;
                foreach (char value in prefix)
                {
                    if (char.IsLetterOrDigit(value))
                    {
                        LetandDig++;
                    }
                }
                if (prefix.Contains("_") || prefix.Contains("-") || prefix.Contains(".") || LetandDig > 0)
                {
                    certain_char++;
                }

                if (!prefix.EndsWith("_") || !prefix.EndsWith("-") || !prefix.EndsWith("."))
                {
                    end_prefix++;
                }

                if (certain_char == 0)
                {
                    Console.WriteLine("        Your supplied value is not a valid email address");
                }
                else if (end_prefix == 0)
                {
                    Console.WriteLine("        Your supplied value is not a valid email address");
                }
                else
                {
                    int LetandDig2 = 0;
                    string suffix = email.Substring(at_position + 1);
                    foreach (char value in suffix)
                    {
                        if (char.IsLetterOrDigit(value))
                        {
                            LetandDig2++;
                        }
                    }
                    int certain_char2 = 0;
                    if (suffix.Contains("-") || suffix.Contains(".") || LetandDig2 > 0)
                    {
                        certain_char2++;
                    }
                    foreach (char ch in suffix)
                    {
                        string string_char_suffix = ch.ToString();
                        if (string_char_suffix == ".")
                        {
                            period_detection++;
                        }
                    }
                    if ((suffix.EndsWith(".")) || (suffix.StartsWith(".")))
                    {
                        start_end_period++;
                    }

                    int last_period = suffix.LastIndexOf(".");
                    string after_period = suffix.Substring(last_period + 1);
                    int letters = 0;
                    foreach (char ch in after_period)
                    {
                        if (char.IsLetter(ch))
                        {
                            letters++;
                        }
                    }
                    int letters_only = 0;
                    if (letters == after_period.Length)
                    {
                        letters_only++;
                    }

                    if (certain_char2 == 0)
                    {
                        Console.WriteLine("        Your supplied value is not a valid email address");
                    }
                    else if (period_detection == 0)
                    {
                        Console.WriteLine("        Your supplied value is not a valid email address");
                    }
                    else if (start_end_period > 0)
                    {
                        Console.WriteLine("        Your supplied value is not a valid email address");
                    }
                    else if (letters_only == 0)
                    {
                        Console.WriteLine("        Your supplied value is not a valid email address");
                    }
                    else
                    {
                        valid = true;
                    }
                }
            }
        }

        public void CheckValidPassword(string password, out bool valid)
        {
            valid = false;
            int count_up = 0;
            int count_low = 0;
            int count_digit = 0;
            int count_nonalpha = 0;

            char[] characters_upper = password.ToCharArray();
            foreach (char value in characters_upper)
            {
                if (char.IsUpper(value))
                {
                    count_up++;
                }
            }

            char[] characters_lower = password.ToCharArray();
            foreach (char value in characters_lower)
            {
                if (char.IsLower(value))
                {
                    count_low++;
                }
            }

            char[] characters_digit = password.ToCharArray();
            foreach (char value in characters_digit)
            {
                if (char.IsDigit(value))
                {
                    count_digit++;
                }
            }

            char[] characters_nonalpha = password.ToCharArray();
            foreach (char value in characters_nonalpha)
            {
                if (!char.IsLetterOrDigit(value))
                {
                    count_nonalpha++;
                }
            }

            if (count_up == 0)
            {
                Console.WriteLine("        Your supplied value is not a valid password");
            }
            else if (count_low == 0)
            {
                Console.WriteLine("        Your supplied value is not a valid password");
            }
            else if (count_digit == 0)
            {
                Console.WriteLine("        Your supplied value is not a valid password");
            }
            else if (count_nonalpha == 0)
            {
                Console.WriteLine("        Your supplied value is not a valid password");
            }
            else if (password.Length < 8)
            {
                Console.WriteLine("        Your supplied value is not a valid password");
            }
            else
            {
                valid = true;
            }

        }

        public void CheckUnit(string string_entered_unit, out int valid_unit)
        {
            valid_unit = -1;
            if (int.TryParse((string_entered_unit), out int entered_unit))
            {
                if (entered_unit >= 0)
                {
                    valid_unit = entered_unit;
                }
                else
                {
                    Console.WriteLine("        Unit number must be a non-negative integer");
                }
            }
        }

        public void CheckStreet(string string_entered_street, out int valid_street)
        {
            valid_street = 0;
            if (int.TryParse((string_entered_street), out int entered_street))
            {
                if (entered_street > 0)
                {
                    valid_street = entered_street;
                }
                else
                {
                    Console.WriteLine("        Street number must be a greater than 0");
                    Console.WriteLine(valid_street);
                }
            }
            else
            {
                Console.WriteLine("        Street number must be a positive integer");
            }
        }

        public void StreetName(string entered_street_name, out string valid_street)
        {
            bool all_letters = true;
            foreach (char ch in entered_street_name)
            {
                if (char.IsDigit(ch))
                {
                    all_letters = false;
                }
            }
            valid_street = "";
            if (entered_street_name.Length == 0)
            {
                Console.WriteLine("        Street name must be of arbitary length");
            }
            else if (!all_letters)
            {
                Console.WriteLine("        Street name must be only letters");
            }
            else
            {
                valid_street = entered_street_name;
            }
        }

        public void CityName(string entered_city, out string valid_city)
        {
            bool all_letters = true;
            foreach (char ch in entered_city)
            {
                if (char.IsDigit(ch))
                {
                    all_letters = false;
                }
            }
            valid_city = "";
            if (entered_city.Length == 0)
            {
                Console.WriteLine("        City name must be of arbitary length");
            }
            else if (!all_letters)
            {
                Console.WriteLine("        City name must be all letters");
            }
            else
            {
                valid_city = entered_city;
            }
        }

        public void StreetSuffix(string entered_suffix, out string valid_suffix)
        {
            bool all_letters = true;
            foreach (char ch in entered_suffix)
            {
                if (char.IsDigit(ch))
                {
                    all_letters = false;
                }
            }
            valid_suffix = "";
            if (entered_suffix.Length == 0)
            {
                Console.WriteLine("        Please enter street suffix");
            }
            else if (!all_letters)
            {
                Console.WriteLine("        Street suffix must be only letters");
            }
            else
            {
                valid_suffix = entered_suffix;
            }
        }

        public void Postcode(string entered_postcode, out int valid_postcode)
        {
            if (int.TryParse((entered_postcode), out int int_postcode))
            {
                if (int_postcode <= 9999 && int_postcode >= 1000)
                {
                    valid_postcode = int_postcode;
                }
                else
                {
                    Console.WriteLine("        Postcode must be within 1000 and 9999");
                    valid_postcode = 0;
                }
            }
            else
            {
                Console.WriteLine("        Postcode must be within 1000 and 9999");
                valid_postcode = 0;
            }

        }

        public void State(string entered_state, out string valid_state)
        {
            valid_state = "";
            if (entered_state.Length == 0)
            {
                Console.WriteLine("        State/Territory must be either QLD, NSW, VIC, TAS, SA, WA, NT or ACT");
            }
            else
            {
                entered_state = entered_state.ToUpper();
                switch (entered_state)
                {
                    case "QLD":
                        valid_state = entered_state;
                        break;
                    case "NSW":
                        valid_state = entered_state;
                        break;
                    case "VIC":
                        valid_state = entered_state;
                        break;
                    case "TAS":
                        valid_state = entered_state;
                        break;
                    case "SA":
                        valid_state = entered_state;
                        break;
                    case "WA":
                        valid_state = entered_state;
                        break;
                    case "NT":
                        valid_state = entered_state;
                        break;
                    case "ACT":
                        valid_state = entered_state;
                        break;
                    default:
                        Console.WriteLine("        State/Territory must be either QLD, NSW, VIC, TAS, SA, WA, NT or ACT");
                        break;
                }
            }


        }

        public void CheckProduct(string product_name, out bool valid)
        {
            valid = false;
            if (product_name == "")
            {
                Console.WriteLine("       Your supplied value is not a valid name");
            }
            else
            {
                valid = true;
            }
        }
        public void CheckProductDescription(string product_name, string description, out bool valid)
        {
            valid = false;
            if (description == product_name)
            {
                Console.WriteLine("        Description cannot be the same as product name**");
            }
            else
            {
                valid = true;
            }
        }

        public void CheckCurrency(string cost, out bool valid)
        {
            int FromDollar = cost.IndexOf("$") + 1;
            int FromPeriod = cost.IndexOf(".");
            valid = false;
            int digit_count = 0;
            if (cost.IndexOf("$") != 0)
            {
                Console.WriteLine("        Must have $ sign**");
            }
            else if (cost.IndexOf("$") == 0)
            {
                if (!cost.Contains("."))
                {
                    Console.WriteLine("        Please enter valid number");
                }
                else if (!valid)
                {
                    string dollars = cost.Substring(FromDollar, FromPeriod - FromDollar);
                    foreach (char number in dollars)
                    {
                        if (char.IsDigit(number))
                        {
                            digit_count += 1;
                        }
                    }
                    if (digit_count == dollars.Length)
                    {
                        valid = true;
                    }
                    else
                    {
                        Console.WriteLine("        Cost must be a whole number**");
                    }
                }
                if (valid)
                {
                    string cents = cost.Substring(FromPeriod + 1);
                    if (cents.Length != 2)
                    {
                        Console.WriteLine("        Must have appropriate cents**");
                        valid = false;
                    }
                    else
                    {
                        foreach (char number in cents)
                        {
                            if (char.IsDigit(number))
                            {
                                valid = true;
                            }
                            else
                            {
                                Console.WriteLine("        Error must be whole numbers**");
                                valid = false;
                            }
                        }
                    }
                }
            }

        }
    }
}