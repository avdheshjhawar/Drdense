using System;
using System.Collections.Generic;
using DrDenese.Pages;
using OpenQA.Selenium;

namespace DrDenese.Helpers
{
    public static class PageStorage
    {
        static PageStorage()
        {
            Instances = new Dictionary<Type, AbstractPage>();
        }

        private static readonly IDictionary<Type, AbstractPage> Instances;

        public static IWebDriver Driver { get; set; }

        public static void Initialize(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        public static T GetPage<T>() where T:AbstractPage
        {
            try
            {
                return (T) Instances[typeof (T)];
            }
            catch (KeyNotFoundException)
            {
                Instances.Add(typeof(T), (T)Activator.CreateInstance(typeof(T), Driver));
                return GetPage<T>();
            }
        }

        public static void Clear()
        {
            Instances.Clear();
            Driver = null;
        }
    }
}
