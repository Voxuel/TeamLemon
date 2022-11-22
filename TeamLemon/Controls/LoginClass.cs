﻿using System;
using System.Collections.Generic;
using System.Text;
using TeamLemon.Models;
using System.Linq;

namespace TeamLemon.Controls
{
    /// <summary>
    /// Class to handle login control and logic.
    /// </summary>
    class LoginClass
    {
        /// <summary>
        /// Core login method, Checks for username and password
        /// also checks if the logged in user is an admin or not.
        /// </summary>
        /// <param name="allUsers"></param>
        public static void Login(Dictionary<int, Person> allUsers)
        {
            var menus = new MenuClass();
            bool ok = false;
            var current = new Person();
            do
            {
                Console.WriteLine("Welcome the bank\n");
                Console.Write("Username: ");
                var username = Console.ReadLine();
                Console.Write("\nPassword: ");
                var password = Console.ReadLine();
                current = LoginValidation(allUsers, username, password);
                if (current != null && current.LogInAttempt != 0)
                {
                    ok = true;
                }
                else
                {
                    continue;
                }
            } while (ok == false);

            if (current.IsAdmin == true)
            {
                menus.AdminMenu(current);
            }
            else
            {
                menus.UserMenu(current);
            }

        }


        /// <summary>
        /// Method to check if the user exists
        /// </summary>
        /// <param name="allUsers"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Returns either the current user if it exists, else returns null</returns>
        private static Person LoginValidation(Dictionary<int, Person> allUsers, string username, string password)
        {
            bool found = false;
            Person current = new Person();
            foreach (var item in allUsers)
            {
                if (item.Value.Name == username && item.Value.Password == password)
                {
                    found = true;
                    current = item.Value;
                    break;
                }
                else if (item.Value.Name == username && item.Value.Password != password)
                {
                    item.Value.LoginAttempts--;
                }
                else if (item.Value.Name != username && item.Value.Password == password)
                {
                    item.Value.LoginAttempts--;
                }
            }
            if (found == true && current.LoginAttempts != 0)
            {
                return current;
            }
            else
            {
                Console.WriteLine("The user dosn't exist");
                return null;
            }
        }
    }
}

