using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace eUseControl.Web.Models
{
     public class UserProfil
     {
          public string Username { get; set; }
          [Required(ErrorMessage = "Campul este obligatoriu!")]
          public string First { get; set; }
          [Required(ErrorMessage = "Campul este obligatoriu!")]
          public string Last { get; set; }
          [Required(ErrorMessage = "Campul este obligatoriu!")]
          public string Adresa { get; set; }
          [Required(ErrorMessage = "Campul este obligatoriu!")]
          public string Telefonul { get; set; }

          [Required(ErrorMessage = "Campul este obligatoriu!")]
          public string Email { get; set; }

          [Required(ErrorMessage = "Campul este obligatoriu!")]
          public string ReEmail { get; set; }

          [Required(ErrorMessage = "Campul este obligatoriu!")]
          public string PasswordCurent { get; set; }

          [Required(ErrorMessage = "Campul este obligatoriu!")]
          public string Password { get; set; }

          [Required(ErrorMessage = "Campul este obligatoriu!")]
          public string RePassword { get; set; }
     }
}