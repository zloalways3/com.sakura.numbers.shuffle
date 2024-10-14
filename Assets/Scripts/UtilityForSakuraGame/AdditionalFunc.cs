using System;
using System.Collections.Generic;
using System.Linq;

namespace UtilityForSakuraGame
{
    public static class AdditionalFunc
    {
        public static List<int> ShuffleListWithOrderBy(List<int> list)
        {
            Random random = new Random();
            return list.OrderBy(x => random.Next()).ToList();
        }
    }
}