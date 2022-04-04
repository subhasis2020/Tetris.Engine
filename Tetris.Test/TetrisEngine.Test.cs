using Moq;
using Tetris.TetrisEngine;
using Xunit;
using System.Threading.Tasks;
using System;

namespace Tetris.Test
{
    public class TetrisEnginTest
    {
        private string[] _inputDataScanrio_One;
        private string[] _inputDataScanrio_Two;
        private string[] _inputDataScanrio_Three;
        public TetrisEnginTest() => Setup();

        /// <summary>
        /// Data setup
        /// </summary>
        private void Setup()
        {
            _inputDataScanrio_One = new string[] {"I0","I4","Q8" };
            _inputDataScanrio_Two = new string[] { "T1", "Z3", "I4" };
            _inputDataScanrio_Three = new string[] { "Q0", "I2", "I6", "I0", "I6", "I6", "Q2", "Q4" };
        }

        /// <summary>
        /// A line in the input file contains “I0,I4,Q8”
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Tetris_Test_Scanario_One()
        {
            // Arrange           
            ITetrisEngine mockTetrisEnginee = Mock.Of<TetrisBaseEngine>();

            // Act
            string result = await mockTetrisEnginee.Play(_inputDataScanrio_One);

            // Assert            
            Assert.Equal("1",result);
        }

        /// <summary>
        /// A line in the input file contains “T1,Z3,I4”.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Tetris_Test_Scanario_Two()
        {
            // Arrange           
            ITetrisEngine mockTetrisEnginee = Mock.Of<TetrisBaseEngine>();

            // Act
            string result = await mockTetrisEnginee.Play(_inputDataScanrio_Two);

            // Assert            
            Assert.Equal("4", result);
        }

        /// <summary>
        /// A line in the input file contains “Q0,I2,I6,I0,I6,I6,Q2,Q4”.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Tetris_Test_Scanario_Three()
        {
            // Arrange           
            ITetrisEngine mockTetrisEnginee = Mock.Of<TetrisBaseEngine>();

            // Act
            string result = await mockTetrisEnginee.Play(_inputDataScanrio_Three);

            // Assert            
            Assert.Equal("3", result);
        }
    }
}