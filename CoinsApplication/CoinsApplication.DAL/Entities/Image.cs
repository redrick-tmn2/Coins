using CoinsApplication.DAL.Infrastructure;

namespace CoinsApplication.DAL.Entities
{
    public class Image : Entity<Image>
    {
        public virtual byte[] Content { get; set; }
        public virtual Coin Coin { get; set; }
    }
}