using System;
using System.Collections.Generic;
using System.Linq;

namespace StackOverflowClone.Data
{
    public static class Leaderboard
    {
        public static IList<Competitor> Calculate(ApplicationDbContext context) 
            => context.Users
                .OrderByDescending(u => u.Points)
                .ThenBy(u => u.UserName)
                .Select(u => new Competitor
                {
                    Points = u.Points,
                    Name = u.UserName
                })
                .ToList();
    }

    public class Competitor : IEquatable<Competitor>
    {
        public int Points { get; set; }
        public string Name { get; set; }

        public bool Equals(Competitor other)
        {
            if (other == null)
                return false;
                
            return other.Points == this.Points && other.Name.Equals(this.Name);
        }
    }
}