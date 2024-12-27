namespace GradeBook.Test{

public class UnitTest1
{
    [Fact]
    public void BookClaculatesAnAverageGrade()
    {
        //arrange
        var book  = new Book("");
        book.addGrade(89.1);
        book.addGrade(90.5);
        book.addGrade(77.3);
        //act
        var result = book.getStatistics();
        //assert
        Assert.Equal(85.6,result.Average,1);
        Assert.Equal(90.5,result.HighestScore,1);
        Assert.Equal(77.3,result.LowestScore,1);
        Assert.Equal('B',result.Letter);

    }
}
}