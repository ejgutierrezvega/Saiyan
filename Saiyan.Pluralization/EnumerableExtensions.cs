﻿using System.Collections.Generic;

namespace Saiyan.Pluralization
{
    using System;

    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> ts, Action<T> action)
        {
            foreach (var t in ts)
            {
                action(t);
            }
        }
    }
}
