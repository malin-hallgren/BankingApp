﻿using BankingApp.Utilities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Users
{
    // should we make this class abstract?
    internal class BasicUser
    {
        // Properties
        
        public string UserName { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        private static PasswordHasher<BasicUser> Hasher { get; set; } = new PasswordHasher<BasicUser>();

        // Constructor
        public BasicUser(string userName, string name, string phoneNumber, string emailAddress, string password)
        {
            UserName = userName;
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            Password = password;
        }

        public override string ToString()
        {
            return $"Name: {Name}\nUsername: {UserName}\nPhone Number: {PhoneNumber}\nEmail Address: {EmailAddress}";
        }

        public static string GenerateUsername(User user)
        {
            string[] parts = user.Name.Trim().Split(' ');
            string userName = " ";
            
            foreach (var name in parts)
            {
                userName += name.ToLower().Substring(0, 2);
            }

            userName = userName.Trim();

            List<BasicUser> currentUsers = BankApp.GetUserList();
            List<BasicUser> matchingUsers = currentUsers.Where(x => x.UserName.StartsWith(userName)).ToList();


            int indexer = matchingUsers.Count + 1;
            userName = userName + indexer.ToString();

            return userName;
        }

        public static BasicUser GetUserType(BasicUser user)
        {
            if (user is User)
            {
                return (User)user;
            }
            else
            {
                return (Admin)user;
            }  
        }

        /// <summary>
        /// Hashes the password entry for a user
        /// </summary>
        /// <param name="user">the user for which the password is valid for</param>
        /// <param name="plainText">password in plain text</param>
        /// <returns></returns>
        public static string PasswordHash(BasicUser user, string plainText)
        {
            return Hasher.HashPassword(user, plainText);
        }

       

        /// <summary>
        /// Logs in a user unless it is blocked
        /// </summary>
        /// <returns>Currently returns a bool, potentially should return the user?</returns>
        public static (BasicUser?, bool) LogInCheck()
        {
            bool ongoingLogin = true;
            int attempts = 0;
            var loginStatus = new PasswordVerificationResult();
            string blockedMessage = $"The account has been blocked due to repeated failed access attempts. Please contact an admin";
            BasicUser? loginUser = null;

            while (ongoingLogin)
            {
                Console.WriteLine("Input your username:");

                string username = InputHelpers.ValidString().ToLower();
                var users = BankApp.GetUserList();
                if (users.Exists(x => x.UserName.Contains(username)))
                {
                    loginUser = users.Find(x => x.UserName.Contains(username));
                    Console.Clear();

                    //Break out to method CheckBlock?
                    if (loginUser is User)
                    {
                        User current = (User)loginUser;
                        if (current.IsBlocked)
                        {
                            Console.WriteLine(blockedMessage);
                            break;
                        }
                    }

                    while (ongoingLogin && attempts < 3)
                    {
                        Console.WriteLine("Input your password");
                        string input = InputHelpers.InputPassword();

                        attempts++;

                        loginStatus = Hasher.VerifyHashedPassword(loginUser, loginUser.Password, input);

                        if (loginStatus == PasswordVerificationResult.Failed)
                        {
                            Console.Clear();
                            Console.WriteLine($"Incorrect password entered. You have {3 - attempts} attempts left.");
                            continue;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"Welcome to *REDACTED* Bank, {loginUser.Name}");
                            Console.ReadLine();
                            ongoingLogin = false;
                            break;
                        }
                    }
                    if (attempts >= 3 && loginUser is User)
                    {
                        Console.Clear();

                        User currentUser = (User)loginUser;
                        currentUser.IsBlocked = true;
                        Console.WriteLine(blockedMessage);
                    }
                    break;
                }
                else
                {

                    Console.Clear();
                    Console.WriteLine("No User with that username exists.\nPress ENTER to try again");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
            }
            return (loginUser, loginStatus == PasswordVerificationResult.Success);
        }
    }
}