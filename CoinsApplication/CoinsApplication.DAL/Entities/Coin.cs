using CoinsApplication.DAL.Infrastructure;

namespace CoinsApplication.DAL.Entities
{
    public class Coin : IEntity
    {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual int Year { get; set; }

        public virtual byte[] Image { get; set; }

        public virtual Country Country { get; set; }

        public virtual Currency Currency { get; set; }
    }
}