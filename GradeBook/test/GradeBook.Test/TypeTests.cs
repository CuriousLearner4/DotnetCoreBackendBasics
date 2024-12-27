namespace GradeBook.Test{

    public delegate string Logger(string message);
    public class TypeTests{

        [Fact]
        public void writeLogDelegate(){
            Logger logger = new Logger(log);
            Assert.Equal("hello",logger("hello"));
        }

        string log(string message){
            return message;
        }

        [Fact]
        public void ValuesAlsoPassByValue(){
            var x = getInt();
            setInt(x);
            Assert.Equal(3,x);
        }

        int getInt(){
            return 3;
        }
        void setInt(int x){
            x = 42;
        }

        [Fact]
        public void Csharpispassbyref(){
            var book1 = new Book("Hii");
            setName1(ref book1,"new name");
            Assert.Equal("new name",book1.Name);
        }

        void setName1(ref Book book,string name){
            book= new Book(name);
        }
        

        [Fact]
        public void Csharpispassbyvalue(){
            var book1 = new Book("Hii");
            setName1(book1,"new name");
            Assert.Equal("Hii",book1.Name);
        }

        void setName1(Book book,string name){
            book= new Book(name);
        }

        [Fact]
        public void CanSetNameFromReference(){
            var book1 = new Book("Hii");
            setName(book1,"new name");
            Assert.Equal("new name",book1.Name);
        }

        void setName(Book book,string name){
            book.Name = name;
        }

        [Fact]
        public void TwoVarCanReferenceSameObject(){
            var book1 = new Book("Hii");
            var book2 = book1;
            Assert.Same(book1,book2);
            Assert.True(Object.ReferenceEquals(book1,book2));
        }

        [Fact]
        public void GetBookReturnDifferentBooks(){
            var book1 = GetBook("book1");
            var book2 = GetBook("book2");
            Assert.Equal("book1",book1.Name);
            Assert.Equal("book2",book2.Name);

        }

        Book GetBook(string name){
            return new Book(name);
        }
    }
}