using BankingApp.Accounts;
using BankingApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace BankingApp.Utilities
{
    internal class JsonHelpers
    {
        private static readonly JsonSerializerOptions Options;


        //Tells the serializer that if it gets a list of Basic Users or Account it needs to note
        //each individual objects actual type, and write a property for it to identify
        //the type upon deserialization
        static JsonHelpers()
        {
            var resolver = new DefaultJsonTypeInfoResolver();

            resolver.Modifiers.Add((JsonTypeInfo ti) =>
            {
                if(ti.Type == typeof(BasicUser))
                {
                    ti.PolymorphismOptions = new JsonPolymorphismOptions
                    {
                        TypeDiscriminatorPropertyName = $"type",
                        IgnoreUnrecognizedTypeDiscriminators = false,
                        DerivedTypes = 
                        {
                            new JsonDerivedType(typeof(User), nameof(User)),
                            new JsonDerivedType(typeof(Admin), nameof(Admin))
                        }
                    };
                }
                else if (ti.Type == typeof(Account))
                {
                    ti.PolymorphismOptions = new JsonPolymorphismOptions
                    {
                        TypeDiscriminatorPropertyName = $"type",
                        IgnoreUnrecognizedTypeDiscriminators = false,
                        DerivedTypes =
                        {
                            new JsonDerivedType(typeof(SavingsAccount), nameof(SavingsAccount))
                        }
                    };
                }
            });

            Options = new JsonSerializerOptions
            {
                WriteIndented = true,
                TypeInfoResolver = resolver
            };
        }


        /// <summary>
        /// Saves the passed list to a json file at specified path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToSave">List to be saved</param>
        /// <param name="filepath">The path where to save the list</param>
        public static void SaveList<T>(List<T> listToSave, string filepath)
        {
            try
            {
                string json;

                if(listToSave is List<BasicUser> || listToSave is List<Account>)
                {
                    json = JsonSerializer.Serialize(listToSave, Options);
                }
                else
                {
                    json = JsonSerializer.Serialize(listToSave);
                }

                File.WriteAllText(filepath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file {filepath}: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads a list, if list is of type BasicUser or Account applies polymorphism options
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filepath">the path from which to load</param>
        /// <returns>Returns the loaded list</returns>
        public static List<T> LoadList<T>(string filepath)
        {
            List<T> toLoad = new List<T>();

            try
            {
                if (File.Exists(filepath))
                {
                    string json = File.ReadAllText(filepath);
                    if (typeof(T) == typeof(BasicUser) || typeof(T) == typeof(Account))
                    {
                        toLoad = JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
                    }
                    else
                    {
                        toLoad = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
                    }
                }
                else
                {
                    Console.WriteLine($"File {filepath} not found.\nCreating new {filepath} containing empty list...");
                    SaveList(toLoad, filepath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File {filepath} cannot be read: {ex.Message}\nCreating new {filepath} containing empty list...");
                SaveList(toLoad, filepath);
            }

            return toLoad;
        }

        public static Dictionary<TKey, TValue> LoadDict<TKey, TValue>(string path)
        {
            Dictionary<TKey, TValue> dictToLoadTo = new Dictionary<TKey, TValue>();
            try
            {
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    dictToLoadTo = JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(json) ?? new Dictionary<TKey, TValue>();
                }
                else
                {
                    Console.WriteLine($"File {path} not found. Creating {path}...");
                    SaveDict(path, dictToLoadTo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File {path} cannot be read: {ex.Message}\nOverwriting {path}...");
                SaveDict(path, dictToLoadTo);
            }

            return dictToLoadTo;
        }

        public static void SaveDict<TKey, TValue>(string path, Dictionary<TKey, TValue> dictToSave)
        {
            try
            {
                string json = JsonSerializer.Serialize(dictToSave);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file {path}: {ex.Message}");
            }
        }
    }
}
