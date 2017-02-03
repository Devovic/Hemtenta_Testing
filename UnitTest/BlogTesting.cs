using System;
using System.Text;
using System.Collections.Generic;
using Hemtenta_Antonio_Mirkovic.blog;
using Xunit;
using Moq;

namespace UnitTest
{
    public class BlogTesting
    {
        const string username = "username"; // ändra
        IBlog blog;
        Mock<IAuthenticator> authMock;
        User user = new User(username);
        public BlogTesting()
        {
            authMock = new Mock<IAuthenticator>();
            authMock.Setup(x => x.GetUserFromDatabase(username)).Returns(new User(username));

            blog = new Blog(authMock.Object);
        }

        [Fact]
        public void UserIsLoggedIn_Success()
        {
            blog.LoginUser(user);
            authMock.Verify((x) => x.GetUserFromDatabase(username), Times.Exactly(1));
            
            //blog.LoginUser(new User("Antonio"));
            Assert.True(blog.UserIsLoggedIn);
        }

        [Fact]
        public void UserIsLoggedIn_Fail()
        {
            blog.LoginUser(new User("pero"));
            
            //blog.LoginUser(new User("Antonio"));
            Assert.False(blog.UserIsLoggedIn);
        }

        [Fact]
        public void UserLoggedIn_Invalid_Throws()
        {
            Assert.Throws<Exception>(() =>blog.LoginUser(null));
        }

        [Fact]
        public void LogoutUser_Success()
        {
            //var user = new User("Antåäölonio");
            blog.LoginUser(user);
            blog.LogoutUser(user);
            Assert.False(blog.UserIsLoggedIn);
        }
        
        [Fact]
        public void LogoutUser_Failed()
        {
            Assert.Throws<Exception>(() => blog.LogoutUser(null));
        }

        [Fact]
        public void PublishPage_returns_true()
        {
            blog.LoginUser(user);

            var page = new Page { Title = "title", Content = "content" };
            //authMock.Verify((x) => x.GetUserFromDatabase(username), Times.Exactly(1));
            //blog.LoginUser(new User("Antonio"));
            Assert.True(blog.PublishPage(page));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("",null)]
        [InlineData(null,"")]
        public void PublishPage_WrongValue_Throws(string title, string content)
        {
            var page = new Page { Title = title, Content = content};
            Assert.Throws<Exception>(() => blog.PublishPage(page));

        }
        [Fact]
        public void PublishPage_PageIsNull_Throws()
        {
            Assert.Throws<Exception>(() => blog.PublishPage(null));
        }

        [Fact]
        public void PublishPage_user_not_loggedin()
        {
            var page = new Page { Title = "title", Content = "content" };
            Assert.False(blog.PublishPage(page));
        }

        [Fact]
        public void SendMail_returns_1()
        {
            blog.LoginUser(user);
            //blog.LoginUser(new User("Antonio"));
            var sendmailResult = blog.SendEmail("asd", "ased", "asd");

            Assert.Equal(1, sendmailResult);
        }
        [Fact]
        public void SendMail_UserLoggedIn_But_Wrong_MailInput_Return_0()
        {
            //blog.LoginUser(new User("Antonio"));
            blog.LoginUser(user);
            var sendmailResult = blog.SendEmail(null, null, null);

            Assert.Equal(0, sendmailResult);

        }

        [Fact]
        public void SendMail_UserNotLoggedIn_ButValid_MailValues_Return_0()
        {
            var sendmailResult = blog.SendEmail("afaw", "awfwaf", "afawf");
            Assert.Equal(0, sendmailResult);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("", "", null)]
        [InlineData("", null, "")]
        [InlineData(null, "", "")]
        [InlineData(null, null , "")]
        [InlineData("", null , null)]
        [InlineData(null, "" , null)]
        public void SendMail_WrongMailValues_Return_0(string address, string caption, string body)
        {
            var sendmailResult = blog.SendEmail(address, caption, body);
            Assert.Equal(0, sendmailResult);

        }

    }
}
