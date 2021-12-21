using pathfindin_dfs_bfs;
using Xunit;

namespace TestProject_Learning
{
    public class UnitTest1
    {
        [Fact]
        public void Enter_false_visted_Check_IF_Last_Value_Is_False()
        {
            //Arrange
            int row = 3;
            int column = 5;
            bool expectec = false;

            //Act
            var actual = Obliczenia.Enter_false_visted(row, column);

            //numbers from 0 so -1
            //assert
            Assert.Equal(actual[row-1, column-1], expectec);
        }

        //Co jeœli damy 0,0? nie ma punktu -1,-1
        [Theory]
        [InlineData(0,0)]
        [InlineData(4,5)]
        [InlineData(5,6)]
        [InlineData(6,7)]
        [InlineData(20,40)]          
        //[InlineData(int.MaxValue,int.MaxValue)] 
        //wywala b³ad System.OutOfMemoryException : Array dimensions exceeded supported range.
        //i w sumie array tak du¿y powinien wywalaæ b³ad
        public void Enter_value_to_grid_Menu_Check_Random_Row_Col_eq_0(int row, int col)
        {
            int expected = 0;
            //Act
            var actual = Obliczenia.Enter_value_to_grid(row, col);

            //assert
            Assert.Equal(expected, actual[row-1, col-1]);
        }
    }
}