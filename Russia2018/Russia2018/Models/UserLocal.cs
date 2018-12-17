﻿
//Copia de modelo User para usar localmente en BD SQL Light
namespace Russia2018.Models
{
    using SQLite.Net.Attributes;
    using System.Collections.Generic;

    public class UserLocal
    {
        [PrimaryKey]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string ImagePath { get; set; }

        public int UserTypeId { get; set; }

        public byte[] ImageArray { get; set; }

        public string Password { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return "noimage";
                }
                //si el usuario es Local ...
                if (this.UserTypeId == 5)
                {
                    return string.Format(
                        "http://landsapi1977.azurewebsites.net/{0}",
                        ImagePath.Substring(1));
                }

                return this.ImagePath;

            }
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public override int GetHashCode()
        {
            return UserId;
        }
    }
}