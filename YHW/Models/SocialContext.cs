using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using YHW.Models.Content;
using YHW.Models;

namespace YHW.Models
{
    public class SocialContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Blog>()
                .HasOptional(b => b.Author)
                .WithMany()
                .HasForeignKey(b => b.AuthorID);

            modelBuilder.Entity<Quote>()
                .HasOptional(b => b.Author)
                .WithMany()
                .HasForeignKey(b => b.AuthorID);
        }
        public DbSet<TeamProfile> YHWTeam { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<CollabRequest> CollabRequest { get; set; }
        public DbSet<HttpError> Errors { get; set; }
        public DbSet<Blog> BlogPost { get; set; }
        public DbSet<Quote> QuotePost { get; set; }
        public DbSet<Video> VideoPost { get; set; }
    }

    public class SocialContextInit : DropCreateDatabaseAlways<SocialContext>
    {
        protected override void Seed(SocialContext context)
        {
            base.Seed(context);

            context.BlogPost.Add(
                new Blog
                {
                    Title = "Common Core Standards Leave Parents Feeling Powerless",
                    BlogText =
"A recent opinion piece titled “Your Children are YOURS” from an anti-Common-Core blog “Parents and Educators Against Common Core Standards” reveals some of the doubts and struggles parents are encountering as their childrens’ schools adopt the new standards."+
"\n\n" +
"The author of the post, a Louisiana parent, shares the reasons why she pulled her children out of school and started homeschooling them. Her decision was heavily influenced by the new Common Core Standards that left her feeling powerless. She asks, “At what point did YOUR child become a ward of the state? At what point when you put them on that yellow school bus and sent them on their way did you cease to be in charge?”" +
"\n\n" +
"She urges other parents to get involved, and opt their children out of certain aspects of public education that they feel are unnecessary or inappropriate for them. The author suggests opting out of  “Sex Education Standards” and high stakes tests - which she describes are tests that “the teachers jobs and school funding are tied to ... and/or your child must pass test to pass grade”." +
"\n\n"+
"Despite her own decision to homeschool her kids, the author says, “Homeschooling is NOT the answer for everyone”. Different states have different regulations for home schooling, and in some states, even with homeschooling, “you are still subject to Common Core and it's many tentacles.” ",
                    IsOpinion = true,
                    ImageURL = "~/Content/images/Common-Core-350x200.jpg",
                    SubText = "Common Core Standards Leave Parents Feeling Powerless",
                    CreatedDate = DateTime.Now
                });
            context.QuotePost.Add(
                new Quote 
                {
                    Title = "The Common Core Standards do not include guidelines for sex education.",
                    IsOpinion = false,
                    ImageURL = "~/Content/images/Common-Core-350x200.jpg",
                    SubText = "The Common Core Standards do not include guidelines for sex education.",
                    CreatedDate = DateTime.Now
                });
            context.QuotePost.Add(
                new Quote 
                {
                    Title = "Standards teach what kids should know, not how they should learn",
                    SubText = "The Standards establish what students need to learn but do not dictate how teachers should teach. Instead, schools and teachers will decide how best to help students reach the standards.",
                    IsOpinion = false,
                    ImageURL = "~/Content/images/Common-Core-350x200.jpg",
                    CreatedDate = DateTime.Now
                });
            context.VideoPost.Add(
                new Video
                {
                    Title = "Common Core Questions",
                    VideoURL = "//www.youtube.com/embed/N37exrQC7VA",
                    IsOpinion = true,
                    ImageURL = "~/Content/images/Common-Core-350x200.jpg",
                    SubText = "Watch our latest video on the Common Core!",
                    CreatedDate = DateTime.Now
                });
            context.YHWTeam.Add(
                new TeamProfile
                {
                    Name = "William McGraw",
                    Title = "Supreme Commander",
                    PortraitURL = "~/Content/images/WilliamMcGrawPortrait.jpg",
                    FacebookLink = "http://facebook.com/splith",
                    TwitterLink = "http://twitter.com/therealsplith",
                    LinkedIn = "",
                    YHWTeam = true
                });
            context.YHWTeam.Add(
                new TeamProfile
                {
                    Name = "Carolyn Spencer",
                    Title = "Project Leader",
                    PortraitURL = "",
                    FacebookLink = "https://www.facebook.com/carolynnspencer",
                    TwitterLink = "https://twitter.com/CarolynNSpencer",
                    LinkedIn = "",
                    YHWTeam = true
                });
            context.YHWTeam.Add(
                new TeamProfile
                {
                    Name = "Anastasia Rzhevskaya",
                    Title = "Social Media Manager",
                    PortraitURL = "",
                    //FacebookLink = "https://www.facebook.com/carolynnspencre",
                    TwitterLink = "https://twitter.com/AnastasiaRzhev",
                    LinkedIn = "",
                    YHWTeam = true
                });
            context.YHWTeam.Add(
                new TeamProfile
                {
                    Name = "Justin Hambrecht",
                    Title = "Forced Graphic Designer",
                    PortraitURL = "",
                    FacebookLink = "https://www.facebook.com/justin.hambrecht",
                    TwitterLink = "https://twitter.com/JustinH____",
                    LinkedIn = "",
                    YHWTeam = true
                });
            context.YHWTeam.Add(
                new TeamProfile
                {
                    Name = "Colleen Shiflet",
                    Title = "",
                    PortraitURL = "",
                    //FacebookLink = "https://www.facebook.com/carolynnspencre",
                    //TwitterLink = "https://twitter.com/CarolynNSpencer",
                    LinkedIn = "",
                    YHWTeam = true
                });
        }
    }
}