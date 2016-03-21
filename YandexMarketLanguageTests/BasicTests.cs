using System;

namespace YandexMarketLanguageTests
{
    public class BasicTests
    {
        public static Action Call(Action func)
        {
            return func;
        }

        public static Action Constructor<T>(Func<T> func)
        {
            return () => func();
        }
    }
}