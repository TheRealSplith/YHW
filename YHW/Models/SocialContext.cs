using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace YHW.Models
{
    public class SocialContext : DbContext
    {
        public DbSet<TeamProfile> YHWTeam { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
    }

    public class SocialContextInit : DropCreateDatabaseAlways<SocialContext>
    {
        protected override void Seed(SocialContext context)
        {
            base.Seed(context);

            context.YHWTeam.Add(
                new TeamProfile
                {
                    Name = "William McGraw",
                    Title = "Supreme Commander",
                    PortraitURL = "~/Content/images/WilliamMcGrawPortrait.jpg",
                    FacebookLink = "http://facebook.com/splith",
                    TwitterLink = "http://twitter.com/therealsplith",
                    LinkedIn = ""
                });
            context.YHWTeam.Add(
                new TeamProfile
                {
                    Name = "Carolyn Spencer",
                    Title = "Project Leader",
                    PortraitURL = "",
                    FacebookLink = "https://www.facebook.com/carolynnspencer",
                    TwitterLink = "https://twitter.com/CarolynNSpencer",
                    LinkedIn = ""
                });
            context.YHWTeam.Add(
                new TeamProfile
                {
                    Name = "Anastasia Rzhevskaya",
                    Title = "Social Media Manager",
                    PortraitURL = "",
                    //FacebookLink = "https://www.facebook.com/carolynnspencre",
                    TwitterLink = "https://twitter.com/AnastasiaRzhev",
                    LinkedIn = ""
                });
            context.YHWTeam.Add(
                new TeamProfile
                {
                    Name = "Justin Hambrecht",
                    Title = "Forced Graphic Designer",
                    PortraitURL = "",
                    FacebookLink = "https://www.facebook.com/justin.hambrecht",
                    TwitterLink = "https://twitter.com/JustinH____",
                    LinkedIn = ""
                });
            context.YHWTeam.Add(
                new TeamProfile
                {
                    Name = "Colleen Shiflet",
                    Title = "",
                    PortraitURL = "",
                    //FacebookLink = "https://www.facebook.com/carolynnspencre",
                    //TwitterLink = "https://twitter.com/CarolynNSpencer",
                    LinkedIn = ""
                });
        }
    }
}