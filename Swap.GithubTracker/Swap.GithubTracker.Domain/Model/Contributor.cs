namespace Swap.GithubTracker.Domain.Model
{
    public class Contributor
    {
        public Contributor(string user, int commitsQty)
        {
            this.User = user;
            this.CommitsQuantity = commitsQty;
        }
        public string Name { get; private set; }
        public string User { get; private set; }
        public int CommitsQuantity { get; private set; }

        public void SetName(string name)
        {
            this.Name = name;
        }
    }
}
