namespace Prompt.Abstraction
{
    public interface IRefresher
    {
        int GenomeIndex { get; set; }
        void Refresh();
    }
}
