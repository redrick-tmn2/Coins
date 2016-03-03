﻿namespace CoinsApplication.DAL.Entities
{
    public class Coin
    {
        public string Title { get; set; }
        public byte[] Image { get; set; }

        public Country Country { get; set; }

        public Currency Currency { get; set; }
    }
}