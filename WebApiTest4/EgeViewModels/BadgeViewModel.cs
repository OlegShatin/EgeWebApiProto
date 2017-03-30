﻿using WebApiTest4.Models.EgeModels;

namespace WebApiTest4.EgeViewModels
{
    public class BadgeViewModel
    {
        public BadgeViewModel(Badge seedBadge)
        {
            id = seedBadge.Id;
            img = seedBadge.ImageSrc;
            description = seedBadge.Description;
        }
        public int id { get; private set; }
        public string img { get; private set; }
        public string description { get; private set; }
    }
}