using Agriculure.WebUi.Custom_Classes;
using Agriculure.WebUi.Models;
using Agriculure.WebUi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agriculure.WebUi.BLLs
{
    public class AuthBLL
    {
        private Model1 _dbContext = new Model1();
        public long RegisterUser(UserVM userVM)
        {
            User user = new User
            {
                Address = userVM.Address,
                CompanyName = userVM.CompanyName,
                Email = userVM.Email,
                Liecnse = userVM.Liecnse,
                Name = userVM.Name,
                NID = userVM.NID,
                Password = userVM.Password,
                Phone = userVM.Phone,
                RoleID = userVM.RoleID
            };

            var addedUser = _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            if (addedUser.ID > 0)
                return addedUser.ID;

            return -1;
        }
    }
}