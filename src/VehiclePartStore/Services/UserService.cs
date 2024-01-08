using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehiclePartStore.Dto;
using VehiclePartStore.Infrastructure;

namespace VehiclePartStore.Services
{
    public class UserService
    {
        public void GetAll()
        {
            
        }
        public bool Authorize(UserDto user)
        {
            using (var context = new LocalContext())
            {
               
                string passwordHash = context.Users.Where(x => x.Username == user.Username).Select(x=>x.Password).FirstOrDefault();
                bool isCorrect = BCrypt.Net.BCrypt.Verify(user.Password, passwordHash);

                //Console.WriteLine(BCrypt.Net.BCrypt.HashPassword(user.Username));
                return isCorrect;
            }
        }
    }
}
