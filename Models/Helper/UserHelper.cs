using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;

namespace DINGSOMETHING.Models.Helper
{
    public class UserHelper
    {

        private IEnumerable<Claim> list;

        public UserHelper(ClaimsIdentity identity){
            if(identity == null){
                throw new ArgumentNullException();
            }
            this.list = identity.Claims;
        }

        public string GetUserId(){
            return list.ToList()[1].Value;
        }

        public string GetUserName(){
            return list.ToList()[0].Value;
        }

    }


}