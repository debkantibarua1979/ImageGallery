﻿using System.ComponentModel.DataAnnotations;

namespace ImageGallery.IDP.Client.ViewModels
{
    public class EditImageViewModel
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public Guid Id { get; set; }  
    }
}
