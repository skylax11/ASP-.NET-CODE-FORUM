using letsDoThis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace letsDoThis.EF
{
    public class MyInitilaizer : CreateDatabaseIfNotExists<MyDB>
    {
        
        protected override void Seed(MyDB context)
        {
            User user_ = new User()
            {
                Name = "Devran",
                Surname = "Yılmaz",
                UserName = "skylax",
                Password = "123",
                ActivateGuid = Guid.NewGuid(),
                ProfileImage = "common.png",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                isActivate = true,
                isAdmin = true,
                email = "devranpvp@hotmail.com"
            };
            context.Users.Add(user_);
            for (int i = 0; i < 8; i++)
            {
                User user = new User()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    UserName = FakeData.NameData.GetFirstName() + $"{i}",
                    Password = "123",
                    ActivateGuid = Guid.NewGuid(),
                    ProfileImage = "common.png",
                    CreatedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    isActivate = true,
                    isAdmin = false,
                    email = FakeData.NetworkData.GetEmail()
                };
                
                context.Users.Add(user);
            }
            context.SaveChanges();
            List<User> userlist = context.Users.ToList();
            for (int i = 0; i < 8; i++)
            {
                User owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];
                Post tp = new Post()
                {
                    Title = FakeData.TextData.GetAlphabetical(20),
                    Desc = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                    CreatedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    owner = owner,
                    LikeCount = FakeData.NumberData.GetNumber(1, 9),
                    

                };
                owner.post.Add(tp);
                context.Posts.Add(tp);

                User owner_comment = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];

                Comments comment = new Comments()
                {
                    CommentText = FakeData.TextData.GetSentence(),
                    thePost = tp,
                    Owner = owner_comment,
                    CreatedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),

                };
                context.Comments.Add(comment);
                for (int l = 0; l < tp.LikeCount; l++)
                {
                    Like like = new Like()
                    {
                        likedUser = userlist[l]

                    };
                    tp.like.Add(like);
                }

            }
            context.SaveChanges();
        }
    }
}
