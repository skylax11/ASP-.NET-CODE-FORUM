using letsDoThis.EF;
using letsDoThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace letsDoThis.Management
{
    public class PostManagement
    {
        RepoCommand<Post> command = new RepoCommand<Post>();
        RepoCommand<User> commandUser = new RepoCommand<User>();
        RepoCommand<Like> commandLike = new RepoCommand<Like>();
        RepoCommand<Comments> commandComment = new RepoCommand<Comments>();
        List<string> Errors = new List<string>();

        public int Like(Post post)
        {
            if (post != null)
            {
                User likerUser = HttpContext.Current.Session["login"] as User;
                User theUser = commandUser.Find(x => x.UserID == likerUser.UserID);
                Post thePost = command.Find(y => y.PostID == post.PostID);

                int theLike_ = thePost.like.Where(x => x.likedUser.UserID == theUser.UserID).Select(x => x.Id).FirstOrDefault();
                Like theLike=commandLike.Find(x=>x.Id==theLike_);
                if (theLike!=null)
                {
                    if (thePost != null && theUser != null)
                    {
                        thePost.LikeCount--;
                        thePost.like.Remove(theLike);
                        theUser.like.Remove(theLike);
                        int x = commandLike.Delete(theLike);
                        return x;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    if (thePost != null && theUser != null)
                    {
                        Like newLike = new Like();
                        thePost.LikeCount++;
                        newLike.likedPost = thePost;
                        newLike.likedUser = theUser;
                        commandLike.Insert(newLike);
                        thePost.like.Add(newLike);
                        theUser.like.Add(newLike);
                        int x = command.Save();
                        return x;
                    }
                    else
                    {
                        return 0;
                    }
                }

            }
            else
            {
                return 0;
            }


        }
        public List<Post> ListPost()
        {
            return command.List();
        }
        public Post FindPost(int? id)
        {
            return command.Find(x=>x.PostID==id);
        }
        public int UpdatePost(Post post)
        {
            Post UpdatedPost = FindPost(post.PostID);
            if (post != null)
            {
                if (post.Desc.Length > 2000)
                {
                    Errors.Add("2000 karakterden uzun açıklama olamaz.");
                    HttpContext.Current.Session["EDITerrors"] = Errors;
                    return 0;
                }
                if (post.Title.Length > 100)
                {
                    Errors.Add("100 karakterden uzun açıklama olamaz.");
                    HttpContext.Current.Session["EDITerrors"] = Errors;
                    return 0;
                }
                UpdatedPost.Title = post.Title;
                UpdatedPost.Desc = post.Desc;
                UpdatedPost.ModifiedDate = DateTime.Now;
                int x = command.Save();
                return x;
            }
            else
            {
                return 0;
            }
        }
        public int DeletePost(Post post)
        {
            Post UpdatedPost = FindPost(post.PostID);
            if (post != null)
            {
                post.comment.Clear();
                post.like.Clear();
                command.Delete(post);
                int x = command.Save();
                return x;
            }
            else
            {
                return 0;
            }
        }
        public int AddPost(Post post)
        {
            if (post != null)
            {
                if (post.Desc != null)
                {
                    if (post.Desc.Length > 2000)
                    {
                        Errors.Add("2000 karakterden uzun açıklama olamaz.");
                        HttpContext.Current.Session["ADDPOSTerrors"] = Errors;
                        return 0;
                    }
                }
                 if (post.Title != null)
                {
                    if (post.Title.Length > 100)
                    {
                        Errors.Add("100 karakterden uzun açıklama olamaz.");
                        HttpContext.Current.Session["ADDPOSTerrors"] = Errors;
                        return 0;
                    }
                }
                Post _post = new Post()
                {
                    Title = post.Title,
                    Desc = post.Desc,
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    owner = post.owner
                };
                int x = command.Insert(_post);

                return x;
            }
            else
            {
                return 0;
            }
        }
        public int AddComment(Comments comment)
        {
            if (comment != null)
            {
                Post thePost = command.Find(y => y.PostID == comment.thePost.PostID);
                if (thePost != null)
                {
                    comment.ModifiedDate = DateTime.Now;
                    comment.CreatedDate = DateTime.Now;
                    thePost.comment.Add(comment);
                    int x = commandComment.Insert(comment);
                    return x;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        public int DeleteComment(int id)
        {
            Comments theComment = commandComment.Find(x => x.CommentID == id);
            int xy = commandComment.Delete(theComment);
            return xy;
        }

    }
}