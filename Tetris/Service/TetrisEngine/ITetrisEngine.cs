using System.Threading.Tasks;

namespace Tetris.TetrisEngine
{
    public interface ITetrisEngine
    {
         // <summary>
        /// initializes grid, changes the grid (drops all the shapes from one input line)
        /// then checks if there is full-filled line on the grid and then removes it if so
        /// </summary>
        /// <returns></returns>
        Task<string> Play(string[] _inputData);
    }
}
