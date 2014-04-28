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
                .HasRequired(b => b.Author)
                .WithMany()
                .HasForeignKey(b => b.AuthorID);

            modelBuilder.Entity<Quote>()
                .HasRequired(b => b.Author)
                .WithMany()
                .HasForeignKey(b => b.AuthorID);

            modelBuilder.Entity<Quote>()
                .HasRequired(b => b.Author)
                .WithMany()
                .HasForeignKey(b => b.AuthorID);

            /*
            modelBuilder.Entity<Rating>()
                .HasRequired(r => r.MyComment)
                .WithMany()
                .HasForeignKey(r => r.CommentID);
            */

            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.MyAuthor)
                .WithMany()
                .HasForeignKey(c => c.AuthorID);

            modelBuilder.Entity<Comment>()
                .HasMany(c => c.Ratings)
                .WithRequired(r => r.MyComment)
                .HasForeignKey(r => r.CommentID);
        }
        public DbSet<TeamProfile> YHWTeam { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<CollabRequest> CollabRequest { get; set; }
        public DbSet<HttpError> Errors { get; set; }
        public DbSet<Blog> BlogPost { get; set; }
        public DbSet<Quote> QuotePost { get; set; }
        public DbSet<Video> VideoPost { get; set; }
        public DbSet<YHWProfile> UserProfile { get; set; }
        public DbSet<Comment> ContentComment { get; set; }
        public DbSet<Rating> CommentRating { get; set; }
    }

    public class SocialContextInit : DropCreateDatabaseIfModelChanges<SocialContext>
    {
        protected override void Seed(SocialContext context)
        {
            base.Seed(context);
            byte[] array = null;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                var filePath = "C:\\Users\\billm_000\\Documents\\GitHub\\YHW\\YHW\\Content\\images\\Common-Core-350x200.jpg";
                var image = System.Drawing.Image.FromFile(filePath);
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                array = ms.ToArray();
            }

            context.UserProfile.Add(
                new YHWProfile
                {
                    UserName = "billmcg90@gmail.com",
                    FirstName = "William",
                    LastName = "McGraw",
                    Birthday = new DateTime(1990, 5, 22),
                    FacebookLink = "",
                    TwitterLink = "http://twitter.com/therealsplith",
                    IsMale = true,
                    LinkedIn = "",
                    Title = "Chief Technology Officer"
                });
            context.SaveChanges();

            var rootUser = context.UserProfile.Where(u => u.UserName == "billmcg90@gmail.com").FirstOrDefault();
            if (rootUser == null)
                throw new KeyNotFoundException();

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
                    ImageURL = array,
                    SubText = "Common Core Standards Leave Parents Feeling Powerless",
                    CreatedDate = DateTime.Now,
                    Author = rootUser
                });
            context.QuotePost.Add(
                new Quote 
                {
                    Title = "The Common Core Standards do not include guidelines for sex education.",
                    IsOpinion = false,
                    ImageURL = array,
                    SubText = "The Common Core Standards do not include guidelines for sex education.",
                    CreatedDate = DateTime.Now,
                    Author = rootUser
                });
            context.QuotePost.Add(
                new Quote 
                {
                    Title = "Common Core Standards Establish What To Teach, Not How To Teach",
                    SubText = "The Standards establish what students need to learn but do not dictate how teachers should teach. Instead, schools and teachers will decide how best to help students reach the standards. - CoreStandards.org",
                    IsOpinion = false,
                    ImageURL = array,
                    CreatedDate = DateTime.Now,
                    Author = rootUser
                });
            context.VideoPost.Add(
                new Video
                {
                    Title = "Common Core: Myth or Fact? Teachers ",
                    VideoURL = "//www.youtube.com/watch?v=07Rz5BN9EV8",
                    IsOpinion = true,
                    ImageURL = array,
                    SubText = "Myth or Fact? Are Teachers For or Against the Common Core?",
                    CreatedDate = DateTime.Now,
                    Author = rootUser
                });
            context.VideoPost.Add(
                new Video
                {
                    Title = "Common Core: Myth or Fact? Social Skills ",
                    VideoURL = "//www.youtube.com/watch?v=ItUmpZBkgNk",
                    IsOpinion = true,
                    ImageURL = array,
                    SubText = "Common Core: Myth or Fact? Social Skills ",
                    CreatedDate = DateTime.Now,
                    Author = rootUser
                });
            context.VideoPost.Add(
                new Video
                {
                    Title = "Common Core: Myth or Fact? Obama Administration ",
                    VideoURL = "//www.youtube.com/watch?v=Yc2YxOL0mLs",
                    IsOpinion = true,
                    ImageURL = array,
                    SubText = "Common Core: Myth or Fact? Obama Administration ",
                    CreatedDate = DateTime.Now,
                    Author = rootUser
                });
            context.VideoPost.Add(
                new Video
                {
                    Title = "Common Core Questions",
                    VideoURL = "//www.youtube.com/watch?v=N37exrQC7VA",
                    IsOpinion = true,
                    ImageURL = array,
                    SubText = "Watch our latest video on the Common Core!",
                    CreatedDate = DateTime.Now,
                    Author = rootUser
                });
            context.YHWTeam.Add(
                new TeamProfile
                {
                    Name = "Carolyn Spencer",
                    Title = "Chief Executive Officer",
                    PortraitURL = "~/Content/images/CarolynPortrait.jpg",
                    //FacebookLink = "https://www.facebook.com/carolynnspencer",
                    TwitterLink = "https://twitter.com/CarolynNSpencer",
                    LinkedIn = "http://www.linkedin.com/in/carolynnspencer",
                    YHWTeam = true
                });
            context.YHWTeam.Add(
                new TeamProfile
                {
                    Name = "Anastasia Rzhevskaya",
                    Title = "Chief Marketing Officer",
                    PortraitURL = "~/Content/images/AnaPortrait.jpg",
                    //FacebookLink = "https://www.facebook.com/arzhevskaya",
                    TwitterLink = "https://twitter.com/AnastasiaRzhev",
                    LinkedIn = "http://www.linkedin.com/pub/anastasia-rzhevskaya/57/9a8/a7a/",
                    YHWTeam = true
                });
            context.YHWTeam.Add(
                new TeamProfile
                {
                    Name = "William McGraw",
                    Title = "Chief Technology Officer",
                    PortraitURL = "~/Content/images/WilliamMcGrawPortrait.jpg",
                    //FacebookLink = "http://facebook.com/splith",
                    TwitterLink = "http://twitter.com/therealsplith",
                    LinkedIn = "",
                    YHWTeam = true
                });
            context.YHWTeam.Add(
                new TeamProfile
                {
                    Name = "Justin Hambrecht",
                    Title = "Chief Visualization Officer",
                    PortraitURL = "~/Content/images/JustinPortrait.jpg",
                    //FacebookLink = "https://www.facebook.com/justin.hambrecht",
                    TwitterLink = "https://twitter.com/JustinH____",
                    LinkedIn = "http://www.linkedin.com/in/justinhambrecht/",
                    YHWTeam = true
                });
            context.YHWTeam.Add(
                new TeamProfile
                {
                    Name = "Colleen Shiflet",
                    Title = "Video Production Manager",
                    PortraitURL = "",
                    //FacebookLink = "https://www.facebook.com/carolynnspencre",
                    //TwitterLink = "https://twitter.com/CarolynNSpencer",
                    LinkedIn = "http://www.linkedin.com/in/colleenshiflet",
                    YHWTeam = true
                });
        }
    }
}