﻿using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
using System;
using System.Runtime.InteropServices;

namespace MarkerMetro.Unity.WinLegacy
{
    /** 
     * Converter is not supported in Win8 or WP8.1, but supported in WP8.
     */
    public delegate TOutput Converter<in TInput, out TOutput>(TInput input);

    /**
     * Helpers for missing functions of classes, turned into extensions instead...
    */
    public static class MissingExtensions
    {
        /**
         * Helping Metro convert from System.Format(string,Object) to Windows App Store's System.Format(string,Object[]).
         */
        public static string Format(string format, global::System.Object oneParam)
        {
            return global::System.String.Format(format, new global::System.Object[] { oneParam });
        }

        /**
         * StringBuilder.AppendFormat(arg0,arg1,arg2) isn't implemented on WP8, so use this instead.
         */
        public static StringBuilder AppendFormatEx(this StringBuilder sb, string format, global::System.Object arg0, global::System.Object arg1 = null, global::System.Object arg2 = null)
        {
            return sb.AppendFormat(format, new object[] { arg0, arg1, arg2 });
        }

        /**
         * List<T>.ForEach(Action<T>) isn't implemented on WSA, so use this instead.
         */
        public static void ForEach<T>(this List<T> list, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException();

            foreach (T obj in list)
                action(obj);
        }

        /**
         * List<TOutput>.ConvertAll<TOutput>(Converter) is neither implement in WSA nor in WP8.
         */
        public static List<TOutput> ConvertAll<T, TOutput>(this List<T> list, Converter<T, TOutput> converter)
        {
            if (converter == null)
                throw new ArgumentNullException("converter is null.");
            
            List<TOutput> result = new List<TOutput>(list.Count);
            foreach (T t in list)
                result.Add(converter(t));
            return result;
        }

    }
}

