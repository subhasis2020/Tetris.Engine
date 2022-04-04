using System.Threading.Tasks;

namespace Tetris.TetrisEngine
{
    public interface ITetrisEngine
    {
        Task<string> Play(string[] _inputData);
    }
}