﻿namespace eStore_Admin.Application.RequestModels
{
    public class GamepadRequest
    {
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public string BigImageUrl { get; set; }
        public string ConnectionType { get; set; }
        public string Feedback { get; set; }
        public float Weight { get; set; }
    }
}