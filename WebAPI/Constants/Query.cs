using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Constants
{
    public class Query
    {
        public static string registerQuery = "insert into dbo.Users values(@FirstName , @LastName, @DOB , @Email, @Mobile, @Password,  @Gender, @ProfilePicture)";
        public static string loginQuery = "select * from dbo.Users where Password= @Password and (Mobile= @Mobile or Email= @Email)";
        public static string deleteQuery= "delete from dbo.Users where Mobile = @id";
        public static string getQuery = "select UserId,FirstName,LastName,DOB,Email,Mobile, Password,Gender,ProfilePicture from dbo.Users";
    }
}
