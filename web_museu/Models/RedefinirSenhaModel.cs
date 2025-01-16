﻿using System.ComponentModel.DataAnnotations;

namespace web_museu.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Digite o login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o e-mail")]
        public string Email { get; set; }
    }
}