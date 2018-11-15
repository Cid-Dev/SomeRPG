using System;

namespace Business
{
    public class Behaviour
    {
        public Delegate trigger;
        public Delegate resultok;
        public Delegate resultko;

        private TResult GenericInvoke<TResult>(Delegate del, params object[] args)
        {
            var result = del.DynamicInvoke(args);
            return (TResult)Convert.ChangeType(result, typeof(TResult));
        }

        //declencheur
        public TResult Trigger<TResult>(params object[] args)
        {
            return (GenericInvoke<TResult>(trigger, args));
        }

        //action
        public TResult ResultOk<TResult>(params object[] args)
        {
            return (GenericInvoke<TResult>(resultok, args));
        }

        //action 2
        public TResult ResultKo<TResult>(params object[] args)
        {
            return (GenericInvoke<TResult>(resultko, args));
        }
    }
}
