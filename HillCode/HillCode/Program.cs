using System;

namespace HillCode
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            do
            {
                Console.Write("Menu\n\n[1] Encrypt\n[2] Decrypt\n[3] Quit\n\nInsert Option: ");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Encrypter();
                        break;
                    case 2:
                        Decrypter();
                        break;
                }
            } while (choice != 3);
        }

        static void Encrypter()
        {
            Console.Write("\nInsert Plain Text: ");
            string plain = Console.ReadLine().ToLower();
            int[] ca = new int[2];
            int[] cb = new int[2];
            Console.Write("\nInsert equation 1's 'a' value (C1 = (aP1 + bP2) Mod 26): ");
            ca[0] = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nInsert equation 1's 'b' value (C1 = (aP1 + bP2) Mod 26): ");
            cb[0] = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nInsert equation 2's 'a' value (C2 = (aP1 + bP2) Mod 26): ");
            ca[1] = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nInsert equation 2's 'b' value (C2 = (aP1 + bP2) Mod 26): ");
            cb[1] = Convert.ToInt32(Console.ReadLine());

            plain = plain.Replace(" ", "");

            if ((plain.Length % 2) == 1)
            {
                plain = plain.Insert(plain.Length, "a");
            }

            char[] plainChar = plain.ToCharArray();

            int[] cipherChar = new int[plainChar.Length];

            Console.WriteLine("\nCipher Text: ");

            for (int i = 0; i < cipherChar.Length; i += 2)
            {
                for (int n = 0; n < 2; n++)
                {
                    cipherChar[i + n] = (ca[n] * (Convert.ToInt32(plainChar[i]) - 97) + cb[n] * (Convert.ToInt32(plainChar[i + 1]) - 97)) % 26;
                    char temp = (char)(cipherChar[i + n] + 97);
                    Console.Write(temp.ToString().ToUpper());

                    if (n == 1)
                    {
                        Console.Write(" ");
                    }
                }
            }

            Console.WriteLine("\n");
        }

        static void Decrypter()
        {
            Console.Write("\nInsert Cipher Text: ");
            string cipher = Console.ReadLine().ToLower();
            int[] c1 = new int[2];
            int[] c2 = new int[2];
            Console.Write("\nInsert equation 1's 'a' value (C1 = (aP1 + bP2) Mod 26): ");
            c1[1] = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nInsert equation 1's 'b' value (C1 = (aP1 + bP2) Mod 26): ");
            c1[0] = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nInsert equation 2's 'a' value (C2 = (aP1 + bP2) Mod 26): ");
            c2[1] = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nInsert equation 2's 'b' value (C2 = (aP1 + bP2) Mod 26): ");
            c2[0] = Convert.ToInt32(Console.ReadLine());

            cipher = cipher.Replace(" ", "");

            if ((cipher.Length % 2) == 1)
            {
                cipher = cipher.Insert(cipher.Length, "a");
            }

            char[] cipherChar = cipher.ToCharArray();

            int[] plainChar = new int[cipherChar.Length];

            Console.WriteLine("\nCipher Text: ");

            for (int i = 0; i < plainChar.Length; i += 2)
            {
                for (int n = 0; n < 2; n++)
                {
                    int front = (c1[1] * c2[0]) - (c1[0] * c2[1]) % 26;
                    int inverse = 0;

                    if (front < 0)
                    {
                        front += 26;
                    }

                    while (front >= 26)
                    {
                        front %= 26;
                    }

                    switch (front)
                    {
                        case 1:
                            inverse = 1;
                            break;
                        case 3:
                            inverse = 9;
                            break;
                        case 5:
                            inverse = 21;
                            break;
                        case 7:
                            inverse = 15;
                            break;
                        case 9:
                            inverse = 3;
                            break;
                        case 11:
                            inverse = 19;
                            break;
                        case 15:
                            inverse = 7;
                            break;
                        case 17:
                            inverse = 23;
                            break;
                        case 19:
                            inverse = 11;
                            break;
                        case 21:
                            inverse = 5;
                            break;
                        case 23:
                            inverse = 17;
                            break;
                        case 25:
                            inverse = 25;
                            break;
                    }

                    if (n == 0)
                    {
                        int check = (inverse * ((c2[n] * (Convert.ToInt32(cipherChar[i]) - 97)) - (c1[n] * (Convert.ToInt32(cipherChar[i + 1]) - 97)))) % 26;
                        if (check < 0)
                        {
                            plainChar[i + n] = check + 26;
                        }
                        else
                        {
                            plainChar[i + n] = check;
                        }
                    }
                    else
                    {
                        int check = (-1 * inverse * ((c2[n] * (Convert.ToInt32(cipherChar[i]) - 97)) - (c1[n] * (Convert.ToInt32(cipherChar[i + 1]) - 97)))) % 26;
                        if (check < 0)
                        {
                            plainChar[i + n] = check + 26;
                        }
                        else
                        {
                            plainChar[i + n] = check;
                        }
                    }
                    char temp = (char)(plainChar[i + n] + 97);
                    Console.Write(temp.ToString().ToUpper());
                }
            }

            Console.WriteLine("\n");
        }
    }
}
