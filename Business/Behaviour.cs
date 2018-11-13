using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Behaviour
    {
        public Delegate trigger;
        public Delegate resultok;
        public Delegate resultko;

        //declencheur
        public TResult Trigger<TResult>(params object[] args)
        {
            var result = trigger.DynamicInvoke(args);
            return (TResult)Convert.ChangeType(result, typeof(TResult));
        }

        //action

        public TResult ResultOk<TResult>(params object[] args)
        {
            var result = resultok.DynamicInvoke(args);
            return (TResult)Convert.ChangeType(result, typeof(TResult));
        }

        public TResult ResultKo<TResult>(params object[] args)
        {
            var result = resultko.DynamicInvoke(args);
            return (TResult)Convert.ChangeType(result, typeof(TResult));
        }
    }
}
