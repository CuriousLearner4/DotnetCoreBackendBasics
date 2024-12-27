using StudentAPI.Handlers.Interface;

namespace StudentAPI.Handlers
{
    public abstract class Handler : IHandler
    {
        private Handler next;
        public abstract Task<DateTime?> Handle(int roll);
        public Handler setNextHandler(Handler next)
        {
            this.next = next;
            return next;
        }
        protected async Task<DateTime?> HandleNext(int roll)
        {
            if (next != null)
            {
                return await next.Handle(roll);
            }
            return null;
        }
    }
}
